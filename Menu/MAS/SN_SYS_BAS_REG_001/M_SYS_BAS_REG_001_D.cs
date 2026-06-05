using CH.Helper;
using System.Data;

namespace SYS;

public class M_SYS_BAS_REG_001_D
{
    public DataTable Search(object[] obj)
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
            dtSaveUseer.TableName = "SYS_USER";
            si.DataValue = dtSaveUseer.Copy();
            si.PrimaryKey = new string[] { "CD_COM", "CD_USER" };
            si.SpNameDelete = "AP_SYS_USER_D";
            si.SpParamsDelete = new string[] { "CD_COM", "CD_USER" };
        }

        if (dtSaveUseer != null)
        {
            dtSaveUseer.TableName = "SYS_USER";
            si.DataValue = dtSaveUseer.Copy();
            si.PrimaryKey = new string[] { "CD_COM", "CD_USER" };
            si.SpNameInsert = "AP_SYS_USER_I";
            si.SpNameUpdate = "AP_SYS_USER_U";

            si.SpParamsInsert = new string[] { "CD_COM", "CD_USER", "DC_PASSWORD", "NM_USER", "DT_REG", "DC_EMAIL", "DC_ADDRESS1", "DC_ADDRESS2", "NO_TEL", "YN_ACTIVE", "FG_ROLE", "CD_USER_REG" };
            si.SpParamsUpdate = new string[] { "CD_COM", "CD_USER", "DC_PASSWORD", "NM_USER", "DT_REG", "DC_EMAIL", "DC_ADDRESS1", "DC_ADDRESS2", "NO_TEL", "YN_ACTIVE", "FG_ROLE", "CD_USER_AMD" };
        }
        return DataHelper.Save(si);
    }
}
