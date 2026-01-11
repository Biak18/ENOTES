using DevExpress.Mvvm.Native;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CH.Helper;

public class DBStarter
{
    private DbTransaction tran = null;

    private DbConnection cn = null;

    private DbCommand cmd = null;

    private string _connectionString;

    private static DBStarter instance;

    public DBStarter()
    {
        string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataBaseSettings.ini");

        string dataSource = IniFile.IniReadValue("Database", "DataSource", iniPath);
        string catalog = IniFile.IniReadValue("Database", "InitialCatalog", iniPath);
        string userId = IniFile.IniReadValue("Database", "UserId", iniPath);
        string password = IniFile.IniReadValue("Database", "Password", iniPath);
        string encrypt = IniFile.IniReadValue("Database", "Encrypt", iniPath);
        string trustCert = IniFile.IniReadValue("Database", "TrustServerCertificate", iniPath);


        if (string.IsNullOrWhiteSpace(dataSource))
            throw new Exception("Database DataSource is missing in INI file.");

        _connectionString =
        $"Data Source={dataSource};" +
        $"Initial Catalog={catalog};" +
        $"Integrated Security=False;" +
        $"User ID={userId};" +
        $"Password={password};" +
        $"Encrypt={encrypt};" +
        $"TrustServerCertificate={trustCert};";
    }

