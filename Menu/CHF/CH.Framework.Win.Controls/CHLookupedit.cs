using CH.Helper;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
public class CHLookupedit : LookUpEdit
{
    private const int FIXED_HEIGHT = 23;

    private static Image picBack;

    private static Image picBack_Hover;

    private static Image picBack_M;

    private static Image picBack_Hover_M;

    private static Image picBack_T;

    private static Image picBack_Hover_T;

    private static Image picBack_disable;

    private Rectangle _HoverRectangle;

    private bool _HasFocus;

    private bool _Pressed;

    public Color _colorBack = Color.Transparent;

    public string[] _filtercolumns;

    public object[] _targetobjects;

    private bool _Selected = false;

    private bool _IsRequired = false;

    [Category("ENOTES")]
    [Description("Background Color")]
    public Color colorBack
    {
        get
        {
            return _colorBack;
        }
        set
        {
            _colorBack = value;
        }
    }

    [Category("Filter")]
    public string[] FilterColumns
    {
        get
        {
            return _filtercolumns;
        }
        set
        {
            _filtercolumns = value;
        }
    }

    [Category("Filter")]
    public object[] TargetObjects
    {
        get
        {
            return _targetobjects;
        }
        set
        {
            _targetobjects = value;
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
            IsModified = true;
        }
    }

    [Category("ENOTES")]
    public bool Selected
    {
        get
        {
            return _Selected;
        }
        set
        {
            _Selected = value;
            if (!value)
            {
                _HasFocus = false;
            }

            Invalidate();
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

    static CHLookupedit()
    {
        picBack = A.GetBitmap(Ctrl_Image.ctl_drop);
        picBack_Hover = A.GetBitmap(Ctrl_Image.ctl_drop_on);
        picBack_M = A.GetBitmap(Ctrl_Image.ctl_drop_mainform);
        picBack_Hover_M = A.GetBitmap(Ctrl_Image.ctl_drop_on_mainform);
        picBack_T = A.GetBitmap(Ctrl_Image.ctl_drop_tab);
        picBack_Hover_T = A.GetBitmap(Ctrl_Image.ctl_drop_on_tab);
        picBack_disable = A.GetBitmap(Ctrl_Image.ctl_drop_readonly);
    }

    public CHLookupedit()
    {
        base.MouseMove += ALookUpEdit_MouseMove;
        base.MouseLeave += ALookUpEdit_MouseLeave;
        base.MouseDown += ALookUpEdit_MouseDown;
        base.MouseUp += ALookUpEdit_MouseUp;
        base.Click += ALookUpEdit_Click;
        base.SizeChanged += ALookUpEdit_SizeChanged;
        base.ParentChanged += ALookUpEdit_ParentChanged;
        base.PropertiesChanged += ALookUpEdit_PropertiesChanged;
        base.PopupFilter += ALookUpEdit_PopupFilter;
    }

    private void ALookUpEdit_PopupFilter(object sender, PopupFilterEventArgs e)
    {
        if (_filtercolumns != null && _filtercolumns.Length == _targetobjects.Length)
        {
            string text = string.Empty;
            for (int i = 0; i < _filtercolumns.Length; i++)
            {
                string objectValueToString = A.GetString(_targetobjects[i]);
                text = ((i != 0) ? ((!(objectValueToString == "")) ? ("AND " + _filtercolumns[i] + " == '" + A.GetString(_targetobjects[i]) + "'") : (text + " AND ISNULL(" + _filtercolumns[i] + ", '') == '" + A.GetString(_targetobjects[i]) + "'")) : ((!(objectValueToString == "")) ? (_filtercolumns[i] + " == '" + A.GetString(_targetobjects[i]) + "'") : ("ISNULL(" + _filtercolumns[i] + ", '') == '" + A.GetString(_targetobjects[i]) + "'")));
                text = text + " OR ISNULL(" + Properties.ValueMember + ", '') == ''";
            }

            e.Criteria = CriteriaOperator.Parse(text);
        }
    }

    private void ALookUpEdit_PropertiesChanged(object sender, EventArgs e)
    {
        UserPaint();
    }

    private void ALookUpEdit_ParentChanged(object sender, EventArgs e)
    {
        if (base.Parent != null && !(base.Parent.GetType().Name == "aLayoutPanel"))
        {
            _colorBack = base.Parent.BackColor;
            UserPaint();
        }
    }

    private void ALookUpEdit_SizeChanged(object sender, EventArgs e)
    {
        _HoverRectangle = new Rectangle(0, 0, base.Width, base.Height);
        if (base.Size.Height != 23)
        {
            base.Size = new Size(base.Size.Width, 23);
        }
    }

    protected override void InitLayout()
    {
        base.InitLayout();
        Properties.NullText = string.Empty;
        Properties.BorderStyle = BorderStyles.NoBorder;
        Properties.Appearance.Options.UseBackColor = true;
        if (Properties.Buttons.Count > 0)
        {
            Properties.Buttons[0].Kind = ButtonPredefines.Glyph;
            Properties.Buttons[0].Image = picBack;
        }
    }

    private void ALookUpEdit_Click(object sender, EventArgs e)
    {
        if (!_Pressed)
        {
            _Pressed = true;
            UserPaint();
        }
    }

    private void ALookUpEdit_MouseUp(object sender, MouseEventArgs e)
    {
        if (!_Pressed)
        {
            _Pressed = true;
            UserPaint();
        }
    }

    private void ALookUpEdit_MouseDown(object sender, MouseEventArgs e)
    {
        if (!_Pressed)
        {
            _Pressed = true;
            UserPaint();
        }
    }

    private void ALookUpEdit_MouseLeave(object sender, EventArgs e)
    {
        if (!IsPopupOpen && _Pressed)
        {
            _Pressed = false;
            UserPaint();
        }
    }

    private void ALookUpEdit_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_Pressed)
        {
            if (GetViewInfo().ContentRect.Contains(e.Location) && !_Pressed)
            {
                _Pressed = true;
                UserPaint();
                Invalidate();
            }

            Point location = new Point(e.X, e.Y);
            if (!_Pressed && _HoverRectangle.IntersectsWith(new Rectangle(location, new Size(1, 1))) && !_Pressed)
            {
                _Pressed = true;
                UserPaint();
            }
        }
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        Properties.AutoHeight = false;
    }

