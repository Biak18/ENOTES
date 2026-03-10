using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ENOTES.Deploy;

public class ManifestBuilder
{
    public class ModuleInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }
    }

    public class Manifest
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("modules")]
        public List<ModuleInfo> Modules { get; set; } = new();
    }

    public static Manifest BuildFromFiles(List<string> dllPaths)
    {
        var modules = dllPaths.Select(path => new ModuleInfo
        {
            Name = Path.GetFileName(path),
            Version = FileVersionInfo.GetVersionInfo(path).FileVersion ?? "1.0.0",
            Size = new FileInfo(path).Length
        }).ToList();

        return new Manifest
        {
            Version = DateTime.UtcNow.ToString("yyyy.MM.dd.HHmm"),
            Modules = modules
        };
    }

    public static string Serialize(Manifest manifest)
        => JsonSerializer.Serialize(manifest,
            new JsonSerializerOptions { WriteIndented = true });
}