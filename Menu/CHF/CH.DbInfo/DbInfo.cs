namespace CH.DbInfo;

public class DbInfo
{
    private string _spNameInsert;
    private string _spNameUpdate;
    private string _spNameDelete;
    private string[] _spParamsInsert;
    private string[] _spParamsUpdate;
    private string[] _spParamsDelete;
    private string _primaryKey;
    private object _dataValue;

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
    public string PrimaryKey
    {
        get => _primaryKey;
        set => _primaryKey = value;
    }
    public object DataValue
    {
        get => _dataValue;
        set => _dataValue = value;
    }
}
