using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace CH.Helper;
[SupportedOSPlatform("windows")]
public static class WebStarter
{
    private static Client _client;

    #region Default User
    [Table("sys_user")]
    public class SysUser : BaseModel
    {
        [PrimaryKey("cd_com", false)]
        [Column("cd_com")]
        public string CdCom { get; set; }

        [PrimaryKey("cd_user", false)]
        [Column("cd_user")]
        public string CdUser { get; set; }

        [Column("dc_password")]
        public string DcPassword { get; set; }

        [Column("nm_user")]
        public string NmUser { get; set; }

        // Stored as CHAR(8) like YYYYMMDD
        [Column("dt_reg")]
        public string DtReg { get; set; }

        [Column("dc_email")]
        public string DcEmail { get; set; }

        [Column("dc_address1")]
        public string DcAddress1 { get; set; }

        [Column("dc_address2")]
        public string DcAddress2 { get; set; }

        [Column("no_tel")]
        public string NoTel { get; set; }

        [Column("yn_active")]
        public string YnActive { get; set; }

        [Column("fg_role")]
        public string FgRole { get; set; }

        [Column("tm_reg")]
        public DateTime TmReg { get; set; }

        [Column("cd_user_reg")]
        public string CdUserReg { get; set; }

        [Column("tm_amd")]
        public DateTime TmAmd { get; set; }

        [Column("cd_user_amd")]
        public string CdUserAmd { get; set; }
    }

    private static async void CreateSystemUser()
    {
        try
        {
            var hasher = new PasswordHasher<string>();
            string tempPassword = "1"; // temp
            string hashedPassword = hasher.HashPassword(null, tempPassword);
            SysUser sysUser = new SysUser()
            {
                CdCom = "SYSTEM",
                CdUser = "SYSTEM",
                DcPassword = hashedPassword,
                NmUser = "System Administrator",
                DtReg = DateTime.Now.ToString("yyyyMMdd"),
                YnActive = "Y",
                FgRole = "SUPER_ADMIN"
            };

            var insertTask = await _client.From<SysUser>().Insert(sysUser);
        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
        }
    }
    #endregion

    public static async Task Init()
    {
        try
        {
            if (_client != null)
                return;

            //var options = new SupabaseOptions
            //{
            //    AutoConnectRealtime = true
            //};

            string key = SecureStore.Unprotect(CH.AppContext.Key);
            _client = new Client(CH.AppContext.Url, key);
            await _client.InitializeAsync();
            //_client.Auth.AddStateChangedListener((sender, changed) =>
            //{
            //    switch (changed)
            //    {
            //        case AuthState.SignedIn:
            //            break;
            //        case AuthState.SignedOut:
            //            break;
            //        case AuthState.UserUpdated:
            //            break;
            //        case AuthState.PasswordRecovery:
            //            break;
            //        case AuthState.TokenRefreshed:
            //            break;
            //    }
            //});
            var user = await _client.From<SysUser>().Limit(1).Get();

            if (!user.Models.Any())
            {
                CreateSystemUser();
            }

        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
        }
    }



    public static async Task<Client> GetClient()
    {
        await Init();
        return _client;
    }

