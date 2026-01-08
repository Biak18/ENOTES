using DevExpress.XtraLayout;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls
{
    [SupportedOSPlatform("windows")]
    public partial class CHLNumericText : UserControl
    {
        private int _LabelWidth = 6;
        private const int FIXED_HEIGHT = 24;
        private int _labelPadding = 5;

        [Browsable(false)]
        public CHLabel CHLabel => chLabel1;
        [Browsable(false)]
        public CHNumericText CHNumericText => chNumericText1;

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

                chNumericText1.Location = location;
                chNumericText1.Width = base.Width - chNumericText1.Location.X;
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

        [Category("NUMERICTEXT")]
        [DefaultValue("")]
        public object EditValue
        {
            get
            {
                return chNumericText1.EditValue;
            }
            set
            {
                chNumericText1.EditValue = value;
                this.DecimalValueChangedByUser?.Invoke(this, null);
            }
        }

        [Category("NUMERICTEXT")]
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return chNumericText1.Text;
            }
            set
            {
                chNumericText1.Text = value;
            }
        }

        [Category("NUMERICTEXT")]
        [DefaultValue(SetNumericType.NONE)]
        [Browsable(true)]
        public SetNumericType SetNumericType
        {
            get
            {
                return chNumericText1.SetNumericType;
            }
            set
            {
                chNumericText1.SetNumericType = value;
            }
        }

        [Category("NUMERICTEXT")]
        [DefaultValue(0)]
        public decimal DecimalValue
        {
            get
            {
                return chNumericText1.DecimalValue;
            }
            set
            {
                chNumericText1.DecimalValue = value;
                this.DecimalValueChangedByUser?.Invoke(this, null);
            }
        }

        [Category("NUMERICTEXT")]
        [DefaultValue(0)]
        public decimal DecimalPoint
        {
            get
            {
                return chNumericText1.DecimalPoint;
            }
            set
            {
                chNumericText1.DecimalPoint = value;
            }
        }

        [Category("NUMERICTEXT")]
        [DefaultValue("")]
        public string MaskExpression
        {
            get
            {
                return chNumericText1.MaskExpression;
            }
            set
            {
                chNumericText1.MaskExpression = value;
            }
        }

        [Category("NUMERICTEXT")]
        [DefaultValue(false)]
        public bool IsRequired
        {
            get
            {
                return chNumericText1.IsRequired;
            }
            set
            {
                chNumericText1.IsRequired = value;
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

        [Category("NUMERICTEXT")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get
            {
                return chNumericText1.ReadOnly;
            }
            set
            {
                chNumericText1.ReadOnly = value;
            }
        }

        public event EventHandler TextChangedByUser;
        public event EventHandler EditValueChangedByUser;
        public event EventHandler DecimalValueChangedByUser;
        public CHLNumericText()
        {
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            base.SizeChanged += CHLNumericText_SizeChanged;
            chNumericText1.TextChanged += (s, e) => TextChangedByUser?.Invoke(this, e);
            chNumericText1.EditValueChanged += (s, e) => EditValueChangedByUser?.Invoke(this, e);
            base.VisibleChanged += CHLNumericText_VisibleChanged;
        }

        private void CHLNumericText_VisibleChanged(object sender, EventArgs e)
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

        private void CHLNumericText_SizeChanged(object sender, EventArgs e)
        {
            chNumericText1.Width = base.Width - chNumericText1.Location.X;
            if (base.Size.Height != 24)
            {
                base.Size = new Size(base.Size.Width, 24);
            }
        }
    }
}
