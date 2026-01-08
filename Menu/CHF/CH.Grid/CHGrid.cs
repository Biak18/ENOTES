using CH.Helper;
using DevExpress.Export;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CH.Grid;

[SupportedOSPlatform("windows")]
public partial class CHGrid : GridControl
{
    private SetControlBinding _binding = null;
    private string[] _disabledEditingColumns = new string[] { };
    private bool _setEvnet = true;
    private bool _isSelect = false;
    BaseEdit editor;
    public delegate void aGridRowNoHandler();
    private bool _doubleClick = false;
    private bool _keydown = false;
    private bool _sort = true;
    private string _path = AppDomain.CurrentDomain.BaseDirectory + @"ColorSetting.ini";
    private string _EditableColumn;
    private string _UneditableColumn;
    private string _OddRow;
    private string _EvenRow;
    private string _FocusedCell;
    private string _SelectedRow;
    private string _GroupRow;
    private string _GroupRow0;
    private string _GroupRow1;
    private string _GroupRow2;
    private string _FocusedRow;
    private string _FooterPanel;
    private string _RowFont;
    private string _RowFontSize;
    int _FocusedRowPrevhandle;
    private IniFile _inifile = new IniFile();
    #region property
    private bool _isUpper = false;
    private GridView gridView1;

    public CHGrid()
    {
        InitializeComponent();
    }

    public GridView SetGridview
    {
        get
        {
            return this.gridView1;
        }
        set
        {
            this.gridView1 = value;
        }

    }


    [Category("ENOTES"), Browsable(true), Description("Chracter CasCading")]
    public bool isUpper
    {
        get { return _isUpper; }
        set
        {
            _isUpper = value;
        }
    }


    [Category("ENOTES"), Browsable(true), Description("Layout Saving")]
    public bool isSaveLayout { get; set; } = false;

    [Category("ENOTES"), Browsable(true), Description("Layout Version")]
    private string _LayoutVersion = string.Empty;
    public string LayoutVersion
    {
        get
        {
            GridView view = this.MainView as GridView;
            if (view != null)
            {
                view.OptionsLayout.LayoutVersion = _LayoutVersion;
            }
            return _LayoutVersion;
        }
        set
        {
            GridView view = this.MainView as GridView;
            if (view != null)
            {
                _LayoutVersion = value;
                view.OptionsLayout.LayoutVersion = _LayoutVersion;
            }
        }
    }

    [Category("ENOTES"), Browsable(true), Description("When last column, Press Enter key new row")]
    /// <summary>
    /// Press the 'Enter' key in the last column to add a new row
    /// </summary>
    public bool AddNewRowLastColumn { get; set; } = false;

    private int _seq = 1;

    [Category("ENOTES"), Description("Grid order"), DefaultValue(1)]
    public int SEQ
    {
        get { return _seq; }
        set { _seq = value; }
    }


    private string _MenuID = string.Empty;
    /// <summary>
    /// Returns the checked status as a "Y" or "N" value.
    /// </summary>
    [Category("ENOTES"), Browsable(false), Description("MenuCode")]
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

    private string _UserID = string.Empty;
    /// <summary>
    /// Returns the checked status as a "Y" or "N" value.
    /// </summary>
    [Category("ENOTES"), Browsable(false), Description("User ID")]
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

    private bool _YN_Style = true;
    /// <summary>
    /// Return apply config flag 
    /// </summary>
    [Category("ENOTES"), Browsable(true), Description("YN_Style")]
    public bool YN_Style
    {
        get
        {
            return _YN_Style;
        }
        set
        {
            _YN_Style = value;
        }
    }


    private bool _YN_Excel = false;
    /// <summary>
    /// Flag for Excel upload 
    /// </summary>
    [Category("ENOTES"), Browsable(true), Description("YN_Excel")]
    public bool YN_Excel
    {
        get
        {
            return _YN_Excel;
        }
        set
        {
            _YN_Excel = value;
        }
    }



    #region Verify Column
    [Browsable(false), Description("Set the Primary Key on the grid.")]
    public string[] VerifyPrimaryKey { get; set; } = null;

    [Browsable(false), Description("Sets a multiple column that cannot be empty or zero. It will be applied when saved.")]
    public string[] VerifyNotNull { get; set; } = null;

    [Browsable(false), Description("If the grid has a null column value, delete the corresponding row. It will be applied when saved.")]
    public string[] VerifyNullDelete { get; set; } = null;
    #endregion Verify Column

    public bool SetBindingEvnet
    {
        get { return _setEvnet; }
        set { _setEvnet = value; }
    }

    /// <summary>
    /// Get the old value of the active cell. Can only be obtained in CellValueChange.
    /// </summary>
    public string OldValue
    {
        get { return _oldValue; }
    }
    /// <summary>
    /// Get the new value of the active cell. Can only be obtained in CellValueChange.
    /// </summary>
    public string NewValue
    {
        get { return _newValue; }
    }

    private string _GridMode = "";
    public string GridMode
    {
        get
        {
            return _GridMode;
        }
        set
        {
            _GridMode = value;
        }
    }



    #endregion 

    #region  CapsLock 
    [DllImport("User32.dll")]
    public static extern void keybd_event(
      byte bVk, // virtual-key code 
      byte bScan, // hardware scan code 
      int dwFlags, // function options 
      ref int dwExtraInfo // additional keystroke data 
     );


    [DllImport("user32.dll")]
    private static extern short GetKeyState(int keyCode);

    private void PressKey(byte _Key)
    {
        const int KEYUP = 0x0002;
        int Info = 0;

        keybd_event(_Key, 0, 0, ref Info);   // key down
        keybd_event(_Key, 0, KEYUP, ref Info);  // key up

    }
    #endregion

    #region Event 
    bool ChkEvent = false;
    private void InitEvent()
    {
        ChkEvent = true;
        GridView gridView = base.FocusedView as GridView;
        gridView.OptionsBehavior.EditorShowMode = EditorShowMode.Click;
        gridView.OptionsBehavior.FocusLeaveOnTab = true;
        gridView.OptionsBehavior.AutoSelectAllInEditor = true;
        gridView.OptionsBehavior.ImmediateUpdateRowPosition = false;
        gridView.OptionsNavigation.AutoFocusNewRow = true;
        gridView.OptionsNavigation.AutoMoveRowFocus = true;
        gridView.OptionsNavigation.UseTabKey = true;
        gridView.OptionsNavigation.UseOfficePageNavigation = true;
        gridView.OptionsView.RowAutoHeight = true;
        object obj = DataSource;
        int num = 0;
        Graphics graphics = Graphics.FromHwnd(gridView.GridControl.Handle);
        string name = obj.GetType().Name;
        string text = name;
        if (!(text == "DataTable"))
        {
            if (text == "DataSet")
            {
                DataSet dataSet = DataSource as DataSet;
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    num = dataSet.Tables[i].Rows.Count;
                    gridView.IndicatorWidth = Convert.ToInt32(((num <= 9) ? graphics.MeasureString("No.", gridView.PaintAppearance.Row.GetFont()) : graphics.MeasureString(num.ToString(), gridView.PaintAppearance.Row.GetFont())).Width + 1.5f) + GridPainter.Indicator.ImageSize.Width + 20;
                }
            }
        }
        else
        {
            DataTable dataTable = DataSource as DataTable;
            num = dataTable.Rows.Count;
            gridView.IndicatorWidth = Convert.ToInt32(((num <= 9) ? graphics.MeasureString("No.", gridView.PaintAppearance.Row.GetFont()) : graphics.MeasureString(num.ToString(), gridView.PaintAppearance.Row.GetFont())).Width + 1.5f) + GridPainter.Indicator.ImageSize.Width + 20;
        }

        _EditableColumn = _inifile.IniReadValue("Color", "EditableColumn", _path);
        if (_EditableColumn == string.Empty)
        {
            _inifile.IniWriteValue("Color", "EditableColumn", "#696969", _path);
            _EditableColumn = _inifile.IniReadValue("Color", "EditableColumn", _path);
        }

        _UneditableColumn = _inifile.IniReadValue("Color", "UneditableColumn", _path);
        if (_UneditableColumn == string.Empty)
        {
            _inifile.IniWriteValue("Color", "UneditableColumn", "#808080", _path);
            _UneditableColumn = _inifile.IniReadValue("Color", "UneditableColumn", _path);
        }

        _OddRow = _inifile.IniReadValue("Color", "OddRow", _path);
        if (_OddRow == string.Empty)
        {
            _inifile.IniWriteValue("Color", "OddRow", "#f7fbfe", _path);
            _OddRow = _inifile.IniReadValue("Color", "OddRow", _path);
        }

        _EvenRow = _inifile.IniReadValue("Color", "EvenRow", _path);
        if (_EvenRow == string.Empty)
        {
            _inifile.IniWriteValue("Color", "EvenRow", "#ffffff", _path);
            _EvenRow = _inifile.IniReadValue("Color", "EvenRow", _path);
        }

        _FocusedCell = _inifile.IniReadValue("Color", "FocusedCell", _path);
        if (_FocusedCell == string.Empty)
        {
            _inifile.IniWriteValue("Color", "FocusedCell", "#96edf6", _path);
            _FocusedCell = _inifile.IniReadValue("Color", "FocusedCell", _path);
        }

        _SelectedRow = _inifile.IniReadValue("Color", "SelectedRow", _path);
        if (_SelectedRow == string.Empty)
        {
            _inifile.IniWriteValue("Color", "SelectedRow", "#ddfcff", _path);
            _SelectedRow = _inifile.IniReadValue("Color", "SelectedRow", _path);
        }

        _GroupRow0 = _inifile.IniReadValue("Color", "GroupRow0", _path);
        if (_GroupRow0 == string.Empty)
        {
            _inifile.IniWriteValue("Color", "GroupRow0", "#84e7e4", _path);
            _GroupRow0 = _inifile.IniReadValue("Color", "GroupRow0", _path);
        }

        _GroupRow1 = _inifile.IniReadValue("Color", "GroupRow1", _path);
        if (_GroupRow1 == string.Empty)
        {
            _inifile.IniWriteValue("Color", "GroupRow1", "#a8eeec", _path);
            _GroupRow1 = _inifile.IniReadValue("Color", "GroupRow1", _path);
        }

