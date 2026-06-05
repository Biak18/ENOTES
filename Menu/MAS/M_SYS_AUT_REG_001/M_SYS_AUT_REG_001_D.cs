using CH.Helper;
using System.Data;

namespace SYS;

public class M_SYS_AUT_REG_001_D
{
    // Available Menus
    public DataTable SearchAvailableMenus(object[] obj)
    {
        return DataHelper.GetDataTable("AP_SYS_AUT_REG_001_01_S", obj);
    }

    // All Companies, use existing proc
    public DataTable SearchCompanies(object[] obj)
    {
        return DataHelper.GetDataTable("AP_MAS_ORG_REG_001_01_S", obj);
    }

    // All Users, use existing proc
    public DataTable SearchUsers(object[] obj)
    {
        return DataHelper.GetDataTable("AP_SYS_BAS_REG_001_01_S", obj);
    }

    //public async Task<DataTable> Search(object[] obj)
    //{
    //    return await WebHelper.GetDataTable("AP_CUS_REG_001_S", obj);
    //}

    internal bool Save(DataTable dtSaveUseer)
    {
        //DbInfoCollection sc = new DbInfoCollection();
        DbInfo si = new DbInfo();
        if (dtSaveUseer != null)
        {
            dtSaveUseer.TableName = "SYS_COMPANYMENU";
            si.DataValue = dtSaveUseer.Copy();
            si.PrimaryKey = new string[] { "CD_COMPANY", "CD_MENU" };
            si.SpNameDelete = "AP_SYS_COMPANYMENU_D";
            si.SpParamsDelete = new string[] { "CD_COMPANY", "CD_MENU" };
        }

        if (dtSaveUseer != null)
        {
            dtSaveUseer.TableName = "SYS_COMPANYMENU";
            si.DataValue = dtSaveUseer.Copy();
            si.PrimaryKey = new string[] { "CD_COMPANY", "CD_MENU" };
            si.SpNameInsert = "AP_SYS_COMPANYMENU_I";
            si.SpNameUpdate = "AP_SYS_COMPANYMENU_U";

            si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_MENU" };
            si.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_MENU" };
        }
        return DataHelper.Save(si);
    }
}
