using System.Drawing;
using System.Windows.Forms;

namespace CH.Framework.Win
{
    partial class FormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            helpProvider1 = new HelpProvider();
            SuspendLayout();
            // 
            // FormBase
            // 
            AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoValidate = AutoValidate.Disable;
            ClientSize = new Size(817, 479);
            helpProvider1.SetShowHelp(this, true);
            Name = "FormBase";
            Text = "FormBase";
            ResumeLayout(false);
        }

        #endregion

        private HelpProvider helpProvider1;
    }
}