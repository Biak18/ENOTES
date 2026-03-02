using System.Data;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace CH.Helper;

[SupportedOSPlatform("windows")]
internal class WebHelper
{
    public static async Task<DataTable> GetDataTable(string spName, object[] parameters)
    {
        try
        {
            return await WebStarter.GetDataTable(spName, parameters);
        }
        catch
        {
            throw;
        }
    }
}
