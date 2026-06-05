using CH.Helper;
using DevExpress.Utils;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Mask;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
[ToolboxItem(false)]
public partial class CHPeriodEdit : UserControl
{
    private bool _ReadOnly;

    private const int FIXED_HEIGHT = 24;

    private static Image picBack = A.GetBitmap(Ctrl_Image.ctl_calendar);

    private static Image picBack_M = A.GetBitmap(Ctrl_Image.ctl_calendar_main);

    private static Image picBack_T = A.GetBitmap(Ctrl_Image.ctl_calendar_tab);

    private static Image picBack_Hover = A.GetBitmap(Ctrl_Image.ctl_calendar_on);

    private static Image picBack_Hover_M = A.GetBitmap(Ctrl_Image.ctl_calendar_on_main);

    private static Image picBack_Hover_T = A.GetBitmap(Ctrl_Image.ctl_calendar_on_tab);

    private static Image picBack_disable = A.GetBitmap(Ctrl_Image.ctl_calendar_readonly);


    private bool _Pressed;

    private TextBox under_line = new TextBox();

    public Color _colorBack = Color.Transparent;

    private string _format = "yyyy\\/MM\\/dd";

    private string _outFormat = "yyyyMMdd";

    private string _GetMonthLastDay;

    [Category("ENOTES")]
    [Description("DateFormat")]
    public string DateFormat
    {
        get
        {
            return _format;
        }
        set
        {
            _format = value;
            txtDtFrom.Properties.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            txtDtFrom.Properties.Mask.EditMask = _format;
            txtDtFrom.Properties.DisplayFormat.FormatString = _format;
            txtDtFrom.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtDtFrom.Properties.AllowNullInput = DefaultBoolean.True;
            txtDtFrom.Properties.Mask.EditMask = _format;
            txtDtTo.Properties.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            txtDtTo.Properties.Mask.EditMask = _format;
            txtDtTo.Properties.DisplayFormat.FormatString = _format;
            txtDtTo.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtDtTo.Properties.AllowNullInput = DefaultBoolean.True;
            txtDtTo.Properties.Mask.EditMask = _format;
        }
    }

    [Browsable(true)]
    [Description("Set to ReadOnly.")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get
        {
            return _ReadOnly;
        }
        set
        {
            _ReadOnly = value;
            txtDtFrom.ReadOnly = value;
            txtDtFrom.ReadOnly = value;
        }
    }

    [Category("ENOTES")]
    [Browsable(true)]
    [Description("Enter the start date. The format is yyyyMMdd.")]
    public string DtStart
    {
        get
        {
            string result = string.Empty;
            if (txtDtFrom.Text != string.Empty)
            {
                result = DateTime.ParseExact(txtDtFrom.Text, _format, null).ToString(_outFormat);
            }

            return result;
        }
        set
        {
            txtDtFrom.EditValueChanged -= txtDtFrom_EditValueChanged;
            string text = string.Empty;
            if (value != text && DateTime.TryParseExact(value, _outFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _))
            {
                dateNavigatorFrom.DateTime = DateTime.ParseExact(value, _outFormat, null);
                text = dateNavigatorFrom.DateTime.ToString(_format);
            }

            txtDtFrom.Text = text;
            txtDtFrom.EditValueChanged += txtDtFrom_EditValueChanged;
        }
    }

    [Category("SNOTES")]
    [Browsable(true)]
    [Description("Enter the end date. The format is yyyyMMdd.")]
    public string DtEnd
    {
        get
        {
            string result = string.Empty;
            if (txtDtTo.Text != string.Empty)
            {
                result = DateTime.ParseExact(txtDtTo.Text, _format, null).ToString(_outFormat);
            }

            return result;
        }
        set
        {
            txtDtTo.EditValueChanged -= txtDtTo_EditValueChanged;
            string text = string.Empty;
            if (value != text && DateTime.TryParseExact(value, _outFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _))
            {
                dateNavigatorTo.DateTime = DateTime.ParseExact(value, _outFormat, null);
                text = dateNavigatorTo.DateTime.ToString(_format);
            }

            txtDtTo.Text = text;
            txtDtTo.EditValueChanged += txtDtTo_EditValueChanged;
        }
    }


    public CHPeriodEdit()
    {
        InitializeComponent();
        InitializeEvent();
        under_line.BackColor = Color.FromArgb(208, 208, 208);
        under_line.BorderStyle = BorderStyle.None;
        under_line.Multiline = true;
        under_line.Size = new Size(base.Width, 1);
        base.Controls.Add(under_line);
        under_line.Dock = DockStyle.Bottom;
        under_line.BringToFront();
        if (DesignMode)
        {
            PickerControl.Visible = false;
        }

        PickerEdit.Properties.PopupControl = PickerControl;

    }

