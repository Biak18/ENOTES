namespace SYS
{
    partial class M_SYS_BAS_REG_001
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
            chLayoutPanel1 = new CH.Framework.Win.Controls.CHLayoutPanel();
            txt_Search = new CH.Framework.Win.Controls.CHLTextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            chGrid1 = new CH.Grid.CHGrid();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)chLayoutPanel1).BeginInit();
            chLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chGrid1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // chLayoutPanel1
            // 
            chLayoutPanel1.Controls.Add(txt_Search);
            chLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            chLayoutPanel1.Location = new System.Drawing.Point(0, 50);
            chLayoutPanel1.Name = "chLayoutPanel1";
            chLayoutPanel1.Root = Root;
            chLayoutPanel1.SetPanelType = CH.Framework.Win.Controls.CHLayoutPanel.PanelType.MAINFORM;
            chLayoutPanel1.Size = new System.Drawing.Size(1158, 50);
            chLayoutPanel1.TabIndex = 3;
            chLayoutPanel1.Text = "chLayoutPanel1";
            // 
            // txt_Search
            // 
            txt_Search.BackColor = System.Drawing.Color.FromArgb(243, 252, 251);
            txt_Search.EditValue = null;
            txt_Search.LabelText = "Search";
            txt_Search.Location = new System.Drawing.Point(20, 15);
            txt_Search.Name = "txt_Search";
            txt_Search.Size = new System.Drawing.Size(311, 24);
            txt_Search.TabIndex = 0;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, emptySpaceItem1 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 0);
            Root.Size = new System.Drawing.Size(1158, 50);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txt_Search;
            layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.MaxSize = new System.Drawing.Size(0, 35);
            layoutControlItem1.MinSize = new System.Drawing.Size(1, 34);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 5, 5);
            layoutControlItem1.Size = new System.Drawing.Size(331, 40);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(331, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(807, 40);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // chGrid1
            // 
            chGrid1.AddNewRowLastColumn = false;
            chGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            chGrid1.GridMode = "";
            chGrid1.isSaveLayout = false;
            chGrid1.isUpper = false;
            chGrid1.LayoutVersion = "";
            chGrid1.Location = new System.Drawing.Point(0, 100);
            chGrid1.MainView = gridView1;
            chGrid1.MenuID = "";
            chGrid1.Name = "chGrid1";
            chGrid1.SetBindingEvnet = true;
            chGrid1.SetGridview = null;
            chGrid1.Size = new System.Drawing.Size(1158, 579);
            chGrid1.TabIndex = 4;
            chGrid1.UserID = "";
            chGrid1.VerifyNotNull = null;
            chGrid1.VerifyNullDelete = null;
            chGrid1.VerifyPrimaryKey = null;
            chGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            chGrid1.YN_Excel = false;
            chGrid1.YN_Style = true;
            // 
            // gridView1
            // 
            gridView1.GridControl = chGrid1;
            gridView1.IndicatorWidth = 50;
            gridView1.Name = "gridView1";
            // 
            // M_SYS_BAS_REG_001
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            ClientSize = new System.Drawing.Size(1158, 679);
            Controls.Add(chGrid1);
            Controls.Add(chLayoutPanel1);
            IsTopPanelVisible = true;
            Name = "M_SYS_BAS_REG_001";
            Controls.SetChildIndex(chLayoutPanel1, 0);
            Controls.SetChildIndex(chGrid1, 0);
            ((System.ComponentModel.ISupportInitialize)chLayoutPanel1).EndInit();
            chLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chGrid1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private CH.Framework.Win.Controls.CHLayoutPanel chLayoutPanel1;
        private CH.Framework.Win.Controls.CHLTextEdit txt_Search;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private CH.Grid.CHGrid chGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}