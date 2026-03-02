using CH.Helper;
using System.Data;

namespace ENOTES;

internal class ENOTES_D
{
    internal DataTable SearchMenu(object[] obj)
    {
        return DataHelper.GetDataTable("AP_ENOTES_001_S", obj);
    }

    internal DataTable GetUserInfo(object[] obj)
    {

        return DataHelper.GetDataTable("AP_ENOTES_002_S", obj);
    }
}
