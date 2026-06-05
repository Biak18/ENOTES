namespace CH.Framework.Win.Controls
{
    partial class CHLPeriodEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chLabel1 = new CHLabel();
            chPeriodEdit1 = new CHPeriodEdit();
            SuspendLayout();
            // 
            // chLabel1
            // 
            chLabel1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            chLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(40, 154, 221);
            chLabel1.Appearance.Options.UseFont = true;
            chLabel1.Appearance.Options.UseForeColor = true;
            chLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            chLabel1.Location = new System.Drawing.Point(0, 0);
            chLabel1.Name = "chLabel1";
            chLabel1.Size = new System.Drawing.Size(80, 23);
            chLabel1.TabIndex = 0;
            chLabel1.Text = "";
            // 
            // chPeriodEdit1
            // 
            chPeriodEdit1.DateFormat = "yyyy\\/MM\\/dd";
            chPeriodEdit1.DtEnd = "";
            chPeriodEdit1.DtStart = "";
            chPeriodEdit1.Location = new System.Drawing.Point(86, 0);
            chPeriodEdit1.Name = "chPeriodEdit1";
            chPeriodEdit1.Size = new System.Drawing.Size(185, 24);
            chPeriodEdit1.TabIndex = 1;
            // 
            // CHLPeriodEdit
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(chPeriodEdit1);
            Controls.Add(chLabel1);
            Name = "CHLPeriodEdit";
            Size = new System.Drawing.Size(270, 24);
            ResumeLayout(false);
        }

        #endregion

        private CHLabel chLabel1;
        private CHPeriodEdit chPeriodEdit1;
    }
}
