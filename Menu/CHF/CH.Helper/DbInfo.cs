using System.Data;
using System.Linq;

namespace CH.Helper;

public class DbInfo
{
    private string _spNameInsert;
    private string _spNameUpdate;
    private string _spNameDelete;
    private string[] _spParamsInsert;
    private string[] _spParamsUpdate;
    private string[] _spParamsDelete;
    private string[] _primaryKey;
    private DataTable _dataValue;

    public string SpNameInsert
    {
        get => _spNameInsert;
        set => _spNameInsert = value;
    }

    public string SpNameUpdate
    {
        get => _spNameUpdate;
        set => _spNameUpdate = value;
    }

    public string SpNameDelete
    {
        get => _spNameDelete;
        set => _spNameDelete = value;
    }

    public string[] SpParamsInsert
    {
        get => _spParamsInsert;
        set => _spParamsInsert = value;
    }

    public string[] SpParamsUpdate
    {
        get => _spParamsUpdate;
        set => _spParamsUpdate = value;
    }
    public string[] SpParamsDelete
    {
        get => _spParamsDelete;
        set => _spParamsDelete = value;
    }
    public string[] PrimaryKey
    {
        get => _primaryKey;
        set => _primaryKey = value;
    }
    public DataTable DataValue
    {
        get => _dataValue;
        set => _dataValue = value;
    }

    // Auto convert to WebInfo
    public WebInfo ToWebInfo() => new WebInfo
    {
        DataValue = DataValue,
        SpNameInsert = SpNameInsert?.ToLower(),
        SpParamsInsert = SpParamsInsert?.Select(c => c.ToLower()).ToArray(),
        SpNameUpdate = SpNameUpdate?.ToLower(),
        SpParamsUpdate = SpParamsUpdate?.Select(c => c.ToLower()).ToArray(),
        SpNameDelete = SpNameDelete?.ToLower(),
        SpParamsDelete = SpParamsDelete?.Select(c => c.ToLower()).ToArray(),
    };
}
