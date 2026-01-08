using CH.Helper;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
[ToolboxItem(true)]
public class CHLayoutPanel : LayoutControl
{
    public enum PanelType
    {
        NONE = 0,
        MAINFORM = 1,
        CONTAINER = 2,
        ETC = 99
    }

    private bool _IsSaveLayout = true;

    private bool _isGroup = false;

    private bool _GroupBordersVisible = false;

    private bool _readOnly;

    private bool _AutoResize = false;

    private int _seq = 1;

    private PanelType _SetPanelType = PanelType.MAINFORM;

    [Category("SNOTES")]
    [DefaultValue(false)]
    public bool isGroup
    {
        get
        {
            return _isGroup;
        }
        set
        {
            _isGroup = value;
            if (_isGroup)
            {
                LookAndFeel.UseDefaultLookAndFeel = false;
                LookAndFeel.UseWindowsXPTheme = true;
                base.OptionsView.DrawItemBorders = false;
                base.OptionsView.ItemBorderColor = CHColor.Panel_Main;
                base.Root.AppearanceGroup.BorderColor = CHColor.Label_Normal;
                base.Root.GroupBordersVisible = true;
            }
            else
            {
                LookAndFeel.UseDefaultLookAndFeel = false;
                LookAndFeel.UseWindowsXPTheme = true;
                base.OptionsView.DrawItemBorders = true;
                base.OptionsView.ItemBorderColor = CHColor.Panel_Main;
                base.Root.AppearanceGroup.BorderColor = CHColor.Panel_Main;
                base.Root.GroupBordersVisible = false;
            }
        }
    }

    [Category("SNOTES")]
    [DefaultValue(false)]
    public bool GroupBordersVisible
    {
        get
        {
            return _GroupBordersVisible;
        }
        set
        {
            _GroupBordersVisible = value;
        }
    }

    [Category("SNOTES")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get
        {
            return _readOnly;
        }
        set
        {
            _readOnly = value;
            UpdateChildren(base.Controls, _readOnly);
        }
    }

    [DefaultValue(PanelType.MAINFORM)]
    [Category("ENOTES")]
    public PanelType SetPanelType
    {
        get => _SetPanelType;
        set
        {
            _SetPanelType = value;
            UserPaint();
        }
    }

    [Category("ENOTES")]
    [DefaultValue(true)]
    public bool IsSaveLayout
    {
        get
        {
            return _IsSaveLayout;
        }
        set
        {
            _IsSaveLayout = value;
        }
    }

    public CHLayoutPanel()
    {
        base.Root.TextVisible = false;
        if (IsSaveLayout)
        {
            base.CustomizationMode = CustomizationModes.Default;
        }
        else
        {
            base.CustomizationMode = CustomizationModes.Quick;
        }

        AutoSize = false;
        InitEvent();
    }

    private void InitEvent()
    {
        base.PopupMenuShowing += ALayoutPanel_PopupMenuShowing;
        base.ParentChanged += CHLayoutPanel_ParentChanged;
    }

    private void ALayoutPanel_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
    {
        if (base.DesignMode)
        {
            return;
        }

        DXPopupMenu menu = e.Menu;
        for (int num = e.Menu.Items.Count - 1; num >= 0; num--)
        {
            e.Menu.Items.Remove(e.Menu.Items[num]);
        }

        if (((ILayoutControl)this).EnableCustomizationMode)
        {
            menu.Items.Add(new DXMenuItem("&Save layout", SaveConfigLayout));
            menu.Items.Add(new DXMenuItem("&Cancel", CancelConfigLayout));
            return;
        }

        menu.Items.Add(new DXMenuItem("&Change layout", SetLayout));
        menu.Items.Add(new DXMenuItem("&Default layou", InitialLayout));
    }

    private void InitialLayout(object sender, EventArgs e)
    {
        DeleteLayout();
        BeginUpdate();
        RestoreDefaultLayout();
        FN_SizeControl("Fix");
        ((ILayoutControl)this).EnableCustomizationMode = false;
        EndUpdate();
    }

