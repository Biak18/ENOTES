namespace ENOTES.Deploy
{
    partial class DeployForm
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
            checkedListBox1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            lblStatus = new DevExpress.XtraEditors.LabelControl();
            btnBrowse = new CH.Framework.Win.Controls.CHRoundButton();
            btnUpload = new CH.Framework.Win.Controls.CHRoundButton();
            progressBar1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)checkedListBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)progressBar1.Properties).BeginInit();
            SuspendLayout();
            // 
            // checkedListBox1
            // 
            checkedListBox1.Location = new System.Drawing.Point(12, 12);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new System.Drawing.Size(411, 427);
            checkedListBox1.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblStatus.Enabled = false;
            lblStatus.Location = new System.Drawing.Point(14, 479);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(409, 26);
            lblStatus.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.BackColor = System.Drawing.Color.White;
            btnBrowse.BackgroundColor = System.Drawing.Color.White;
            btnBrowse.BorderColor = System.Drawing.Color.Silver;
            btnBrowse.BorderRadius = 32;
            btnBrowse.BorderSize = 1;
            btnBrowse.FlatAppearance.BorderSize = 0;
            btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBrowse.ForeColor = System.Drawing.Color.Black;
            btnBrowse.Location = new System.Drawing.Point(435, 15);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(147, 32);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse";
            btnBrowse.TextColor = System.Drawing.Color.Black;
            btnBrowse.UseVisualStyleBackColor = false;
            // 
            // btnUpload
            // 
            btnUpload.BackColor = System.Drawing.Color.White;
            btnUpload.BackgroundColor = System.Drawing.Color.White;
            btnUpload.BorderColor = System.Drawing.Color.Silver;
            btnUpload.BorderRadius = 32;
            btnUpload.BorderSize = 1;
            btnUpload.FlatAppearance.BorderSize = 0;
            btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpload.ForeColor = System.Drawing.Color.Black;
            btnUpload.Location = new System.Drawing.Point(435, 53);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new System.Drawing.Size(147, 32);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "Upload";
            btnUpload.TextColor = System.Drawing.Color.Black;
            btnUpload.UseVisualStyleBackColor = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(12, 447);
            progressBar1.Name = "progressBar1";
            progressBar1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            progressBar1.Properties.ShowTitle = true;
            progressBar1.Size = new System.Drawing.Size(409, 26);
            progressBar1.TabIndex = 4;
            // 
            // DeployForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(598, 529);
            Controls.Add(progressBar1);
            Controls.Add(btnUpload);
            Controls.Add(btnBrowse);
            Controls.Add(lblStatus);
            Controls.Add(checkedListBox1);
            MaximizeBox = false;
            Name = "DeployForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)checkedListBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)progressBar1.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBox1;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private CH.Framework.Win.Controls.CHRoundButton btnBrowse;
        private CH.Framework.Win.Controls.CHRoundButton btnUpload;
        private DevExpress.XtraEditors.ProgressBarControl progressBar1;
    }
}
