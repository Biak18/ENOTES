using System.Drawing;
using System.Windows.Forms;

namespace CH.Framework.Win
{
    partial class CHFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CHFormBase));
            topPanel = new Panel();
            btnMinimize = new Controls.CHRoundButton();
            btnMaximize = new Controls.CHRoundButton();
            btnClose = new Controls.CHRoundButton();
            btnPrint = new Controls.CHRoundButton();
            btnSave = new Controls.CHRoundButton();
            btnDel = new Controls.CHRoundButton();
            btnAdd = new Controls.CHRoundButton();
            btnSearch = new Controls.CHRoundButton();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(31, 42, 56);
            topPanel.Controls.Add(btnMinimize);
            topPanel.Controls.Add(btnMaximize);
            topPanel.Controls.Add(btnClose);
            topPanel.Controls.Add(btnPrint);
            topPanel.Controls.Add(btnSave);
            topPanel.Controls.Add(btnDel);
            topPanel.Controls.Add(btnAdd);
            topPanel.Controls.Add(btnSearch);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(998, 50);
            topPanel.TabIndex = 2;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.BackColor = Color.Transparent;
            btnMinimize.BackgroundColor = Color.Transparent;
            btnMinimize.BorderColor = Color.FromArgb(147, 112, 147);
            btnMinimize.BorderRadius = 0;
            btnMinimize.BorderSize = 0;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.ForeColor = Color.White;
            btnMinimize.Image = (Image)resources.GetObject("btnMinimize.Image");
            btnMinimize.Location = new Point(866, 5);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(40, 40);
            btnMinimize.TabIndex = 13;
            btnMinimize.TextColor = Color.White;
            btnMinimize.UseVisualStyleBackColor = false;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.BackColor = Color.Transparent;
            btnMaximize.BackgroundColor = Color.Transparent;
            btnMaximize.BorderColor = Color.FromArgb(147, 112, 147);
            btnMaximize.BorderRadius = 0;
            btnMaximize.BorderSize = 0;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnMaximize.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.ForeColor = Color.White;
            btnMaximize.Image = (Image)resources.GetObject("btnMaximize.Image");
            btnMaximize.Location = new Point(912, 5);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(40, 40);
            btnMaximize.TabIndex = 12;
            btnMaximize.TextColor = Color.White;
            btnMaximize.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.BackgroundColor = Color.Transparent;
            btnClose.BorderColor = Color.FromArgb(147, 112, 147);
            btnClose.BorderRadius = 0;
            btnClose.BorderSize = 0;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 0, 0);
            btnClose.FlatAppearance.MouseOverBackColor = Color.Red;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.Location = new Point(958, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(40, 40);
            btnClose.TabIndex = 11;
            btnClose.TextColor = Color.White;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.Transparent;
            btnPrint.BackgroundColor = Color.Transparent;
            btnPrint.BorderColor = Color.FromArgb(147, 112, 147);
            btnPrint.BorderRadius = 0;
            btnPrint.BorderSize = 0;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.ForeColor = Color.White;
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.Location = new Point(180, 5);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(40, 40);
            btnPrint.TabIndex = 10;
            btnPrint.TextColor = Color.White;
            btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Transparent;
            btnSave.BackgroundColor = Color.Transparent;
            btnSave.BorderColor = Color.FromArgb(147, 112, 147);
            btnSave.BorderRadius = 0;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Image = (Image)resources.GetObject("btnSave.Image");
            btnSave.Location = new Point(135, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(40, 40);
            btnSave.TabIndex = 9;
            btnSave.TextColor = Color.White;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnDel
            // 
            btnDel.BackColor = Color.Transparent;
            btnDel.BackgroundColor = Color.Transparent;
            btnDel.BorderColor = Color.FromArgb(147, 112, 147);
            btnDel.BorderRadius = 0;
            btnDel.BorderSize = 0;
            btnDel.FlatAppearance.BorderSize = 0;
            btnDel.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnDel.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnDel.FlatStyle = FlatStyle.Flat;
            btnDel.ForeColor = Color.White;
            btnDel.Image = (Image)resources.GetObject("btnDel.Image");
            btnDel.Location = new Point(90, 5);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(40, 40);
            btnDel.TabIndex = 8;
            btnDel.TextColor = Color.White;
            btnDel.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.Transparent;
            btnAdd.BackgroundColor = Color.Transparent;
            btnAdd.BorderColor = Color.FromArgb(147, 112, 147);
            btnAdd.BorderRadius = 0;
            btnAdd.BorderSize = 0;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
            btnAdd.Location = new Point(45, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(40, 40);
            btnAdd.TabIndex = 7;
            btnAdd.TextColor = Color.White;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackgroundColor = Color.Transparent;
            btnSearch.BorderColor = Color.FromArgb(147, 112, 147);
            btnSearch.BorderRadius = 0;
            btnSearch.BorderSize = 0;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.White;
            btnSearch.Image = (Image)resources.GetObject("btnSearch.Image");
            btnSearch.Location = new Point(0, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(40, 40);
            btnSearch.TabIndex = 3;
            btnSearch.TextColor = Color.White;
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // CHFormBase
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 599);
            Controls.Add(topPanel);
            Name = "CHFormBase";
            Text = "CHFormBase";
            topPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private Controls.CHRoundButton btnSearch;
        private Controls.CHRoundButton btnPrint;
        private Controls.CHRoundButton btnSave;
        private Controls.CHRoundButton btnDel;
        private Controls.CHRoundButton btnAdd;
        private Controls.CHRoundButton btnMinimize;
        private Controls.CHRoundButton btnMaximize;
        private Controls.CHRoundButton btnClose;
    }
}