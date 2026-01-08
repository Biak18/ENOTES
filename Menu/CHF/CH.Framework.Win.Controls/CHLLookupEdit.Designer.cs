namespace CH.Framework.Win.Controls
{
    partial class CHLLookupEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CHLLookupEdit));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            chLabel1 = new CHLabel();
            chLookupedit1 = new CHLookupedit();
            ((System.ComponentModel.ISupportInitialize)chLookupedit1.Properties).BeginInit();
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
            // chLookupedit1
            // 
            chLookupedit1.colorBack = System.Drawing.SystemColors.Control;
            chLookupedit1.FilterColumns = null;
            chLookupedit1.IsRequired = false;
            chLookupedit1.Location = new System.Drawing.Point(86, 0);
            chLookupedit1.Name = "chLookupedit1";
            chLookupedit1.Properties.Appearance.Options.UseBackColor = true;
            chLookupedit1.Properties.AutoHeight = false;
            chLookupedit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions1.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
            chLookupedit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            chLookupedit1.Properties.NullText = "";
            chLookupedit1.Selected = false;
            chLookupedit1.Size = new System.Drawing.Size(194, 23);
            chLookupedit1.TabIndex = 1;
            chLookupedit1.TargetObjects = null;
            // 
            // CHLLookupedit
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(chLookupedit1);
            Controls.Add(chLabel1);
            Name = "CHLLookupedit";
            Size = new System.Drawing.Size(280, 24);
            ((System.ComponentModel.ISupportInitialize)chLookupedit1.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CHLabel chLabel1;
        private CHLookupedit chLookupedit1;
    }
}
