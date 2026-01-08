using CH.Framework.Common;
using CH.Helper;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win;

[SupportedOSPlatform("windows")]
public partial class FormBase : RibbonForm
{
    public FormBase()
    {
        InitializeComponent();
    }

    public virtual DialogResult ShowMessageBox(string message, MessageType messageType)
    {
        LoadingHelper.EndLoading();
        return new MsgDialog(messageType, message).ShowDialog(this);
    }


    protected virtual void HandleException(Exception ex)
    {
        LoadingHelper.EndLoading();
        ShowMessageBox(ex.Message, MessageType.Error);
    }
}
