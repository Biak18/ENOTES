using System;
using System.IO;
using System.Threading.Tasks;

namespace ENOTES.Deploy;

public class SupabaseUploader
{
    private const string SUPABASE_URL = "https://hfdxxjngsdhwczpusnlj.supabase.co";
    private const string SERVICE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhmZHh4am5nc2Rod2N6cHVzbmxqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc2ODI3MTEzMiwiZXhwIjoyMDgzODQ3MTMyfQ.zfITGFnunbvdj3IriBCF4nR6lKsEZPYihXVjQEkcWp4";
    private const string BUCKET = "updates";

    private Supabase.Client _supabase;

    public async Task InitializeAsync()
    {
        if (_supabase != null) return;
        _supabase = new Supabase.Client(SUPABASE_URL, SERVICE_KEY);
        await _supabase.InitializeAsync();
    }

    public async Task UploadFileAsync(string filePath, string fileName,
        Action<float> onProgress = null)
    {
        await _supabase.Storage
            .From(BUCKET)
            .Upload(filePath, fileName,
                new Supabase.Storage.FileOptions { Upsert = true }, // insert or update
                onProgress: (sender, percent) => onProgress?.Invoke(percent));
    }

    public async Task UploadManifestAsync(ManifestBuilder.Manifest manifest,
        Action<float> onProgress = null)
    {
        string json = ManifestBuilder.Serialize(manifest);
        string tmpPath = Path.GetTempFileName() + ".json";

        await File.WriteAllTextAsync(tmpPath, json);

        await _supabase.Storage
            .From(BUCKET)
            .Upload(tmpPath, "manifest.json",
                new Supabase.Storage.FileOptions { Upsert = true },
                onProgress: (sender, percent) => onProgress?.Invoke(percent));

        File.Delete(tmpPath);
    }
}