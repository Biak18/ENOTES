using CH.Framework.Common;
using CH.Helper;
using DevExpress.XtraEditors;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.IO;

namespace ENOTES;

public partial class ENOTES_LOGIN : Form
{
    string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.ini");
    ENOTES_D _D = new ENOTES_D();
    public event EventHandler LoginSuccess;
    #region Initialize
    public ENOTES_LOGIN()
    {
        InitializeComponent();
        //ApplyBlurEffect();
        InitializeControl();
        InitializeEvent();
    }

    private void InitializeControl()
    {
        string comCode = IniFile.IniReadValue("LoginInfo", "CompanyCode", dirPath);
        string userId = IniFile.IniReadValue("LoginInfo", "UserId", dirPath);

        BtnTxt_Company.EditValue = userId;
        BtnTxt_CdUser.EditValue = userId;
    }

    private void InitializeEvent()
    {
        picBox_Logo.MouseDown += View_MouseDown;

        panel_Main.MouseDown += View_MouseDown;

        lbl_Login.MouseDown += View_MouseDown;

        BtnClose.Click += BtnClose_Click;
        BtnLogin.Click += BtnLogin_Click;
        BtnTxt_Company.KeyDown += BtnTxt_KeyDown;
        BtnTxt_CdUser.KeyDown += BtnTxt_KeyDown;
        BtnTxt_Password.KeyDown += BtnTxt_KeyDown;

        BtnConfig.Click += BtnConfig_Click;
        BtnChgPassword.Click += BtnChgPassword_Click;
    }
    #endregion

    #region Events

    private void View_MouseDown(object sender, MouseEventArgs e)
    {
        BorderlessHelper.MouseMove(this.Handle);
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
        DoLogin();
    }

    private void BtnTxt_KeyDown(object sender, KeyEventArgs e)
    {
        ButtonEdit btn = sender as ButtonEdit;
        if (e.KeyCode == Keys.Enter)
        {
            switch (btn.Name)
            {
                case nameof(BtnTxt_Company):
                    BtnTxt_CdUser.Focus();
                    break;

                case nameof(BtnTxt_CdUser):
                    BtnTxt_Password.Focus();
                    break;

                case nameof(BtnTxt_Password):
                    DoLogin();
                    break;
            }
        }
    }

    private void BtnConfig_Click(object sender, EventArgs e)
    {
        try
        {
            using (ENOTES_CONFIG enotes_config = new ENOTES_CONFIG())
            {
                if (enotes_config.ShowDialog() == DialogResult.OK)
                {

                }
            };
        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, MessageType.Error);
        }
    }

    private void BtnChgPassword_Click(object sender, EventArgs e)
    {
        //
    }


    private void DoLogin()
    {
        try
        {
            LoadingHelper.StartLoading(this, "Loading...", "Please wait");
            string cdCom = BtnTxt_Company.Text.Trim();
            string cdUser = BtnTxt_CdUser.Text.Trim();
            string inputPassword = BtnTxt_Password.Text;

            if (cdCom == "")
            {
                Msg.ShowMessageBox("Company Code is Required.", MessageType.Warning);
                BtnTxt_Company.Focus();
                return;
            }

            if (cdUser == "")
            {
                Msg.ShowMessageBox("User Code is Required.", MessageType.Warning);
                BtnTxt_CdUser.Focus();
                return;
            }

            if (inputPassword == "")
            {
                Msg.ShowMessageBox("Password is Required.", MessageType.Warning);
                BtnTxt_Password.Focus();
                return;
            }
            var hasher2 = new PasswordHasher<string>();
            string hashedPassword = hasher2.HashPassword(null, inputPassword);
            SysUser user = new SysUser
            {
                CdCom = "SYS",
                CdUser = "SYSTEM",
                DcPassword = hashedPassword,
                NmUser = "SYSTEM",
                FgRole = "SYSTEM",
                DtReg = DateTime.UtcNow.ToString("yyyyMMdd"),
                YnActive = "Y",
                TmReg = DateTime.UtcNow
            };
            //await Init();
            //await _client.From<SysUser>().Insert(user);
            //var json = await _client.Rpc("ap_enotes_002_s", new Dictionary<string, object> { { "p_cd_com", "SYS" }, { "p_cd_user", "SYSTEM" } });

            DataTable dt_user_info = _D.GetUserInfo(new object[] { cdCom, cdUser });

            if (dt_user_info == null || dt_user_info.Rows.Count == 0)
            {
                Msg.ShowMessageBox("Invalid user or password.", MessageType.Error);
                return;
            }

            if (A.GetString(dt_user_info.Rows[0]["YN_ACTIVE"]) != "Y")
            {
                Msg.ShowMessageBox("User is disabled.", MessageType.Error);
                return;
            }

            string storedHash = A.GetString(dt_user_info.Rows[0]["DC_PASSWORD"]);

            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(
                null,
                storedHash,
                inputPassword
            );

            if (result != PasswordVerificationResult.Success)
            {
                Msg.ShowMessageBox("Invalid user or password.", MessageType.Error);
                return;
            }
            CH.AppContext.Login(dt_user_info.Rows[0]);
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, MessageType.Error);
        }
        finally
        {
            LoadingHelper.EndLoading();
        }
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