    public void UserPaint()
    {
        if (!(_colorBack != Color.Transparent))
        {
            return;
        }

        if (Properties.ReadOnly)
        {
            base.Enabled = false;
            Properties.Appearance.BackColor = CHColor.Control_ReadOnly;
            if (Properties.Buttons.Count > 0)
            {
                Properties.Buttons[0].Image = picBack_disable;
                Properties.Buttons[0].Enabled = false;
            }

            return;
        }

        base.Enabled = true;
        if (_colorBack == CHColor.Panel_Main)
        {
            Properties.Appearance.BackColor = CHColor.Control_Normal;
        }
        else if (_colorBack == CHColor.Panel_Tab)
        {
            Properties.Appearance.BackColor = CHColor.Control_Normal_Tab;
        }

        if (Properties.Buttons.Count <= 0)
        {
            return;
        }

        if (_IsRequired || _Pressed)
        {
            if (_colorBack == CHColor.Panel_Main)
            {
                Properties.Buttons[0].Image = picBack_Hover_M;
            }
            else if (_colorBack == CHColor.Panel_Tab)
            {
                Properties.Buttons[0].Image = picBack_Hover_T;
            }
            else
            {
                Properties.Buttons[0].Image = picBack_Hover;
            }
        }
        else if (_colorBack == CHColor.Panel_Main)
        {
            Properties.Buttons[0].Image = picBack_M;
        }
        else if (_colorBack == CHColor.Panel_Tab)
        {
            Properties.Buttons[0].Image = picBack_T;
        }
        else
        {
            Properties.Buttons[0].Image = picBack;
        }

        Properties.Buttons[0].Enabled = true;
    }
}
