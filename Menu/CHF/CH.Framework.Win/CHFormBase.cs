using CH.Framework.Win.Controls;
using CH.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win;
[SupportedOSPlatform("windows")]
public partial class CHFormBase : FormBase
{
    private List<CHLayoutPanel> aLayoutPanels;
    private Control[] FormControlAll;
    public CHFormBase()
    {
        InitializeComponent();
        base.Load += CHFormBase_Load;
    }

    private void CHFormBase_Load(object sender, EventArgs e)
    {
        FormControlAll = A.GetAllControls((Control)sender);
        aLayoutPanels = FormControlAll.OfType<CHLayoutPanel>().ToList();
        if (aLayoutPanels.Count > 0)
        {
            ApplyFormLayoutSetting("Load");
        }
    }

    public bool IsTopPanelVisible
    {
        get { return topPanel.Visible; }
        set { topPanel.Visible = value; }
    }

    private void ApplyFormLayoutSetting(string status)
    {
        foreach (CHLayoutPanel aLayoutPanel in aLayoutPanels)
        {
            if (aLayoutPanel.IsSaveLayout)
            {
                aLayoutPanel.BeginUpdate();
                if (status == "Load")
                {
                    aLayoutPanel.InitBorder();
                    aLayoutPanel.SetDefaultLayout();
                    aLayoutPanel.LoadLayout();
                    aLayoutPanel.FN_SizeControl("Fix");
                }

                aLayoutPanel.EndUpdate();
            }
        }
    }


    public virtual void OnSearch()
    {
        try
        {

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public virtual void OnAddrow()
    {
        try
        {

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }


    public virtual void OnDeleteRow()
    {
        try
        {

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public virtual void OnSave()
    {
        try
        {

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }


    public virtual void OnPrint()
    {
        try
        {

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
}
