using CH.Helper;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win
{
    [SupportedOSPlatform("windows")]
    public partial class PopupBase : FormBase
    {
        [DefaultValue(typeof(string), "")]
        [Description("Set Title")]
        private string _popupTitle = string.Empty;

        private bool _isSizeable = false;

        private Hashtable _ReturnData = new Hashtable();

        public PopupBase()
        {
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            topPanel.MouseDown += TopPanel_MouseDown;
            btnClose.Click += BtnClose_Click;
            btnMaximize.Click += BtnMaximize_Click;
            btnMinimize.Click += BtnMinimize_Click;
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BorderlessHelper.MouseMove(this.Handle);
            }
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Maximized)
            {
                base.WindowState = FormWindowState.Normal;
            }
            else
            {
                FormLocationModify();
            }
        }

        private void FormLocationModify()
        {
            Screen[] allScreens = Screen.AllScreens;
            if (allScreens.Length == 1)
            {
                base.WindowState = FormWindowState.Maximized;
                return;
            }

            base.StartPosition = FormStartPosition.Manual;
            base.WindowState = FormWindowState.Maximized;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            OnCancel();
        }



        [DefaultValue(true)]
        [Description("Resize Able")]
        public bool isSizeable
        {
            get
            {
                return _isSizeable;
            }
            set
            {
                _isSizeable = value;
                if (value)
                {
                    base.FormBorderStyle = FormBorderStyle.Sizable;
                }
                else
                {
                    base.FormBorderStyle = FormBorderStyle.None;
                }
            }
        }

        public string PopupTitle
        {
            get
            {
                return _popupTitle;
            }
            set
            {
                _popupTitle = value;
                Text = value + " - Popup";
                lblTitle.Text = value;
            }
        }

        public Hashtable ReturnData
        {
            get
            {
                return _ReturnData;
            }
            set
            {
                _ReturnData = value;
            }
        }

        protected virtual void OnSearch()
        {
        }

        protected virtual void OnOK()
        {
        }

        protected virtual void OnCancel()
        {
            if (base.Modal)
            {
                base.DialogResult = DialogResult.Cancel;
            }
            else
            {
                Close();
            }
        }

        protected virtual void OnSave()
        {
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    if (Environment.OSVersion.Version.Build >= 22000) // Win11 check
        //    {
        //        BorderlessHelper.SetWindowCorner(this.Handle, BorderlessHelper.DwmWindowCornerPreference.Round);
        //    }
        //    else
        //    {
        //        BorderlessHelper.SetWindowCorner(this, 16); // custom radius for older Windows
        //    }
        //}
    }
}
