namespace M_TEST_001
{
    partial class M_TEST_001
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
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            chTree1 = new CH.Grid.CHTree();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chTree1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.BackColor = System.Drawing.Color.FromArgb(243, 252, 251);
            layoutControl1.Controls.Add(textEdit1);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            layoutControl1.Location = new System.Drawing.Point(0, 50);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(841, 37);
            layoutControl1.TabIndex = 3;
            layoutControl1.Text = "layoutControl1";
            // 
            // textEdit1
            // 
            textEdit1.Location = new System.Drawing.Point(75, 7);
            textEdit1.Name = "textEdit1";
            textEdit1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(230, 243, 251);
            textEdit1.Properties.Appearance.Options.UseBackColor = true;
            textEdit1.Properties.AutoHeight = false;
            textEdit1.Size = new System.Drawing.Size(252, 23);
            textEdit1.StyleController = layoutControl1;
            textEdit1.TabIndex = 0;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, emptySpaceItem1 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(841, 37);
            Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = textEdit1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(324, 27);
            layoutControlItem2.Text = "Menu Name";
            layoutControlItem2.TextSize = new System.Drawing.Size(56, 13);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(324, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(507, 27);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerControl1.Location = new System.Drawing.Point(0, 87);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(chTree1);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new System.Drawing.Size(841, 402);
            splitContainerControl1.SplitterPosition = 294;
            splitContainerControl1.TabIndex = 4;
            // 
            // chTree1
            // 
            chTree1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            chTree1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            chTree1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 11F);
            chTree1.Appearance.Row.Options.UseFont = true;
            chTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            chTree1.Location = new System.Drawing.Point(0, 0);
            chTree1.Name = "chTree1";
            chTree1.OptionsBehavior.Editable = false;
            chTree1.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            chTree1.OptionsNavigation.AutoFocusNewNode = true;
            chTree1.OptionsSelection.EnableAppearanceFocusedCell = false;
            chTree1.OptionsSelection.MultiSelect = true;
            chTree1.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            chTree1.OptionsView.ShowColumns = false;
            chTree1.OptionsView.ShowHorzLines = false;
            chTree1.OptionsView.ShowIndicator = false;
            chTree1.OptionsView.ShowVertLines = false;
            chTree1.Size = new System.Drawing.Size(294, 402);
            chTree1.TabIndex = 0;
            // 
            // M_TEST_001
            // 
            Appearance.BackColor = System.Drawing.Color.FromArgb(243, 252, 251);
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(841, 489);
            Controls.Add(splitContainerControl1);
            Controls.Add(layoutControl1);
            IsTopPanelVisible = true;
            Name = "M_TEST_001";
            Text = "M_TEST_001";
            Controls.SetChildIndex(layoutControl1, 0);
            Controls.SetChildIndex(splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chTree1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private CH.Grid.CHTree chTree1;
    }
}