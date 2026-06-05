using CH.Helper;
using System.Data;

namespace MAS;

public class M_MAS_ORG_REG_001_D
{
    public DataTable Search(object[] obj)
    {
        return DataHelper.GetDataTable("AP_MAS_ORG_REG_001_01_S", obj);
    }

    internal bool Save(DataTable dtSaveUseer)
    {
        //DbInfoCollection sc = new DbInfoCollection();
        DbInfo si = new DbInfo();
        if (dtSaveUseer != null)
        {
            dtSaveUseer.TableName = "MAS_COMPANY";
            si.DataValue = dtSaveUseer.Copy();
            si.PrimaryKey = new string[] { "CD_COMPANY" };
            si.SpNameDelete = "AP_MAS_COMPANY_D";
            si.SpParamsDelete = new string[] { "CD_COMPANY" };

        }

        if (dtSaveUseer != null)
        {
            dtSaveUseer.TableName = "MAS_COMPANY";
            si.DataValue = dtSaveUseer.Copy();
            si.PrimaryKey = new string[] { "CD_COMPANY" };
            si.SpNameInsert = "AP_MAS_COMPANY_I";
            si.SpNameUpdate = "AP_MAS_COMPANY_U";

            si.SpParamsInsert = new string[] { "CD_COMPANY", "NM_COMPANY", "NM_SHORT", "DC_IMAGE_URL", "DC_ADDRESS1", "DC_ADDRESS2", "DC_CITY", "DC_STATE", "DC_POSTAL_CODE", "DC_COUNTRY", "NO_PHONE", "NO_FAX", "DC_EMAIL", "DC_WEBSITE", "NO_TAX_ID", "NO_REG_NO", "FL_ACTIVE" };
            si.SpParamsUpdate = new string[] { "CD_COMPANY", "NM_COMPANY", "NM_SHORT", "DC_IMAGE_URL", "DC_ADDRESS1", "DC_ADDRESS2", "DC_CITY", "DC_STATE", "DC_POSTAL_CODE", "DC_COUNTRY", "NO_PHONE", "NO_FAX", "DC_EMAIL", "DC_WEBSITE", "NO_TAX_ID", "NO_REG_NO", "FL_ACTIVE" };
        }
        return DataHelper.Save(si);
    }
}