    public void DeleteLayout()
    {
        string path = FN_GetFileName();
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public void SaveLayout()
    {
        string xmlFile = FN_GetFileName();
        SaveLayoutToXml(xmlFile);
    }

    public void LoadLayout()
    {
        string text = FN_GetFileName();
        if (File.Exists(text))
        {
            RestoreLayoutFromXml(text);
        }
    }

    private void SetLayout(object sender, EventArgs e)
    {
        SuspendLayout();
        ((ILayoutControl)this).EnableCustomizationMode = true;
        FN_SizeControl("Free");
        ResumeLayout();
    }

    private void SaveConfigLayout(object sender, EventArgs e)
    {
        SuspendLayout();
        string xmlFile = FN_GetFileName();
        SaveLayoutToXml(xmlFile);
        FN_SizeControl("Fix");
        ((ILayoutControl)this).EnableCustomizationMode = false;
    }

    private void CancelConfigLayout(object sender, EventArgs e)
    {
        FN_SizeControl("Fix");
        ((ILayoutControl)this).EnableCustomizationMode = false;
    }

    private string FN_GetFileName()
    {
        string text = AppDomain.CurrentDomain.BaseDirectory + "FormLayout\\";
        A.SetDirectorySecurity(text);
        string empty = string.Empty;
        empty = ((!(base.ParentForm is CHFormBase)) ? (base.ParentForm.GetType().FullName + "_" + base.Name + ".xml") : (((CHFormBase)base.ParentForm).Name + "_" + base.Name + ".xml"));
        return text + "\\" + empty;
    }

    public void FN_SizeControl(string str_Figure)
    {
        Size size = new Size(0, 0);
        BeginUpdate();
        if (!_AutoResize)
        {
            foreach (object item in base.Items)
            {
                if (item.GetType().ToString() == "DevExpress.XtraLayout.LayoutControlItem")
                {
                    ((LayoutControlItem)item).SizeConstraintsType = SizeConstraintsType.Custom;
                    if (str_Figure == "Fix")
                    {
                        size = ((LayoutControlItem)item).Size;
                        ((LayoutControlItem)item).MinSize = size;
                        ((LayoutControlItem)item).MaxSize = size;
                    }
                    else if (str_Figure == "Free")
                    {
                        ((LayoutControlItem)item).SizeConstraintsType = SizeConstraintsType.Default;
                    }
                }
            }
        }

        EndUpdate();
    }

    public void InitBorder()
    {
        BeginUpdate();
        if (_isGroup)
        {
            LookAndFeel.UseDefaultLookAndFeel = false;
            LookAndFeel.UseWindowsXPTheme = true;
            base.OptionsView.DrawItemBorders = false;
            base.OptionsView.ItemBorderColor = CHColor.Panel_Main;
            base.Root.AppearanceGroup.BorderColor = CHColor.Label_Normal;
            base.Root.GroupBordersVisible = true;
        }
        else
        {
            LookAndFeel.UseDefaultLookAndFeel = false;
            LookAndFeel.UseWindowsXPTheme = true;
            base.OptionsView.DrawItemBorders = true;
            base.OptionsView.ItemBorderColor = CHColor.Panel_Main;
            base.Root.AppearanceGroup.BorderColor = CHColor.Panel_Main;
            base.Root.GroupBordersVisible = false;
        }

        EndUpdate();
    }

    private void CHLayoutPanel_ParentChanged(object sender, EventArgs e)
    {
        if (base.Parent != null)
        {
            base.BackColor = base.Parent.BackColor;
        }
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
        if (!base.DesignMode)
            return;
        base.OnControlAdded(e);

        foreach (var item in base.Items)
        {
            //DevExpress.XtraLayout.LayoutControlItem
            if (item.GetType().ToString() == "DevExpress.XtraLayout.LayoutControlItem" && ((LayoutControlItem)item).Control == e.Control)
            {
                ((LayoutControlItem)item).SizeConstraintsType = SizeConstraintsType.Custom;
                ((LayoutControlItem)item).ControlMinSize = new Size(0, 34);
                ((LayoutControlItem)item).ControlMaxSize = new Size(0, 35);
                ((LayoutControlItem)item).MinSize = new Size(0, 34);
                ((LayoutControlItem)item).MaxSize = new Size(0, 35);
                ((LayoutControlItem)item).Height = 35;
                ((LayoutControlItem)item).TextVisible = false;
                ((LayoutControlItem)item).ControlAlignment = ContentAlignment.MiddleLeft;
                ((LayoutControlItem)item).Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 5, 5);
                break;
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (!base.DesignMode)
        {
        }
    }

    protected override void InitLayout()
    {
        base.InitLayout();
    }

    private void UpdateChildren(ControlCollection controls, bool readOnly)
    {
        foreach (Control ctrl in controls)
        {
            if (ctrl is BaseEdit)
            {
                (ctrl as BaseEdit).Properties.ReadOnly = readOnly;
            }
            else
            {
                UpdateChildren(ctrl.Controls, readOnly);
            }
        }
    }

    public void UserPaint()
    {
        switch (SetPanelType)
        {
            case PanelType.NONE:
                BackColor = CHColor.Panel_Tab;
                break;
            case PanelType.MAINFORM:
                BackColor = CHColor.Panel_Main;
                break;
        }

        Invalidate();
    }
}