    public DBStarter(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static DBStarter GetInstance()
    {
        if (!CheckDbConnection())
        {
            throw new Exception("Please check you internet.");
        }

        if (instance == null)
        {
            instance = new DBStarter();
            instance.InitializeSystemUserIfNeeded();
        }

        return instance;
    }

    private static bool CheckDbConnection()
    {
        try
        {
            string connectionString = "Data Source=192.168.100.54,1433;Initial Catalog=THEDUMPDUMP;Integrated Security=False;User ID=CHAN;Password=CHAN;Encrypt=False;TrustServerCertificate=True;";
            SqlConnection val = new SqlConnection(connectionString);
            try
            {
                ((DbConnection)(object)val).Open();
                return true;
            }
            finally
            {
                ((IDisposable)val)?.Dispose();
            }
        }
        catch
        {
            //throw new Exception(ex.Message);
            return false;
        }
    }

    public void InitializeSystemUserIfNeeded()
    {
        object cnt = ExecuteScalar("SELECT COUNT(1) FROM SYS_USER", CommandType.Text, null);

        if ((int)cnt == 0)
        {
            CreateSystemUser();
        }
    }

    private void CreateSystemUser()
    {
        var hasher = new PasswordHasher<string>();
        string tempPassword = "1"; // temp
        string hashedPassword = hasher.HashPassword(null, tempPassword);

        object[] obj = new object[]
        {
            "SYSTEM",
            "SYSTEM",
            hashedPassword,
            "System Administrator",
            DateTime.Today.ToString("yyyyMMdd"),
            DBNull.Value,
            DBNull.Value,
            DBNull.Value,
            DBNull.Value,
            "Y",
            "SUPER_ADMIN",
            DBNull.Value
        };

        ExecuteNonQuery("AP_SYS_USER_I", CommandType.StoredProcedure, obj);
    }


    protected internal static void ResetInstance()
    {
        instance = new DBStarter();
    }

    protected internal static void ResetInstance(string connectionString)
    {
        instance = new DBStarter(connectionString);
    }

    private void CreateConnection()
    {
        if (cn == null)
        {
            cn = new SqlConnection(_connectionString);
        }
    }

    private void CreateCommand()
    {
        if (cmd == null)
        {
            cmd = new SqlCommand();

            if (cn == null)
            {
                CreateConnection();
            }

            cmd.Connection = cn;
            cmd.CommandTimeout = 12000;

            if (tran != null)
            {
                cmd.Transaction = tran;
            }
        }
    }

    public void BeginTransaction()
    {
        if (tran != null)
        {
            return;
        }
        try
        {
            if (cn == null)
            {
                CreateConnection();
            }

            if (cn.State != System.Data.ConnectionState.Open)
            {
                cn.Open();
            }

            tran = cn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }
        catch (SqlException)
        {
            cn.Close();
            throw;
        }
        catch (Exception)
        {
            cn.Close();
            throw;
        }
    }

    public void CommitTransaction()
    {
        if (tran == null)
        {
            return;
        }

        try
        {
            tran.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            cn.Close();
            tran.Dispose();
            tran = null;
        }
    }

    public void RollbackTransaction()
    {
        if (tran == null)
        {
            return;
        }

        try
        {
            tran.Rollback();
        }
        finally
        {
            cn.Close();
            tran.Dispose();
            tran = null;
        }
    }

    private void ResetCommand(string query, CommandType commandType)
    {
        if (cn == null)
        {
            CreateConnection();
        }

        if (cmd == null)
        {
            CreateCommand();
        }

        cmd.Connection = cn;
        cmd.CommandText = query;
        cmd.CommandType = commandType;

        if (tran != null)
        {
            cmd.Transaction = tran;
        }

        if (cmd.Parameters != null)
        {
            cmd.Parameters.Clear();
        }

        if (cn.State != ConnectionState.Open)
        {
            cn.Open();
        }
    }


    private string[] GetParameters(string spName)
    {
        ResetCommand(spName, CommandType.StoredProcedure);
        SqlCommandBuilder.DeriveParameters((SqlCommand)cmd);

        string[] parameterNames = cmd.Parameters
            .Cast<SqlParameter>()
            .Select(p => p.ParameterName)
            .ToArray();

        return parameterNames;
    }

    public DataTable FillDataTable(string query)
    {
        try
        {
            ResetCommand(query, CommandType.Text);
            DataTable dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter((SqlCommand)cmd);
            Debug.WriteLine("■■■■■■■■■■FillDataTable■■■■■■■■■■");
            Debug.WriteLine("■■■■■Query = " + query);
            dataAdapter.Fill(dataTable);

            return dataTable;
        }
        catch
        {
            throw;
        }
    }

    public DataTable FillDataTable(string spName, object[] parameterValues)
    {
        try
        {
            ResetCommand(spName, CommandType.StoredProcedure);

            BaseDataProvider.LoadParameters((SqlCommand)cmd);

            // Count only input parameters
            var inputParams = cmd.Parameters
                .Cast<SqlParameter>()
                .Where(p => p.Direction == ParameterDirection.Input ||
                            p.Direction == ParameterDirection.InputOutput)
                .ToArray();

            if (inputParams.Length != parameterValues.Length)
            {
                throw new ArgumentException(
                    "Parameter count does not match parameter value count.");
            }

            // Assign values
            for (int i = 0; i < inputParams.Length; i++)
            {
                inputParams[i].Value = parameterValues[i] ?? DBNull.Value;
            }

            var dataAdapter = new SqlDataAdapter((SqlCommand)cmd);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Debug.WriteLine("■■■■■■■■■■FillDataTable■■■■■■■■■■");
            Debug.WriteLine("■■■■■spName = " + spName);
            for (int i = 0; i < parameterValues.Length; i++)
            {
                Debug.WriteLine("■■■■■parameters[{0}] = {1}", i, parameterValues[i]);
            }
            return dataTable;
        }
        catch
        {
            throw;
        }
    }

    public DataSet FillResultSet(string query)
    {
        try
        {
            ResetCommand(query, CommandType.Text);
            DataSet dataSet = new DataSet();
            var dataAdapter = new SqlDataAdapter((SqlCommand)cmd);
            Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
            Debug.WriteLine("■■■■■Query = " + query);
            dataAdapter.Fill(dataSet);

            return dataSet;
        }
        catch
        {
            throw;
        }
    }

    public DataSet FillResultSet(string spName, object[] parameterValues)
    {
        try
        {
            ResetCommand(spName, CommandType.StoredProcedure);
            BaseDataProvider.LoadParameters((SqlCommand)cmd);

            // Count only input parameters
            var inputParams = cmd.Parameters
                .Cast<SqlParameter>()
                .Where(p => p.Direction == ParameterDirection.Input ||
                            p.Direction == ParameterDirection.InputOutput)
                .ToArray();

            if (inputParams.Length != parameterValues.Length)
            {
                throw new ArgumentException(
              "Parameter count does not match parameter value count.");
            }

            for (int i = 0; i < inputParams.Length; i++)
            {
                inputParams[i].Value = parameterValues[i] ?? DBNull.Value;
            }

            var dataAdapter = new SqlDataAdapter((SqlCommand)cmd);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
            Debug.WriteLine("■■■■■spName = " + spName);
            for (int i = 0; i < parameterValues.Length; i++)
            {
                Debug.WriteLine("■■■■■parameters[{0}] = {1}", i, parameterValues[i]);
            }

            return dataSet;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public object ExecuteScalar(string cmdText, CommandType cmdType, object[] cmdParams)
    {
        try
        {
            ResetCommand(cmdText, cmdType);
            object result = null;
            if (cmdType == CommandType.StoredProcedure)
            {
                BaseDataProvider.LoadParameters((SqlCommand)cmd);

                // Count only input parameters
                var inputParams = cmd.Parameters
                    .Cast<SqlParameter>()
                    .Where(p => p.Direction == ParameterDirection.Input ||
                                p.Direction == ParameterDirection.InputOutput)
                    .ToArray();

                if (inputParams.Length != cmdParams.Length)
                {
                    throw new ArgumentException(
                  "Parameter count does not match parameter value count.");
                }

                for (int i = 0; i < inputParams.Length; i++)
                {
                    inputParams[i].Value = cmdParams[i] ?? DBNull.Value;
                }
            }
            result = cmd.ExecuteScalar();
            Debug.WriteLine("■■■■■■■■■■ExecuteScalar■■■■■■■■■■");
            Debug.WriteLine($"■■■■■Command: {cmdText}");

            if (cmdParams != null)
            {
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    Debug.WriteLine($"■■■■■ Param[{i}] = {cmdParams[i]}");
                }
            }
            return result;
        }
        catch
        {

            throw;
        }
    }

    public int ExecuteNonQuery(string spName, CommandType cmdType, object[] cmdParams)
    {
        try
        {
            ResetCommand(spName, cmdType);

            if (cmdType == CommandType.StoredProcedure)
            {
                BaseDataProvider.LoadParameters((SqlCommand)cmd);

                var inputParams = cmd.Parameters
                    .Cast<SqlParameter>()
                    .Where(p => p.Direction == ParameterDirection.Input ||
                                p.Direction == ParameterDirection.InputOutput)
                    .ToArray();

                if (inputParams.Length != (cmdParams?.Length ?? 0))
                {
                    throw new ArgumentException(
                        $"Parameter count mismatch. " +
                        $"Expected {inputParams.Length}, got {(cmdParams?.Length ?? 0)}");
                }

                // Assign parameter values
                for (int i = 0; i < inputParams.Length; i++)
                {
                    inputParams[i].Value = cmdParams[i] ?? DBNull.Value;
                }
            }
            else if (cmdType == CommandType.Text)
            {
                if (cmdParams != null)
                {
                    for (int i = 0; i < cmdParams.Length; i++)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = $"@p{i + 1}";
                        param.Value = cmdParams[i] ?? DBNull.Value;
                        cmd.Parameters.Add(param);
                    }
                }
            }

            int rowsAffected = cmd.ExecuteNonQuery();

            Debug.WriteLine("■■■■■■■■■■ ExecuteNonQuery ■■■■■■■■■■");
            Debug.WriteLine($"■■■■■ Command: {spName}");
            Debug.WriteLine($"■■■■■ Rows Affected: {rowsAffected}");

            if (cmdParams != null)
            {
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    Debug.WriteLine($"■■■■■ Param[{i}] = {cmdParams[i]}");
                }
            }

            return rowsAffected;
        }
        catch
        {
            throw;
        }
    }


