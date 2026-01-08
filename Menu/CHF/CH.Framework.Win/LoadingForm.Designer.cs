namespace CH.Framework.Win
{
    partial class LoadingForm
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
            progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            SuspendLayout();
            // 
            // progressPanel1
            // 
            progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            progressPanel1.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            progressPanel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(40, 154, 221);
            progressPanel1.Appearance.Options.UseBackColor = true;
            progressPanel1.Appearance.Options.UseFont = true;
            progressPanel1.Appearance.Options.UseForeColor = true;
            progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            progressPanel1.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(40, 154, 221);
            progressPanel1.AppearanceCaption.Options.UseFont = true;
            progressPanel1.AppearanceCaption.Options.UseForeColor = true;
            progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            progressPanel1.AppearanceDescription.ForeColor = System.Drawing.Color.FromArgb(40, 154, 221);
            progressPanel1.AppearanceDescription.Options.UseFont = true;
            progressPanel1.AppearanceDescription.Options.UseForeColor = true;
            progressPanel1.AutoHeight = true;
            progressPanel1.AutoWidth = true;
            progressPanel1.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            progressPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            progressPanel1.Location = new System.Drawing.Point(0, 0);
            progressPanel1.Name = "progressPanel1";
            progressPanel1.Size = new System.Drawing.Size(207, 74);
            progressPanel1.TabIndex = 0;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(207, 74);
            Controls.Add(progressPanel1);
            Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            ForeColor = System.Drawing.Color.FromArgb(40, 154, 221);
            Name = "LoadingForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "LoadingForm";
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
    }
}