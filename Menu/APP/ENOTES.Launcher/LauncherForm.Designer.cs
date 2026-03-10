namespace ENOTES.Launcher
{
    partial class LauncherForm
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
            label1 = new Label();
            lblStatus = new Label();
            flatProgressBar1 = new FlatProgressBar();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.White;
            label1.Location = new Point(25, 15);
            label1.Name = "label1";
            label1.Size = new Size(91, 30);
            label1.TabIndex = 0;
            label1.Text = "ENOTES";
            // 
            // lblStatus
            // 
            lblStatus.ForeColor = Color.FromArgb(180, 200, 220);
            lblStatus.Location = new Point(25, 55);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(300, 20);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Starting...";
            // 
            // flatProgressBar1
            // 
            flatProgressBar1.FillColor = Color.FromArgb(40, 154, 221);
            flatProgressBar1.Location = new Point(25, 80);
            flatProgressBar1.Maximum = 100;
            flatProgressBar1.Name = "flatProgressBar1";
            flatProgressBar1.Size = new Size(300, 23);
            flatProgressBar1.TabIndex = 3;
            flatProgressBar1.Text = "flatProgressBar1";
            flatProgressBar1.Value = 0;
            // 
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 42, 56);
            ClientSize = new Size(350, 125);
            Controls.Add(flatProgressBar1);
            Controls.Add(lblStatus);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LauncherForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblStatus;
        private FlatProgressBar flatProgressBar1;
    }
}
