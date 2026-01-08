using CH.Framework.Common;
using CH.Framework.Win.Controls;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
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
        public enum DwmWindowCornerPreference
        {
            Default = 0,
            DoNotRound = 1,
            Round = 2,
            RoundSmall = 3
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int DwmSetWindowAttribute(
            IntPtr hwnd,
            int attr,
            ref DwmWindowCornerPreference pref,
            int size);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRoundRectRgn(
    int left, int top, int right, int bottom, int width, int height);

        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

        #endregion

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
                    btnOKYes.Text = "Yes";
                    btnYesNo.Text = "No";
                    btnNoCancel.Text = "Cancel";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = btnNoCancel;
                    txtTitle = "Confirmation";
                    imgBox.Image = svgImageCollection1.GetImage(0);
                    break;

                case MessageType.Error:
                    btnYesNo.Visible = false;
                    btnNoCancel.Visible = false;
                    btnOKYes.Text = "OK";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = null;
                    txtTitle = "Error";
                    imgBox.Image = svgImageCollection1.GetImage(1);
                    break;

                case MessageType.Warning:
                    btnYesNo.Visible = false;
                    btnNoCancel.Visible = false;
                    btnOKYes.Text = "OK";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = null;
                    txtTitle = "Warning";
                    imgBox.Image = svgImageCollection1.GetImage(2);
                    break;

                default:
                    btnYesNo.Visible = false;
                    btnNoCancel.Visible = false;
                    btnOKYes.Text = "OK";
                    base.AcceptButton = btnOKYes;
                    base.CancelButton = null;
                    txtTitle = "Information";
                    imgBox.Image = svgImageCollection1.GetImage(3);
                    break;
            }

            memoEdit_Desc.Text = _msgText.Replace("\r\n", "\n");
            lblTitle.Text = txtTitle;
        }

        private void InitializeEvent()
        {
            btnOKYes.Click += Btn_Click;
            btnYesNo.Click += Btn_Click;
            btnNoCancel.Click += Btn_Click;
            topPanel.MouseDown += TopPanel_MouseDown;
            topPanel.MouseMove += TopPanel_MouseMove;
            lblTitle.MouseDown += LblTitle_MouseDown;
            lblTitle.MouseMove += LblTitle_MouseMove;
            base.KeyDown += MsgDialog_KeyDown;
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; };
        }

        private void MsgDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void LblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                base.Location = new Point(base.Left - (mousePoint.X - e.X), base.Top - (mousePoint.Y - e.Y));
            }
        }

        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                base.Location = new Point(base.Left - (mousePoint.X - e.X), base.Top - (mousePoint.Y - e.Y));
            }
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

        private void ApplyWin11RoundedCorners()
        {
            var pref = DwmWindowCornerPreference.Round;
            DwmSetWindowAttribute(this.Handle, 33, ref pref, sizeof(int));
        }

        private void ApplyLegacyRoundedRegion(int radius = 12)
        {
            IntPtr region = CreateRoundRectRgn(
                0, 0, this.Width + 1, this.Height + 1,
                radius, radius);

            SetWindowRgn(this.Handle, region, true);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000))
                ApplyWin11RoundedCorners();
            else
                ApplyLegacyRoundedRegion();
        }


    }
}
