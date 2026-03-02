using System;
using System.Data;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace CH.Helper;

[SupportedOSPlatform("windows")]
public static class DataHelper
{
    #region GetDataTable
    public static DataTable GetDataTable(string query)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().FillDataTable(query);
            else
                return Task.Run(async () =>
                    await WebStarter.GetDataTable(query, Array.Empty<object>())
                ).GetAwaiter().GetResult();
        }
        catch { throw; }
    }

    public static DataTable GetDataTable(string spName, object[] parameters)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().FillDataTable(spName, parameters);
            else
                return Task.Run(async () =>
                    await WebStarter.GetDataTable(spName, parameters)
                ).GetAwaiter().GetResult();
        }
        catch { throw; }
    }
    #endregion

    #region GetDataSet
    public static DataSet GetDataSet(string query)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().FillResultSet(query);
            else
                throw new NotSupportedException("GetDataSet is not supported in Web mode. Use GetDataTable instead.");
        }
        catch { throw; }
    }

    public static DataSet GetDataSet(string spName, object[] parameters)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().FillResultSet(spName, parameters);
            else
                throw new NotSupportedException("GetDataSet is not supported in Web mode. Use GetDataTable instead.");
        }
        catch { throw; }
    }
    #endregion

    #region ExecuteScalar
    public static object ExecuteScalar(string query)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().ExecuteScalar(query, CommandType.Text, null);
            else
                return Task.Run(async () =>
                {
                    DataTable dt = await WebStarter.GetDataTable(query, Array.Empty<object>());
                    return dt?.Rows.Count > 0 ? dt.Rows[0][0] : null;
                }).GetAwaiter().GetResult();
        }
        catch { throw; }
    }

    public static object ExecuteScalar(string spName, CommandType cmdType, object[] cmdParams)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().ExecuteScalar(spName, cmdType, cmdParams);
            else
                return Task.Run(async () =>
                {
                    DataTable dt = await WebStarter.GetDataTable(spName, cmdParams ?? Array.Empty<object>());
                    return dt?.Rows.Count > 0 ? dt.Rows[0][0] : null;
                }).GetAwaiter().GetResult();
        }
        catch { throw; }
    }
    #endregion

    #region ExecuteNonQuery
    public static int ExecuteNonQuery(string query)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().ExecuteNonQuery(query, CommandType.Text, null);
            else
                return Task.Run(async () =>
                {
                    await WebStarter.GetDataTable(query, Array.Empty<object>());
                    return 1;
                }).GetAwaiter().GetResult();
        }
        catch { throw; }
    }

    public static int ExecuteNonQuery(string spName, CommandType cmdType, object[] cmdParams)
    {
        try
        {
            if (CH.AppContext.IsDbMode)
                return DBStarter.GetInstance().ExecuteNonQuery(spName, cmdType, cmdParams);
            else
                return Task.Run(async () =>
                {
                    await WebStarter.GetDataTable(spName, cmdParams ?? Array.Empty<object>());
                    return 1;
                }).GetAwaiter().GetResult();
        }
        catch { throw; }
    }
    #endregion

    #region Save
    public static bool Save(DbInfo info)
    {
        DBStarter dbStarter = null;
        try
        {
            if (CH.AppContext.IsDbMode)
            {
                dbStarter = DBStarter.GetInstance();
                dbStarter.BeginTransaction();
                bool result = dbStarter.Save(info);
                dbStarter.CommitTransaction();
                return result;
            }
            else
            {
                return Task.Run(async () =>
                    await WebStarter.Save(info.ToWebInfo())
                ).GetAwaiter().GetResult();
            }
        }
        catch
        {
            dbStarter?.RollbackTransaction();
            throw;
        }
    }

    public static bool Save(DbInfoCollection infos)
    {
        DBStarter dbStarter = null;
        try
        {
            if (CH.AppContext.IsDbMode)
            {
                dbStarter = DBStarter.GetInstance();
                dbStarter.BeginTransaction();
                bool result = dbStarter.Save(infos);
                dbStarter.CommitTransaction();
                return result;
            }
            else
            {
                return Task.Run(async () =>
                {
                    var web = new WebInfoCollection();
                    infos.ForEach(i => web.Add(i.ToWebInfo()));
                    return await WebStarter.Save(web);
                }).GetAwaiter().GetResult();
            }
        }
        catch
        {
            dbStarter?.RollbackTransaction();
            throw;
        }
    }
    #endregion
}