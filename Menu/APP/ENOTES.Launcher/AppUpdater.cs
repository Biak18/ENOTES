using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ENOTES.Launcher;

public class AppUpdater
{
    private const string SUPABASE_URL = "https://hfdxxjngsdhwczpusnlj.supabase.co";
    private const string BUCKET = "updates";
    private const string LOCAL_VERSION_FILE = "versions.json";
    private static string SERVICE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhmZHh4am5nc2Rod2N6cHVzbmxqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc2ODI3MTEzMiwiZXhwIjoyMDgzODQ3MTMyfQ.zfITGFnunbvdj3IriBCF4nR6lKsEZPYihXVjQEkcWp4";

    private static Supabase.Client _supabase;

    public class ModuleInfo
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("version")] public string Version { get; set; }
        [JsonPropertyName("size")] public long Size { get; set; }
    }

    public class Manifest
    {
        [JsonPropertyName("version")] public string Version { get; set; }
        [JsonPropertyName("modules")] public List<ModuleInfo> Modules { get; set; }
    }

    private static async Task EnsureInitialized()
    {
        if (_supabase != null) return;
        _supabase = new Supabase.Client(SUPABASE_URL, SERVICE_KEY);
        await _supabase.InitializeAsync();
    }

    public static async Task<bool> CheckAndUpdate(
        IProgress<(string message, int percent)> progress)
    {
        try
        {
            await EnsureInitialized();

            // 1. Download manifest
            progress.Report(("Checking for updates...", 5));

            var manifestBytes = await _supabase.Storage
                .From(BUCKET)
                .Download("manifest.json", null);

            var json = Encoding.UTF8.GetString(manifestBytes);
            var remoteManifest = JsonSerializer.Deserialize<Manifest>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (remoteManifest?.Modules == null)
            {
                progress.Report(("No manifest found.", 100));
                return true;
            }

            // 2. Load local manifest
            Manifest localManifest = null;
            if (File.Exists(LOCAL_VERSION_FILE))
            {
                var localJson = await File.ReadAllTextAsync(LOCAL_VERSION_FILE);
                localManifest = JsonSerializer.Deserialize<Manifest>(localJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // 3. Find outdated modules
            var toUpdate = remoteManifest.Modules.Where(remote =>
            {
                var local = localManifest?.Modules
                    .FirstOrDefault(m => m.Name == remote.Name);
                return local == null || local.Version != remote.Version;
            }).ToList();

            if (toUpdate.Count == 0)
            {
                progress.Report(("All modules up to date!", 100));
                return true;
            }

            // 4. Download outdated DLLs
            int i = 0;
            foreach (var module in toUpdate)
            {
                i++;
                int pct = 10 + (int)((i / (double)toUpdate.Count) * 85);
                progress.Report(($"Updating {module.Name}... ({i}/{toUpdate.Count})", pct));

                var bytes = await _supabase.Storage
                    .From(BUCKET)
                    .Download(module.Name, onProgress: (s, p) =>
                    {
                        // Optional inner progress per file
                    });

                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string tmpPath = Path.Combine(baseDir, module.Name + ".tmp");
                string dstPath = Path.Combine(baseDir, module.Name);

                await File.WriteAllBytesAsync(tmpPath, bytes);
                if (File.Exists(dstPath)) File.Delete(dstPath);
                File.Move(tmpPath, dstPath);
            }

            // 5. Save updated local manifest
            var updated = JsonSerializer.Serialize(remoteManifest, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(LOCAL_VERSION_FILE, updated);

            progress.Report(("All updates applied!", 100));
            return true;
        }
        catch (Exception ex)
        {
            progress.Report(($"Update failed: {ex.Message}", 0));
            return false;
        }
    }
}