        _GroupRow2 = _inifile.IniReadValue("Color", "GroupRow2", _path);
        if (_GroupRow2 == string.Empty)
        {
            _inifile.IniWriteValue("Color", "GroupRow2", "#cdf5f4", _path);
            _GroupRow2 = _inifile.IniReadValue("Color", "GroupRow2", _path);
        }

        _FocusedRow = _inifile.IniReadValue("Color", "FocusedRow", _path);
        if (_FocusedRow == string.Empty)
        {
            _inifile.IniWriteValue("Color", "FocusedRow", "#ddfcff", _path);
            _FocusedRow = _inifile.IniReadValue("Color", "FocusedRow", _path);
        }

        _FooterPanel = _inifile.IniReadValue("Color", "FooterPanel", _path);
        if (_FooterPanel == string.Empty)
        {
            _inifile.IniWriteValue("Color", "FooterPanel", "#fffdee", _path);
            _FooterPanel = _inifile.IniReadValue("Color", "FooterPanel", _path);
        }

        _RowFont = _inifile.IniReadValue("Font", "Font", _path);
        if (_RowFont == string.Empty)
        {
            _inifile.IniWriteValue("Font", "Font", "맑은 고딕", _path);
            _RowFont = _inifile.IniReadValue("Font", "Font", _path);
        }

        _RowFontSize = _inifile.IniReadValue("Size", "Size", _path);
        if (_RowFontSize == string.Empty)
        {
            _inifile.IniWriteValue("Size", "Size", "12", _path);
            _RowFontSize = _inifile.IniReadValue("Size", "Size", _path);
        }

        ViewRepositoryCollection viewCollection = base.ViewCollection;
        foreach (GridView item in viewCollection)
        {
            item.OptionsBehavior.ImmediateUpdateRowPosition = false;
            item.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            item.RowCountChanged += gridView_RowCountChanged;
            item.RowCellStyle += View_RowCellStyle;
            item.OptionsView.EnableAppearanceEvenRow = true;
            item.OptionsView.EnableAppearanceOddRow = true;
            item.Appearance.Row.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
            item.Appearance.Row.ForeColor = ColorTranslator.FromHtml(_EditableColumn);
            item.Appearance.Row.BorderColor = Color.FromArgb(227, 227, 227);
            item.Appearance.HeaderPanel.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
            item.Appearance.HeaderPanel.ForeColor = Color.FromArgb(38, 143, 205);
            item.Appearance.HeaderPanel.BorderColor = Color.FromArgb(227, 227, 227);
            item.Appearance.HeaderPanel.Options.UseBackColor = true;
            item.Appearance.HeaderPanel.BackColor = Color.FromArgb(0, 0, 0);
            item.Appearance.OddRow.BackColor = ColorTranslator.FromHtml(_OddRow);
            item.Appearance.EvenRow.BackColor = ColorTranslator.FromHtml(_EvenRow);
            item.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(_FocusedRow);
            item.Appearance.FocusedCell.BackColor = ColorTranslator.FromHtml(_FocusedCell);
            item.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml(_SelectedRow);
            item.Appearance.FooterPanel.BackColor = ColorTranslator.FromHtml(_FooterPanel);
            item.Appearance.GroupRow.BackColor = ColorTranslator.FromHtml(_GroupRow0);
            if (item.GetType() == typeof(BandedGridView))
            {
                BandedGridView bandedGridView = item as BandedGridView;
                bandedGridView.Appearance.BandPanel.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
                bandedGridView.Appearance.BandPanel.ForeColor = Color.FromArgb(38, 143, 205);
                bandedGridView.Appearance.BandPanel.BorderColor = Color.FromArgb(227, 227, 227);
                bandedGridView.Appearance.BandPanel.Options.UseBackColor = true;
                bandedGridView.Appearance.BandPanel.BackColor = Color.FromArgb(0, 0, 0);
                bandedGridView.Appearance.GroupRow.BackColor = ColorTranslator.FromHtml(_GroupRow0);
                bandedGridView.OptionsBehavior.ImmediateUpdateRowPosition = false;
            }
        }