    private void InitializeEvent()
    {
        base.KeyDown += CHPeriodEdit_KeyDown;
        base.Enter += CHPeriodEdit_Enter;

        dateNavigatorFrom.EditValueChanged += dateNavigator_EditDateModified;
        dateNavigatorTo.EditValueChanged += dateNavigator_EditDateModified;
        dateNavigatorFrom.MouseDown += DateNavigatorFrom_MouseDown;
        dateNavigatorTo.MouseDown += DateNavigatorTo_MouseDown;
        dateNavigatorFrom.MouseDoubleClick += DateNavigatorFrom_MouseDoubleClick;
        PickerEdit.QueryCloseUp += PcikerEdit_QueryCloseUp;
        PickerEdit.QueryPopUp += PickerEdit_QueryPopUp;
        txtDtFrom.EditValueChanged += txtDtFrom_EditValueChanged;
        txtDtTo.EditValueChanged += txtDtTo_EditValueChanged;
        txtDtFrom.KeyDown += TxtDtFrom_KeyDown;
        txtDtTo.KeyDown += TxtDtTo_KeyDown;
        PickerEdit.MouseMove += PickerEdit_MouseMove;
        PickerEdit.MouseLeave += PickerEdit_MouseLeave;
        PickerEdit.MouseDown += PickerEdit_MouseDown;
        PickerEdit.MouseUp += PickerEdit_MouseUp;
        PickerEdit.Click += PickerEdit_Click;
        base.ParentChanged += APeriodEdit_ParentChanged;
    }

    private void APeriodEdit_ParentChanged(object sender, EventArgs e)
    {
        if (base.Parent != null && !(base.Parent.GetType().Name == "CHLayoutPanel"))
        {
            _colorBack = base.Parent.BackColor;
            UserPaint();
        }
    }

    private void PickerEdit_Click(object sender, EventArgs e)
    {
        if (!_Pressed)
        {
            _Pressed = true;
            Pressed();
        }
    }

    private void PickerEdit_MouseUp(object sender, MouseEventArgs e)
    {
        if (!_Pressed)
        {
            _Pressed = true;
            Pressed();
        }
    }

    private void PickerEdit_MouseDown(object sender, MouseEventArgs e)
    {
        if (!_Pressed)
        {
            _Pressed = true;
            Pressed();
        }
    }

    private void PickerEdit_MouseLeave(object sender, EventArgs e)
    {
        if (!PickerEdit.IsPopupOpen && _Pressed)
        {
            _Pressed = false;
            Pressed();
        }
    }

