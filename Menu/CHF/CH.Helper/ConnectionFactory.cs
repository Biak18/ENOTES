using System;
using System.IO;
using System.Runtime.Versioning;

namespace CH.Helper;
[SupportedOSPlatform("windows")]
public static class ConnectionFactory
{
    public static string GetDbConnectionString(string server = null)
    {
        string iniPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "DataBaseSettings.ini"
        );

        //if (!File.Exists(iniPath))
        //{
        //    IniFile.IniWriteSingle("Database", "DataSource", server, iniPath);
        //}

        if (string.IsNullOrWhiteSpace(server))
        {
            server = IniFile.IniReadValue("Database", "DataSource", iniPath);
        }

        if (string.IsNullOrWhiteSpace(server))
            throw new InvalidOperationException("Database server is not configured.");

        string catalog = IniFile.IniReadValue("Database", "InitialCatalog", iniPath);
        string userId = IniFile.IniReadValue("Database", "UserId", iniPath);
        string password = SecureStore.Unprotect(IniFile.IniReadValue("Database", "Password", iniPath));
        string encrypt = IniFile.IniReadValue("Database", "Encrypt", iniPath);
        string trustCert = IniFile.IniReadValue("Database", "TrustServerCertificate", iniPath);

        return
            $"Data Source={server};" +
            $"Initial Catalog={catalog};" +
            $"User ID={userId};" +
            $"Password={password};" +
            $"Encrypt={encrypt};" +
            $"TrustServerCertificate={trustCert};";
    }

    //public static string GetWebConnection()
    //{

    //}
}