    // for get datatable
    private static async Task<List<T>> GetData<T>(string fnName, object param = null)
    {
        try
        {
            await Init();
            var json = await _client.Rpc(fnName.ToLower(), param);


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json.Content);

            return data;
        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
            return null;
        }
    }

    private static async Task<List<dynamic>> GetData(string fnName, object param = null)
    {
        try
        {
            await Init();
            var json = await _client.Rpc(fnName.ToLower(), param);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(json.Content);
            return data;
        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
            return null;
        }
    }

    private static async Task<BaseResponse> GetJsonData(string fnName, object param = null)
    {
        try
        {
            await Init();
            var json = await _client.Rpc(fnName.ToLower(), param);
            return json;
        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
            return null;
        }
    }


    public static async Task<DataTable> GetDataTable(string fnName, object[] parameterValues)
    {
        try
        {
            await Init();
            string[] paramNames = await GetFuncArgs(fnName);
            object rpcParam = ConvertParams(parameterValues, paramNames);
            var json = await _client.Rpc(fnName.ToLower(), rpcParam);
            DataTable dataTable;

            if (string.IsNullOrEmpty(json.Content) || json.Content.Trim() == "[]")
            {
                dataTable = await BuildEmptyTable(fnName);
            }
            else
            {
                dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json.Content);
            }


            foreach (DataColumn column in dataTable.Columns)
            {
                column.ColumnName = column.ColumnName.ToUpperInvariant();
            }
            dataTable.AcceptChanges();
            Debug.WriteLine("■■■■■■■■■■FillDataTable■■■■■■■■■■");
            Debug.WriteLine("■■■■■spName = " + fnName);
            for (int i = 0; i < parameterValues.Length; i++)
            {
                Debug.WriteLine("■■■■■parameters[{0}] = {1}", i, parameterValues[i]);
            }
            return dataTable;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static async Task<DataTable> BuildEmptyTable(string fnName)
    {
        try
        {
            var json = await _client.Rpc("get_function_return_columns", new { p_fn_name = fnName.ToLower() });

            if (string.IsNullOrEmpty(json.Content) || json.Content.Trim() == "[]")
                return new DataTable();

            var columns = JsonConvert.DeserializeObject<List<ReturnColumn>>(json.Content);

            var dt = new DataTable();

            foreach (var col in columns)
            {
                dt.Columns.Add(col.ColumnName.ToUpperInvariant(), PgTypeToClr(col.DataType));
            }

            return dt;
        }
        catch
        {
            return new DataTable();
        }
    }

    private class ReturnColumn
    {
        [JsonProperty("column_name")]
        public string ColumnName { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }
    }

    private static Type PgTypeToClr(string pgType) => pgType?.ToLower() switch
    {
        "int4" or "int2" or "integer" => typeof(int),
        "int8" or "bigint" => typeof(long),
        "bool" or "boolean" => typeof(bool),
        "numeric" or "decimal" or "float8" => typeof(decimal),
        "timestamp" or "timestamptz" => typeof(DateTime),
        "text" or "varchar" or "bpchar" => typeof(string),
        _ => typeof(string)
    };

    private static async Task<string[]> GetFuncArgs(string fnName)
    {
        try
        {
            await Init();

            var result = await _client.Rpc(
                "get_function_args",
                new { p_name = fnName.ToLower() }
            );

            string args = result.Content.ToString().Replace("\"", "");//REMOVE "

            if (string.IsNullOrWhiteSpace(args))
                return Array.Empty<string>();

            // "p_cd_menu character varying, p_nm_menu character varying"
            var parameters = args
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
               .Select(p => p.Trim().Split(' ', 2)[0])// name only
               .ToArray();

            return parameters;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    private static object ConvertParams(object[] parameters, string[] paramNames)
    {
        if (parameters == null || paramNames == null)
            return null;

        if (parameters.Length != paramNames.Length)
            throw new ArgumentException("Parameters count mismatch.");

        var dict = new Dictionary<string, object>();
        for (int i = 0; i < parameters.Length; i++)
        {
            dict[paramNames[i]] = parameters[i] ?? DBNull.Value;
        }

        return dict;
    }


    public static DataTable ToDataTable<T>(this IList<T> data)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();

        // Create columns
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            table.Columns.Add(prop.Name.ToUpper(), typeof(object));
        }

        // Fill rows
        foreach (T item in data)
        {
            object[] values = new object[props.Count];

            for (int i = 0; i < props.Count; i++)
            {
                var val = props[i].GetValue(item);
            }

            table.Rows.Add(values);
        }

        return table;
    }

    public static async Task<bool> Save(WebInfo info)
    {
        try
        {
            DataTable dt = info.DataValue;

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Added)
                    await ExecuteRpc(row, info.SpNameInsert, info.SpParamsInsert);
                else if (row.RowState == DataRowState.Modified)
                    await ExecuteRpc(row, info.SpNameUpdate, info.SpParamsUpdate);
                else if (row.RowState == DataRowState.Deleted)
                    await ExecuteRpc(row, info.SpNameDelete, info.SpParamsDelete);
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<bool> Save(WebInfoCollection collection)
    {
        try
        {
            foreach (var info in collection)
            {
                DataTable dt = info.DataValue;
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                        await ExecuteRpc(row, info.SpNameInsert, info.SpParamsInsert);
                    else if (row.RowState == DataRowState.Modified)
                        await ExecuteRpc(row, info.SpNameUpdate, info.SpParamsUpdate);
                    else if (row.RowState == DataRowState.Deleted)
                        await ExecuteRpc(row, info.SpNameDelete, info.SpParamsDelete);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static async Task ExecuteRpc(DataRow dataRow, string fnName, string[] colNames)
    {
        await Init();

        Debug.WriteLine($"Executing: {fnName}");

        string[] paramNames = await GetFuncArgs(fnName);

        if (paramNames.Length == 0) return;

        if (colNames.Length != paramNames.Length)
            throw new Exception("The number of columns does not match the number of parameters.");

        var dict = new Dictionary<string, object>();
        for (int i = 0; i < colNames.Length; i++)
        {
            string colName = colNames[i];

            object value = dataRow.RowState == DataRowState.Deleted
                ? dataRow[colName, DataRowVersion.Original]
                : dataRow[colName];

            if (colName.StartsWith("dt") && value != null && value.ToString().Length > 8)
            {
                value = value.ToString().Substring(0, 10).Replace("-", "");
            }

            dict[paramNames[i]] = value ?? DBNull.Value;

            Debug.WriteLine($"■■■■■ {paramNames[i]} = {dict[paramNames[i]]}");
        }

        await _client.Rpc(fnName.ToLower(), dict);
    }
}

