namespace CH.Framework.Win.Controls
{
    partial class CHPeriodEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CHPeriodEdit));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            PickerEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            PickerControl = new DevExpress.XtraEditors.PopupContainerControl();
            dateNavigatorTo = new DevExpress.XtraScheduler.DateNavigator();
            dateNavigatorFrom = new DevExpress.XtraScheduler.DateNavigator();
            txtDtFrom = new DevExpress.XtraEditors.TextEdit();
            txtDtTo = new DevExpress.XtraEditors.TextEdit();
            Label = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)PickerEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PickerControl).BeginInit();
            PickerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dateNavigatorTo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigatorTo.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigatorFrom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigatorFrom.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDtFrom.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDtTo.Properties).BeginInit();
            SuspendLayout();
            // 
            // PickerEdit
            // 
            PickerEdit.AllowDrop = true;
            PickerEdit.Location = new System.Drawing.Point(0, 0);
            PickerEdit.Name = "PickerEdit";
            PickerEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            PickerEdit.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(194, 212, 228);
            PickerEdit.Properties.Appearance.Options.UseBackColor = true;
            PickerEdit.Properties.Appearance.Options.UseBorderColor = true;
            PickerEdit.Properties.AutoHeight = false;
            PickerEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions1.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
            PickerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            PickerEdit.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            PickerEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            PickerEdit.Properties.PopupControl = PickerControl;
            PickerEdit.Properties.PopupSizeable = false;
            PickerEdit.Properties.ShowPopupCloseButton = false;
            PickerEdit.Properties.ShowPopupShadow = false;
            PickerEdit.Size = new System.Drawing.Size(185, 24);
            PickerEdit.TabIndex = 0;
            PickerEdit.TabStop = false;
            // 
            // PickerControl
            // 
            PickerControl.Appearance.BackColor = System.Drawing.Color.White;
            PickerControl.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            PickerControl.Appearance.Options.UseBackColor = true;
            PickerControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            PickerControl.Controls.Add(dateNavigatorTo);
            PickerControl.Controls.Add(dateNavigatorFrom);
            PickerControl.Location = new System.Drawing.Point(2, 25);
            PickerControl.Margin = new System.Windows.Forms.Padding(0);
            PickerControl.MaximumSize = new System.Drawing.Size(420, 270);
            PickerControl.MinimumSize = new System.Drawing.Size(420, 270);
            PickerControl.Name = "PickerControl";
            PickerControl.Size = new System.Drawing.Size(420, 270);
            PickerControl.TabIndex = 1;
            // 
            // dateNavigatorTo
            // 
            dateNavigatorTo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            dateNavigatorTo.CalendarAppearance.DayCellSpecial.FontStyleDelta = System.Drawing.FontStyle.Bold;
            dateNavigatorTo.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            dateNavigatorTo.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateNavigatorTo.DateTime = new System.DateTime(2001, 11, 20, 0, 0, 0, 0);
            dateNavigatorTo.Dock = System.Windows.Forms.DockStyle.Right;
            dateNavigatorTo.EditValue = new System.DateTime(2001, 11, 20, 0, 0, 0, 0);
            dateNavigatorTo.FirstDayOfWeek = System.DayOfWeek.Sunday;
            dateNavigatorTo.Location = new System.Drawing.Point(208, 2);
            dateNavigatorTo.Name = "dateNavigatorTo";
            dateNavigatorTo.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Single;
            dateNavigatorTo.ShowTodayButton = false;
            dateNavigatorTo.ShowWeekNumbers = false;
            dateNavigatorTo.Size = new System.Drawing.Size(210, 266);
            dateNavigatorTo.TabIndex = 2;
            // 
            // dateNavigatorFrom
            // 
            dateNavigatorFrom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            dateNavigatorFrom.CalendarAppearance.DayCellSpecial.FontStyleDelta = System.Drawing.FontStyle.Bold;
            dateNavigatorFrom.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            dateNavigatorFrom.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateNavigatorFrom.DateTime = new System.DateTime(2046, 4, 20, 0, 0, 0, 0);
            dateNavigatorFrom.Dock = System.Windows.Forms.DockStyle.Left;
            dateNavigatorFrom.EditValue = new System.DateTime(2046, 4, 20, 0, 0, 0, 0);
            dateNavigatorFrom.FirstDayOfWeek = System.DayOfWeek.Sunday;
            dateNavigatorFrom.Location = new System.Drawing.Point(2, 2);
            dateNavigatorFrom.Name = "dateNavigatorFrom";
            dateNavigatorFrom.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Single;
            dateNavigatorFrom.ShowTodayButton = false;
            dateNavigatorFrom.ShowWeekNumbers = false;
            dateNavigatorFrom.Size = new System.Drawing.Size(210, 266);
            dateNavigatorFrom.TabIndex = 1;
            // 
            // txtDtFrom
            // 
            txtDtFrom.Location = new System.Drawing.Point(0, 0);
            txtDtFrom.Name = "txtDtFrom";
            txtDtFrom.Properties.Appearance.BackColor = System.Drawing.Color.White;
            txtDtFrom.Properties.Appearance.Options.UseBackColor = true;
            txtDtFrom.Properties.AutoHeight = false;
            txtDtFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtDtFrom.Size = new System.Drawing.Size(75, 24);
            txtDtFrom.TabIndex = 2;
            // 
            // txtDtTo
            // 
            txtDtTo.Location = new System.Drawing.Point(87, 0);
            txtDtTo.Name = "txtDtTo";
            txtDtTo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            txtDtTo.Properties.Appearance.Options.UseBackColor = true;
            txtDtTo.Properties.AutoHeight = false;
            txtDtTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtDtTo.Size = new System.Drawing.Size(75, 24);
            txtDtTo.TabIndex = 3;
            // 
            // Label
            // 
            Label.Appearance.BackColor = System.Drawing.Color.Transparent;
            Label.Appearance.Font = new System.Drawing.Font("Arial", 7F);
            Label.Appearance.Options.UseBackColor = true;
            Label.Appearance.Options.UseFont = true;
            Label.Appearance.Options.UseTextOptions = true;
            Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            Label.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            Label.Location = new System.Drawing.Point(75, 0);
            Label.Margin = new System.Windows.Forms.Padding(0);
            Label.Name = "Label";
            Label.Size = new System.Drawing.Size(12, 24);
            Label.TabIndex = 4;
            Label.Text = "~";
            Label.UseMnemonic = false;
            // 
            // CHPeriodEdit
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(PickerControl);
            Controls.Add(Label);
            Controls.Add(txtDtTo);
            Controls.Add(txtDtFrom);
            Controls.Add(PickerEdit);
            Name = "CHPeriodEdit";
            Size = new System.Drawing.Size(185, 24);
            ((System.ComponentModel.ISupportInitialize)PickerEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)PickerControl).EndInit();
            PickerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dateNavigatorTo.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigatorTo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigatorFrom.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigatorFrom).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDtFrom.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDtTo.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit PickerEdit;
        public DevExpress.XtraEditors.TextEdit txtDtFrom;
        public DevExpress.XtraEditors.TextEdit txtDtTo;
        private DevExpress.XtraEditors.LabelControl Label;
        private DevExpress.XtraEditors.PopupContainerControl PickerControl;
        private DevExpress.XtraScheduler.DateNavigator dateNavigatorTo;
        private DevExpress.XtraScheduler.DateNavigator dateNavigatorFrom;
    }
}
