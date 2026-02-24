using Microsoft.AspNetCore.Identity;
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
            DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json.Content);

            foreach (DataColumn column in dataTable.Columns)
            {
                column.ColumnName = column.ColumnName.ToUpperInvariant();
            }

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
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
            return null;
        }
    }

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
            Msg.ShowMessageBox(ex.Message, Framework.Common.MessageType.Error);
            return Array.Empty<string>();
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
}