        gridView.CellValueChanging += View_CellValueChanging;
        gridView.CellValueChanged += View_CellValueChanged;
        gridView.ColumnPositionChanged += View_ColumnPositionChanged;
        gridView.FocusedColumnChanged += View_FocusedColumnChanged;
        gridView.EndSorting += View_EndSorting;
        gridView.KeyDown += View_KeyDown;
        gridView.ShownEditor += View_ShownEditor;
        gridView.MouseDown += View_MouseDown;
        gridView.ValidateRow += View_ValidateRow;
        gridView.InitNewRow += View_InitNewRow;
        gridView.RowDeleting += View_RowDeleting;
        gridView.PopupMenuShowing += View_PopupMenuShowing;
        gridView.CustomDrawFooter += View_CustmDrawFooter;
        gridView.CustomColumnGroup += View_CustomColumnGroup;
        gridView.DragObjectOver += View_DragObjectOver;
        gridView.FocusedRowChanged += View_FocusedRowChanged;
        gridView.ShowingEditor += View_ShowingEditor;
        gridView.CustomDrawColumnHeader += View_CustomDrawColumnHeader;
    }

    private void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
    {
        if (e.Column == null) return;
        GridView gridView = sender as GridView;
        var font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
        var pen = gridView.PaintAppearance.HorzLine.GetBackPen(e.Cache);
        StringFormat stringFormat = new StringFormat();
        stringFormat.FormatFlags = StringFormatFlags.NoWrap;
        stringFormat.Trimming = StringTrimming.EllipsisCharacter;
        stringFormat.Alignment = StringAlignment.Center;
        if (VerifyNotNull != null && Array.IndexOf(VerifyNotNull, e.Column.FieldName) >= 0)
        {
            bool flag = false;
            e.DefaultDraw();
            if (e.Appearance.ForeColor != Color.White)
            {
                flag = true;
            }

            e.Appearance.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), FontStyle.Bold, GraphicsUnit.Pixel);

            e.Appearance.DrawString(e.Cache, e.Column.Caption, e.Info.CaptionRect, new SolidBrush(Color.FromArgb(38, 143, 205)), stringFormat);
            e.Info.InnerElements.DrawObjects(e.Info, e.Info.Cache, Point.Empty);
            e.Handled = true;
            if (flag)
            {
                e.Appearance.ForeColor = Color.White;
                gridView.InvalidateColumnHeader(e.Column);
            }
        }
        else
        {
            e.Info.AllowColoring = true;
            e.Graphics.DrawRectangle(pen, new Rectangle(e.Bounds.X - (int)pen.Width, e.Bounds.Y - (int)pen.Width, e.Bounds.Width, e.Bounds.Height));//border
            e.Appearance.DrawString(e.Cache, e.Column.Caption, e.Info.CaptionRect, new SolidBrush(Color.FromArgb(38, 143, 205)), stringFormat);
            e.Handled = true;
        }

    }


    private void View_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
    {
        GridView gridView = sender as GridView;
        if (e.MenuType == GridMenuType.Column)
        {
            GridViewColumnMenu gridViewColumnMenu = e.Menu as GridViewColumnMenu;
            //gridViewColumnMenu.Items.Clear();
            if (gridViewColumnMenu.Column != null)
            {
                gridViewColumnMenu.Items.Add(CreateItem("Full Excel", gridView, gridViewColumnMenu.Column, "FullExcel", null, grouping: true));
                gridViewColumnMenu.Items.Add(CreateItem("Selected row Excel", gridView, gridViewColumnMenu.Column, "SelectedRowExcel", null, grouping: false));
                return;
            }

            GridViewBandMenu gridViewBandMenu = e.Menu as GridViewBandMenu;
            gridViewBandMenu?.Items?.Clear();
            if (gridViewBandMenu.Band.Columns.Count > 0)
            {
                if (gridViewBandMenu.Band.Columns[0] != null)
                {
                    gridViewBandMenu.Items.Add(CreateItem("Full Excel", gridView, gridViewBandMenu.Band.Columns[0], "FullExcel", null, grouping: true));
                    gridViewBandMenu.Items.Add(CreateItem("Selected row Excel", gridView, gridViewBandMenu.Band.Columns[0], "SelectedRowExcel", null, grouping: false));
                }
            }
            else
            {
                gridViewBandMenu.Items.Add(CreateItem("Full Excel", gridView, null, "FullExcel", null, grouping: true));
                gridViewBandMenu.Items.Add(CreateItem("Selected row Excel", gridView, null, "SelectedRowExcel", null, grouping: false));
            }
        }
        else if (e.MenuType == GridMenuType.Row)
        {

        }
        else if (e.MenuType == GridMenuType.Group)
        {

        }
    }

    private DXMenuItem CreateItem(string caption, GridView view, GridColumn column, string Id, Image image, bool grouping)
    {
        DXMenuItem dXMenuItem = new DXMenuItem(caption, OnItemClick);
        dXMenuItem.BeginGroup = grouping;
        dXMenuItem.Tag = new MenuInfo(view, column, Id);
        return dXMenuItem;
    }

    public class MenuInfo
    {
        public GridView View;

        public GridColumn Column;

        public FixedStyle Style;

        public GridStringId GridStringId;

        public string Id;

        public int No_line_menu;

        public string MenuId;

        public string CD_FUNCTION;

        public int NO_SEQ_DW;

        public string Target;

        public Control Control;

        public string Ynexc;

        public string CD_POPMENU;

        public Band Band;

        public MenuInfo(GridView view, GridColumn column, FixedStyle style)
        {
            View = view;
            Column = column;
            Style = style;
        }

        public MenuInfo(GridView view, GridColumn column, GridStringId gridStringId)
        {
            View = view;
            Column = column;
            GridStringId = gridStringId;
        }

        public MenuInfo(GridView view, GridColumn column, string id)
        {
            View = view;
            Column = column;
            Id = id;
        }

        public MenuInfo(Control control, string menuid, int no_seq_dw, int no_line_menu, string cd_function, string target, string ynexc, string cd_popmenu)
        {
            Control = control;
            MenuId = menuid;
            NO_SEQ_DW = no_seq_dw;
            No_line_menu = no_line_menu;
            CD_FUNCTION = cd_function;
            Target = target;
            Ynexc = ynexc;
            CD_POPMENU = cd_popmenu;
        }
    }

    private void RealColumnEdit_QueryProcessKey(object sender, DevExpress.XtraEditors.Controls.QueryProcessKeyEventArgs e)
    {
        Control ctrl = sender as Control;
        if (ctrl.GetType().Name == "MemoEdit")
            e.IsNeededKey = e.KeyData == Keys.Right || e.KeyData == Keys.Left || e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Escape;
        else if (ctrl.GetType().Name == "LookUpEdit")
        {
            //
        }
        else
        {
            e.IsNeededKey = e.KeyData == Keys.Right || e.KeyData == Keys.Left || e.KeyData == Keys.Escape;
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down)
            {
                GridView view = this.FocusedView as GridView;
                view.CloseEditor();

            }
        }

    }

    private void View_ShowingEditor(object sender, CancelEventArgs e)
    {
        GridView view = sender as GridView;

        if (view.GetFocusedRowCellValue("CONDITION") != null && (bool)view.GetFocusedRowCellValue("CONDITION"))
            e.Cancel = true;

        if (view.FocusedColumn.ColumnEdit != null)
        {
            if (view.FocusedColumn.ColumnEdit.GetType() == typeof(RepositoryItemTextEdit) || view.FocusedColumn.ColumnEdit.GetType() == typeof(RepositoryItemMemoEdit))
            {
                if (!_doubleClick && !_keydown)
                    e.Cancel = true;
                else
                {
                    _doubleClick = false;
                    _keydown = false;
                }
            }
        }
        else
        {
            if (!_doubleClick && !_keydown)
                e.Cancel = true;
            else
            {
                _doubleClick = false;
                _keydown = false;
            }
        }

    }

    private void View_DragObjectOver(object sender, DragObjectOverEventArgs e)
    {
        GridColumn column = e.DragObject as GridColumn;
        if (column != null && e.DropInfo.Index < 0)
        {
            e.DropInfo.Valid = false;
        }
    }
    private void View_InitNewRow(object sender, InitNewRowEventArgs e)
    {
        GridView view = sender as GridView;

        //if numeric type column , set 0
        for (int i = 0; i < view.Columns.Count; i++)
        {
            if (view.Columns[i].DisplayFormat.FormatType == FormatType.Numeric)
            {
                view.SetRowCellValue(e.RowHandle, view.Columns[i], 0);
            }
        }

        if (view.VisibleColumns.Count == 0)
        {
            return;
        }

        view.FocusedColumn = view.VisibleColumns[0];
        view.ShowEditor();
        view.Focus();

        if (VerifyPrimaryKey == null) return;

        for (int i = 0; i < VerifyPrimaryKey.Length; i++)
        {
            view.SetRowCellValue(e.RowHandle, VerifyPrimaryKey[i], string.Empty);
        }
    }

    private void View_RowDeleting(object sender, EventArgs e)
    {
        GridView view = sender as GridView;
        if (view.FocusedRowHandle + 1 == view.RowCount)
            _FocusedRowPrevhandle = view.FocusedRowHandle - 1;
        else
            _FocusedRowPrevhandle = view.FocusedRowHandle;
    }

    private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        GridView view = sender as GridView;
        if (e.Column.OptionsColumn.AllowEdit == false && YN_Style == true)
        {
            e.Appearance.Options.UseForeColor = true;
            e.Appearance.ForeColor = ColorTranslator.FromHtml(_UneditableColumn);
        }
    }

    private void View_ValidateRow(object sender, ValidateRowEventArgs e)
    {
        GridView view = this.FocusedView as GridView;

        if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (view.FocusedColumn == null)
                    view.FocusedColumn = view.VisibleColumns[0];

                view.ShowEditor();
                view.HideEditor();
            }));
        }
    }

    private void View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
        GridView view = sender as GridView;
        if (e.FocusedRowHandle < 0)
            return;

        view.Appearance.SelectedRow.ForeColor = Color.FromArgb(0, 144, 139);
        view.EndSorting -= View_EndSorting;

        DataTable dt = this.DataSource as DataTable;
        //if (view.Columns["CONDITION"] != null)
        //{
        //    view.Columns["CONDITION"].UnboundExpression = "False";
        //    if (ConditionCollection != null && ConditionCollection.Count != 0)
        //    {
        //        for (int i = 0; i < this.ConditionCollection.Count; i++)
        //        {
        //            if (view.FocusedColumn.FieldName == ConditionCollection[i].Column)
        //                if (dt.Rows[dt.Rows.IndexOf(view.GetFocusedDataRow())].RowState == DataRowState.Added)
        //                    view.Columns["CONDITION"].UnboundExpression = "False";
        //                else
        //                {
        //                    if (view.GetFocusedRowCellValue("CONDITION") != null && !(bool)view.GetFocusedRowCellValue("CONDITION"))
        //                    {
        //                        if (ConditionCollection[i].Condition == "Isnewrow")
        //                            view.Columns["CONDITION"].UnboundExpression = "True";
        //                        else
        //                        {
        //                            view.Columns["CONDITION"].UnboundExpression = ConditionCollection[i].Condition;
        //                        }
        //                    }
        //                }
        //        }
        //    }
        //}
        view.EndSorting += View_EndSorting;

    }

    private void View_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
    {
        GridView view = sender as GridView;
        if (view.FocusedRowHandle < 0)
            return;


        DataTable dt = this.DataSource as DataTable;
        //if (view.Columns["CONDITION"] != null)
        //{
        //    view.Columns["CONDITION"].UnboundExpression = "False";
        //    if (ConditionCollection != null && ConditionCollection.Count != 0)
        //    {
        //        for (int i = 0; i < this.ConditionCollection.Count; i++)
        //        {
        //            if (e.FocusedColumn.FieldName == ConditionCollection[i].Column)
        //                if (dt.Rows[dt.Rows.IndexOf(view.GetFocusedDataRow())].RowState == DataRowState.Added)
        //                    view.Columns["CONDITION"].UnboundExpression = "False";
        //                else
        //                {
        //                    if (view.GetFocusedRowCellValue("CONDITION") != null && !(bool)view.GetFocusedRowCellValue("CONDITION"))
        //                    {
        //                        if (ConditionCollection[i].Condition == "Isnewrow")
        //                            view.Columns["CONDITION"].UnboundExpression = "True";
        //                        else
        //                            view.Columns["CONDITION"].UnboundExpression = ConditionCollection[i].Condition;
        //                    }
        //                }

        //        }
        //    }
        //}

        if (sender.GetType().Name == nameof(GridView))
        {
            DataRow currentRow = view.GetFocusedDataRow();
            if (currentRow == null) return;
            if (currentRow.RowState == DataRowState.Added && view.FocusedRowHandle == 0)
            {
                if (e.PrevFocusedColumn.ColumnEdit == null)
                {
                    if (e.PrevFocusedColumn.ReadOnly == true)
                    {
                        return;
                    }
                    else if (e.PrevFocusedColumn.OptionsColumn.AllowEdit == false)
                    {
                        return;
                    }

                    if (_previousRowHandle == view.FocusedRowHandle && _value != null)
                    {
                        view.SetRowCellValue(view.FocusedRowHandle, e.PrevFocusedColumn, _value);
                    }
                    _value = null;
                }
                else if (e.PrevFocusedColumn.ColumnEdit.GetType().Name == "RepositoryItemLookUpEdit")
                {
                    if (e.PrevFocusedColumn.ReadOnly == true)
                    {
                        return;
                    }
                    else if (e.PrevFocusedColumn.OptionsColumn.AllowEdit == false)
                    {
                        return;
                    }

                    if (_previousRowHandle == view.FocusedRowHandle && _value != null)
                        view.SetRowCellValue(view.FocusedRowHandle, e.PrevFocusedColumn, _value);

                    _value = null;
                }
                else
                {
                    _value = null;
                }

                view.PostEditor();
                view.GetFocusedDataRow().EndEdit();
                view.UpdateCurrentRow();
            }
        }
        e.FocusedColumn.RealColumnEdit.QueryProcessKey += RealColumnEdit_QueryProcessKey;

    }

    private void View_CustmDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
    {
        e.Painter.DrawObject(e.Info);

        GridView view = sender as GridView;
        GridViewInfo gvi = view.GetViewInfo() as GridViewInfo;

        DevExpress.Utils.Drawing.FooterCellInfoArgs info = new DevExpress.Utils.Drawing.FooterCellInfoArgs(e.Cache);
        info.Appearance.Assign(view.PaintAppearance.FooterPanel);
        Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, view.IndicatorWidth, e.Bounds.Height);
        rect.Inflate(-1, -1);
        info.Bounds = rect;
        info.DisplayText = string.Format("{0}", view.DataRowCount);
        gvi.Painter.ElementsPainter.FooterCell.DrawObject(info);

        e.Handled = true;
    }


    private void View_CustomColumnGroup(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
    {
        if (e.Column.ColumnEdit != null)
        {
            if (e.Column.ColumnEdit.EditorTypeName == "DateEdit")
            {
                e.Handled = true;
                e.Result = Comparer.Default.Compare(e.Value1, e.Value2);
            }
        }
    }


    #region Last new Row
    private bool CheckAddNewRow()
    {
        GridView view = this.FocusedView as GridView;

        if (view.FocusedColumn.VisibleIndex == view.VisibleColumns.Count - 1)
        {
            if (view.IsNewItemRow(view.FocusedRowHandle))
            {
                view.PostEditor();
                view.UpdateCurrentRow();
            }

            if (view.IsLastRow && AddNewRowLastColumn)
                return AddNewRow();
        }
        return false;
    }

    private bool AddNewRow()
    {
        GridView view = this.FocusedView as GridView;

        view.AddNewRow();
        view.FocusedColumn = view.VisibleColumns[0];
        return true;
    }
    #endregion

    private void View_MouseDown(object sender, MouseEventArgs e)
    {
        GridView view = sender as GridView;
        GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
        if (view != null && e != null)
            KeepSelection(e, view);
        if (hitInfo.HitTest == GridHitTest.RowCell && hitInfo.Column.FieldName != "CheckMarkSelection")
        {
            if (Control.ModifierKeys != Keys.Shift && Control.ModifierKeys != Keys.Control)
            {
                if (view.IsDataRow(hitInfo.RowHandle) && hitInfo.InRowCell && hitInfo.Column.FieldName != GridView.CheckBoxSelectorColumnName)
                {
                    //view.FocusedColumn = hitInfo.Column;
                    //view.FocusedRowHandle = hitInfo.RowHandle;
                    //DXMouseEventArgs.GetMouseArgs(e).Handled = false;
                }
            }
            else
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    var methodInfo = typeof(GridView).GetMethod("ChangeGroupRowSelection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    methodInfo.Invoke(view, new object[] { hitInfo.RowHandle });
                }
                else
                {
                    if (GridMode == "POP")
                    {
                        if (hitInfo.Column.FieldName == GridView.CheckBoxSelectorColumnName)
                            SelectRowsEx(view, view.FocusedRowHandle, hitInfo.RowHandle);
                    }
                    else
                    {
                        SelectRowsEx2(view, view.FocusedRowHandle, hitInfo.RowHandle);
                    }
                }
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            }
            view.FocusedRowHandle = hitInfo.RowHandle;
        }
    }

    private void View_KeyDown(object sender, KeyEventArgs e)
    {
        _keydown = true;
        GridView view = sender as GridView;

        if (e.Control && e.KeyCode == Keys.C)
        {
            if (view.OptionsSelection.MultiSelectMode == GridMultiSelectMode.RowSelect || view.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect)
            {
                Clipboard.SetText(view.GetFocusedDisplayText());
                e.Handled = true;
            }
            else
            {
                Copy(view, false);
                //Clipboard.SetText(view.GetSelectedCells());
                //e.Handled = true;
            }
        }
    }

    private string[,] selection;
    private Rectangle selectedArea = Rectangle.Empty;

    private void ClearSelection()
    {
        selection = null;
        selectedArea.X = int.MaxValue;
    }
    private void Copy(GridView fView, bool cut)
    {
        ClearSelection();
        GridCell[] cells = fView.GetSelectedCells();
        if (cells.Length == 0) return;
        int minCol, minRow, maxCol, maxRow;
        minCol = minRow = int.MaxValue;
        maxCol = maxRow = 0;
        foreach (GridCell cell in cells)
        {
            minCol = Math.Min(cell.Column.VisibleIndex, minCol);
            minRow = Math.Min(cell.RowHandle, minRow);
            maxCol = Math.Max(cell.Column.VisibleIndex, maxCol);
            maxRow = Math.Max(cell.RowHandle, maxRow);
        }
        selection = new string[maxCol - minCol + 1, maxRow - minRow + 1];
        foreach (GridCell cell in cells)
        {
            selection[cell.Column.VisibleIndex - minCol, cell.RowHandle - minRow] =
                fView.GetRowCellValue(cell.RowHandle, cell.Column).ToString();
            if (cut) fView.SetRowCellValue(cell.RowHandle, cell.Column, null);
        }
        selectedArea = new Rectangle(minCol, minRow, maxCol - minCol, maxRow - minRow);
    }

    private void View_ShownEditor(object sender, EventArgs e)
    {
        GridView view = sender as GridView;
        editor = view.ActiveEditor;
    }

    private void View_EndSorting(object sender, EventArgs e)
    {
        GridView gridView = sender as GridView;
        if (gridView.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CheckBoxRowSelect)
        {
            gridView.ClearSelection();
            gridView.FocusedRowHandle = _FocusedRowPrevhandle;
            gridView.SelectRow(gridView.FocusedRowHandle);
        }
    }

    private int GetNextIndex(GridView grid)
    {
        int index = grid.VisibleColumns.IndexOf(grid.FocusedColumn);
        if (index < grid.VisibleColumns.Count - 1)
            index++;
        else
            index = 0;
        return index;
    }
    private void KeepSelection(MouseEventArgs e, GridView view)
    {
        try
        {
            GridHitInfo hi = view.CalcHitInfo(e.Location);

            if (hi.InRowCell)
            {
                if (hi.Column == null) return;
                if (hi.Column.FieldName == "DX$CheckboxSelectorColumn")
                {
                    bool isSelected = view.IsRowSelected(hi.RowHandle);

                    if (isSelected)
                    {
                        view.UnselectRow(hi.RowHandle);
                        (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;

                    }
                    else
                    {
                        view.SelectRow(hi.RowHandle);
                        (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
                    }
                }

                if (hi.Column.ColumnEdit != null)
                {
                    if (hi.Column.ColumnEdit.GetType() == typeof(RepositoryItemCheckEdit) && hi.Column.FieldName != "CheckMarkSelection")
                    {
                        view.FocusedRowHandle = hi.RowHandle;
                        view.FocusedColumn = hi.Column;
                        view.ShowEditor();

                        CheckEdit edit = (view.ActiveEditor as CheckEdit);
                        if (edit != null)
                        {
                            edit.Toggle();
                            (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
                        }
                    }
                    else if (hi.Column.ColumnEdit.GetType() == typeof(RepositoryItemTextEdit) || hi.Column.ColumnEdit.GetType() == typeof(RepositoryItemMemoEdit))
                    {
                        view.FocusedRowHandle = hi.RowHandle;

                        if (view.IsEditorFocused)
                        {
                            view.CloseEditor();

                        }
                    }
                    else
                    {
                        view.FocusedRowHandle = hi.RowHandle;
                        view.FocusedColumn = hi.Column;
                        view.ShowEditor();
                    }
                }
                else
                {
                    if (view.GetFocusedDataRow() != null)
                        view.GetFocusedDataRow().EndEdit();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void OnItemClick(object sender, EventArgs e)
    {
        DXMenuItem dXMenuItem = sender as DXMenuItem;
        if (!(dXMenuItem.Tag is MenuInfo menuInfo))
        {
            return;
        }

        if (menuInfo.GridStringId == GridStringId.MenuColumnClearAllSorting)
        {
            menuInfo.Column.View.ClearSorting();
        }

        if (menuInfo.GridStringId == GridStringId.MenuColumnRemoveColumn)
        {
            if (menuInfo.View.GetType() == typeof(GridView))
            {
                menuInfo.Column.Visible = false;
            }
            else
            {
                menuInfo.Band.Visible = false;
            }
        }

        if (menuInfo.GridStringId == GridStringId.MenuColumnColumnCustomization)
        {
            menuInfo.View.ShowCustomization();
        }

        if (menuInfo.GridStringId == GridStringId.MenuColumnGroup)
        {
            menuInfo.Column.GroupIndex = menuInfo.View.GroupedColumns.Count;
        }

        if (menuInfo.GridStringId == GridStringId.MenuGroupPanelClearGrouping)
        {
            menuInfo.View.ClearGrouping();
        }

        if (menuInfo.GridStringId == GridStringId.MenuColumnGroupBox)
        {
            if (!menuInfo.View.OptionsView.ShowGroupPanel)
            {
                _sort = menuInfo.View.OptionsCustomization.AllowSort;
            }

            menuInfo.View.OptionsView.ShowGroupPanel = ((!menuInfo.View.OptionsView.ShowGroupPanel) ? true : false);
            if (menuInfo.View.OptionsView.ShowGroupPanel)
            {
                menuInfo.View.OptionsCustomization.AllowSort = true;
            }
            else
            {
                menuInfo.View.OptionsCustomization.AllowSort = _sort;
            }
        }

        if (menuInfo.GridStringId == GridStringId.MenuColumnFindFilterShow)
        {
            menuInfo.Column.View.ShowFindPanel();
        }

        if (menuInfo.Id == "FullExcel")
        {
            ItemExcel_Click(SelectedRowOnly: false);
        }

        if (menuInfo.Id == "SelectedRowExcel")
        {
            ItemExcel_Click(SelectedRowOnly: true);
        }
    }

    string DownloadPath = string.Empty;
    //private void ItemExcel_Click(bool SelectedRowOnly)
    //{
    //    GridView view = this.MainView as GridView;

    //    //view.BestFitColumns();
    //    view.OptionsPrint.AutoWidth = false;
    //    view.OptionsPrint.PrintSelectedRowsOnly = SelectedRowOnly;
    //    //Download
    //    DownloadPath = AppDomain.CurrentDomain.BaseDirectory + "\\Download";
    //    DirectoryInfo di = new DirectoryInfo(DownloadPath);
    //    if (di.Exists == false)
    //        di.Create();

    //    XlsxExportOptionsEx xlsxOptions = new XlsxExportOptionsEx();
    //    xlsxOptions.ShowGridLines = true;
    //    xlsxOptions.SheetName = "Export";
    //    xlsxOptions.ExportType = DevExpress.Export.ExportType.DataAware;//.WYSIWYG;    // ExportType
    //    //xlsxOptions.TextExportMode = TextExportMode.Value;
    //    xlsxOptions.TextExportMode = TextExportMode.Value;

    //    SaveFileDialog saveFileDialog = new SaveFileDialog();
    //    saveFileDialog.FileName = "ENOTES_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

    //    if (DownloadPath == string.Empty || DownloadPath == "")
    //    {
    //        saveFileDialog.Filter = "Excel|*.xlsx";
    //        saveFileDialog.Title = "Save an Excel File";
    //        saveFileDialog.ShowDialog();

    //        if (saveFileDialog.FileName != "")
    //        {
    //            this.ExportToXlsx(saveFileDialog.FileName, xlsxOptions);
    //            Process process = new Process();
    //            process.StartInfo.FileName = saveFileDialog.FileName;
    //            process.Start();
    //        }
    //    }
    //    else
    //    {
    //        if (saveFileDialog.FileName != "")
    //        {
    //            this.ExportToXlsx(DownloadPath + @"\\" + saveFileDialog.FileName, xlsxOptions);
    //            Process process = new Process();
    //            process.StartInfo.FileName = DownloadPath + @"\\" + saveFileDialog.FileName;
    //            process.Start();
    //        }
    //    }
    //}

    private void ItemExcel_Click(bool SelectedRowOnly)
    {
        aGrid_VisibleChange();
        GridView gridView = base.MainView as GridView;
        gridView.OptionsPrint.AutoWidth = false;
        gridView.OptionsPrint.PrintSelectedRowsOnly = SelectedRowOnly;
        DownloadPath = AppDomain.CurrentDomain.BaseDirectory + "\\Download";
        DirectoryInfo directoryInfo = new DirectoryInfo(DownloadPath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        XlsxExportOptionsEx xlsxExportOptionsEx = new XlsxExportOptionsEx();
        xlsxExportOptionsEx.ShowGridLines = true;
        xlsxExportOptionsEx.SheetName = "Export";
        xlsxExportOptionsEx.ExportType = ExportType.DataAware;
        xlsxExportOptionsEx.TextExportMode = TextExportMode.Value;
        xlsxExportOptionsEx.CustomizeCell += XlsxOptions_CustomizeCell;
        xlsxExportOptionsEx.ShowGroupSummaries = DefaultBoolean.True;
        xlsxExportOptionsEx.ShowTotalSummaries = DefaultBoolean.True;
        if (gridView.GetType() == typeof(BandedGridView) || gridView.GetType() == typeof(AdvBandedGridView))
        {
            xlsxExportOptionsEx.ShowColumnHeaders = DefaultBoolean.False;
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        //object scalar = DBHelper.GetScalar("SELECT NM_MENU FROM SYS_MMENU WHERE  CD_MENU = '" + Global.CdMenu + "'");
        string empty = "ENOTES_";
        //empty = ((scalar == null || !(scalar.ToString() != string.Empty)) ? ("SNOTES_" + SEQ) : (scalar.ToString() + "_" + SEQ));
        saveFileDialog.FileName = empty + "[" + DateTime.Now.ToString("yyyyMMddhhmmss") + "].xlsx";
        if (DownloadPath == string.Empty || DownloadPath == "")
        {
            saveFileDialog.Filter = "Excel|*.xlsx";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                ExportToXlsx(saveFileDialog.FileName, xlsxExportOptionsEx);
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo(saveFileDialog.FileName)
                {
                    UseShellExecute = true
                };
                process.Start();
            }
        }
        else if (saveFileDialog.FileName != "")
        {
            ExportToXlsx(DownloadPath + "\\\\" + saveFileDialog.FileName, xlsxExportOptionsEx);
            Process process2 = new Process();
            process2.StartInfo = new ProcessStartInfo(DownloadPath + "\\\\" + saveFileDialog.FileName)
            {
                UseShellExecute = true
            };
            process2.Start();
        }

        aGrid_VisibleChange();

    }

    private void aGrid_VisibleChange()
    {
        BeginUpdate();
        if (base.MainView is GridView gridView)
        {
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                if (!(A.GetString(gridView.Columns[i].Tag) != ""))
                {
                    continue;
                }

                string fieldName = gridView.Columns[i].FieldName.Replace("CD_", "NM_");
                if (gridView.Columns[fieldName] != null)
                {
                    if (gridView.Columns[i].Visible && !gridView.Columns[fieldName].Visible)
                    {
                        gridView.Columns[fieldName].Caption = gridView.Columns[i].Caption;
                        gridView.Columns[fieldName].Width = gridView.Columns[i].Width;
                        int visibleIndex = gridView.Columns[i].VisibleIndex;
                        int visibleIndex2 = gridView.Columns[fieldName].VisibleIndex;
                        gridView.Columns[i].VisibleIndex = visibleIndex2;
                        gridView.Columns[fieldName].VisibleIndex = visibleIndex;
                        gridView.Columns[i].Visible = false;
                        gridView.Columns[fieldName].Visible = true;
                    }
                    else if (!gridView.Columns[i].Visible && gridView.Columns[fieldName].Visible)
                    {
                        int visibleIndex = gridView.Columns[i].VisibleIndex;
                        int visibleIndex2 = gridView.Columns[fieldName].VisibleIndex;
                        gridView.Columns[i].VisibleIndex = visibleIndex2;
                        gridView.Columns[fieldName].VisibleIndex = visibleIndex;
                        gridView.Columns[i].Visible = true;
                        gridView.Columns[fieldName].Visible = false;
                    }
                }
            }
        }

        EndUpdate();
    }

    private void XlsxOptions_CustomizeCell(CustomizeCellEventArgs e)
    {
        GridView gridView = base.MainView as GridView;
        if (e.AreaType == SheetAreaType.DataArea && Regex.IsMatch(e.ColumnFieldName, A.WildCardToRegular("CD_*")))
        {
            string fieldName = "NM" + e.ColumnFieldName.Substring(2);
            if (gridView.Columns[fieldName] != null && !gridView.Columns[fieldName].Visible)
            {
                e.Value = A.GetString(gridView.GetRowCellValue(e.RowHandle, fieldName));
                e.Handled = true;
            }
        }
    }


    private void AGrid_MouseLeave(object sender, EventArgs e)
    {
        GridView view = this.FocusedView as GridView;
        view.PostEditor();
        view.ShowEditor();
        view.HideEditor();

        view.UpdateCurrentRow();
    }

    object _value;
    int _previousRowHandle;
    private void View_CellValueChanging(object sender, CellValueChangedEventArgs e)
    {
        GridView view = this.FocusedView as GridView;
        if (view.OptionsView.ShowAutoFilterRow && e.RowHandle == 0)
            return;

        if (e.Column.ColumnEdit != null)
        {
            if (view.ActiveEditor == null)
                return;
            if (view.ActiveEditor.GetType().Name == "MemoEdit")
            {

            }
            else if (view.ActiveEditor.GetType().Name == "TextEdit")
            {
                //view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);

                //view.PostEditor();
                view.GetFocusedDataRow().EndEdit();
                //view.UpdateCurrentRow();
                return;
            }
            else if (view.ActiveEditor.GetType().Name == "ButtonEdit")
            {

                return;
            }
            else if (view.ActiveEditor.GetType().Name == "LookUpEdit")
            {
                _value = e.Value;

                //view.PostEditor();
                //view.GetFocusedDataRow().EndEdit();
                //view.UpdateCurrentRow();
                return;
            }
            else if (view.ActiveEditor.GetType().Name == "DateEdit")
            {
                _value = e.Value;


                return;
            }
            else
            {
                view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);

                view.PostEditor();
                if (view.GetFocusedDataRow() != null)
                    view.GetFocusedDataRow().EndEdit();
                view.UpdateCurrentRow();
            }
        }
        else
        {
            _value = e.Value;
            _previousRowHandle = e.RowHandle;
            view.GetFocusedDataRow().EndEdit();
        }
    }

    private string _oldValue = string.Empty;
    private string _newValue = string.Empty;

    private void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
    {
        GridView view = this.FocusedView as GridView;

        if (view.ActiveEditor == null)
        {
            view.UpdateCurrentRow();
            return;
        }


        string oldValue = string.Empty;
        string newValue = GetString(e.Value);//view.ActiveEditor != null ? GetString(view.ActiveEditor.EditValue) : GetString(view.GetFocusedRowCellValue(e.Column));  


        //if (view.ActiveEditor == null)
        //{
        //    view.PostEditor();
        //    view.UpdateCurrentRow();
        //    view.CloseEditor();
        //    view.ValidateEditor();
        //    return;
        //}

        if (view.ActiveEditor.GetType().Name == "CheckEdit")
        {
            CheckEdit _CheckEdit = view.ActiveEditor as CheckEdit;
            if (_CheckEdit.Checked)
            {
                oldValue = "Y";
                newValue = "N";
            }
            else
            {
                oldValue = "N";
                newValue = "Y";
            }
            _oldValue = newValue;
            _newValue = oldValue;
        }
        else if (view.ActiveEditor.GetType().Name == "MemoEdit")
        {
            //view.GetFocusedDataRow().EndEdit();
            view.CloseEditor();
        }
        else if (view.ActiveEditor.GetType().Name == "LookupEdit")
        {
            return;
        }
        else
        {
            oldValue = GetString(view.ActiveEditor.OldEditValue);
            newValue = GetString(view.ActiveEditor.EditValue);

            _oldValue = oldValue;
            _newValue = newValue;


        }

        if (oldValue == newValue) return;

        view.PostEditor();
        view.UpdateCurrentRow();
    }

    private void AGrid_DataSourceChanged(object sender, EventArgs e)
    {
        if (!ChkEvent)
            InitEvent();

        DataTable dt = this.DataSource as DataTable;

        if (VerifyPrimaryKey == null) return;

        DataColumn[] pkColumn = new DataColumn[VerifyPrimaryKey.Length];
        for (int i = 0; i < VerifyPrimaryKey.Length; i++)
        {
            pkColumn[i] = dt.Columns[VerifyPrimaryKey[i]];
        }

        dt.PrimaryKey = pkColumn;
    }

    private void gridView_RowCountChanged(object sender, EventArgs e)
    {
        GridView view = ((GridView)sender);

        if (!view.GridControl.IsHandleCreated) return;

        object obj = this.DataSource;
        int _cnt = 0;

        Graphics gr = Graphics.FromHwnd(view.GridControl.Handle);
        SizeF size;

        switch (obj.GetType().Name)
        {
            case nameof(DataTable):
                DataTable _dt = this.DataSource as DataTable;
                _cnt = _dt.Rows.Count;
                if (_cnt > 9)
                    size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                else
                    size = gr.MeasureString("No.", view.PaintAppearance.Row.GetFont());
                view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + 20;
                break;

            case nameof(DataSet):
                DataSet _ds = this.DataSource as DataSet;
                for (int i = 0; i < _ds.Tables.Count; i++)
                {
                    _cnt = _ds.Tables[i].Rows.Count;
                    if (_cnt > 9)
                        size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                    else
                        size = gr.MeasureString("No.", view.PaintAppearance.Row.GetFont());
                    view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + 20;
                }
                break;
        }
    }

    private void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
    {
        GridView view = sender as GridView;
        var font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
        var pen = view.PaintAppearance.HorzLine.GetBackPen(e.Cache);
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            //e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //e.Info.DisplayText = " " + (e.RowHandle + 1).ToString();
            //e.Info.Appearance.ForeColor = Color.FromArgb(105, 105, 105);
            //e.Info.Appearance.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
            //e.Info.Appearance.BackColor = Color.White;

            e.Handled = true;


            string displayText = e.RowHandle >= 0 ? (e.RowHandle + 1).ToString() : "";
            Size textSize = TextRenderer.MeasureText(displayText, font);
            e.Appearance.ForeColor = Color.FromArgb(105, 105, 105);
            e.Appearance.BackColor = Color.White;
            e.Appearance.FillRectangle(e.Cache, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width - 4, e.Bounds.Y - 4));
            Rectangle textRect = (view.FocusedRowHandle == e.RowHandle) ? new Rectangle(e.Bounds.X + 5, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height) : e.Bounds;
            e.Graphics.DrawRectangle(pen, new Rectangle(e.Bounds.X, e.Bounds.Y - (int)pen.Width, e.Bounds.Width - 1, e.Bounds.Height));//border
            e.Graphics.DrawString(displayText, font, new SolidBrush(Color.FromArgb(105, 105, 105)), textRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            Size iconSize = ImageCollection.GetImageListSize(e.Info.ImageCollection);
            Rectangle iconRect = new Rectangle(
                e.Bounds.X + (e.Bounds.Width - (iconSize.Width + 20) - (textSize.Width)) / 2,
                e.Bounds.Y + (e.Bounds.Height - iconSize.Height) / 2,
                iconSize.Width,
                iconSize.Height
            );

            ImageCollection.DrawImageListImage(e.Cache, e.Info.ImageCollection, e.Info.ImageIndex, iconRect);
        }
        if (e.Info.Kind == DevExpress.Utils.Drawing.IndicatorKind.Header)
        {
            e.Handled = true;
            string displayText = "No.";
            e.Appearance.BackColor = Color.White;
            e.Appearance.FillRectangle(e.Cache, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width - 4, e.Bounds.Y - 4));
            e.Graphics.DrawRectangle(pen, new Rectangle(e.Bounds.X, e.Bounds.Y - (int)pen.Width, e.Bounds.Width - 1, e.Bounds.Height));//border
            e.Graphics.DrawString(displayText, font, new SolidBrush(Color.FromArgb(38, 143, 205)), e.Bounds, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        if (e.Info.Kind == DevExpress.Utils.Drawing.IndicatorKind.Band)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            e.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            e.Info.DisplayText = " " + "No.";
            e.Info.Appearance.ForeColor = Color.FromArgb(38, 143, 205);
            e.Info.Appearance.Font = new Font(_RowFont, Convert.ToSingle(_RowFontSize), GraphicsUnit.Pixel);
        }
    }

    private void View_ColumnPositionChanged(object sender, EventArgs e)
    {
        GridView view = this.FocusedView as GridView;

        view.PostEditor();
        if (view.GetFocusedDataRow() == null) return;
        view.GetFocusedDataRow().EndEdit();
        view.UpdateCurrentRow();

        view.CloseEditor();
        if (view.FocusedColumn.ColumnEdit != null)
            if (view.FocusedColumn.ColumnEdit.GetType().Name == "RepositoryItemLookUpEdit")
                view.ShowEditor();
    }

    void MoveFocus(GridView view, KeyEventArgs e)
    {
        try
        {
            view.PostEditor();
            view.GetFocusedDataRow().EndEdit();
            view.UpdateCurrentRow();
            PopupBaseEdit edit = view.ActiveEditor as PopupBaseEdit;
            if (edit != null && edit.IsPopupOpen)
                view.CloseEditor();
            switch (e.KeyCode)
            {
                case Keys.Tab:
                case Keys.Enter:
                    if (view.FocusedColumn == view.VisibleColumns.Last())
                    {
                        if (view.IsLastRow)
                        {
                            CheckAddNewRow();
                        }
                        else
                        {
                            view.FocusedRowHandle += 1;
                            view.InvertRowSelection(view.FocusedRowHandle - 1);
                            view.InvertRowSelection(view.FocusedRowHandle);
                            view.FocusedColumn = view.VisibleColumns.First();
                            if (!view.FocusedColumn.OptionsColumn.AllowEdit && this.GridMode == "REG")
                                MoveFocus(view, e);
                        }
                    }
                    else
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                        if (!view.FocusedColumn.OptionsColumn.AllowEdit && this.GridMode == "REG")
                            MoveFocus(view, e);
                    }
                    break;
                case Keys.Left:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (view.FocusedColumn != view.VisibleColumns.First())
                        view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex - 1];
                    else if (view.FocusedColumn == view.VisibleColumns.First() && view.FocusedRowHandle == 0)
                    {
                        view.FocusedColumn = view.FocusedColumn;
                    }
                    else
                    {
                        view.FocusedRowHandle -= 1;
                        view.InvertRowSelection(view.FocusedRowHandle);
                        view.InvertRowSelection(view.FocusedRowHandle + 1);
                        view.FocusedColumn = view.VisibleColumns.Last();
                    }
                    break;
                case Keys.Right:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (view.FocusedColumn != view.VisibleColumns.Last())
                        view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                    else if (view.FocusedColumn == view.VisibleColumns.Last() && view.FocusedRowHandle == view.RowCount - 1)
                    {
                        view.FocusedColumn = view.FocusedColumn;
                    }
                    else
                    {
                        view.FocusedRowHandle += 1;
                        view.InvertRowSelection(view.FocusedRowHandle - 1);
                        view.InvertRowSelection(view.FocusedRowHandle);
                        view.FocusedColumn = view.VisibleColumns.First();
                    }
                    break;
                case Keys.Up:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (view.FocusedRowHandle != 0)
                    {
                        view.FocusedRowHandle -= 1;
                        if (!(view.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect))
                        {
                            view.InvertRowSelection(view.FocusedRowHandle);
                            view.InvertRowSelection(view.FocusedRowHandle + 1);
                        }

                    }
                    break;
                case Keys.Down:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (view.FocusedRowHandle != view.RowCount - 1)
                    {
                        view.FocusedRowHandle += 1;
                        if (!(view.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect))
                        {
                            view.InvertRowSelection(view.FocusedRowHandle - 1);
                            view.InvertRowSelection(view.FocusedRowHandle);
                        }

                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    void MoveFocusPre(GridView view, KeyEventArgs e)
    {
        GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
        GridColumnInfoArgs ci1 = viewInfo.ColumnsInfo.FirstColumnInfo;
        GridColumnInfoArgs ci2 = viewInfo.ColumnsInfo.LastColumnInfo;

        if (ci1.Column == view.FocusedColumn)
        {
            view.UnselectRow(view.FocusedRowHandle);
            view.FocusedColumn = ci2.Column;
            view.FocusedRowHandle = view.FocusedRowHandle - 1;
            e.Handled = true;
            if (!view.FocusedColumn.OptionsColumn.AllowEdit && this.GridMode == "REG")
                MoveFocusPre(view, e);

        }
    }

    private void AGrid_MouseWheel(object sender, MouseEventArgs e)
    {
        GridView view = this.FocusedView as GridView;

        if (Control.ModifierKeys == Keys.Control)
        {
            if (e.Delta > 0)
            {
                if (view.Appearance.FilterPanel.FontSizeDelta < 20)
                {
                    view.Appearance.FilterPanel.FontSizeDelta++;
                    view.Appearance.Row.FontSizeDelta++;
                    view.Appearance.HeaderPanel.FontSizeDelta++;
                }
            }
            else
            {
                if (view.Appearance.FilterPanel.FontSizeDelta > -5)
                {
                    view.Appearance.FilterPanel.FontSizeDelta--;
                    view.Appearance.Row.FontSizeDelta--;
                    view.Appearance.HeaderPanel.FontSizeDelta--;
                }
            }

            //view.BestFitColumns();
        }
    }

    private void AGrid_MouseDown(object sender, MouseEventArgs e)
    {
        GridView view = this.FocusedView as GridView;


        DataTable dtGrid = new DataTable();
        DataTable dtData = new DataTable();

        dtData = this.DataSource as DataTable;

        dtGrid.Columns.Add("Name");
        dtGrid.Columns.Add("Field");
        dtGrid.Columns.Add("Type");
        dtGrid.Columns.Add("Vislble");
        dtGrid.Columns.Add("Editable");

        foreach (GridColumn item in view.Columns)
        {
            DataRow newRow = dtGrid.NewRow();
            newRow["Name"] = item.Caption;
            newRow["Field"] = item.FieldName;
            newRow["Type"] = item.ColumnType;
            newRow["Vislble"] = item.Visible.ToString();
            newRow["Editable"] = item.OptionsColumn.AllowEdit.ToString();

            dtGrid.Rows.Add(newRow);
        }
    }

    #endregion

    #region Method

    public void SetBinding(Control container, GridView aGridView, object[] EnableControlsIfAdded)
    {
        _binding = new SetControlBinding(aGridView, container, EnableControlsIfAdded);
    }

    public void SetBindingEvnetPossible(bool _isEvent)
    {
        if (_binding != null)
        {
            if (_isEvent)
                _binding.InitControlEvent();
            else
                _binding.InitControlEventDelete();
        }
    }

    //public void SelectRow(int rowHandle)
    //{
    //    _binding.SelectRow(rowHandle);
    //}

    public void DoRowUp(string OrderFieldName)
    {
        GridView view = this.FocusedView as GridView;

        view.GridControl.Focus();
        int index = view.FocusedRowHandle;
        if (index <= 0) return;

        DataRow row1 = view.GetDataRow(index);
        DataRow row2 = view.GetDataRow(index - 1);
        object val1 = row1[OrderFieldName];
        object val2 = row2[OrderFieldName];
        row1[OrderFieldName] = val2;
        row2[OrderFieldName] = val1;

        object[] row = row1.ItemArray;

        row1.ItemArray = row2.ItemArray;
        row2.ItemArray = row;

        view.FocusedRowHandle = index - 1;
    }

    public void DoRowDown(string OrderFieldName)
    {
        GridView view = this.FocusedView as GridView;

        view.GridControl.Focus();
        int index = view.FocusedRowHandle;
        if (index >= view.DataRowCount - 1) return;

        DataRow row1 = view.GetDataRow(index);
        DataRow row2 = view.GetDataRow(index + 1);
        object val1 = row1[OrderFieldName];
        object val2 = row2[OrderFieldName];

        row1[OrderFieldName] = val2;
        row2[OrderFieldName] = val1;

        object[] row = row1.ItemArray;

        row1.ItemArray = row2.ItemArray;
        row2.ItemArray = row;

        view.FocusedRowHandle = index + 1;
    }

    public void Insertrow(DataTable dt, int index)
    {
        dt.Rows.InsertAt(dt.NewRow(), index);
        Binding(dt);
    }

    public void Insertrow(DataTable dt, Hashtable ha_Filter)
    {
        DataRow I_Row = null;

        int Row_index = 0;

        I_Row = dt.NewRow();

        System.Text.StringBuilder Query = null;

        Query = new System.Text.StringBuilder();


        foreach (DictionaryEntry entry in ha_Filter)
        {
            I_Row[entry.Key.ToString()] = entry.Value;

            Query.AppendFormat(" {0} = '{1}' AND", entry.Key.ToString(), entry.Value.ToString());
        }


        if (Query.Length > 0)
        {
            Query.Remove(Query.Length - 3, 3);
        }


        DataRow[] result = dt.Select(Query.ToString());

        if (result.Length > 0)
        {
            Row_index = dt.Rows.IndexOf(result[result.Length - 1]);
        }

        dt.Rows.InsertAt(I_Row, Row_index);

        Binding(dt);
    }


    public DataTable GetDataTable
    {
        get
        {
            return this.DataSource as DataTable;
        }
    }


    #region Datasource Getchange / Accecpt Change
    private DataTable _dt = new DataTable();
    /// <summary>
    /// Get the grid dataSource state.
    /// </summary>
    /// <returns>DataTable</returns>
    public DataTable GetChanges()
    {
        AGrid_MouseLeave(null, null);
        _dt = this.DataSource as DataTable;

        //NullDelete();
        NullCheck();

        GridView view = this.FocusedView as GridView;

        view.PostEditor();
        view.UpdateCurrentRow();

        DataTable dtChange = _dt.GetChanges();
        return dtChange;
    }


    public DataTable GetChanges(bool yn_check)
    {
        DataTable dtChange = null;

        AGrid_MouseLeave(null, null);
        _dt = this.DataSource as DataTable;

        if (yn_check)
        {
            //NullDelete();
            NullCheck();
        }


        GridView view = this.FocusedView as GridView;

        view.PostEditor();
        view.UpdateCurrentRow();
        if (_dt != null)
        {
            dtChange = _dt.GetChanges();
        }
        return dtChange;
    }

    public bool GetChangesYn(bool yn_check)
    {
        DataTable dt = null;
        try
        {
            dt = new DataTable();
            dt = this.GetChanges(yn_check);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
        finally
        {
            if (dt != null)
                dt.Dispose();
        }
    }



    /// <summary>
    /// null validation check
    /// </summary>
    public void NullCheck()
    {
        if (VerifyNotNull != null) VerifyNullCheckColumn();
    }

    /// <summary>
    /// auto delete rows base on null check  
    /// </summary>
    public void NullDelete()
    {
        if (VerifyNullDelete != null) VerifyNullAutoDelete();
    }

    /// <summary>
    /// AddNewRow
    /// UpdateCurrentRow
    /// </summary>
    public void Addrow()
    {
        GridView view = this.FocusedView as GridView;

        try
        {
            this.Focus();
            view.AddNewRow();
            view.UpdateCurrentRow();
            this.Focus();
        }
        catch { }
    }

    public void Addrow(int position)
    {
        GridView view = this.FocusedView as GridView;
        DataTable dt = null;
        DataRow dr = null;

        try
        {

            if (view.RowCount < position)
                position = view.RowCount;

            dt = this.DataSource as DataTable;
            dr = dt.NewRow();
            dt.Rows.InsertAt(dr, position);



        }
        catch { }
        finally
        {
            if (dt != null)
                dt.Dispose();
        }

    }



    public void Deleterow()
    {
        GridView view = this.FocusedView as GridView;

        try
        {
            bool Focus_move = false;

            if (view.RowCount < 1) return;

            view.BeginDataUpdate();

            if (view.FocusedRowHandle + 1 != view.RowCount)
            {
                Focus_move = true;
            }


            view.DeleteRow(view.FocusedRowHandle);



            view.EndDataUpdate();

            if (Focus_move)
            {
                view.MoveNext();
                view.MovePrev();
            }
            else
            {
                //view.MovePrev();
                view.MoveNext();
            }

            //if (view.FocusedRowHandle > -1)
            //    view.SelectRow(view.FocusedRowHandle);

            //view.MoveNext();

            //if (view.GetSelectedRows().Length > 0)
            //    view.DeleteSelectedRows();
            //else
            //    view.DeleteRow(view.FocusedRowHandle);

        }
        catch { }

    }

    public void Deleterow(int RowHandle)
    {
        GridView view = this.FocusedView as GridView;

        try
        {

            if (view.RowCount < 1) return;

            view.BeginDataUpdate();

            view.DeleteRow(RowHandle);

            view.EndDataUpdate();

            view.MoveNext();
            view.MovePrev();

            //if (view.GetSelectedRows().Length > 0)
            //    view.DeleteSelectedRows();
            //else
            //    view.DeleteRow(view.FocusedRowHandle);

        }
        catch { }

    }


    /// <summary>
    /// Accept the changes of the grid's Datasource
    /// </summary>
    public void AcceptChanges()
    {
        if (_dt != null)
            _dt.AcceptChanges();
    }
    #endregion Datasource Getchange / Accecpt Change

    #region Column Get/Set
    public object GetCol(string ColumnName)
    {
        GridView view = this.FocusedView as GridView;
        return view.GetFocusedDataRow() == null ? 0 : view.GetFocusedDataRow().Field<object>(ColumnName);
    }

    public object GetCol(int rowHandle, string ColumnName)
    {
        GridView view = this.FocusedView as GridView;
        return view.GetDataRow(rowHandle).Field<object>(ColumnName);
    }

    public void SetCol(string ColumnName, object value)
    {
        try
        {
            GridView view = this.FocusedView as GridView;
            view.UpdateCurrentRow();

            DataView dv = view.DataSource as DataView;
            DataTable dt = dv.Table;

            dt.Rows[view.GetFocusedDataSourceRowIndex()][ColumnName] = value;
        }
        catch
        {
            throw;
        }
    }

    public void SetCol(GridColumn Column, object value)
    {
        GridView view = this.FocusedView as GridView;
        view.SetFocusedRowCellValue(Column, value);
    }

    public void SetCol(int rowHandle, string ColumnName, object value)
    {
        try
        {
            GridView view = this.FocusedView as GridView;
            view.UpdateCurrentRow();

            DataView dv = view.DataSource as DataView;
            DataTable dt = dv.Table;

            dt.Rows[rowHandle][ColumnName] = value;
        }
        catch
        {
            throw;
        }
    }

    public void SetCol(int rowHandle, GridColumn Column, object value)
    {
        GridView view = this.FocusedView as GridView;
        view.SetRowCellValue(rowHandle, Column, value);
    }

    private void VerifyNullAutoDelete()
    {
        GridView view = this.FocusedView as GridView;

        for (int i = view.RowCount; i >= 0; --i)
        {
            for (int j = 0; j < VerifyNullDelete.Length; j++)
            {
                if (GetCol(VerifyNullDelete[j]) == null || GetCol(VerifyNullDelete[j]).ToString() == string.Empty)
                {
                    (DataSource as DataTable).Rows[i - 1].Delete();
                }
            }
        }
    }

    private void VerifyNullCheckColumn()
    {
        GridView view = this.FocusedView as GridView;

        DataTable dt = this.DataSource as DataTable;
        DataTable dtChanges = dt.GetChanges();

        if (dtChanges == null) return;

        foreach (DataRow row in dtChanges.Rows)
        {
            if (row.RowState == DataRowState.Deleted) continue;

            foreach (string item in this.VerifyNotNull)
            {
                if (dtChanges.Columns[item].DataType != typeof(decimal))
                {
                    if (GetString(row[item]) == string.Empty)
                    {
                        view.FocusedColumn = view.Columns[item];
                        view.ShowEditor();
                        view.HideEditor();
                        throw new System.Exception(string.Format("[{0}] is Required.", view.Columns[item].Caption != "" ? view.Columns[item].Caption : item));
                    }
                }

                if (dtChanges.Columns[item].DataType == typeof(decimal))
                {
                    if (GetDecimal(row[item]) == decimal.Zero)
                    {
                        view.FocusedColumn = view.Columns[item];
                        view.ShowEditor();
                        view.HideEditor();
                        throw new System.Exception(string.Format("[{0}] is Required.", view.Columns[item].Caption != "" ? view.Columns[item].Caption : item));
                    }
                }
            }
        }
    }
    #endregion Column Get/Set
    private void SelectRowsEx(GridView view, int startRowHandle, int endRowHandle)
    {
        if (startRowHandle == GridControl.InvalidRowHandle || endRowHandle == GridControl.InvalidRowHandle)
        {
            return;
        }

        var selectAllGroupRowsMethodInfo = typeof(GridView).GetMethod("SelectAllGroupRows", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (startRowHandle == endRowHandle)
        {
            if (view.IsGroupRow(startRowHandle))
            {
                selectAllGroupRowsMethodInfo.Invoke(view, new object[] { startRowHandle });
            }
            else
            {
                view.SelectRow(startRowHandle);
            }
            return;
        }

        int startIndex = view.GetVisibleIndex(startRowHandle);
        int endIndex = view.GetVisibleIndex(endRowHandle);

        if (startIndex < 0 || endIndex < 0)
        {
            return;
        }

        if (startIndex > endIndex)
        {
            int tmp = endIndex;
            endIndex = startIndex;
            startIndex = tmp;
        }

        try
        {
            view.BeginSelection();

            for (int n = startIndex; n < endIndex + 1; n++)
            {
                int rowHandle = view.GetVisibleRowHandle(n);
                if (view.IsGroupRow(rowHandle))
                {
                    selectAllGroupRowsMethodInfo.Invoke(view, new object[] { rowHandle });
                }
                else
                {
                    if (rowHandle == startIndex || rowHandle == endIndex)
                    {
                        if (!view.IsRowSelected(rowHandle))
                            view.UnselectRow(rowHandle);
                        else
                            view.SelectRow(rowHandle);
                    }
                    else
                    {
                        if (view.IsRowSelected(rowHandle))
                            view.UnselectRow(rowHandle);
                        else
                            view.SelectRow(rowHandle);
                    }

                }
            }
        }
        finally
        {
            view.EndSelection();
        }
    }

    private void SelectRowsEx2(GridView view, int startRowHandle, int endRowHandle)
    {
        bool chk = true;
        if (startRowHandle == GridControl.InvalidRowHandle || endRowHandle == GridControl.InvalidRowHandle)
        {
            return;
        }

        var selectAllGroupRowsMethodInfo = typeof(GridView).GetMethod("SelectAllGroupRows", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (startRowHandle == endRowHandle)
        {
            if (view.IsGroupRow(startRowHandle))
            {
                selectAllGroupRowsMethodInfo.Invoke(view, new object[] { startRowHandle });
            }
            else
            {
                view.SelectRow(startRowHandle);
            }
            return;
        }

        int startIndex = view.GetVisibleIndex(startRowHandle);
        int endIndex = view.GetVisibleIndex(endRowHandle);

        if (startIndex < 0 || endIndex < 0)
        {
            return;
        }

        if (startIndex > endIndex)
        {
            int tmp = endIndex;
            endIndex = startIndex;
            startIndex = tmp;
            chk = false;
        }

        try
        {
            view.BeginSelection();

            for (int n = startIndex; n < endIndex + 1; n++)
            {
                int rowHandle = view.GetVisibleRowHandle(n);
                if (view.IsGroupRow(rowHandle))
                {
                    selectAllGroupRowsMethodInfo.Invoke(view, new object[] { rowHandle });
                }
                else
                {
                    if (rowHandle == startIndex)
                    {
                        if (chk)
                        {
                            if (!view.IsRowSelected(rowHandle))
                                view.UnselectRow(rowHandle);
                            else
                                view.SelectRow(rowHandle);
                        }
                        else
                        {
                            if (view.IsRowSelected(rowHandle))
                                view.UnselectRow(rowHandle);
                            else
                                view.SelectRow(rowHandle);
                        }

                    }
                    else if (rowHandle == endIndex)
                    {
                        if (chk)
                        {
                            if (view.IsRowSelected(rowHandle))
                                view.UnselectRow(rowHandle);
                            else
                                view.SelectRow(rowHandle);
                        }
                        else
                        {
                            if (!view.IsRowSelected(rowHandle))
                                view.UnselectRow(rowHandle);
                            else
                                view.SelectRow(rowHandle);
                        }

                    }
                    else
                    {
                        if (view.IsRowSelected(rowHandle))
                            view.UnselectRow(rowHandle);
                        else
                            view.SelectRow(rowHandle);
                    }

                }
            }
        }
        finally
        {
            view.EndSelection();
        }
    }

    private DataView GetDataView(string ParentFieldName, object value)
    {
        try
        {
            DataView view = new DataView(this.DataSource as DataTable);

            view.RowFilter = @"" + ParentFieldName + " = '" + value + "'";

            return view;
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    public bool ExistChild(string ParentFieldName, object value)
    {
        try
        {
            DataView view = GetDataView(ParentFieldName, value);
            if (view.Count > 0 && A.GetString(value) != string.Empty)
                return true;
            else
                return false;
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    public void DeleteChildRow(string ParentFieldName, string KeyFieldName, object value)
    {
        List<string> list = new List<string>();
        List<DataRow> row = new List<DataRow>();

        try
        {
            DataView view = GetDataView(ParentFieldName, value);

            for (int i = 0; i < view.Count; i++)
            {
                row.Add(view[i].Row);
                if (A.GetString(view[i][KeyFieldName]) != string.Empty)
                    list.Add(A.GetString(view[i][KeyFieldName]));
            }

            for (int i = 0; i < list.Count; i++)
            {
                view.RowFilter = @"" + ParentFieldName + " = '" + list[i] + "'";
                for (int j = 0; j < view.Count; j++)
                {
                    row.Add(view[j].Row);
                    if (A.GetString(view[j][KeyFieldName]) != string.Empty)
                        list.Add(A.GetString(view[j][KeyFieldName]));
                }
            }

            DataTable dt = this.DataSource as DataTable;

            for (int i = 0; i < row.Count; i++)
            {
                dt.Rows[dt.Rows.IndexOf(row[i])].Delete();
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    public void SetNodeValue(string ParentFieldName, object prevalue, object setvalue)
    {
        if (A.GetString(prevalue) != string.Empty && A.GetString(setvalue) != string.Empty)
        {
            DataView view = GetDataView(ParentFieldName, prevalue);

            while (view.Count > 0)
            {
                view[0][ParentFieldName] = setvalue;
            }
        }
    }

    public void SetDropNodeValue(string ParentFieldName, string DestinationNodeParentValue, string KeyFieldName, string SourceNodeValue)
    {
        List<List<object>> list = new List<List<object>>();
        decimal no_level_max = 0;
        decimal no_line_max = 0;

        DataView destination_view = GetDataView(KeyFieldName, DestinationNodeParentValue);
        DataView destination_view_child = GetDataView(ParentFieldName, DestinationNodeParentValue);
        DataView view = new DataView(this.DataSource as DataTable);
        view.RowFilter = KeyFieldName + " = '" + SourceNodeValue + "'";

        bool no_level_exist = destination_view.ToTable().Columns.Contains("NO_LEVEL");
        bool no_line_exist = destination_view.ToTable().Columns.Contains("NO_LINE");
        //bool yn_end_exists = max_view.ToTable().Columns.Contains("YN_END");

        if (no_level_exist)
        {
            no_level_max = A.GetDecimal(destination_view[0]["NO_LEVEL"]);
            no_level_max += 1;
            view[0]["NO_LEVEL"] = no_level_max;
        }

        if (no_line_exist)
        {
            for (int i = 0; i < destination_view_child.Count; i++)
            {
                if (no_line_max < A.GetDecimal(destination_view_child[i]["NO_LINE"]))
                    no_line_max = A.GetDecimal(destination_view_child[i]["NO_LINE"]);
            }
            no_line_max += 1;
            view[0]["NO_LINE"] = no_line_max;
        }

        view[0][ParentFieldName] = DestinationNodeParentValue;

        view = GetDataView(ParentFieldName, SourceNodeValue);

        no_level_max += 1;
        for (int i = 0; i < view.Count; i++)
        {
            if (no_level_exist)
                view[i]["NO_LEVEL"] = no_level_max;
            list.Add(new List<object> { view[i][KeyFieldName], no_level_max });
        }

        for (int i = 0; i < list.Count; i++)
        {
            view = GetDataView(ParentFieldName, list[i][0]);

            if (view.Count > 0)
            {
                list[i][1] = A.GetDecimal(list[i][1]) + 1;

                for (int j = 0; j < view.Count; j++)
                {
                    if (no_level_exist)
                        view[j]["NO_LEVEL"] = list[i][1];
                    //if (yn_end_exists)
                    //    view[j]["YN_END"] = "N"; 
                    list.Add(new List<object> { view[j][KeyFieldName], list[i][1] });
                }
            }
        }
    }
    #endregion

    #region Binding 
    /// <summary>
    /// Binding Grid 
    /// </summary>
    /// <param name="gridControl">aGrid control</param>
    /// <param name="dataSource">Prefer to use DataTable</param>
    public void Binding(object dataSource)
    {
        Binding(dataSource, false);
    }

    /// <summary>
    /// Binding Grid 
    /// </summary>
    /// <param name="gridControl">aGrid control</param>
    /// <param name="dataSource">Prefer to use DataTable</param>
    /// <param name="autoWidth">BestFit Width</param>

    public void Binding(object dataSource, bool autoWidth)
    {
        GridView view = this.MainView as GridView;

        this.BeginUpdate();


        this.DataSource = dataSource;

        this.EndUpdate();
    }

    #endregion


    protected override void InitLayout()
    {
        this.DataSourceChanged += AGrid_DataSourceChanged;
        this.MouseWheel += AGrid_MouseWheel;
        this.MouseDown += AGrid_MouseDown;
        base.InitLayout();
    }

    private static string GetString(object val)
    {
        return GetString(val, false);
    }

    private static string GetString(object val, bool isUpper)
    {
        if (val == null || val == DBNull.Value)
            return "";

        string ret = string.Empty;
        if (isUpper)
            ret = val.ToString().ToUpper();
        else
            ret = val.ToString();

        if (ret == null)
            return "";

        return ret.Trim();
    }

    private static decimal GetDecimal(object val)
    {
        if (val == null || val == DBNull.Value)
            return 0;

        decimal ret = 0;
        try
        {
            ret = Convert.ToDecimal(val);
        }
        catch
        {
        }

        return ret;
    }

    private void InitializeComponent()
    {
        this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
        ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        this.SuspendLayout();
        // 
        // gridView1
        // 
        this.gridView1.GridControl = this;
        this.gridView1.Name = "gridView1";
        // 
        // aGrid
        // 
        this.MainView = this.gridView1;
        this.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
        this.gridView1});
        this.LookAndFeel.UseDefaultLookAndFeel = false;
        this.LookAndFeel.SetSkinStyle(SkinStyle.Office2013LightGray);
        ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        this.ResumeLayout(false);
    }
}
