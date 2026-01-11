using CH.Framework.Common;
using CH.Helper;
using DevExpress.XtraEditors;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ENOTES;

public partial class ENOTES_LOGIN : Form
{
    Point mousePoint;
    ENOTES_D _D = new ENOTES_D();

    #region Initialize
    public ENOTES_LOGIN()
    {
        InitializeComponent();
        //ApplyBlurEffect();
        InitializeEvent();
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

    private void DoLogin()
    {
        try
        {
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

            this.Hide();
            LoadingHelper.StartLoading(this, "Loading...", "Loading form");
            using (ENOTES mainPage = new ENOTES())
            {
                LoadingHelper.EndLoading();
                mainPage.ShowDialog();
            }

            this.Show();

        }
        catch /*(Exception ex)*/
        {
            throw;
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