    private void PickerEdit_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_Pressed && !_Pressed)
        {
            _Pressed = true;
            Pressed();
        }
    }

    private void TxtDtTo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtDtFrom_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            txtDtTo.Focus();
        }
    }
    private void txtDtFrom_EditValueChanged(object sender, EventArgs e)
    {
        string empty = string.Empty;
        if (txtDtFrom.Text != string.Empty)
        {
            DateTime dateTime = DateTime.ParseExact(txtDtFrom.Text, _format, null);
            empty = dateTime.ToString(_format);
            dateNavigatorFrom.DateTime = dateTime;
        }
    }

    private void txtDtTo_EditValueChanged(object sender, EventArgs e)
    {
        string empty = string.Empty;
        if (txtDtTo.Text != string.Empty)
        {
            DateTime dateTime = DateTime.ParseExact(txtDtTo.Text, _format, null);
            empty = dateTime.ToString(_format);
            dateNavigatorTo.DateTime = dateTime;
        }
    }

    private void PickerEdit_QueryPopUp(object sender, CancelEventArgs e)
    {
        if (txtDtFrom.Text == string.Empty)
        {
            dateNavigatorFrom.DateTime = DateTime.Now;
        }

        if (txtDtTo.Text == string.Empty)
        {
            dateNavigatorTo.DateTime = DateTime.Now;
        }
    }

    private void PcikerEdit_QueryCloseUp(object sender, CancelEventArgs e)
    {
        txtDtTo.Focus();
    }

    private void DateNavigatorFrom_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        CalendarHitInfo hitInfo = dateNavigatorFrom.GetHitInfo(e);
        if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
        {
            DtStart = hitInfo.HitDate.ToString(_outFormat);
            PickerControl.OwnerEdit.ClosePopup();
            txtDtFrom.Focus();
        }

        dateNavigatorTo.Refresh();
    }

    private void DateNavigatorTo_MouseDown(object sender, MouseEventArgs e)
    {
        CalendarHitInfo hitInfo = dateNavigatorTo.GetHitInfo(e);
        if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
        {
            DtEnd = hitInfo.HitDate.ToString(_outFormat);
            PickerControl.OwnerEdit.ClosePopup();
            txtDtTo.Focus();
        }

        dateNavigatorFrom.Refresh();
    }

    private void DateNavigatorFrom_MouseDown(object sender, MouseEventArgs e)
    {
        CalendarHitInfo hitInfo = dateNavigatorFrom.GetHitInfo(e);
        if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
        {
            DtStart = hitInfo.HitDate.ToString(_outFormat);
        }

        dateNavigatorTo.Refresh();
    }

    private void dateNavigator_EditDateModified(object sender, EventArgs e)
    {
        txtDtFrom.EditValueChanged -= txtDtFrom_EditValueChanged;
        txtDtTo.EditValueChanged -= txtDtTo_EditValueChanged;
        if (txtDtTo.Text == string.Empty)
        {
            txtDtTo.Text = txtDtFrom.Text;
        }

        if (txtDtFrom.Text == string.Empty)
        {
            DtStart = dateNavigatorFrom.DateTime.ToString(_outFormat);
        }

        txtDtFrom.EditValueChanged += txtDtFrom_EditValueChanged;
        txtDtTo.EditValueChanged += txtDtTo_EditValueChanged;
    }

    private void CHPeriodEdit_Enter(object sender, EventArgs e)
    {
        txtDtFrom.Focus();
    }

    private void CHPeriodEdit_KeyDown(object sender, KeyEventArgs e)
    {
        SelectNextControl((Control)sender, forward: true, tabStopOnly: true, nested: true, wrap: true);
    }

    public void UserPaint()
    {
        if (ReadOnly)
        {
            txtDtFrom.Properties.Appearance.BackColor = CHColor.Control_ReadOnly;
            txtDtTo.Properties.Appearance.BackColor = CHColor.Control_ReadOnly;
            PickerEdit.Properties.Buttons[0].Appearance.Image = picBack_disable;
        }
        else if (_colorBack == CHColor.Panel_Main)
        {
            txtDtFrom.Properties.Appearance.BackColor = CHColor.Control_Normal;
            txtDtTo.Properties.Appearance.BackColor = CHColor.Control_Normal;
            PickerEdit.Properties.Appearance.BackColor = CHColor.Control_Normal;
        }
        else if (_colorBack == CHColor.Panel_Tab)
        {
            txtDtFrom.Properties.Appearance.BackColor = CHColor.Control_Normal_Tab;
            txtDtTo.Properties.Appearance.BackColor = CHColor.Control_Normal_Tab;
            PickerEdit.Properties.Appearance.BackColor = CHColor.Control_Normal_Tab;
        }
    }

    private void Pressed()
    {
        if (_Pressed)
        {
            if (_colorBack == CHColor.Panel_Main)
            {
                PickerEdit.Properties.Buttons[0].Image = picBack_Hover_M;
            }
            else if (_colorBack == CHColor.Panel_Tab)
            {
                PickerEdit.Properties.Buttons[0].Image = picBack_Hover_T;
            }
            else
            {
                PickerEdit.Properties.Buttons[0].Image = picBack_Hover;
            }
        }
        else if (_colorBack == CHColor.Panel_Main)
        {
            PickerEdit.Properties.Buttons[0].Image = picBack_M;
        }
        else if (_colorBack == CHColor.Panel_Tab)
        {
            PickerEdit.Properties.Buttons[0].Image = picBack_T;
        }
        else
        {
            PickerEdit.Properties.Buttons[0].Image = picBack;
        }
    }

    protected override void InitLayout()
    {
        base.InitLayout();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        base.SetBoundsCore(x, y, width, FIXED_HEIGHT, specified);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        int buttonWidth = 23;
        int labelWidth = Label.Width;

        txtDtFrom.Width = (Width - labelWidth - buttonWidth) / 2;
        txtDtTo.Width = txtDtFrom.Width;

        txtDtFrom.Location = new Point(0, 0);
        Label.Location = new Point(txtDtFrom.Width, 0);
        txtDtTo.Location = new Point(Label.Right, 0);

        PickerEdit.Bounds = new Rectangle(0, 0, Width, FIXED_HEIGHT);
    }
}
