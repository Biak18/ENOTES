using CH.Grid;
using System;
using System.Data;
using System.Runtime.Versioning;

namespace CH.Helper;

//Grid Helper
[SupportedOSPlatform("windows")]
public class GH
{
    public static bool GridModifyCheck(CHGrid grd, bool yn_nullchk)
    {
        bool result = false;
        DataTable dataTable = null;
        try
        {
            dataTable = grd.GetChanges(yn_nullchk);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return result;
    }
}
