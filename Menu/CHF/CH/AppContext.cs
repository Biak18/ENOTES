using CH.Helper;
using System;
using System.Data;
using System.IO;
using System.Runtime.Versioning;

namespace CH;
[SupportedOSPlatform("windows")]
public static class AppContext
{
    // Environment
    public static ConnectionMode Mode { get; private set; }

    public static CurrentUser User { get; private set; }

    public static string Url { get; private set; }

    public static string Key { get; private set; }

    public static string ServiceKey { get; private set; }

    // Init
    public static void Configure(ConnectionMode mode)
    {
        Mode = mode;

        if (Mode == ConnectionMode.Web)
        {
            string webInfo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebSettings.ini");

            Url = IniFile.IniReadValue("Web", "Url", webInfo);
            Key = IniFile.IniReadValue("Web", "Key", webInfo);
            ServiceKey = IniFile.IniReadValue("Web", "ServiceKey", webInfo);
        }
    }

    public static bool IsDbMode => Mode == ConnectionMode.DbDirect;
    public static bool IsWebMode => Mode == ConnectionMode.Web;

    public static void Login(DataRow userDto)
    {
        User = new CurrentUser(userDto);
    }

    public static void Logout()
    {
        User = null;
    }

    public static bool IsLoggedIn => User != null;
}
