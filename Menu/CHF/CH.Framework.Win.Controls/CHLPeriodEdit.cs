using DevExpress.XtraLayout;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
public partial class CHLPeriodEdit : UserControl
{
    private int _LabelWidth = 6;

    [Category("LABEL")]
    [DefaultValue(6)]
    public int LabelWidth
    {
        get => _LabelWidth;
        set
        {
            _LabelWidth = value;
            Point location;
            if (value <= 0)
            {
                chLabel1.Width = 0;
                location = new Point(0, 0);
            }
            else
            {
                chLabel1.Width = 19 + 12 * (value - 1);
                location = new Point(chLabel1.Width + 7, 0);
            }

            chPeriodEdit1.Location = location;
            chPeriodEdit1.Width = base.Width - chPeriodEdit1.Location.X;
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

    [Category("DATEEDIT")]
    [DefaultValue("")]
    public override string Text
    {
        get
        {
            return chPeriodEdit1.Text;
        }
        set
        {
            chPeriodEdit1.Text = value;
        }
    }

    [Category("DATEEDIT")]
    [DefaultValue("yyyy\\/MM\\/dd")]
    public string DateFormat
    {
        get
        {
            return chPeriodEdit1.DateFormat;
        }
        set
        {
            chPeriodEdit1.DateFormat = value;
        }
    }

    [Category("DATEEDIT")]
    [DefaultValue("")]
    public string DtStart
    {
        get
        {
            return chPeriodEdit1.DtStart;
        }
        set
        {
            chPeriodEdit1.DtStart = value;
        }
    }

    [Category("DATEEDIT")]
    [DefaultValue("")]
    public string DtEnd
    {
        get
        {
            return chPeriodEdit1.DtEnd;
        }
        set
        {
            chPeriodEdit1.DtEnd = value;
        }
    }

    [Category("DATEEDIT")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get
        {
            return chPeriodEdit1.ReadOnly;
        }
        set
        {
            chPeriodEdit1.ReadOnly = value;
        }
    }

    public CHLPeriodEdit()
    {
        InitializeComponent();
        InitEvent();
    }

    private void InitEvent()
    {
        base.SizeChanged += This_SizeChanged;
        base.VisibleChanged += ALPeriodEdit_VisibleChanged;
        base.ParentChanged += ALPeriodEdit_ParentChanged;
    }

    private void Parent_BackColorChanged(object sender, EventArgs e)
    {
        BackColor = base.Parent.BackColor;
        chPeriodEdit1.BackColor = BackColor;
        chPeriodEdit1._colorBack = BackColor;
        chPeriodEdit1.UserPaint();
    }

    private void ALPeriodEdit_ParentChanged(object sender, EventArgs e)
    {
        if (base.Parent != null)
        {
            BackColor = base.Parent.BackColor;
            chPeriodEdit1.BackColor = BackColor;
            chPeriodEdit1._colorBack = BackColor;
            chPeriodEdit1.UserPaint();
            base.Parent.BackColorChanged -= Parent_BackColorChanged;
            base.Parent.BackColorChanged += Parent_BackColorChanged;
        }
    }

    private void ALPeriodEdit_VisibleChanged(object sender, EventArgs e)
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

    private void This_SizeChanged(object sender, EventArgs e)
    {
        chPeriodEdit1.Width = base.Width - chPeriodEdit1.Location.X;
        if (base.Size.Height != 24)
        {
            base.Size = new Size(base.Size.Width, 24);
        }
    }

}
