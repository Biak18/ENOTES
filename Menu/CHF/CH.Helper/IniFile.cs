using System.Runtime.InteropServices;
using System.Text;

namespace CH.Helper;

public class IniFile
{
    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int Size, string filePat);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    private static extern long WritePrivateProfileString(string Section, string Key, string val, string filePath);

    public void IniWriteValue(string Section, string Key, string Value, string avaPath)
    {
        WritePrivateProfileString(Section, Key, Value, avaPath);
    }

    public string IniReadValue(string Section, string Key, string avsPath)
    {
        StringBuilder stringBuilder = new StringBuilder(2000);
        int privateProfileString = GetPrivateProfileString(Section, Key, "", stringBuilder, 2000, avsPath);
        return stringBuilder.ToString();
    }
}
