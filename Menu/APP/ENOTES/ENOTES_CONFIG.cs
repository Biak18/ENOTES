using CH.Framework.Common;
using CH.Framework.Win;
using CH.Helper;
using System.Data;
using System.IO;

namespace ENOTES
{
    public partial class ENOTES_CONFIG : FormBase
    {
        #region Initialize
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string _serverName = null;
        string _url = null;
        string _key = null;
        public ENOTES_CONFIG()
        {
            InitializeComponent();
            InitializeControl();

            InitializeEvent();
        }

        private void InitializeControl()
        {
            SetControl ctr = new SetControl();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CODE", typeof(string)));
            dt.Columns.Add(new DataColumn("NAME", typeof(string)));

            DataRow dr = dt.NewRow();
            dr["CODE"] = "Supabase";
            dr["NAME"] = "Supabase";
            dt.Rows.Add(dr);

            ctr.SetCombobox(chLookupedit1, dt);

            _serverName = IniFile.IniReadValue("Database", "DataSource", Path.Combine(basePath, "DataBaseSettings.ini"));
            ServerTextEdit.Text = _serverName;

            string webPath = Path.Combine(basePath, "WebSettings.ini");
            _url = IniFile.IniReadValue("Web", "Url", webPath);
            _key = IniFile.IniReadValue("Web", "Key", webPath);
            WebServiceUrl.Text = _url;
            WebServiceKey.Text = SecureStore.Unprotect(_key);

            if (CH.AppContext.IsDbMode)
            {
                ModeRadio.SelectedIndex = 0;
            }
            else
            {
                ModeRadio.SelectedIndex = 1;
                WebPanel.Visible = true;
                DBPanel.Visible = false;
            }
        }

        private void InitializeEvent()
        {
            TopPanel.MouseDown += TopPanel_MouseDown;
            BtnClose.Click += BtnClose_Click;

            ModeRadio.EditValueChanged += ModeRadio_EditValueChanged;
            BtnDBCheck.Click += BtnDBCheck_Click;
            BtnOk.Click += BtnOk_Click;
            BtnCancel.Click += BtnCancel_Click;

            BtnWebUrlCheck.Click += BtnWebUrlCheck_Click;
        }
        #endregion

        #region Events
        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            BorderlessHelper.MouseMove(this.Handle);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModeRadio_EditValueChanged(object sender, EventArgs e)
        {
            bool webVisible = (int)ModeRadio.EditValue == 0;

            DBPanel.Visible = webVisible;
            WebPanel.Visible = !webVisible;
        }

        private void BtnDBCheck_Click(object sender, EventArgs e)
        {
            try
            {
                string server = ServerTextEdit.Text.Trim();

                if (string.IsNullOrEmpty(server))
                {
                    Msg.ShowMessageBox("Server is required.", MessageType.Warning);
                    ServerTextEdit.Focus();
                    return;
                }

                string error = "";

                bool isValidDb = DbConnectionTester.TestByServer(server, out error);


                if (!isValidDb)
                {
                    Msg.ShowMessageBox($"Connection failed: {error}", MessageType.Error);
                    return;
                }
                Msg.ShowMessageBox("Database connection successful.", MessageType.Information);
                _serverName = server;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void BtnWebUrlCheck_Click(object sender, EventArgs e)
        {
            try
            {
                string url = WebServiceUrl.Text.Trim();
                string key = WebServiceKey.Text.Trim();

                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
                {
                    Msg.ShowMessageBox("The Url and Key are required.", MessageType.Error);
                    return;
                }
                string errorMessage = "";

                bool isValidWebUrl = await ApiConnectionTester.TestSupaBaseAsync(url, key, msg => errorMessage = msg);
                if (!isValidWebUrl)
                {
                    Msg.ShowMessageBox($"Connection failed: {errorMessage}", MessageType.Error);
                    return;
                }
                Msg.ShowMessageBox("Web connection successful.", MessageType.Information);
                _url = url;
                _key = key;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            bool isDbDirect = (int)ModeRadio.EditValue == 0;

            if (isDbDirect)
            {
                if (_serverName != ServerTextEdit.Text.Trim())
                {
                    Msg.ShowMessageBox(
                        "Please check the database connection before saving.",
                        MessageType.Warning
                    );
                    return;
                }

                string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataBaseSettings.ini");
                IniFile.IniWriteSingle("Database", "DataSource", _serverName, iniPath);//save the sername to DataBaseSettings config
            }
            else
            {
                if (_url != WebServiceUrl.Text.Trim())
                {
                    Msg.ShowMessageBox(
                    "Please check the web connection before saving.",
                    MessageType.Warning
                );
                    return;
                }
                // web
                string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebSettings.ini");
                IniFile.IniWriteSingle("Web", "Url", _url, iniPath);
                IniFile.IniWriteSingle("Web", "Key", SecureStore.Protect(_key), iniPath);
            }

            IniFile.IniWriteSingle("App", "ConnectionMode", isDbDirect ? "DbDirect" : "WebService", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.ini"));
            CH.AppContext.Configure(isDbDirect ? CH.ConnectionMode.DbDirect : CH.ConnectionMode.Web);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Win
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Environment.OSVersion.Version.Build >= 22000) // Win11 check
            {
                BorderlessHelper.SetWindowCorner(this.Handle, BorderlessHelper.DwmWindowCornerPreference.Round);
            }
            else
            {
                BorderlessHelper.SetWindowCorner(this, 16); // custom radius for older Windows
            }


        }
        #endregion
    }
}
