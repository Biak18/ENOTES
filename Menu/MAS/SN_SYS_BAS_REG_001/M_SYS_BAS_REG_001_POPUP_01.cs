using CH.Framework.Win;
using CH.Helper;
using Microsoft.AspNetCore.Identity;
using System;

namespace M_SYS_BAS_REG_001;

public partial class M_SYS_BAS_REG_001_POPUP_01 : PopupBase
{
    private string oldPassword;

    public string OldPassword
    {
        get { return oldPassword; }
        set { oldPassword = value; }
    }

    private bool isNewUser;

    public bool IsNewUser
    {
        get { return isNewUser; }
        set
        {
            isNewUser = value;
        }
    }


    public M_SYS_BAS_REG_001_POPUP_01()
    {
        InitializeComponent();
        CenterToParent();

        btn_ChgPwd.Click += Btn_ChgPwd_Click;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        txt_OldPwd.Visible = !IsNewUser;
    }

    private void Btn_ChgPwd_Click(object sender, System.EventArgs e)
    {
        try
        {
            var oldHasher = new PasswordHasher<string>();
            var newHasher = new PasswordHasher<string>();


            string oldPwd = oldPassword;
            string oldPwd2 = txt_OldPwd.Text;

            var hashedOldPassword = oldHasher.VerifyHashedPassword(null, oldPwd, oldPwd2);

            string newPwd = txt_NewPwd.Text;

            if (!IsNewUser && hashedOldPassword != PasswordVerificationResult.Success)
            {
                Msg.ShowMessageBox("The current password you entered is incorrect.", CH.Framework.Common.MessageType.Information);
                return;
            }
            if (!IsNewUser && oldPwd2 == newPwd)
            {
                Msg.ShowMessageBox("The new password must be different from the current password.", CH.Framework.Common.MessageType.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(newPwd))
            {
                Msg.ShowMessageBox("Please enter a new password.", CH.Framework.Common.MessageType.Information);
                return;
            }

            ReturnData.Add("newPwd", newHasher.HashPassword(null, newPwd));
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        catch (System.Exception ex)
        {
            Msg.ShowMessageBox(ex.Message, CH.Framework.Common.MessageType.Error);
        }
    }



}
