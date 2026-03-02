using System.Data;
using System.Runtime.Versioning;

namespace CH.Helper;
[SupportedOSPlatform("windows")]
internal static class DBHelper
{

    public static DataTable GetDataTable(string Query)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.FillDataTable(Query);
        }
        catch
        {
            throw;
        }
    }


    // Simple DataTable retrieval
    public static DataTable GetDataTable(string spName, object[] parameters)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.FillDataTable(spName, parameters);
        }
        catch
        {
            throw;
        }
    }

    public static DataSet GetDataSet(string Query)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.FillResultSet(Query);
        }
        catch
        {
            throw;
        }
    }

    // Simple DataSet retrieval
    public static DataSet GetDataSet(string spName, object[] parameters, string[] parameterNames = null)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.FillResultSet(spName, parameters);
        }
        catch
        {
            throw;
        }
    }

    public static object ExecuteScalar(string Query)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.ExecuteScalar(Query, CommandType.Text, null);
        }
        catch
        {
            throw;
        }
    }

    public static object ExecuteScalar(string Query, CommandType cmdType, object[] cmdParams)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.ExecuteScalar(Query, cmdType, cmdParams);
        }
        catch
        {
            throw;
        }
    }

    public static int ExecuteNonQuery(string Query)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.ExecuteNonQuery(Query, CommandType.Text, null);
        }
        catch
        {
            throw;
        }
    }

    public static int ExecuteNonQuery(string Query, CommandType cmdType, object[] cmdParams)
    {
        try
        {
            DBStarter dbStarter = DBStarter.GetInstance();
            return dbStarter.ExecuteNonQuery(Query, cmdType, cmdParams);
        }
        catch
        {
            throw;
        }
    }

    // Save single table
    public static bool Save(DbInfo info)
    {
        DBStarter dbStarter = DBStarter.GetInstance();
        try
        {
            if (CH.AppContext.IsDbMode)
            {
                dbStarter.BeginTransaction();
                bool result = dbStarter.Save(info);
                dbStarter.CommitTransaction();
                return result;
            }
            else
            {
                // later
                return true;
            }

        }
        catch
        {
            dbStarter.RollbackTransaction();
            throw;
        }
    }

    // Save multiple tables in a transaction
    public static bool Save(DbInfoCollection infos)
    {
        DBStarter dbStarter = DBStarter.GetInstance();
        try
        {
            dbStarter.BeginTransaction();
            bool result = dbStarter.Save(infos);
            dbStarter.CommitTransaction();
            return result;
        }
        catch
        {
            dbStarter.RollbackTransaction();
            throw;
        }
    }
}
