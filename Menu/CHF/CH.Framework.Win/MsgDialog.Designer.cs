namespace CH.Framework.Win
{
    partial class MsgDialog
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgDialog));
            imgBox = new System.Windows.Forms.PictureBox();
            lblTitle = new System.Windows.Forms.Label();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            btnNoCancel = new Controls.CHRoundButton();
            btnYesNo = new Controls.CHRoundButton();
            btnOKYes = new Controls.CHRoundButton();
            topPanel = new System.Windows.Forms.Panel();
            btnClose = new Controls.CHRoundButton();
            memoEdit_Desc = new DevExpress.XtraEditors.MemoEdit();
            svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);
            ((System.ComponentModel.ISupportInitialize)imgBox).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoEdit_Desc.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)svgImageCollection1).BeginInit();
            SuspendLayout();
            // 
            // imgBox
            // 
            imgBox.Location = new System.Drawing.Point(3, 4);
            imgBox.Name = "imgBox";
            imgBox.Size = new System.Drawing.Size(25, 25);
            imgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            imgBox.TabIndex = 0;
            imgBox.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(32, 4);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(131, 25);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Information";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(42, 56, 75);
            flowLayoutPanel2.Controls.Add(btnNoCancel);
            flowLayoutPanel2.Controls.Add(btnYesNo);
            flowLayoutPanel2.Controls.Add(btnOKYes);
            flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanel2.Location = new System.Drawing.Point(0, 132);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            flowLayoutPanel2.Size = new System.Drawing.Size(300, 43);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // btnNoCancel
            // 
            btnNoCancel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            btnNoCancel.BackgroundColor = System.Drawing.SystemColors.ButtonShadow;
            btnNoCancel.BorderColor = System.Drawing.Color.FromArgb(147, 112, 147);
            btnNoCancel.BorderRadius = 16;
            btnNoCancel.BorderSize = 0;
            btnNoCancel.FlatAppearance.BorderSize = 0;
            btnNoCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(140, 140, 140);
            btnNoCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            btnNoCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNoCancel.ForeColor = System.Drawing.Color.Black;
            btnNoCancel.Location = new System.Drawing.Point(212, 5);
            btnNoCancel.Name = "btnNoCancel";
            btnNoCancel.Size = new System.Drawing.Size(85, 32);
            btnNoCancel.TabIndex = 0;
            btnNoCancel.Text = "No";
            btnNoCancel.TextColor = System.Drawing.Color.Black;
            btnNoCancel.UseVisualStyleBackColor = false;
            // 
            // btnYesNo
            // 
            btnYesNo.BackColor = System.Drawing.Color.FromArgb(98, 98, 98);
            btnYesNo.BackgroundColor = System.Drawing.Color.FromArgb(98, 98, 98);
            btnYesNo.BorderColor = System.Drawing.Color.FromArgb(147, 112, 147);
            btnYesNo.BorderRadius = 16;
            btnYesNo.BorderSize = 0;
            btnYesNo.FlatAppearance.BorderSize = 0;
            btnYesNo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(70, 70, 70);
            btnYesNo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnYesNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnYesNo.ForeColor = System.Drawing.Color.Black;
            btnYesNo.Location = new System.Drawing.Point(121, 5);
            btnYesNo.Name = "btnYesNo";
            btnYesNo.Size = new System.Drawing.Size(85, 32);
            btnYesNo.TabIndex = 1;
            btnYesNo.Text = "Yes";
            btnYesNo.TextColor = System.Drawing.Color.Black;
            btnYesNo.UseVisualStyleBackColor = false;
            // 
            // btnOKYes
            // 
            btnOKYes.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnOKYes.BackgroundColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnOKYes.BorderColor = System.Drawing.Color.FromArgb(147, 112, 147);
            btnOKYes.BorderRadius = 16;
            btnOKYes.BorderSize = 0;
            btnOKYes.FlatAppearance.BorderSize = 0;
            btnOKYes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(0, 100, 210);
            btnOKYes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnOKYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOKYes.ForeColor = System.Drawing.Color.Black;
            btnOKYes.Location = new System.Drawing.Point(30, 5);
            btnOKYes.Name = "btnOKYes";
            btnOKYes.Size = new System.Drawing.Size(85, 32);
            btnOKYes.TabIndex = 2;
            btnOKYes.Text = "OK";
            btnOKYes.TextColor = System.Drawing.Color.Black;
            btnOKYes.UseVisualStyleBackColor = false;
            // 
            // topPanel
            // 
            topPanel.BackColor = System.Drawing.Color.FromArgb(31, 42, 56);
            topPanel.Controls.Add(btnClose);
            topPanel.Controls.Add(lblTitle);
            topPanel.Controls.Add(imgBox);
            topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            topPanel.Location = new System.Drawing.Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new System.Drawing.Size(300, 32);
            topPanel.TabIndex = 3;
            // 
            // btnClose
            // 
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
            btnClose.Location = new System.Drawing.Point(269, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(31, 32);
            btnClose.TabIndex = 2;
            btnClose.TextColor = System.Drawing.Color.White;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // memoEdit_Desc
            // 
            memoEdit_Desc.Dock = System.Windows.Forms.DockStyle.Top;
            memoEdit_Desc.Location = new System.Drawing.Point(0, 32);
            memoEdit_Desc.Name = "memoEdit_Desc";
            memoEdit_Desc.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(42, 56, 75);
            memoEdit_Desc.Properties.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            memoEdit_Desc.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            memoEdit_Desc.Properties.Appearance.Options.UseBackColor = true;
            memoEdit_Desc.Properties.Appearance.Options.UseFont = true;
            memoEdit_Desc.Properties.Appearance.Options.UseForeColor = true;
            memoEdit_Desc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            memoEdit_Desc.Properties.ReadOnly = true;
            memoEdit_Desc.Size = new System.Drawing.Size(300, 100);
            memoEdit_Desc.TabIndex = 4;
            // 
            // svgImageCollection1
            // 
            svgImageCollection1.ImageSize = new System.Drawing.Size(24, 24);
            svgImageCollection1.Add("question", (DevExpress.Utils.Svg.SvgImage)resources.GetObject("svgImageCollection1.question"));
            svgImageCollection1.Add("error", (DevExpress.Utils.Svg.SvgImage)resources.GetObject("svgImageCollection1.error"));
            svgImageCollection1.Add("warning", (DevExpress.Utils.Svg.SvgImage)resources.GetObject("svgImageCollection1.warning"));
            svgImageCollection1.Add("information", (DevExpress.Utils.Svg.SvgImage)resources.GetObject("svgImageCollection1.information"));
            // 
            // MsgDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(300, 175);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(memoEdit_Desc);
            Controls.Add(topPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "MsgDialog";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "MsgDialog";
            ((System.ComponentModel.ISupportInitialize)imgBox).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)memoEdit_Desc.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)svgImageCollection1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PictureBox imgBox;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Controls.CHRoundButton btnNoCancel;
        private Controls.CHRoundButton btnYesNo;
        private Controls.CHRoundButton btnOKYes;
        private System.Windows.Forms.Panel topPanel;
        private DevExpress.XtraEditors.MemoEdit memoEdit_Desc;
        private Controls.CHRoundButton btnClose;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
    }
}