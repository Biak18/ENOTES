using DevExpress.XtraLayout;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls
{
    [SupportedOSPlatform("windows")]
    public partial class CHLTextEdit : UserControl
    {
        private int _LabelWidth = 6;
        private const int FIXED_HEIGHT = 24;
        private int _labelPadding = 5;

        [Browsable(false)]
        public CHLabel CHLabel => chLabel1;
        [Browsable(false)]
        public CHTextEdit CHTextEdit => chTextEdit1;


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

                chTextEdit1.Location = location;
                chTextEdit1.Width = base.Width - chTextEdit1.Location.X;
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

        [Category("TEXTEDIT")]
        [DefaultValue("")]
        public object EditValue
        {
            get
            {
                return chTextEdit1.EditValue;
            }
            set
            {
                chTextEdit1.EditValue = value;
            }
        }

        [Category("TEXTEDIT")]
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return chTextEdit1.Text;
            }
            set
            {
                chTextEdit1.Text = value;
            }
        }

        [Category("TEXTEDIT")]
        [DefaultValue(false)]
        public bool OnSearch
        {
            get
            {
                return chTextEdit1.OnSearch;
            }
            set
            {
                chTextEdit1.OnSearch = value;
            }
        }

        [Category("TEXTEDIT")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get
            {
                return chTextEdit1.ReadOnly;
            }
            set
            {
                chTextEdit1.ReadOnly = value;
            }
        }

        public event EventHandler TextChangedByUser;
        public event EventHandler EditValueChangedByUser;

        public CHLTextEdit()
        {
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            base.SizeChanged += CHLTextedit_SizeChanged;
            base.ParentChanged += CHLTextedit_ParentChanged;

            chTextEdit1.TextChanged += (s, e) => TextChangedByUser?.Invoke(this, e);
            chTextEdit1.EditValueChanged += (s, e) => EditValueChangedByUser?.Invoke(this, e);
            base.VisibleChanged += CHLTextedit_VisibleChanged;
        }

        private void CHLTextedit_VisibleChanged(object sender, EventArgs e)
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

        private void CHLTextedit_SizeChanged(object sender, EventArgs e)
        {
            chTextEdit1.Width = base.Width - chTextEdit1.Location.X;

            if (base.Size.Height != 24)
            {
                base.Size = new Size(base.Size.Width, 24);
            }
        }

        private void CHLTextedit_ParentChanged(object sender, EventArgs e)
        {
            if (base.Parent != null)
            {
                BackColor = base.Parent.BackColor;
                chTextEdit1._colorBack = BackColor;
                chTextEdit1.UserPaint();
                Parent.BackColorChanged -= Parent_BackColorChanged;
                Parent.BackColorChanged += Parent_BackColorChanged;
            }
        }

        private void Parent_BackColorChanged(object sender, EventArgs e)
        {
            BackColor = base.Parent.BackColor;
            chTextEdit1._colorBack = BackColor;
            chTextEdit1.UserPaint();
        }

    }
}
