namespace CH.Framework.Win
{
    partial class PopupBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupBase));
            topPanel = new System.Windows.Forms.Panel();
            lblTitle = new DevExpress.XtraEditors.LabelControl();
            btnMinimize = new CH.Framework.Win.Controls.CHRoundButton();
            btnMaximize = new CH.Framework.Win.Controls.CHRoundButton();
            btnClose = new CH.Framework.Win.Controls.CHRoundButton();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = System.Drawing.Color.FromArgb(31, 42, 56);
            topPanel.Controls.Add(lblTitle);
            topPanel.Controls.Add(btnMinimize);
            topPanel.Controls.Add(btnMaximize);
            topPanel.Controls.Add(btnClose);
            topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            topPanel.Location = new System.Drawing.Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new System.Drawing.Size(827, 40);
            topPanel.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            lblTitle.Appearance.Options.UseFont = true;
            lblTitle.Appearance.Options.UseForeColor = true;
            lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblTitle.Location = new System.Drawing.Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            lblTitle.Size = new System.Drawing.Size(144, 40);
            lblTitle.TabIndex = 17;
            lblTitle.Text = "Popup";
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnMinimize.BackColor = System.Drawing.Color.Transparent;
            btnMinimize.BackgroundColor = System.Drawing.Color.Transparent;
            btnMinimize.BorderColor = System.Drawing.Color.FromArgb(147, 112, 147);
            btnMinimize.BorderRadius = 0;
            btnMinimize.BorderSize = 0;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(24, 32, 43);
            btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(42, 56, 75);
            btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMinimize.ForeColor = System.Drawing.Color.White;
            btnMinimize.Image = (System.Drawing.Image)resources.GetObject("btnMinimize.Image");
            btnMinimize.Location = new System.Drawing.Point(701, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new System.Drawing.Size(40, 40);
            btnMinimize.TabIndex = 16;
            btnMinimize.TextColor = System.Drawing.Color.White;
            btnMinimize.UseVisualStyleBackColor = false;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnMaximize.BackColor = System.Drawing.Color.Transparent;
            btnMaximize.BackgroundColor = System.Drawing.Color.Transparent;
            btnMaximize.BorderColor = System.Drawing.Color.FromArgb(147, 112, 147);
            btnMaximize.BorderRadius = 0;
            btnMaximize.BorderSize = 0;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(24, 32, 43);
            btnMaximize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(42, 56, 75);
            btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMaximize.ForeColor = System.Drawing.Color.White;
            btnMaximize.Image = (System.Drawing.Image)resources.GetObject("btnMaximize.Image");
            btnMaximize.Location = new System.Drawing.Point(744, 0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new System.Drawing.Size(40, 40);
            btnMaximize.TabIndex = 15;
            btnMaximize.TextColor = System.Drawing.Color.White;
            btnMaximize.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.BackColor = System.Drawing.Color.Transparent;
            btnClose.BackgroundColor = System.Drawing.Color.Transparent;
            btnClose.BorderColor = System.Drawing.Color.FromArgb(147, 112, 147);
            btnClose.BorderRadius = 0;
            btnClose.BorderSize = 0;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Image = (System.Drawing.Image)resources.GetObject("btnClose.Image");
            btnClose.Location = new System.Drawing.Point(787, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(40, 40);
            btnClose.TabIndex = 14;
            btnClose.TextColor = System.Drawing.Color.White;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // PopupBase
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(827, 525);
            Controls.Add(topPanel);
            Name = "PopupBase";
            Text = "PopupBase";
            topPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private Controls.CHRoundButton btnMinimize;
        private Controls.CHRoundButton btnMaximize;
        private Controls.CHRoundButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblTitle;
    }
}