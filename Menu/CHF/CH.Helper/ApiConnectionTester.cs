using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;


namespace CH.Helper;
[SupportedOSPlatform("windows")]
public static class ApiConnectionTester
{
    // for supabase
    public static async Task<bool> TestSupaBaseAsync(string url, string key, Action<string> setError)
    {
        try
        {
            //var options = new Supabase.SupabaseOptions
            //{
            //    AutoConnectRealtime = true
            //};
            var supabase = new Supabase.Client(url, key/*, options*/);
            await supabase.InitializeAsync();

            return true;
        }
        catch (Exception ex)
        {
            setError(ex.Message);
            return false;
        }
    }

    // for appwrite
}
