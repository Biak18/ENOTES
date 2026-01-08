namespace ENOTES
{
    partial class ENOTES
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ENOTES));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            topPanel = new Panel();
            btnMinimize = new CH.Framework.Win.Controls.CHRoundButton();
            btnMaximize = new CH.Framework.Win.Controls.CHRoundButton();
            btnClose = new CH.Framework.Win.Controls.CHRoundButton();
            btnPrint = new CH.Framework.Win.Controls.CHRoundButton();
            btnSave = new CH.Framework.Win.Controls.CHRoundButton();
            btnDel = new CH.Framework.Win.Controls.CHRoundButton();
            btnAdd = new CH.Framework.Win.Controls.CHRoundButton();
            btnSearch = new CH.Framework.Win.Controls.CHRoundButton();
            logo = new DevExpress.XtraEditors.PictureEdit();
            leftPanel = new Panel();
            menuTree = new DevExpress.XtraTreeList.TreeList();
            imageCollection1 = new DevExpress.Utils.ImageCollection(components);
            btnFilterMenu = new DevExpress.XtraEditors.ButtonEdit();
            myTooltip = new ToolTip(components);
            xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(components);
            mainPanel = new Panel();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logo.Properties).BeginInit();
            leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)menuTree).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageCollection1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnFilterMenu.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).BeginInit();
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
            topPanel.Controls.Add(logo);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1160, 50);
            topPanel.TabIndex = 1;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.BackColor = Color.Transparent;
            btnMinimize.BackgroundColor = Color.Transparent;
            btnMinimize.BorderColor = Color.FromArgb(147, 112, 147);
            btnMinimize.BorderRadius = 40;
            btnMinimize.BorderSize = 0;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.ForeColor = Color.White;
            btnMinimize.Image = (Image)resources.GetObject("btnMinimize.Image");
            btnMinimize.Location = new Point(1020, 5);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(40, 40);
            btnMinimize.TabIndex = 9;
            btnMinimize.TextColor = Color.White;
            btnMinimize.UseVisualStyleBackColor = false;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.BackColor = Color.Transparent;
            btnMaximize.BackgroundColor = Color.Transparent;
            btnMaximize.BorderColor = Color.FromArgb(147, 112, 147);
            btnMaximize.BorderRadius = 40;
            btnMaximize.BorderSize = 0;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnMaximize.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.ForeColor = Color.White;
            btnMaximize.Image = (Image)resources.GetObject("btnMaximize.Image");
            btnMaximize.Location = new Point(1066, 5);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(40, 40);
            btnMaximize.TabIndex = 8;
            btnMaximize.TextColor = Color.White;
            btnMaximize.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.BackgroundColor = Color.Transparent;
            btnClose.BorderColor = Color.FromArgb(147, 112, 147);
            btnClose.BorderRadius = 40;
            btnClose.BorderSize = 0;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 0, 0);
            btnClose.FlatAppearance.MouseOverBackColor = Color.Red;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.Location = new Point(1112, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(40, 40);
            btnClose.TabIndex = 7;
            btnClose.TextColor = Color.White;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.Transparent;
            btnPrint.BackgroundColor = Color.Transparent;
            btnPrint.BorderColor = Color.FromArgb(147, 112, 147);
            btnPrint.BorderRadius = 40;
            btnPrint.BorderSize = 0;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.ForeColor = Color.White;
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.Location = new Point(410, 5);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(40, 40);
            btnPrint.TabIndex = 6;
            btnPrint.TextColor = Color.White;
            btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Transparent;
            btnSave.BackgroundColor = Color.Transparent;
            btnSave.BorderColor = Color.FromArgb(147, 112, 147);
            btnSave.BorderRadius = 40;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Image = (Image)resources.GetObject("btnSave.Image");
            btnSave.Location = new Point(365, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(40, 40);
            btnSave.TabIndex = 5;
            btnSave.TextColor = Color.White;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnDel
            // 
            btnDel.BackColor = Color.Transparent;
            btnDel.BackgroundColor = Color.Transparent;
            btnDel.BorderColor = Color.FromArgb(147, 112, 147);
            btnDel.BorderRadius = 40;
            btnDel.BorderSize = 0;
            btnDel.FlatAppearance.BorderSize = 0;
            btnDel.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnDel.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnDel.FlatStyle = FlatStyle.Flat;
            btnDel.ForeColor = Color.White;
            btnDel.Image = (Image)resources.GetObject("btnDel.Image");
            btnDel.Location = new Point(320, 5);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(40, 40);
            btnDel.TabIndex = 4;
            btnDel.TextColor = Color.White;
            btnDel.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.Transparent;
            btnAdd.BackgroundColor = Color.Transparent;
            btnAdd.BorderColor = Color.FromArgb(147, 112, 147);
            btnAdd.BorderRadius = 40;
            btnAdd.BorderSize = 0;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
            btnAdd.Location = new Point(275, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(40, 40);
            btnAdd.TabIndex = 3;
            btnAdd.TextColor = Color.White;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackgroundColor = Color.Transparent;
            btnSearch.BorderColor = Color.FromArgb(147, 112, 147);
            btnSearch.BorderRadius = 40;
            btnSearch.BorderSize = 0;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 32, 43);
            btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 56, 75);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.White;
            btnSearch.Image = (Image)resources.GetObject("btnSearch.Image");
            btnSearch.Location = new Point(230, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(40, 40);
            btnSearch.TabIndex = 2;
            btnSearch.TextColor = Color.White;
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // logo
            // 
            logo.Dock = DockStyle.Left;
            logo.EditValue = resources.GetObject("logo.EditValue");
            logo.Location = new Point(0, 0);
            logo.Name = "logo";
            logo.Properties.AllowFocused = false;
            logo.Properties.Appearance.BackColor = Color.FromArgb(31, 42, 56);
            logo.Properties.Appearance.Options.UseBackColor = true;
            logo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            logo.Properties.ReadOnly = true;
            logo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            logo.Properties.ShowEditMenuItem = DevExpress.Utils.DefaultBoolean.False;
            logo.Properties.ShowMenu = false;
            logo.Size = new Size(210, 50);
            logo.TabIndex = 1;
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(menuTree);
            leftPanel.Controls.Add(btnFilterMenu);
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Location = new Point(0, 50);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(210, 630);
            leftPanel.TabIndex = 3;
            // 
            // menuTree
            // 
            menuTree.Appearance.Row.Font = new Font("Malgun Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuTree.Appearance.Row.Options.UseFont = true;
            menuTree.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            menuTree.Dock = DockStyle.Fill;
            menuTree.Font = new Font("Malgun Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuTree.Location = new Point(0, 38);
            menuTree.LookAndFeel.UseDefaultLookAndFeel = false;
            menuTree.Name = "menuTree";
            menuTree.OptionsBehavior.Editable = false;
            menuTree.OptionsBehavior.ReadOnly = true;
            menuTree.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            menuTree.OptionsView.RowImagesShowMode = DevExpress.XtraTreeList.RowImagesShowMode.InIndent;
            menuTree.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            menuTree.RowHeight = 40;
            menuTree.Size = new Size(210, 592);
            menuTree.StateImageList = imageCollection1;
            menuTree.TabIndex = 0;
            menuTree.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            // 
            // imageCollection1
            // 
            imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
            imageCollection1.Images.SetKeyName(0, "folder_4673908.png");
            imageCollection1.Images.SetKeyName(1, "open-folder_12075797.png");
            imageCollection1.Images.SetKeyName(2, "notes_6988438.png");
            // 
            // btnFilterMenu
            // 
            btnFilterMenu.Dock = DockStyle.Top;
            btnFilterMenu.EditValue = "";
            btnFilterMenu.Location = new Point(0, 0);
            btnFilterMenu.Name = "btnFilterMenu";
            btnFilterMenu.Properties.Appearance.BackColor = Color.FromArgb(42, 56, 75);
            btnFilterMenu.Properties.Appearance.Font = new Font("Malgun Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFilterMenu.Properties.Appearance.Options.UseBackColor = true;
            btnFilterMenu.Properties.Appearance.Options.UseFont = true;
            btnFilterMenu.Properties.AutoHeight = false;
            btnFilterMenu.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions1.Image = (Image)resources.GetObject("editorButtonImageOptions1.Image");
            btnFilterMenu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 25, false, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            btnFilterMenu.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            btnFilterMenu.Size = new Size(210, 38);
            btnFilterMenu.TabIndex = 0;
            // 
            // xtraTabbedMdiManager1
            // 
            xtraTabbedMdiManager1.AppearancePage.Header.Font = new Font("Myanmar Text", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            xtraTabbedMdiManager1.AppearancePage.Header.Options.UseFont = true;
            xtraTabbedMdiManager1.AppearancePage.HeaderActive.BackColor = Color.White;
            xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseBackColor = true;
            xtraTabbedMdiManager1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            xtraTabbedMdiManager1.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            xtraTabbedMdiManager1.FloatOnDrag = DevExpress.Utils.DefaultBoolean.True;
            xtraTabbedMdiManager1.MdiParent = this;
            xtraTabbedMdiManager1.PinPageButtonShowMode = DevExpress.XtraTab.PinPageButtonShowMode.InActiveTabPageHeader;
            xtraTabbedMdiManager1.UseDocumentSelector = DevExpress.Utils.DefaultBoolean.True;
            // 
            // mainPanel
            // 
            mainPanel.BackgroundImageLayout = ImageLayout.Stretch;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(210, 50);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(950, 630);
            mainPanel.TabIndex = 4;
            // 
            // ENOTES
            // 
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1160, 680);
            Controls.Add(mainPanel);
            Controls.Add(leftPanel);
            Controls.Add(topPanel);
            FormBorderStyle = FormBorderStyle.None;
            IsMdiContainer = true;
            Name = "ENOTES";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logo.Properties).EndInit();
            leftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)menuTree).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageCollection1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnFilterMenu.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private DevExpress.XtraEditors.PictureEdit logo;
        private CH.Framework.Win.Controls.CHRoundButton btnSearch;
        private Panel leftPanel;
        private CH.Framework.Win.Controls.CHRoundButton btnPrint;
        private CH.Framework.Win.Controls.CHRoundButton btnSave;
        private CH.Framework.Win.Controls.CHRoundButton btnDel;
        private CH.Framework.Win.Controls.CHRoundButton btnAdd;
        private CH.Framework.Win.Controls.CHRoundButton btnMinimize;
        private CH.Framework.Win.Controls.CHRoundButton btnMaximize;
        private CH.Framework.Win.Controls.CHRoundButton btnClose;
        private ToolTip myTooltip;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private Panel mainPanel;
        private DevExpress.XtraTreeList.TreeList menuTree;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.ButtonEdit btnFilterMenu;
    }
}
