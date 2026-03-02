using System.Data;

namespace CH.Helper;

public class WebInfo
{
    public DataTable DataValue { get; set; }
    public string SpNameInsert { get; set; }
    public string[] SpParamsInsert { get; set; }
    public string SpNameUpdate { get; set; }
    public string[] SpParamsUpdate { get; set; }
    public string SpNameDelete { get; set; }
    public string[] SpParamsDelete { get; set; }

    public string[] PrimaryKey { get; set; }
}
