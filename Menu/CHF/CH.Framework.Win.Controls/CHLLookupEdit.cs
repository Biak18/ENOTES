using DevExpress.XtraLayout;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
public partial class CHLLookupEdit : UserControl
{
    private int _LabelWidth = 6;

    private const int FIXED_HEIGHT = 24;

    [Browsable(false)]
    public CHLabel CHLabel => chLabel1;

    [Browsable(false)]
    public CHLookupedit CHLookupedit => chLookupedit1;

    [Category("LABEL")]
    [DefaultValue(6)]
    public int LabelWidth
    {
        get
        {
            return _LabelWidth;
        }
        set
        {
            _LabelWidth = value;
            Point location;
            if (value == 0)
            {
                chLabel1.Width = 0;
                location = new Point(0, 0);
            }
            else
            {
                chLabel1.Width = 19 + 12 * (value - 1);
                location = new Point(chLabel1.Width + 7, 0);
            }

            chLookupedit1.Location = location;
            chLookupedit1.Width = base.Width - chLookupedit1.Location.X;
        }
    }

    [Category("LABEL")]
    [DefaultValue("")]
    public string LabelText
    {
        get
        {
            return chLabel1.Text;
        }
        set
        {
            chLabel1.Text = value;
        }
    }

    [Category("LOOKUPEDIT")]
    [DefaultValue("")]
    public object EditValue
    {
        get
        {
            return chLookupedit1.EditValue;
        }
        set
        {
            chLookupedit1.EditValue = value;
        }
    }

    [Category("LOOKUPEDIT")]
    [DefaultValue(false)]
    public bool Selected
    {
        get
        {
            return chLookupedit1.Selected;
        }
        set
        {
            chLookupedit1.Selected = value;
        }
    }

    [Category("LOOKUPEDIT")]
    [DefaultValue(false)]
    public bool IsRequired
    {
        get
        {
            return chLookupedit1.IsRequired;
        }
        set
        {
            chLookupedit1.IsRequired = value;
            if (value)
            {
                chLabel1.Font = new Font("Segoe UI", 12f, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            else
            {
                chLabel1.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
            }
        }
    }

    [Category("LOOKUPEDIT")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get
        {
            return chLookupedit1.ReadOnly;
        }
        set
        {
            chLookupedit1.ReadOnly = value;
        }
    }

    public event EventHandler TextChangedByUser;
    public event EventHandler EditValueChangedByUser;


    public CHLLookupEdit()
    {
        InitializeComponent();
        InitEvent();
    }

    private void InitEvent()
    {
        base.SizeChanged += CHLLookupedit_SizeChanged;
        chLookupedit1.TextChanged += (s, e) => TextChangedByUser?.Invoke(this, e);
        chLookupedit1.EditValueChanged += (s, e) => EditValueChangedByUser?.Invoke(this, e);
        base.ParentChanged += CHLLookupedit_ParentChanged;
        base.VisibleChanged += CHLLookupEdit_VisibleChanged;
    }

    private void CHLLookupEdit_VisibleChanged(object sender, EventArgs e)
    {
        if (base.Parent != null && base.Parent.GetType().Name == "CHLayoutPanel")
        {
            CHLayoutPanel cHLayoutPanel = base.Parent as CHLayoutPanel;
            LayoutControlItem layoutControlItem = cHLayoutPanel.GetItemByControl(this);
            if (layoutControlItem != null)
            {
                layoutControlItem.ContentVisible = base.Visible;
            }
        }
    }

    private void CHLLookupedit_ParentChanged(object sender, EventArgs e)
    {
        if (Parent == null)
            return;

        BackColor = Parent.BackColor;
        chLookupedit1._colorBack = BackColor;
        chLookupedit1.UserPaint();

        Parent.BackColorChanged -= Parent_BackColorChanged;
        Parent.BackColorChanged += Parent_BackColorChanged;
    }


    private void Parent_BackColorChanged(object sender, EventArgs e)
    {
        BackColor = base.Parent.BackColor;
        chLookupedit1._colorBack = BackColor;
        chLookupedit1.UserPaint();
    }

    private void CHLLookupedit_SizeChanged(object sender, EventArgs e)
    {
        chLookupedit1.Width = base.Width - chLookupedit1.Location.X;
        if (base.Size.Height != 24)
        {
            base.Size = new Size(base.Size.Width, 24);
        }
    }
}
