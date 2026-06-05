using CH.Helper;
using System.Data;

namespace SYS;

public class M_SYS_AUT_REG_002_D
{
    // Available Menus
    public DataTable SearchMenus()
    {
        return DataHelper.GetDataTable("AP_SYS_AUT_REG_002_01_S");
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
        }

        DbInfo si = new DbInfo();
        dtSave.TableName = "SYS_ROLEMENU";
        si.DataValue = dtSave.Copy();

        si.PrimaryKey = new string[] { "FG_ROLE", "CD_MENU" };

        si.SpNameInsert = "AP_SYS_ROLEMENU_I";
        si.SpNameDelete = "AP_SYS_ROLEMENU_D";

        si.SpParamsInsert = new string[] { "FG_ROLE", "CD_MENU" };
        si.SpParamsDelete = new string[] { "FG_ROLE", "CD_MENU" };

        return DataHelper.Save(si);
    }
}
