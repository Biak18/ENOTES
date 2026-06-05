using CH.Helper;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Versioning;
using System.Text;
using System.Windows.Forms;

namespace CH.Grid;

[SupportedOSPlatform("windows")]
public class CHTreelist : TreeList
{
    private string _path = AppDomain.CurrentDomain.BaseDirectory + "ColorSetting.ini";

    private string _EditableColumn;

    private string _OddRow;

    private string _EvenRow;

    private string _FocusedCell;

    private string _SelectedRow;

    private string _GroupRow;

    private string _FocusedRow;

    private string _FooterPanel;

    private string _RowFont;

    private string _RowFontSize;

    private int _seq = 1;

    private string _MenuID = string.Empty;

    private string _UserID = string.Empty;

    private string _TreelistMode = "";

    private bool ChkEvent = false;

    private IContainer components = null;

    private TreeList aTreeList;

    [Category("ENOTES")]
    [Description("Treelist order")]
    [DefaultValue(1)]
    public int SEQ
    {
        get
        {
            return _seq;
        }
        set
        {
            _seq = value;
        }
    }

    [Category("ENOTES")]
    [Browsable(false)]
    [Description("MenuCode")]
    public string MenuID
    {
        get
        {
            return _MenuID;
        }
        set
        {
            _MenuID = value;
        }
    }

    [Category("ENOTES")]
    [Browsable(false)]
    [Description("User ID")]
    public string UserID
    {
        get
        {
            return _UserID;
        }
        set
        {
            _UserID = value;
        }
    }

    public string TreelistMode
    {
        get
        {
            return _TreelistMode;
        }
        set
        {
            _TreelistMode = value;
        }
    }

    public CHTreelist()
    {
        InitializeComponent();
    }

    private void InitEvent()
    {
        ChkEvent = true;
        object obj = base.DataSource;
        int num = 0;
        Graphics graphics = Graphics.FromHwnd(base.Handle);
        string name = obj.GetType().Name;
        string text = name;
        if (!(text == "DataTable"))
        {
            if (text == "DataSet")
            {
                DataSet dataSet = base.DataSource as DataSet;
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    num = dataSet.Tables[i].Rows.Count;
                    IndicatorWidth = Convert.ToInt32(((num <= 9) ? graphics.MeasureString("No.", base.ViewInfo.PaintAppearance.Row.GetFont()) : graphics.MeasureString(num.ToString(), base.ViewInfo.PaintAppearance.Row.GetFont())).Width + 1.5f) + GridPainter.Indicator.ImageSize.Width + 20;
                }
            }
        }
        else
        {
            DataTable dataTable = base.DataSource as DataTable;
            num = dataTable.Rows.Count;
            IndicatorWidth = Convert.ToInt32(((num <= 9) ? graphics.MeasureString("No.", base.ViewInfo.PaintAppearance.Row.GetFont()) : graphics.MeasureString(num.ToString(), base.ViewInfo.PaintAppearance.Row.GetFont())).Width + 1.5f) + GridPainter.Indicator.ImageSize.Width + 20;
        }

