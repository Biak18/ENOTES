using CH.Framework.Win.Controls;
using DevExpress.XtraEditors.Controls;
using System.Data;
using System.Drawing;
using System.Runtime.Versioning;

namespace CH.Helper;
[SupportedOSPlatform("windows")]
public class SetControl
{
    public void ClearCombobox(CHLookupedit ctr)
    {
        ctr.Properties.Columns.Clear();
    }

    public void ClearCombobox(CHLLookupEdit ctr)
    {
        ClearCombobox(ctr);
    }

    public void SetCombobox(CHLLookupEdit ctr, DataTable dt, bool codeView)
    {
        SetCombobox(ctr.CHLookupedit, dt, codeView);
    }

    public void SetCombobox(CHLLookupEdit ctr, DataTable dt)
    {
        SetCombobox(ctr.CHLookupedit, dt);
    }

    public void SetCombobox(CHLLookupEdit ctr, DataTable dt, bool codeView, bool edit)
    {
        SetCombobox(ctr.CHLookupedit, dt, codeView: false);
    }

    public void SetCombobox(CHLookupedit ctr, DataTable dt, bool codeView)
    {
        ctr.Properties.Columns.Clear();
        ctr.Properties.ValueMember = "CODE";
        ctr.Properties.DisplayMember = "NAME";
        ctr.Properties.DataSource = dt;
        ctr.Properties.ShowFooter = false;
        ctr.Properties.ShowHeader = false;
        ctr.Properties.NullText = string.Empty;
        ctr.Properties.ShowNullValuePromptWhenFocused = true;
        ctr.Properties.AutoHeight = false;
        ctr.Properties.ShowLines = false;
        ctr.Properties.PopupSizeable = true;
        ctr.Properties.PopupResizeMode = ResizeMode.LiveResize;
        ctr.Properties.PopupFormMinSize = new Size(50, 50);
        ctr.Properties.ShowFooter = false;
        ctr.Properties.UseDropDownRowsAsMaxCount = true;
        ctr.Properties.DropDownRows = 15;
        ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
        if (codeView)
        {
            LookUpColumnInfo lookUpColumnInfo = new LookUpColumnInfo("CODE", "Code", 50);
            LookUpColumnInfo lookUpColumnInfo2 = new LookUpColumnInfo("NAME", "Name", 50);
            ctr.Properties.Columns.AddRange(new LookUpColumnInfo[2] { lookUpColumnInfo, lookUpColumnInfo2 });
        }
        else
        {
            LookUpColumnInfo lookUpColumnInfo3 = new LookUpColumnInfo("NAME", "Name", 50);
            ctr.Properties.Columns.AddRange(new LookUpColumnInfo[1] { lookUpColumnInfo3 });
        }

        if (dt.Rows.Count > 0)
        {
            ctr.EditValue = dt.Rows[0][ctr.Properties.ValueMember];
        }
    }

    public void SetCombobox(CHLookupedit ctr, object dt)
    {
        SetCombobox(ctr, dt);
    }

    public void SetCombobox(CHLookupedit ctr, DataTable dt)
    {
        SetCombobox(ctr, dt, codeView: false);
    }
}
