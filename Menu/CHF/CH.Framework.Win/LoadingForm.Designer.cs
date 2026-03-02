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
            chLoadingPanel1 = new Controls.CHLoadingPanel();
            SuspendLayout();
            // 
            // chLoadingPanel1
            // 
            chLoadingPanel1.BackColor = System.Drawing.Color.FromArgb(31, 42, 56);
            chLoadingPanel1.Caption = "Please wait";
            chLoadingPanel1.Description = "Loading...";
            chLoadingPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            chLoadingPanel1.Location = new System.Drawing.Point(0, 0);
            chLoadingPanel1.Name = "chLoadingPanel1";
            chLoadingPanel1.Size = new System.Drawing.Size(280, 140);
            chLoadingPanel1.TabIndex = 0;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(280, 140);
            Controls.Add(chLoadingPanel1);
            Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            ForeColor = System.Drawing.Color.FromArgb(40, 154, 221);
            Name = "LoadingForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "LoadingForm";
            ResumeLayout(false);
        }

        #endregion

        private Controls.CHLoadingPanel chLoadingPanel1;
    }
}