namespace ENOTES
{
    partial class ENOTES_LOGIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ENOTES_LOGIN));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            picBox_Logo = new PictureBox();
            BtnLogin = new CH.Framework.Win.Controls.CHRoundButton();
            panel_Main = new Panel();
            BtnConfig = new CH.Framework.Win.Controls.CHRoundButton();
            BtnChgPassword = new CH.Framework.Win.Controls.CHRoundButton();
            BtnClose = new CH.Framework.Win.Controls.CHRoundButton();
            lbl_Login = new DevExpress.XtraEditors.LabelControl();
            BtnTxt_Password = new CH.Framework.Win.Controls.CHTextBoxOutline();
            BtnTxt_CdUser = new CH.Framework.Win.Controls.CHTextBoxOutline();
            BtnTxt_Company = new CH.Framework.Win.Controls.CHTextBoxOutline();
            ((System.ComponentModel.ISupportInitialize)picBox_Logo).BeginInit();
            panel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BtnTxt_Password.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnTxt_CdUser.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnTxt_Company.Properties).BeginInit();
            SuspendLayout();
            // 
            // picBox_Logo
            // 
            picBox_Logo.BackColor = Color.FromArgb(31, 42, 56);
            picBox_Logo.BackgroundImage = (Image)resources.GetObject("picBox_Logo.BackgroundImage");
            picBox_Logo.BackgroundImageLayout = ImageLayout.Stretch;
            picBox_Logo.Dock = DockStyle.Left;
            picBox_Logo.Location = new Point(0, 0);
            picBox_Logo.Name = "picBox_Logo";
            picBox_Logo.Size = new Size(550, 450);
            picBox_Logo.TabIndex = 0;
            picBox_Logo.TabStop = false;
            // 
            // BtnLogin
            // 
            BtnLogin.BackColor = Color.FromArgb(0, 212, 255);
            BtnLogin.BackgroundColor = Color.FromArgb(0, 212, 255);
            BtnLogin.BorderColor = Color.Black;
            BtnLogin.BorderRadius = 40;
            BtnLogin.BorderSize = 0;
            BtnLogin.FlatAppearance.BorderSize = 0;
            BtnLogin.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 170, 204);
            BtnLogin.FlatStyle = FlatStyle.Flat;
            BtnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnLogin.ForeColor = Color.White;
            BtnLogin.Location = new Point(74, 305);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(232, 41);
            BtnLogin.TabIndex = 2;
            BtnLogin.Text = "LOGIN";
            BtnLogin.TextColor = Color.White;
            BtnLogin.UseVisualStyleBackColor = false;
            // 
            // panel_Main
            // 
            panel_Main.Controls.Add(BtnConfig);
            panel_Main.Controls.Add(BtnChgPassword);
            panel_Main.Controls.Add(BtnClose);
            panel_Main.Controls.Add(lbl_Login);
            panel_Main.Controls.Add(BtnTxt_Password);
            panel_Main.Controls.Add(BtnTxt_CdUser);
            panel_Main.Controls.Add(BtnTxt_Company);
            panel_Main.Controls.Add(BtnLogin);
            panel_Main.Dock = DockStyle.Fill;
            panel_Main.Location = new Point(550, 0);
            panel_Main.Name = "panel_Main";
            panel_Main.Size = new Size(360, 450);
            panel_Main.TabIndex = 3;
            // 
            // BtnConfig
            // 
            BtnConfig.BackColor = Color.Transparent;
            BtnConfig.BackgroundColor = Color.Transparent;
            BtnConfig.BackgroundImage = (Image)resources.GetObject("BtnConfig.BackgroundImage");
            BtnConfig.BackgroundImageLayout = ImageLayout.Center;
            BtnConfig.BorderColor = Color.FromArgb(147, 112, 147);
            BtnConfig.BorderRadius = 40;
            BtnConfig.BorderSize = 0;
            BtnConfig.Cursor = Cursors.Hand;
            BtnConfig.FlatAppearance.BorderSize = 0;
            BtnConfig.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnConfig.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnConfig.FlatStyle = FlatStyle.Flat;
            BtnConfig.Font = new Font("Segoe UI", 6.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnConfig.ForeColor = Color.White;
            BtnConfig.ImageAlign = ContentAlignment.TopCenter;
            BtnConfig.Location = new Point(74, 371);
            BtnConfig.Name = "BtnConfig";
            BtnConfig.Size = new Size(90, 67);
            BtnConfig.TabIndex = 9;
            BtnConfig.Text = "Configuration";
            BtnConfig.TextAlign = ContentAlignment.BottomCenter;
            BtnConfig.TextColor = Color.White;
            BtnConfig.UseCompatibleTextRendering = true;
            BtnConfig.UseVisualStyleBackColor = false;
            // 
            // BtnChgPassword
            // 
            BtnChgPassword.BackColor = Color.Transparent;
            BtnChgPassword.BackgroundColor = Color.Transparent;
            BtnChgPassword.BackgroundImage = (Image)resources.GetObject("BtnChgPassword.BackgroundImage");
            BtnChgPassword.BackgroundImageLayout = ImageLayout.Center;
            BtnChgPassword.BorderColor = Color.FromArgb(147, 112, 147);
            BtnChgPassword.BorderRadius = 40;
            BtnChgPassword.BorderSize = 0;
            BtnChgPassword.Cursor = Cursors.Hand;
            BtnChgPassword.FlatAppearance.BorderSize = 0;
            BtnChgPassword.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnChgPassword.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnChgPassword.FlatStyle = FlatStyle.Flat;
            BtnChgPassword.Font = new Font("Segoe UI", 6.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnChgPassword.ForeColor = Color.White;
            BtnChgPassword.ImageAlign = ContentAlignment.TopCenter;
            BtnChgPassword.Location = new Point(216, 371);
            BtnChgPassword.Name = "BtnChgPassword";
            BtnChgPassword.Size = new Size(90, 67);
            BtnChgPassword.TabIndex = 9;
            BtnChgPassword.Text = "Change Password";
            BtnChgPassword.TextAlign = ContentAlignment.BottomCenter;
            BtnChgPassword.TextColor = Color.White;
            BtnChgPassword.UseCompatibleTextRendering = true;
            BtnChgPassword.UseVisualStyleBackColor = false;
            // 
            // BtnClose
            // 
            BtnClose.BackColor = Color.FromArgb(31, 42, 56);
            BtnClose.BackgroundColor = Color.FromArgb(31, 42, 56);
            BtnClose.BackgroundImage = (Image)resources.GetObject("BtnClose.BackgroundImage");
            BtnClose.BackgroundImageLayout = ImageLayout.Center;
            BtnClose.BorderColor = Color.FromArgb(147, 112, 147);
            BtnClose.BorderRadius = 40;
            BtnClose.BorderSize = 0;
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.ForeColor = Color.White;
            BtnClose.Location = new Point(308, 9);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new Size(40, 40);
            BtnClose.TabIndex = 8;
            BtnClose.TextColor = Color.White;
            BtnClose.UseVisualStyleBackColor = false;
            // 
            // lbl_Login
            // 
            lbl_Login.Appearance.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lbl_Login.Appearance.ForeColor = Color.FromArgb(0, 212, 255);
            lbl_Login.Appearance.Options.UseFont = true;
            lbl_Login.Appearance.Options.UseForeColor = true;
            lbl_Login.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lbl_Login.Enabled = false;
            lbl_Login.Location = new Point(74, 58);
            lbl_Login.Name = "lbl_Login";
            lbl_Login.Padding = new Padding(50, 0, 0, 0);
            lbl_Login.Size = new Size(232, 57);
            lbl_Login.TabIndex = 7;
            lbl_Login.Text = "LOGIN";
            // 
            // BtnTxt_Password
            // 
            BtnTxt_Password.EditValue = "";
            BtnTxt_Password.Location = new Point(74, 245);
            BtnTxt_Password.Name = "BtnTxt_Password";
            BtnTxt_Password.OutlineColor = Color.White;
            BtnTxt_Password.Properties.Appearance.BackColor = Color.FromArgb(31, 42, 56);
            BtnTxt_Password.Properties.Appearance.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnTxt_Password.Properties.Appearance.ForeColor = Color.White;
            BtnTxt_Password.Properties.Appearance.Options.UseBackColor = true;
            BtnTxt_Password.Properties.Appearance.Options.UseFont = true;
            BtnTxt_Password.Properties.Appearance.Options.UseForeColor = true;
            BtnTxt_Password.Properties.AutoHeight = false;
            BtnTxt_Password.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions1.Image = (Image)resources.GetObject("editorButtonImageOptions1.Image");
            BtnTxt_Password.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 25, false, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            BtnTxt_Password.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            BtnTxt_Password.Properties.PasswordChar = '*';
            BtnTxt_Password.Size = new Size(232, 40);
            BtnTxt_Password.TabIndex = 6;
            // 
            // BtnTxt_CdUser
            // 
            BtnTxt_CdUser.EditValue = "";
            BtnTxt_CdUser.Location = new Point(74, 193);
            BtnTxt_CdUser.Name = "BtnTxt_CdUser";
            BtnTxt_CdUser.OutlineColor = Color.White;
            BtnTxt_CdUser.Properties.Appearance.BackColor = Color.FromArgb(31, 42, 56);
            BtnTxt_CdUser.Properties.Appearance.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnTxt_CdUser.Properties.Appearance.ForeColor = Color.White;
            BtnTxt_CdUser.Properties.Appearance.Options.UseBackColor = true;
            BtnTxt_CdUser.Properties.Appearance.Options.UseFont = true;
            BtnTxt_CdUser.Properties.Appearance.Options.UseForeColor = true;
            BtnTxt_CdUser.Properties.AutoHeight = false;
            BtnTxt_CdUser.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions2.Image = (Image)resources.GetObject("editorButtonImageOptions2.Image");
            BtnTxt_CdUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 25, false, true, true, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            BtnTxt_CdUser.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            BtnTxt_CdUser.Size = new Size(232, 40);
            BtnTxt_CdUser.TabIndex = 5;
            // 
            // BtnTxt_Company
            // 
            BtnTxt_Company.EditValue = "";
            BtnTxt_Company.Location = new Point(74, 141);
            BtnTxt_Company.Name = "BtnTxt_Company";
            BtnTxt_Company.OutlineColor = Color.White;
            BtnTxt_Company.Properties.Appearance.BackColor = Color.FromArgb(31, 42, 56);
            BtnTxt_Company.Properties.Appearance.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnTxt_Company.Properties.Appearance.ForeColor = Color.White;
            BtnTxt_Company.Properties.Appearance.Options.UseBackColor = true;
            BtnTxt_Company.Properties.Appearance.Options.UseFont = true;
            BtnTxt_Company.Properties.Appearance.Options.UseForeColor = true;
            BtnTxt_Company.Properties.AutoHeight = false;
            BtnTxt_Company.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions3.Image = (Image)resources.GetObject("editorButtonImageOptions3.Image");
            BtnTxt_Company.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 25, false, true, true, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            BtnTxt_Company.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            BtnTxt_Company.Size = new Size(232, 40);
            BtnTxt_Company.TabIndex = 4;
            // 
            // ENOTES_LOGIN
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 42, 56);
            ClientSize = new Size(910, 450);
            Controls.Add(panel_Main);
            Controls.Add(picBox_Logo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ENOTES_LOGIN";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ENOTES_LOGIN";
            ((System.ComponentModel.ISupportInitialize)picBox_Logo).EndInit();
            panel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)BtnTxt_Password.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnTxt_CdUser.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnTxt_Company.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picBox_Logo;
        private CH.Framework.Win.Controls.CHRoundButton BtnLogin;
        private Panel panel_Main;
        private CH.Framework.Win.Controls.CHTextBoxOutline BtnTxt_Company;
        private CH.Framework.Win.Controls.CHTextBoxOutline BtnTxt_Password;
        private CH.Framework.Win.Controls.CHTextBoxOutline BtnTxt_CdUser;
        private DevExpress.XtraEditors.LabelControl lbl_Login;
        private CH.Framework.Win.Controls.CHRoundButton BtnClose;
        private CH.Framework.Win.Controls.CHRoundButton BtnChgPassword;
        private CH.Framework.Win.Controls.CHRoundButton BtnConfig;
    }
}