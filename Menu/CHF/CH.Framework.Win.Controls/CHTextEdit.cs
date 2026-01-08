using CH.Helper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using System.Runtime.Versioning;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
public class CHTextEdit : ButtonEdit
{
    private const int FIXED_HEIGHT = 24;

    private const int BUTTON_FIXED_WIDTH = 9;

    public Color _colorBack = Color.Transparent;

    private static Image picBack;

    private static Image picBack_M;

    private static Image picBack_T;

    private bool _OnSearch = false;

    public bool OnSearch
    {
        get
        {
            return _OnSearch;
        }
        set
        {
            _OnSearch = value;
        }
    }

    static CHTextEdit()
    {
        picBack = A.GetBitmap(Ctrl_Image.txtclear);
        picBack_M = A.GetBitmap(Ctrl_Image.txtClear_main);
        picBack_T = A.GetBitmap(Ctrl_Image.txtClear_tab);

        //Skin currentSkin = CommonSkins.GetSkin(UserLookAndFeel.Default);
        //SkinElement element = currentSkin[CommonSkins.SkinButton];
        //element.Image.SetImage(picBack, Color.White);

    }

    public CHTextEdit()
    {
        if (Properties.Buttons.Count > 0)
        {
            Properties.Buttons[0].Appearance.Options.UseBackColor = true;
            Properties.Buttons[0].Width = 9;
        }
        InitEvent();
    }

    private void InitEvent()
    {
        base.GotFocus += CHTextEdit_GotFocus;
        base.Leave += CHTextEdit_Leave;
        base.ButtonClick += CHTextEdit_ButtonClick;
        base.SizeChanged += CHTextEdit_SizeChanged;
        base.ParentChanged += CHTextEdit_ParentChanged;
        base.PropertiesChanged += CHTextEdit_PropertiesChanged;
        base.Properties.CustomDrawButton += Properties_CustomDrawButton;
    }

    private void Properties_CustomDrawButton(object sender, CustomDrawButtonEventArgs e)
    {
        if (base.ReadOnly) return;
        if (e.Button.Kind == ButtonPredefines.Glyph)
        {
            var rect = e.Bounds;

            e.Graphics.FillRectangle(Brushes.White, rect);

            using (var pen = new Pen(Color.DarkGray, 1))
            {
                e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }

            if (e.Button.Image != null)
            {
                var imgRect = new Rectangle(
                    rect.X + (rect.Width - e.Button.Image.Width) / 2,
                    rect.Y + (rect.Height - e.Button.Image.Height) / 2,
                    e.Button.Image.Width,
                    e.Button.Image.Height
                );
                e.Graphics.DrawImage(e.Button.Image, imgRect);
            }

            e.Handled = true;
        }
    }


    private void CHTextEdit_PropertiesChanged(object sender, System.EventArgs e)
    {
        //if (!IsDesignMode)
        UserPaint();
    }

    public void UserPaint()
    {
        if (!(_colorBack != Color.Transparent))
        {
            return;
        }

        //if (Properties.Buttons.Count == 0)
        //    return;

        if (base.ReadOnly)
        {
            Properties.Appearance.BackColor = CHColor.Control_ReadOnly;
            Properties.Buttons[0].Appearance.BackColor = CHColor.Control_ReadOnly;
        }
        else
        {
            if (_colorBack == CHColor.Panel_Main)
            {
                Properties.Buttons[0].Image = picBack_M;
                Properties.Appearance.BackColor = CHColor.Control_Normal;
                Properties.Buttons[0].Appearance.BackColor = CHColor.Control_Normal;
            }
            else if (_colorBack == CHColor.Panel_Tab)
            {
                Properties.Buttons[0].Image = picBack_T;
                Properties.Appearance.BackColor = CHColor.Control_Normal_Tab;
                Properties.Buttons[0].Appearance.BackColor = CHColor.Control_Normal_Tab;
            }
            else
            {
                Properties.Buttons[0].Image = picBack;
            }
        }
    }

    private void CHTextEdit_ParentChanged(object sender, System.EventArgs e)
    {
        if (Parent != null && !(base.Parent.GetType().Name == "CHLayoutPanel"))
            _colorBack = base.Parent.BackColor;
    }

    private void CHTextEdit_SizeChanged(object sender, System.EventArgs e)
    {
        if (base.Size.Height != 24)
        {
            base.Size = new Size(base.Size.Width, 24);
        }
    }

    private void CHTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
        if (!base.ReadOnly)
            base.Text = string.Empty;
    }

    private void CHTextEdit_Leave(object sender, System.EventArgs e)
    {
        if (Properties.Buttons.Count > 0)
        {
            Properties.Buttons[0].Visible = false;
        }
    }

    private void CHTextEdit_GotFocus(object sender, System.EventArgs e)
    {
        if (Properties.Buttons.Count > 0 && !base.ReadOnly)
        {
            Properties.Buttons[0].Visible = true;
        }
    }

    protected override void OnPropertiesChanged()
    {
        base.OnPropertiesChanged();
        if (base.Height != FIXED_HEIGHT)
            base.Height = FIXED_HEIGHT;
    }

    protected override void InitLayout()
    {
        base.InitLayout();
        LookAndFeel.UseDefaultLookAndFeel = false;
        Properties.AutoHeight = false;
        Properties.BorderStyle = BorderStyles.NoBorder;

        if (Properties.Buttons.Count > 0)
        {
            Properties.Buttons[0].Kind = ButtonPredefines.Glyph;
            Properties.Buttons[0].Visible = false;
        }

        if (Properties.Buttons.Count == 0)
        {
            EditorButton button = new EditorButton(ButtonPredefines.Glyph);
            Properties.Buttons.Add(button);
            //Properties.Buttons[0].Width = 9;
            Properties.Buttons[0].Image = picBack;
            Properties.Buttons[0].Visible = false;
        }

        UserPaint();

    }
}
