using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CH.Helper;

public class IniFile
{
    //[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int Size, string filePat);

    //[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    //private static extern long WritePrivateProfileString(string Section, string Key, string val, string filePath);

    //public void IniWriteValue(string Section, string Key, string Value, string avaPath)
    //{
    //    WritePrivateProfileString(Section, Key, Value, avaPath);
    //}

    //public string IniReadValue(string Section, string Key, string avsPath)
    //{
    //    StringBuilder stringBuilder = new StringBuilder(2000);
    //    int privateProfileString = GetPrivateProfileString(Section, Key, "", stringBuilder, 2000, avsPath);
    //    return stringBuilder.ToString();
    //}



    /// <summary>
    /// Use when you want to write single value
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="filePath"></param>
    public static void IniWriteSingle(string section, string key, string value, string filePath)
    {
        var dict = new Dictionary<string, string>
        {
            { key, value }
        };

        IniWriteValues(section, dict, filePath);
    }

    /// <summary>
    /// Use when you want to write multi values
    /// </summary>
    /// <param name="section"></param>
    /// <param name="codeKey"></param>
    /// <param name="nameKey"></param>
    /// <param name="codeValue"></param>
    /// <param name="nameValue"></param>
    /// <param name="filePath"></param>
    public static void IniWriteCodeAndName(string section, string codeKey, string nameKey, string codeValue, string nameValue, string filePath)
    {
        var dict = new Dictionary<string, string>
        {
            { codeKey, codeValue },
            { nameKey, nameValue }
        };

        IniWriteValues(section, dict, filePath);
    }

    private static void IniWriteValues(string section, Dictionary<string, string> keyValues, string filePath)
    {
        List<string> lines = new();
        if (File.Exists(filePath))
            lines = File.ReadAllLines(filePath).ToList();

        bool inTargetSection = false;
        bool sectionExists = false;
        var updatedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < lines.Count; i++)
        {
            string trimmed = lines[i].Trim();

            // Detect section
            if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
            {
                inTargetSection = trimmed.Equals($"[{section}]", StringComparison.OrdinalIgnoreCase);
                if (inTargetSection) sectionExists = true;
                continue;
            }

            // Replace key-value if in section
            if (inTargetSection && trimmed.Contains('='))
            {
                var parts = trimmed.Split('=', 2);
                var key = parts[0].Trim();

                if (keyValues.TryGetValue(key, out string newValue))
                {
                    lines[i] = $"{key}={newValue}";
                    updatedKeys.Add(key);
                }
            }
        }

        // If section didn't exist, add it and all key-values
        if (!sectionExists)
        {
            lines.Add($"[{section}]");
            foreach (var kvp in keyValues)
            {
                lines.Add($"{kvp.Key}={kvp.Value}");
                updatedKeys.Add(kvp.Key);
            }
        }
        else
        {
            // If section exists, but some keys were not updated, append them after the section
            int insertIndex = lines.FindLastIndex(l => l.StartsWith("[") && l.Equals($"[{section}]", StringComparison.OrdinalIgnoreCase));
            foreach (var kvp in keyValues)
            {
                if (!updatedKeys.Contains(kvp.Key))
                    lines.Insert(insertIndex + 1, $"{kvp.Key}={kvp.Value}");
            }
        }

        File.WriteAllLines(filePath, lines, Encoding.UTF8);
    }

    public static string IniReadValue(string Section, string Key, string filePath)
    {
        if (!File.Exists(filePath))
            return "";

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
        bool sectionFound = false;

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();

            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                sectionFound = trimmedLine.Equals($"[{Section}]", StringComparison.OrdinalIgnoreCase);
                continue;
            }

            if (sectionFound && trimmedLine.Contains("="))
            {
                string[] keyValue = trimmedLine.Split('=', 2);

                if (keyValue.Length == 2 && keyValue[0].Trim().Equals(Key, StringComparison.OrdinalIgnoreCase))
                    return keyValue[1].Trim();
            }
        }

        return "";
    }
}
