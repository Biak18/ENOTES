namespace SYS
{
    partial class M_SYS_AUT_REG_002
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
            chTreelist1 = new CH.Grid.CHTreelist();
            ((System.ComponentModel.ISupportInitialize)chTreelist1).BeginInit();
            SuspendLayout();
            // 
            // chTreelist1
            // 
            chTreelist1.Dock = System.Windows.Forms.DockStyle.Fill;
            chTreelist1.Location = new System.Drawing.Point(0, 50);
            chTreelist1.MenuID = "";
            chTreelist1.Name = "chTreelist1";
            chTreelist1.Size = new System.Drawing.Size(998, 549);
            chTreelist1.TabIndex = 0;
            chTreelist1.TreelistMode = "";
            chTreelist1.UserID = "";
            // 
            // M_SYS_AUT_REG_002
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            ClientSize = new System.Drawing.Size(998, 599);
            Controls.Add(chTreelist1);
            IsTopPanelVisible = true;
            Name = "M_SYS_AUT_REG_002";
            Controls.SetChildIndex(chTreelist1, 0);
            ((System.ComponentModel.ISupportInitialize)chTreelist1).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private CH.Grid.CHTreelist chTreelist1;
    }
}