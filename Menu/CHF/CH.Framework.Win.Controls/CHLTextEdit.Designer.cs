namespace CH.Framework.Win.Controls
{
    partial class CHLTextEdit
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CHLTextEdit));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            chLabel1 = new CHLabel();
            chTextEdit1 = new CHTextEdit();
            ((System.ComponentModel.ISupportInitialize)chTextEdit1.Properties).BeginInit();
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
            chLabel1.Size = new System.Drawing.Size(79, 23);
            chLabel1.TabIndex = 0;
            // 
            // chTextEdit1
            // 
            chTextEdit1.Location = new System.Drawing.Point(86, 0);
            chTextEdit1.Name = "chTextEdit1";
            chTextEdit1.OnSearch = false;
            chTextEdit1.Properties.AutoHeight = false;
            chTextEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions1.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
            serializableAppearanceObject1.BackColor = System.Drawing.Color.White;
            serializableAppearanceObject1.Options.UseBackColor = true;
            chTextEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            chTextEdit1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            chTextEdit1.Size = new System.Drawing.Size(196, 24);
            chTextEdit1.TabIndex = 1;
            // 
            // CHLTextEdit
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(chTextEdit1);
            Controls.Add(chLabel1);
            Name = "CHLTextEdit";
            Size = new System.Drawing.Size(280, 24);
            ((System.ComponentModel.ISupportInitialize)chTextEdit1.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CHLabel chLabel1;
        private CHTextEdit chTextEdit1;
    }
}
