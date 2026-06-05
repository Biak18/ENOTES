using CH.Framework.Win.Controls;
using CH.Grid;
using CH.Helper;
using DevExpress.XtraGrid.Views.Grid;
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

    private bool FN_ModifiedCheck_Grid(bool yn_nullchk)
    {
        GridView gridView = null;
        IEnumerable<Control> all = GetAll(this, typeof(CHGrid));
        if (all.Any())
        {
            foreach (Control item in all)
            {
                CHGrid grid = item as CHGrid;
                if (grid != null)
                {
                    gridView = grid.MainView as GridView;
                    if (GH.GridModifyCheck(grid, yn_nullchk))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerable<Control> GetAll(Control control, Type type)
    {
        IEnumerable<Control> enumerable = control.Controls.Cast<Control>();
        return from c in enumerable.SelectMany((Control ctrl) => GetAll(ctrl, type)).Concat(enumerable)
               where c.GetType() == type
               select c;
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
