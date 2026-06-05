using CH.Helper;
using System.Data;

namespace SYS;

public class M_SYS_AUT_REG_003_D
{
    // Available Menus
    public DataTable SearchMenus(object[] obj)
    {
        return DataHelper.GetDataTable("AP_SYS_AUT_REG_003_01_S", obj);
    }


    // All Users base on company code
    public DataTable SearchUsers(object[] obj)
    {
        return DataHelper.GetDataTable("AP_SYS_BAS_REG_001_01_S", obj);
    }

    internal bool Save(DataTable dtSave)
    {
        dtSave.AcceptChanges();

        DataRow[] rowsToProcess = dtSave.Select();
        foreach (DataRow row in rowsToProcess)
        {
            string crudType = A.GetString(row["CRUD_TYPE"]);

            if (crudType == "D")
            {
                row.Delete();
            }
            else if (crudType == "I")
            {
                row.SetAdded();
            }
            else if (crudType == "U")
            {
                row.SetModified();
            }
        }

        DbInfo si = new DbInfo();
        dtSave.TableName = "SYS_USERMENU";
        si.DataValue = dtSave.Copy();

        si.PrimaryKey = new string[] { "CD_COMPANY", "CD_USER", "CD_MENU" };

        si.SpNameInsert = "AP_SYS_USERMENU_I";
        si.SpNameDelete = "AP_SYS_USERMENU_D";
        si.SpNameUpdate = "AP_SYS_USERMENU_U"; // Add your Update Stored Procedure here!

        si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_USER", "CD_MENU", "FG_ACTION" };
        si.SpParamsDelete = new string[] { "CD_COMPANY", "CD_USER", "CD_MENU" };
        si.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_USER", "CD_MENU", "FG_ACTION" };

        return DataHelper.Save(si);
    }
}
