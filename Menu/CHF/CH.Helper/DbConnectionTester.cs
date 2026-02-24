using Microsoft.Data.SqlClient;
using System;
using System.Runtime.Versioning;
namespace CH.Helper;
[SupportedOSPlatform("windows")]
public static class DbConnectionTester
{
    public static bool Test(string connectionString, out string error)
    {
        try
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            error = null;
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }

    public static bool TestByServer(string server, out string error)
    {
        string connectionString =
            ConnectionFactory.GetDbConnectionString(server);

        return Test(connectionString, out error);
    }
}