        _EditableColumn = IniFile.IniReadValue("Color", "EditableColumn", _path);
        if (_EditableColumn == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "EditableColumn", "#696969", _path);
            _EditableColumn = IniFile.IniReadValue("Color", "EditableColumn", _path);
        }

        _OddRow = IniFile.IniReadValue("Color", "OddRow", _path);
        if (_OddRow == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "OddRow", "#f7fbfe", _path);
            _OddRow = IniFile.IniReadValue("Color", "OddRow", _path);
        }

        _EvenRow = IniFile.IniReadValue("Color", "EvenRow", _path);
        if (_EvenRow == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "EvenRow", "#ffffff", _path);
            _EvenRow = IniFile.IniReadValue("Color", "EvenRow", _path);
        }

        _FocusedCell = IniFile.IniReadValue("Color", "FocusedCell", _path);
        if (_FocusedCell == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "FocusedCell", "#96edf6", _path);
            _FocusedCell = IniFile.IniReadValue("Color", "FocusedCell", _path);
        }

        _SelectedRow = IniFile.IniReadValue("Color", "SelectedRow", _path);
        if (_SelectedRow == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "SelectedRow", "#ddfcff", _path);
            _SelectedRow = IniFile.IniReadValue("Color", "SelectedRow", _path);
        }

        _GroupRow = IniFile.IniReadValue("Color", "GroupRow", _path);
        if (_GroupRow == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "GroupRow", "#84e7e4", _path);
            _GroupRow = IniFile.IniReadValue("Color", "GroupRow", _path);
        }

        _FocusedRow = IniFile.IniReadValue("Color", "FocusedRow", _path);
        if (_FocusedRow == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "FocusedRow", "#ddfcff", _path);
            _FocusedRow = IniFile.IniReadValue("Color", "FocusedRow", _path);
        }

        _FooterPanel = IniFile.IniReadValue("Color", "FooterPanel", _path);
        if (_FooterPanel == string.Empty)
        {
            IniFile.IniWriteSingle("Color", "FooterPanel", "#fffdee", _path);
            _FooterPanel = IniFile.IniReadValue("Color", "FooterPanel", _path);
        }

        _RowFont = IniFile.IniReadValue("Font", "Font", _path);
        if (_RowFont == string.Empty)
        {
            IniFile.IniWriteSingle("Font", "Font", "맑은 고딕", _path);
            _RowFont = IniFile.IniReadValue("Font", "Font", _path);
        }

        _RowFontSize = IniFile.IniReadValue("Size", "Size", _path);
        if (_RowFontSize == string.Empty)
        {
            IniFile.IniWriteSingle("Size", "Size", "12", _path);
            _RowFontSize = IniFile.IniReadValue("Size", "Size", _path);
        }

        base.OptionsView.EnableAppearanceEvenRow = true;
        base.OptionsView.EnableAppearanceOddRow = true;
        base.OptionsBehavior.ResizeNodes = false;
        base.Appearance.HideSelectionRow.BackColor = ColorTranslator.FromHtml(_FocusedRow);
        base.Appearance.HideSelectionRow.Options.UseBackColor = true;
        base.Appearance.Row.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
        base.Appearance.Row.ForeColor = ColorTranslator.FromHtml(_EditableColumn);
        base.Appearance.Row.BorderColor = Color.FromArgb(227, 227, 227);
        base.Appearance.HeaderPanel.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
        base.Appearance.HeaderPanel.ForeColor = Color.FromArgb(38, 143, 205);
        base.Appearance.HeaderPanel.BorderColor = Color.FromArgb(227, 227, 227);
        base.Appearance.HeaderPanel.Options.UseBackColor = true;
        base.Appearance.HeaderPanel.BackColor = Color.FromArgb(0, 0, 0);
        base.Appearance.OddRow.BackColor = ColorTranslator.FromHtml(_OddRow);
        base.Appearance.EvenRow.BackColor = ColorTranslator.FromHtml(_EvenRow);
        base.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(_FocusedRow);
        base.Appearance.FocusedCell.BackColor = ColorTranslator.FromHtml(_FocusedCell);
        base.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml(_SelectedRow);
        base.Appearance.FooterPanel.BackColor = ColorTranslator.FromHtml(_FooterPanel);
        base.CustomDrawNodeIndicator += aTreelist_CustomDrawNodeIndicator;
        base.CustomDrawColumnHeader += aTreelist_CustomDrawColumnHeader;
        base.CustomDrawFooter += aTreelist_CustomDrawFooter;
    }

    protected override void InitLayout()
    {
        base.DataSourceChanged += aTreelist_DataSourceChanged;
        base.MouseWheel += ATreelist_MouseWheel;
        base.MouseDown += ATreelist_MouseDown;
        base.KeyDown += ATreelist_KeyDown;
        base.InitLayout();
    }

    private void aTreelist_DataSourceChanged(object sender, EventArgs e)
    {
        if (!ChkEvent)
        {
            InitEvent();
        }
    }

    private void aTreelist_CustomDrawColumnHeader(object sender, CustomDrawColumnHeaderEventArgs e)
    {
        if (DesignMode) return;
        CHTreelist treelist = sender as CHTreelist;
        if (treelist == null) return;

        e.Handled = true;

        Color headerBackColor = Color.White;
        Color captionColor = Color.FromArgb(38, 143, 205);
        Color accentColor = Color.FromArgb(32, 130, 188);

        using (var font = new Font(_RowFont ?? "맑은 고딕", Convert.ToSingle(_RowFontSize ?? "12"), GraphicsUnit.Pixel))
        using (var captionBrush = new SolidBrush(captionColor))
        using (var accentBrush = new SolidBrush(accentColor))
        using (var backBrush = new SolidBrush(headerBackColor))
        {
            var pen = treelist.ViewInfo.PaintAppearance.HorzLine.GetBackPen(e.Cache);

            // ("No.")
            if (e.ColumnType == HitInfoType.ColumnButton)
            {
                e.Cache.FillRectangle(backBrush, e.Bounds);

                var innerBounds = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width - 4, e.Bounds.Height - 4);
                e.Cache.FillRectangle(backBrush, innerBounds);

                e.Graphics.DrawRectangle(pen, new Rectangle(e.Bounds.X, e.Bounds.Y - (int)pen.Width, e.Bounds.Width - 1, e.Bounds.Height));

                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.DrawString("No.", font, captionBrush, e.Bounds, sf);
                }

                e.Graphics.FillRectangle(accentBrush, new Rectangle(e.Bounds.X, e.Bounds.Y, 5, e.Bounds.Height));
                return;
            }

            // column header blocks
            e.Cache.FillRectangle(backBrush, e.Bounds);
            e.Graphics.DrawRectangle(pen, new Rectangle(e.Bounds.X - (int)pen.Width, e.Bounds.Y - (int)pen.Width, e.Bounds.Width, e.Bounds.Height));

            if (e.Column == null) return;

            // header text caption 
            using (var sf = new StringFormat
            {
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.EllipsisCharacter,
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                e.Graphics.DrawString(e.Column.Caption, font, captionBrush, e.Info.CaptionRect, sf);
            }

            e.Info.InnerElements.DrawObjects(e.Info, e.Cache, Point.Empty);
        }
    }

    private void aTreelist_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
    {
        if (DesignMode) return;
        CHTreelist treeList = sender as CHTreelist;
        if (treeList == null) return;

        if (e.Info.IsRowIndicator && e.Node != null)
        {
            e.Handled = true;

            var font = new Font(_RowFont ?? "맑은 고딕", Convert.ToSingle(_RowFontSize ?? "12"), GraphicsUnit.Pixel);
            var pen = treeList.ViewInfo.PaintAppearance.HorzLine.GetBackPen(e.Cache);

            int visibleIndex = treeList.GetVisibleIndexByNode(e.Node);
            string displayText = visibleIndex >= 0 ? (visibleIndex + 1).ToString() : "";

            Size textSize = TextRenderer.MeasureText(displayText, font);
            e.Appearance.ForeColor = Color.FromArgb(105, 105, 105);
            e.Appearance.BackColor = Color.White;

            e.Appearance.FillRectangle(e.Cache, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width - 4, e.Bounds.Height - 4));

            Rectangle textRect = (treeList.FocusedNode == e.Node) ? new Rectangle(e.Bounds.X + 5, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height) : e.Bounds;
            e.Graphics.DrawRectangle(pen, new Rectangle(e.Bounds.X, e.Bounds.Y - (int)pen.Width, e.Bounds.Width - 1, e.Bounds.Height));
            e.Graphics.DrawString(displayText, font, new SolidBrush(Color.FromArgb(105, 105, 105)), textRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            Size iconSize = ImageCollection.GetImageListSize(e.Info.ImageCollection);
            Rectangle iconRect = new Rectangle(
                e.Bounds.X + (e.Bounds.Width - (iconSize.Width + 20) - (textSize.Width)) / 2 + 5,
                e.Bounds.Y + (e.Bounds.Height - iconSize.Height) / 2,
                iconSize.Width,
                iconSize.Height
            );

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 130, 188)), new Rectangle(e.Bounds.X, e.Bounds.Y - 1, 5, e.Bounds.Height));
            ImageCollection.DrawImageListImage(e.Cache, e.Info.ImageCollection, e.Info.ImageIndex, iconRect);
        }
    }

    private void aTreelist_CustomDrawFooter(object sender, CustomDrawEventArgs e)
    {
        e.Painter.DrawObject(e.ObjectArgs);
        TreeList treeList = sender as TreeList;
        Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, treeList.IndicatorWidth, e.Bounds.Height);
        e.Cache.DrawRectangle(e.Cache.GetPen(Color.LightGray), rectangle);
        rectangle.Width -= 8;
        e.Cache.DrawString($"{treeList.VisibleNodesCount}", e.Appearance.GetFont(), e.Appearance.GetForeBrush(e.Cache), rectangle, new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center
        });
        e.Handled = true;
    }

    private void ATreelist_MouseWheel(object sender, MouseEventArgs e)
    {
        if (Control.ModifierKeys != Keys.Control)
        {
            return;
        }

        if (e.Delta > 0)
        {
            if (Appearance.FilterPanel.FontSizeDelta < 20)
            {
                Appearance.FilterPanel.FontSizeDelta++;
                Appearance.Row.FontSizeDelta++;
                Appearance.HeaderPanel.FontSizeDelta++;
            }
        }
        else if (Appearance.FilterPanel.FontSizeDelta > -5)
        {
            Appearance.FilterPanel.FontSizeDelta--;
            Appearance.Row.FontSizeDelta--;
            Appearance.HeaderPanel.FontSizeDelta--;
        }
    }

    private void ATreelist_MouseDown(object sender, MouseEventArgs e)
    {
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        dataTable2 = base.DataSource as DataTable;
        dataTable.Columns.Add("Name");
        dataTable.Columns.Add("Field");
        dataTable.Columns.Add("Type");
        dataTable.Columns.Add("Vislble");
        dataTable.Columns.Add("Editable");
        foreach (TreeListColumn column in Columns)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["Name"] = column.Caption;
            dataRow["Field"] = column.FieldName;
            dataRow["Type"] = column.ColumnType;
            dataRow["Vislble"] = column.Visible.ToString();
            dataRow["Editable"] = column.OptionsColumn.AllowEdit.ToString();
            dataTable.Rows.Add(dataRow);
        }

        //if (e.Button == MouseButtons.Right && Control.ModifierKeys == (Keys.Shift | Keys.Control))
        //{
        //    TreelistInfo treelistInfo = new TreelistInfo(dataTable, dataTable2);
        //    treelistInfo.ShowDialog();
        //}
    }

    private void ATreelist_KeyDown(object sender, KeyEventArgs e)
    {
        CHTreelist aTreelist2 = sender as CHTreelist;
        if (!e.Control || e.KeyCode != Keys.C)
        {
            return;
        }

        aTreelist2.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.True;
        aTreelist2.CopyToClipboard();
        aTreelist2.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
        e.Handled = true;
        e.SuppressKeyPress = true;
        string text = Clipboard.GetText();
        string[] array = text.Split("\r\n");
        StringBuilder stringBuilder = new StringBuilder();
        if (array != null)
        {
            foreach (TreeListColumn column in aTreelist2.Columns)
            {
                if (column.Visible)
                {
                    array[0] = array[0].Replace(column.Caption, column.FieldName);
                }
            }

            int num = 0;
            string[] array2 = array;
            foreach (string value in array2)
            {
                stringBuilder.Append(value);
                num++;
                if (num != array.Length)
                {
                    stringBuilder.Append("\r\n");
                }
            }
        }

        Clipboard.SetText(stringBuilder.ToString());
        stringBuilder = null;
    }

    public void Binding(object dataSource)
    {
        base.DataSource = dataSource;
    }
    private DataTable _dt = new DataTable();

    public DataTable GetChanges()
    {
        base.PostEditor();
        base.EndCurrentEdit();

        _dt = base.DataSource as DataTable;

        if (_dt == null) return null;

        return _dt.GetChanges();
    }

    public void AcceptChanges()
    {
        if (base.DataSource is DataTable activeTable)
        {
            activeTable.AcceptChanges();
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.aTreeList = new DevExpress.XtraTreeList.TreeList();
        ((System.ComponentModel.ISupportInitialize)this).BeginInit();
        base.SuspendLayout();
        base.Location = new System.Drawing.Point(0, 0);
        base.Name = "aTree1";
        base.Size = new System.Drawing.Size(200, 400);
        base.TabIndex = 0;
        ((System.ComponentModel.ISupportInitialize)this).EndInit();
        base.ResumeLayout(false);
    }
}
