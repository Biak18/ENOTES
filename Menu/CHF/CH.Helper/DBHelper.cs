using System.Data;

namespace CH.Helper;

public static class DBHelper
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
    public static DataSet GetDataSet(string spName, object[] parameters)
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
            dbStarter.BeginTransaction();
            bool result = dbStarter.Save(info);
            dbStarter.CommitTransaction();
            return result;
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
