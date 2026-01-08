using CH.Helper;
using System.Data;

namespace M_TEST_001;

public class M_TEST_001_D
{
    public DataTable Search(object[] obj)
    {
        return DBHelper.GetDataTable("AP_CUS_REG_001_S", obj);
    }

    internal bool Save(DataTable dtSaveMenu)
    {
        //DbInfoCollection sc = new DbInfoCollection();
        DbInfo si = new DbInfo();
        if (dtSaveMenu != null)
        {

            dtSaveMenu.TableName = "SYS_MENU";
            si.DataValue = dtSaveMenu.Copy();
            si.PrimaryKey = new string[] { "CD_MENU" };
            si.SpNameDelete = "AP_SYS_MENU_D";
            si.SpParamsDelete = new string[] { "CD_MENU" };
        }

        if (dtSaveMenu != null)
        {
            dtSaveMenu.TableName = "SYS_MENU";
            si.DataValue = dtSaveMenu.Copy();
            si.PrimaryKey = new string[] { "CD_MENU" };
            si.SpNameInsert = "AP_SYS_MENU_I";
            si.SpNameUpdate = "AP_SYS_MENU_U";

            si.SpParamsInsert = new string[] { "CD_MENU", "NM_MENU", "NM_NETWINDOW", "FG_TYPE", "CD_MODULE", "NO_POS", "CD_MENU_PARENT" };
            si.SpParamsUpdate = new string[] { "CD_MENU", "NM_MENU", "NM_NETWINDOW", "FG_TYPE", "CD_MODULE", "NO_POS", "CD_MENU_PARENT" };
        }
        return DBHelper.Save(si);
    }
}