    public bool Save(DbInfo info)
    {
        try
        {
            DataTable dt = info.DataValue;

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    ExecuteNonQuery(row, info.SpNameInsert, info.SpParamsInsert);
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    ExecuteNonQuery(row, info.SpNameUpdate, info.SpParamsUpdate);
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    ExecuteNonQuery(row, info.SpNameDelete, info.SpParamsDelete);
                }
            }
            return true;
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
    }

    public bool Save(DbInfoCollection collection)
    {
        try
        {
            foreach (var info in collection)
            {
                DataTable dt = info.DataValue;
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                        ExecuteNonQuery(row, info.SpNameInsert, info.SpParamsInsert);
                    else if (row.RowState == DataRowState.Modified)
                        ExecuteNonQuery(row, info.SpNameUpdate, info.SpParamsUpdate);
                    else if (row.RowState == DataRowState.Deleted)
                        ExecuteNonQuery(row, info.SpNameDelete, info.SpParamsDelete);
                }
            }
            return true;
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
    }

    private void ExecuteNonQuery(DataRow dataRow, string spName, string[] colNames)
    {
        ResetCommand(spName, CommandType.StoredProcedure);
        BaseDataProvider.LoadParameters((SqlCommand)cmd);

        Debug.WriteLine($"Executing: {spName}");

        int index = 0;

        if (colNames.Length != cmd.Parameters.Count - 1)
        {
            throw new Exception("The number of columns does not match the number of parameters.");
        }

        foreach (SqlParameter p in cmd.Parameters)
        {
            if (p.Direction == ParameterDirection.Input ||
                p.Direction == ParameterDirection.InputOutput)
            {
                string colName = colNames[index];

                if (dataRow.RowState == DataRowState.Deleted)
                    p.Value = dataRow[colName, DataRowVersion.Original] ?? DBNull.Value;
                else
                    p.Value = dataRow[colName] ?? DBNull.Value;

                index++;

                Debug.WriteLine("■■■■■parameters[{0}] = {1}", p, (dataRow.RowState == DataRowState.Deleted ? dataRow[colName, DataRowVersion.Original] : dataRow[colName]));
                if (index >= colNames.Length)
                    break;

            }
        }

        cmd.ExecuteNonQuery();
    }

}
