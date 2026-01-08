using DevExpress.Data.Mask.Internal;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;
[SupportedOSPlatform("windows")]
public class CHNumericText : TextEdit
{

    private const int FIXED_HEIGHT = 24;

    private decimal _DecimalValue = default(decimal);

    private decimal _DecimalPoint = default(decimal);

    private string _MaskExpression = string.Empty;

    public Color _colorBack = Color.Transparent;

    private bool _iscolorBack = false;

    protected SetNumericType _SetNumericType = SetNumericType.NONE;

    private bool _IsRequired = false;

    [Category("ENOTES")]
    [Browsable(true)]
    public SetNumericType SetNumericType
    {
        get
        {
            return _SetNumericType;
        }
        set
        {
            _SetNumericType = value;
            decimal decimalValue = DecimalValue;
            if (_SetNumericType == SetNumericType.ROUNDUP)
            {
                decimalValue = Math.Round(DecimalValue, 2, MidpointRounding.AwayFromZero);
            }
            //else if (_SetNumericType != SetNumericType.ROUNDDOWN && _SetNumericType != SetNumericType.CEIL && _SetNumericType != SetNumericType.FLOOR)
            //{
            //}

            DecimalValue = decimalValue;
        }
    }

    [Browsable(true)]
    public decimal DecimalValue
    {
        get
        {
            _DecimalValue = decimal.Parse(Text.ToString());
            return _DecimalValue;
        }
        set
        {
            EditValue = decimal.Parse(value.ToString());
            _DecimalValue = decimal.Parse(value.ToString());
            this.DecimalValueChanged?.Invoke(this, null);
            IsModified = true;
            DoValidate();
        }
    }

    [Browsable(true)]
    public decimal DecimalPoint
    {
        get
        {
            return _DecimalPoint;
        }
        set
        {
            _DecimalPoint = value;
            string editMask = "n" + value;
            base.Properties.Mask.EditMask = editMask;
            base.Properties.Mask.MaskType = MaskType.Numeric;
        }
    }

    [Category("ENOTES")]
    [Description("Mask Expression")]
    public string MaskExpression
    {
        get
        {
            return _MaskExpression;
        }
        set
        {
            _MaskExpression = value;
            MaskSettings<MaskType>.Numeric numeric = base.Properties.MaskSettings.Configure<MaskSettings<MaskType>.Numeric>();
            numeric.MaskExpression = value;
            numeric.AutoHideDecimalSeparator = true;
            numeric.HideInsignificantZeros = true;
        }
    }

    public override object EditValue
    {
        get
        {
            return base.EditValue;
        }
        set
        {
            base.EditValue = value;
            this.DecimalValueChanged?.Invoke(this, null);
            IsModified = true;
        }
    }

    [Category("ENOTES")]
    [Browsable(true)]
    public bool IsRequired
    {
        get
        {
            return _IsRequired;
        }
        set
        {
            _IsRequired = value;
        }
    }

    [Browsable(true)]
    public event EventHandler DecimalValueChanged;

    public CHNumericText()
    {
        Init();
    }

    private void Init()
    {
        base.ImeMode = ImeMode.Disable;
        base.Properties.NullText = "0";
        base.Properties.AutoHeight = false;
        RightToLeft = RightToLeft.No;
        base.Properties.Appearance.Options.UseTextOptions = true;
        base.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
        base.Properties.DisplayFormat.FormatType = FormatType.Numeric;
        base.Properties.EditFormat.FormatType = FormatType.Numeric;
        string editMask = "n" + _DecimalPoint;
        base.Properties.Mask.EditMask = editMask;
        base.Properties.Mask.MaskType = MaskType.Numeric;
        base.Properties.Mask.UseMaskAsDisplayFormat = true;
        base.Properties.Appearance.Options.UseBackColor = true;
        base.Properties.BorderStyle = BorderStyles.NoBorder;
        base.Properties.AppearanceReadOnly.BackColor = CHColor.Control_ReadOnly;
        base.SizeChanged += CHNumericText_SizeChanged;
        base.ParentChanged += CHNumericText_ParentChanged;
    }

    private void CHNumericText_SizeChanged(object sender, EventArgs e)
    {
        if (base.Size.Height != 24)
        {
            base.Size = new Size(base.Size.Width, 24);
        }
    }

    private void CHNumericText_ParentChanged(object sender, EventArgs e)
    {
        if (base.Parent != null && !(base.Parent.GetType().Name == "CHLayoutPanel"))
        {
            _colorBack = base.Parent.BackColor;
            UserPaint();
        }
    }

    protected override void InitLayout()
    {
        Focus();
        base.InitLayout();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        base.Properties.AutoHeight = false;
    }

    public void UserPaint()
    {
        if (_colorBack == CHColor.Panel_Main)
        {
            base.Properties.Appearance.BackColor = CHColor.Control_Normal;
        }
        else if (_colorBack == CHColor.Panel_Tab)
        {
            base.Properties.Appearance.BackColor = CHColor.Control_Normal_Tab;
        }
    }
}
