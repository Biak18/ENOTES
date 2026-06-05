using Supabase;

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CH.Helper;

// File Helper
[SupportedOSPlatform("windows")]
public class FH
{
    private static Client _client;

    public static async Task Init()
    {
        try
        {
            if (_client != null)
                return;
            string SERVICE_KEY = SecureStore.Unprotect(CH.AppContext.ServiceKey);
            _client = new Client(CH.AppContext.Url, SERVICE_KEY);
            await _client.InitializeAsync();
        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
        }
    }


    public static async Task<string> UploadFileAsync(
        string filePath, string fileName, string folder = "")
    {
        await Init();

        string storagePath = string.IsNullOrEmpty(folder)
            ? fileName
            : $"{folder}/{fileName}";

        string ext = Path.GetExtension(fileName);
        string nameOnly = Path.GetFileNameWithoutExtension(fileName);
        storagePath = string.IsNullOrEmpty(folder)
            ? $"{nameOnly}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{ext}"
            : $"{folder}/{nameOnly}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{ext}";

        await _client.Storage
            .From("files")
            .Upload(filePath, storagePath,
                new Supabase.Storage.FileOptions { Upsert = true });

        return _client.Storage
            .From("files")
            .GetPublicUrl(storagePath);
    }

    // Download and run
    public static async Task SaveRun(string imageUrl)
    {
        await Init();

        Uri uri = new Uri(imageUrl);
        string[] segments = uri.AbsolutePath.Split('/');


        string fileName = segments[^1];
        string folder = segments[^2];
        string storagePath = $"{folder}/{fileName}";

        string downloadPath = Path.Combine(
            Application.StartupPath, "TempDownload", folder);

        Directory.CreateDirectory(downloadPath);

        string fullFilePath = Path.Combine(downloadPath, fileName);

        var bytes = await _client.Storage
            .From("files")
            .DownloadPublicFile(storagePath);

        await using (var fileStream = File.Create(fullFilePath))
            await fileStream.WriteAsync(bytes);

        Process.Start(new ProcessStartInfo(fullFilePath)
        {
            UseShellExecute = true
        });
    }

    // Delete Async
    public static async Task DeleteFileAsync(string imageUrl)
    {
        await Init();

        Uri uri = new Uri(imageUrl);
        string[] segments = uri.AbsolutePath.Split('/');


        string fileName = segments[^1];
        string folder = segments[^2];
        string storagePath = $"{folder}/{fileName}";

        await _client.Storage.From("files").Remove(storagePath);
    }
}

