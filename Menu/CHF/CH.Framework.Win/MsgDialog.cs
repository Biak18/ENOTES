using CH.Framework.Common;
using CH.Framework.Win.Controls;
using CH.Helper;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win
{
    [SupportedOSPlatform("windows")]
    public partial class MsgDialog : Form
    {
        MessageType _msgType;
        string _msgText;
        Point mousePoint;

        #region Dll import
        //    public enum DwmWindowCornerPreference
        //    {
        //        Default = 0,
        //        DoNotRound = 1,
        //        Round = 2,
        //        RoundSmall = 3
        //    }

        //    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        //    private static extern int DwmSetWindowAttribute(
        //        IntPtr hwnd,
        //        int attr,
        //        ref DwmWindowCornerPreference pref,
        //        int size);

        //    [DllImport("gdi32.dll")]
        //    static extern IntPtr CreateRoundRectRgn(
        //int left, int top, int right, int bottom, int width, int height);

        //    [DllImport("user32.dll")]
        //    static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

        #endregion

        // Theme colors
        private readonly Color _backMain = Color.FromArgb(31, 42, 56);
        private readonly Color _backContent = Color.FromArgb(42, 56, 75);
        private readonly Color _accent = Color.FromArgb(40, 154, 221);
        private readonly Color _textPrimary = Color.White;
        private readonly Color _textSecondary = Color.FromArgb(180, 200, 220);
        private readonly Color _borderColor = Color.FromArgb(55, 75, 100);

        // Button colors per type
        private Color _btnPrimaryColor;
        private string _iconChar;
        private Color _iconColor;

        public MsgDialog()
        {
            InitializeComponent();
        }


        public MsgDialog(MessageType msgType, string msgText)
        {
            InitializeComponent();
            _msgType = msgType;
            _msgText = msgText;
            FormInitialize();
        }

        private void FormInitialize()
        {
            InitializeEvent();
            string txtTitle = string.Empty;
            //string txtDescription = string.Empty;
            switch (_msgType)
            {
                case MessageType.Question:
                    _btnPrimaryColor = Color.FromArgb(40, 154, 221);
                    btnOKYes.Visible = false;
                    btnYesNo.Text = "Yes";
                    btnNoCancel.Text = "No";
                    btnYesNo.BackColor = btnOKYes.BackColor;
                    btnYesNo.FlatAppearance.MouseOverBackColor = btnOKYes.FlatAppearance.MouseOverBackColor;
                    btnYesNo.FlatAppearance.MouseDownBackColor = btnOKYes.FlatAppearance.MouseDownBackColor;
                    base.AcceptButton = btnYesNo;
                    base.CancelButton = btnNoCancel;
                    txtTitle = "Confirmation";
                    imgBox.Image = svgImageCollection1.GetImage(0);
                    btnNoCancel.Focus();
                    break;

                case MessageType.YesNoCancel:
                    _btnPrimaryColor = Color.FromArgb(40, 154, 221);
                    btnOKYes.Text = "Yes";
                    btnYesNo.Text = "No";
                    btnNoCancel.Text = "Cancel";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = btnNoCancel;
                    txtTitle = "Confirmation";
                    imgBox.Image = svgImageCollection1.GetImage(0);
                    break;

                case MessageType.Error:
                    _btnPrimaryColor = Color.FromArgb(220, 53, 69);
                    btnYesNo.Visible = false;
                    btnNoCancel.Visible = false;
                    btnOKYes.Text = "OK";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = null;
                    txtTitle = "Error";
                    imgBox.Image = svgImageCollection1.GetImage(1);
                    break;

                case MessageType.Warning:
                    _btnPrimaryColor = Color.FromArgb(255, 152, 0);
                    btnYesNo.Visible = false;
                    btnNoCancel.Visible = false;
                    btnOKYes.Text = "OK";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = null;
                    txtTitle = "Warning";
                    imgBox.Image = svgImageCollection1.GetImage(2);
                    break;

                default:
                    _btnPrimaryColor = Color.FromArgb(40, 154, 221);
                    btnYesNo.Visible = false;
                    btnNoCancel.Visible = false;
                    btnOKYes.Text = "OK";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = null;
                    txtTitle = "Information";
                    imgBox.Image = svgImageCollection1.GetImage(3);
                    break;
            }
            ApplyTheme();
            memoEdit_Desc.Text = _msgText.Replace("\r\n", "\n");
            lblTitle.Text = txtTitle;

            // Fade in
            this.Opacity = 0;
            var fade = new Timer { Interval = 12 };
            fade.Tick += (s, e) =>
            {
                this.Opacity += 0.2;
                if (this.Opacity >= 1) { this.Opacity = 1; fade.Stop(); fade.Dispose(); }
            };
            this.Shown += (s, e) => fade.Start();
        }

        private void ApplyTheme()
        {
            Color secBack = Color.FromArgb(55, 75, 100);
            Color secHover = Color.FromArgb(70, 95, 125);
            Color secDown = Color.FromArgb(45, 60, 80);

            switch (_msgType)
            {
                case MessageType.Question:
                    btnYesNo.BackColor = _btnPrimaryColor;
                    btnYesNo.FlatAppearance.MouseOverBackColor = ControlPaint.Light(_btnPrimaryColor, 0.2f);
                    btnYesNo.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(_btnPrimaryColor, 0.1f);

                    btnNoCancel.BackColor = secBack;
                    btnNoCancel.FlatAppearance.MouseOverBackColor = secHover;
                    btnNoCancel.FlatAppearance.MouseDownBackColor = secDown;
                    break;

                case MessageType.YesNoCancel:
                    btnOKYes.BackColor = _btnPrimaryColor;
                    btnOKYes.FlatAppearance.MouseOverBackColor = ControlPaint.Light(_btnPrimaryColor, 0.2f);
                    btnOKYes.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(_btnPrimaryColor, 0.1f);

                    btnYesNo.BackColor = secBack;
                    btnYesNo.FlatAppearance.MouseOverBackColor = secBack;
                    btnYesNo.FlatAppearance.MouseDownBackColor = secBack;

                    btnNoCancel.BackColor = secBack;
                    btnNoCancel.FlatAppearance.MouseOverBackColor = secHover;
                    btnNoCancel.FlatAppearance.MouseDownBackColor = secDown;
                    break;

                default:
                    btnOKYes.BackColor = _btnPrimaryColor;
                    btnOKYes.FlatAppearance.MouseOverBackColor = ControlPaint.Light(_btnPrimaryColor, 0.2f);
                    btnOKYes.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(_btnPrimaryColor, 0.1f);
                    break;
            }
        }

        private void InitializeEvent()
        {
            btnOKYes.Click += Btn_Click;
            btnYesNo.Click += Btn_Click;
            btnNoCancel.Click += Btn_Click;
            topPanel.MouseDown += TopPanel_MouseDown;
            lblTitle.MouseDown += LblTitle_MouseDown;
            base.KeyDown += MsgDialog_KeyDown;
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; };
            topPanel.Paint += TopPanel_Paint;
            bottomPanel.Paint += BottomPanel_Paint;
        }

        private void BottomPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = e.ClipRectangle.Width;
            //top separator line
            using (var pen = new Pen(_borderColor, 1f))
            {
                g.DrawLine(pen, 0, 0, width, 0);
            }
        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = e.ClipRectangle.Width;
            int height = e.ClipRectangle.Height;
            //bottom separator line
            using (var pen = new Pen(_borderColor, 1f))
            {
                g.DrawLine(pen, 0, height - 1, width, height - 1);
            }
        }

        private void MsgDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            BorderlessHelper.MouseMove(this.Handle);
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            BorderlessHelper.MouseMove(this.Handle);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            CHRoundButton btn = sender as CHRoundButton;
            base.DialogResult = GetDialogResult(btn);
            //switch (btn.Name)
            //{
            //    case "btnOKYes":
            //        base.DialogResult = GetDialogResult(btn);
            //        break;

            //    case "btnYesNo":
            //        base.DialogResult = GetDialogResult(btn);
            //        break;

            //    case "btnNoCancel":
            //        base.DialogResult = GetDialogResult(btn);
            //        break;
            //}
        }

        private DialogResult GetDialogResult(CHRoundButton btn)
        {
            return btn.Text switch
            {
                "OK" => DialogResult.OK,
                "Yes" => DialogResult.Yes,
                "No" => DialogResult.No,
                "Cancel" => DialogResult.Cancel,
                _ => DialogResult.Cancel,
            };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                base.DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        #region Win
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Environment.OSVersion.Version.Build >= 22000) // Win11 check
            {
                BorderlessHelper.SetWindowCorner(this.Handle, BorderlessHelper.DwmWindowCornerPreference.Round);
            }
            else
            {
                BorderlessHelper.SetWindowCorner(this, 16); // custom radius for older Windows
            }
        }
        #endregion


    }
}
