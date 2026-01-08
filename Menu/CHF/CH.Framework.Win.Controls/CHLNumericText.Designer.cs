namespace CH.Framework.Win.Controls
{
    partial class CHLNumericText
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chLabel1 = new CHLabel();
            chNumericText1 = new CHNumericText();
            ((System.ComponentModel.ISupportInitialize)chNumericText1.Properties).BeginInit();
            SuspendLayout();
            // 
            // chLabel1
            // 
            chLabel1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            chLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(40, 154, 221);
            chLabel1.Appearance.Options.UseFont = true;
            chLabel1.Appearance.Options.UseForeColor = true;
            chLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            chLabel1.Location = new System.Drawing.Point(0, 0);
            chLabel1.Name = "chLabel1";
            chLabel1.Size = new System.Drawing.Size(79, 24);
            chLabel1.TabIndex = 0;
            // 
            // chNumericText1
            // 
            chNumericText1.DecimalPoint = new decimal(new int[] { 0, 0, 0, 0 });
            chNumericText1.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            chNumericText1.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            chNumericText1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            chNumericText1.IsRequired = false;
            chNumericText1.Location = new System.Drawing.Point(86, 0);
            chNumericText1.MaskExpression = "";
            chNumericText1.Name = "chNumericText1";
            chNumericText1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(230, 243, 251);
            chNumericText1.Properties.Appearance.Options.UseBackColor = true;
            chNumericText1.Properties.Appearance.Options.UseTextOptions = true;
            chNumericText1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            chNumericText1.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(201, 208, 214);
            chNumericText1.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            chNumericText1.Properties.AutoHeight = false;
            chNumericText1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            chNumericText1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            chNumericText1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            chNumericText1.Properties.Mask.UseMaskAsDisplayFormat = true;
            chNumericText1.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            chNumericText1.Properties.MaskSettings.Set("mask", "n0");
            chNumericText1.Properties.NullText = "0";
            chNumericText1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            chNumericText1.SetNumericType = SetNumericType.NONE;
            chNumericText1.Size = new System.Drawing.Size(196, 24);
            chNumericText1.TabIndex = 1;
            // 
            // CHLNumericText
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(chNumericText1);
            Controls.Add(chLabel1);
            Name = "CHLNumericText";
            Size = new System.Drawing.Size(280, 24);
            ((System.ComponentModel.ISupportInitialize)chNumericText1.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CHLabel chLabel1;
        private CHNumericText chNumericText1;
    }
}
