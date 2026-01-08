using CH.Framework.Common;
using CH.Framework.Win;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Helper;
[SupportedOSPlatform("windows")]
public class Msg
{
    public static DialogResult ShowMessageBox(string msgCode, MessageType msgType)
    {
        LoadingHelper.EndLoading();
        return new MsgDialog(msgType, msgCode).ShowDialog();
    }

    //public static DialogResult ShowMessageBoxDetail(string messageText, string messageDetailText, MessageType msgType)
    //{
    //    LoadingHelper.EndLoading();
    //    return ((Form)(object)new MsgDetailDialog(messageText, messageDetailText, msgType)).ShowDialog();
    //}
}
