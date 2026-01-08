using CH.Grid;
using DevExpress.Data;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Helper;

public enum aGridColumnStyle
{
    Default,
    Text,
    Numeric,
    Date,
    DateFull,
    Time,
    Time2,
    Time3,
    CheckBox,
    Button,
    Ym,
    LookUpEdit,
    MemoEdit,
    YY,
    Picture,
    D0,
    D1,
    D2,
    D3,
    D4
}

[SupportedOSPlatform("windows")]
public static class aGridHelper
{
    public class SetColumn
    {
        protected GridView gridView;
        protected GridColumn columnObject;
        private int _Width;
        private bool _Editable = false;
        private bool _Visible = true;
        private string _FormatString;
        private string _MaskInput;
        private bool _ShowHeaderCheckBox = true;

        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
                columnObject.Width = value;
            }
        }

        public bool Editable
        {
            get
            {
                return _Editable;
            }
            set
            {
                _Editable = value;
                columnObject.OptionsColumn.AllowEdit = value;
            }
        }

        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
                columnObject.Visible = value;
            }
        }

        public string FormatString
        {
            get
            {
                return _FormatString;
            }
            set
            {
                _FormatString = value;
                columnObject.DisplayFormat.FormatType = FormatType.Custom;
                columnObject.DisplayFormat.FormatString = value;
            }
        }

        public string MaskInput
        {
            get
            {
                return _MaskInput;
            }
            set
            {
                _MaskInput = value;
                columnObject.DisplayFormat.FormatType = FormatType.Custom;
                columnObject.DisplayFormat.FormatString = value;
            }
        }

        public bool ShowHeaderCheckBox
        {
            get
            {
                return _ShowHeaderCheckBox;
            }
            set
            {
                _ShowHeaderCheckBox = value;
                columnObject.Tag = value;
            }
        }

        public SetColumn(GridView gridView)
        {
            this.gridView = gridView;
        }

        public SetColumn(GridView gridView, string[] columnField, string[] columnTitle)
        {
            this.gridView = gridView;
            for (int i = 0; i < columnField.Length; i++)
            {
                columnField[i] = columnField[i].Replace(" ", "");
                columnObject = gridView.Columns.AddVisible(columnField[i], columnTitle[i]);
                Width = 120;
                Editable = false;
                columnObject.OptionsColumn.FixedWidth = true;
                ApplyColumnStyle(aGridColumnStyle.Text);
                gridView.Columns.Add(columnObject);
            }
        }

        public SetColumn(GridView gridView, string[] columnField, string[] columnTitle, aGridColumnStyle columnStyle)
        {
            this.gridView = gridView;
            for (int i = 0; i < columnField.Length; i++)
            {
                columnField[i] = columnField[i].Replace(" ", "");
                columnObject = gridView.Columns.AddVisible(columnField[i], columnTitle[i]);
                Width = 120;
                Editable = false;
                columnObject.OptionsColumn.FixedWidth = true;
                ApplyColumnStyle(columnStyle);
                gridView.Columns.Add(columnObject);
            }
        }

        public SetColumn(GridView gridView, string[] columnField, string[] columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable)
        {
            this.gridView = gridView;
            for (int i = 0; i < columnField.Length; i++)
            {
                columnField[i] = columnField[i].Replace(" ", "");
                columnObject = gridView.Columns.AddVisible(columnField[i], columnTitle[i]);
                Width = columnWidth;
                Editable = editable;
                columnObject.OptionsColumn.FixedWidth = true;
                ApplyColumnStyle(columnStyle);
                gridView.Columns.Add(columnObject);
            }
        }

        public SetColumn(GridView gridView, string columnField)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, string.Empty);
            gridView.Columns.Add(columnObject);
        }

        public SetColumn(GridView gridView, string columnField, bool visible)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, string.Empty);
            Visible = visible;
            gridView.Columns.Add(columnObject);
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = editable;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(aGridColumnStyle.Text);
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, bool visible)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = editable;
            Visible = visible;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(aGridColumnStyle.Text);
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = editable;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(columnStyle);
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable, string[,] data2D)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = editable;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(columnStyle, data2D);
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable, DataTable dataTable)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = editable;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(columnStyle, dataTable);
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable, bool visible)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = editable;
            Visible = visible;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(columnStyle);
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable, bool visible, string formatstring)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = editable;
            Visible = visible;
            FormatString = formatstring;
            ApplyColumnStyle(columnStyle);
            columnObject.OptionsColumn.FixedWidth = true;
        }

        public SetColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, aGridColumnStyle columnStyle, UnboundColumnType unboundColumnType, string unboundExpression)
        {
            this.gridView = gridView;
            columnField = columnField.Replace(" ", "");
            columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
            Width = columnWidth;
            Editable = false;
            Visible = true;
            columnObject.OptionsColumn.FixedWidth = true;
            columnObject.UnboundType = unboundColumnType;
            columnObject.UnboundExpression = unboundExpression;
            ApplyColumnStyle(columnStyle);
        }

        protected void ApplyColumnStyle(aGridColumnStyle columnStyle)
        {
            RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
            RepositoryItemDateEdit repositoryItemDateEdit = new RepositoryItemDateEdit();
            RepositoryItemMemoEdit repositoryItemMemoEdit = new RepositoryItemMemoEdit();
            RepositoryItemPictureEdit repositoryItemPictureEdit = new RepositoryItemPictureEdit();
            columnObject.SortMode = ColumnSortMode.Default;

            switch (columnStyle)
            {
                case aGridColumnStyle.Default:
                case aGridColumnStyle.Text:
                    columnObject.DisplayFormat.FormatType = FormatType.None;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    break;

                case aGridColumnStyle.MemoEdit:
                    columnObject.DisplayFormat.FormatType = FormatType.None;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.ColumnEdit = repositoryItemMemoEdit;
                    break;

                case aGridColumnStyle.Numeric:
                    columnObject.DisplayFormat.FormatType = FormatType.Custom;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.DisplayFormat.FormatString = FormatString;
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = FormatString;
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;

                case aGridColumnStyle.Date:
                    {
                        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        //yyyy/MM/dd
                        string editMask3 = "([0-9][0-9][0-9][0-9])/(0[0-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])";
                        repositoryItemDateEdit.Mask.EditMask = editMask3;
                        repositoryItemDateEdit.Mask.UseMaskAsDisplayFormat = true;
                        repositoryItemDateEdit.Mask.MaskType = MaskType.RegEx;
                        columnObject.ColumnEdit = repositoryItemDateEdit;
                        columnObject.SortMode = ColumnSortMode.Custom;
                        repositoryItemDateEdit.CustomDisplayText += edit_CustomDisplayText;
                        repositoryItemDateEdit.KeyDown += riItemDateEdit_KeyDown;
                    }
                    break;

                case aGridColumnStyle.DateFull:
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    MaskInput = "yyyy/MM/dd HH:mm";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.DateTime;
                    repositoryItemTextEdit.Mask.EditMask = MaskInput;
                    repositoryItemTextEdit.DisplayFormat.FormatType = FormatType.DateTime;
                    repositoryItemTextEdit.DisplayFormat.FormatString = MaskInput;
                    repositoryItemTextEdit.Mask.UseMaskAsDisplayFormat = true;
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;

                case aGridColumnStyle.Ym:
                    {
                        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        string editMask3 = "([0-9][0-9][0-9][0-9])/(0[0-9]|1[0-2])";
                        repositoryItemDateEdit.Mask.EditMask = editMask3;
                        repositoryItemDateEdit.Mask.UseMaskAsDisplayFormat = true;
                        repositoryItemDateEdit.Mask.MaskType = MaskType.RegEx;
                        repositoryItemDateEdit.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                        columnObject.ColumnEdit = repositoryItemDateEdit;
                        columnObject.SortMode = ColumnSortMode.Custom;
                        break;
                    }

                case aGridColumnStyle.YY:
                    {
                        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        string editMask3 = "([0-9][0-9][0-9][0-9])";
                        repositoryItemDateEdit.Mask.EditMask = editMask3;
                        repositoryItemDateEdit.Mask.UseMaskAsDisplayFormat = true;
                        repositoryItemDateEdit.Mask.MaskType = MaskType.RegEx;
                        repositoryItemDateEdit.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                        columnObject.ColumnEdit = repositoryItemDateEdit;
                        columnObject.SortMode = ColumnSortMode.Custom;
                        break;
                    }

                case aGridColumnStyle.Time:
                    {
                        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        string editMask2 = "([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]";
                        repositoryItemTextEdit.Mask.MaskType = MaskType.RegEx;
                        repositoryItemTextEdit.Mask.EditMask = editMask2;
                        repositoryItemTextEdit.Mask.UseMaskAsDisplayFormat = true;
                        columnObject.ColumnEdit = repositoryItemTextEdit;
                        break;
                    }

                case aGridColumnStyle.Time2:
                    columnObject.DisplayFormat.FormatType = FormatType.Custom;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    MaskInput = "hh:mm:ss";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Simple;
                    repositoryItemTextEdit.Mask.EditMask = MaskInput;
                    repositoryItemTextEdit.Mask.UseMaskAsDisplayFormat = true;
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aGridColumnStyle.Time3:
                    {
                        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        string editMask = "([0-9][0-9][0-9][0-9]):[0-5][0-9]:[0-5][0-9]";
                        repositoryItemTextEdit.Mask.MaskType = MaskType.RegEx;
                        repositoryItemTextEdit.Mask.EditMask = editMask;
                        repositoryItemTextEdit.Mask.UseMaskAsDisplayFormat = true;
                        columnObject.ColumnEdit = repositoryItemTextEdit;
                        break;
                    }

                case aGridColumnStyle.CheckBox:
                    {
                        columnObject.DisplayFormat.FormatType = FormatType.None;
                        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
                        repositoryItemCheckEdit.ValueChecked = "Y";
                        repositoryItemCheckEdit.ValueUnchecked = "N";
                        repositoryItemCheckEdit.ValueGrayed = "";
                        columnObject.ColumnEdit = repositoryItemCheckEdit;
                        ShowHeaderCheckBox = true;
                        repositoryItemCheckEdit.CheckedChanged += _repChkYN_CheckedChanged;
                        break;
                    }

                case aGridColumnStyle.Picture:
                    columnObject.UnboundType = UnboundColumnType.Object;
                    columnObject.ColumnEdit = repositoryItemPictureEdit;
                    break;

                case aGridColumnStyle.D0:
                    columnObject.DisplayFormat.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.DisplayFormat.FormatString = "n0";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n0";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;

                case aGridColumnStyle.D1:
                    columnObject.DisplayFormat.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.DisplayFormat.FormatString = "n1";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n1";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;

                case aGridColumnStyle.D2:
                    columnObject.DisplayFormat.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.DisplayFormat.FormatString = "n2";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n2";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;

                case aGridColumnStyle.D3:
                    columnObject.DisplayFormat.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.DisplayFormat.FormatString = "n3";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n3";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;

                case aGridColumnStyle.D4:
                    columnObject.DisplayFormat.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.DisplayFormat.FormatString = "n4";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n4";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
            }
        }

        //protected void ApplyColumnStyle(aGridColumnStyle columnStyle, string str_Figure)
        //{
        //    RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
        //    if (columnStyle == aGridColumnStyle.LookUpEdit)
        //    {
        //        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
        //        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
        //        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
        //        repositoryItemLookUpEdit = SetGridLookUpItem(CH.GetCode(str_Figure, addEmptyLine: true));
        //        columnObject.ColumnEdit = repositoryItemLookUpEdit;
        //    }
        //}

        protected void ApplyColumnStyle(aGridColumnStyle columnStyle, string[,] data)
        {
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            if (columnStyle == aGridColumnStyle.LookUpEdit)
            {
                columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("CODE", typeof(string)));
                dataTable.Columns.Add(new DataColumn("NAME", typeof(string)));

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DataRow dr = dataTable.NewRow();
                    dr["CODE"] = data[i, 0];
                    dr["NAME"] = data[i, 1];
                    dataTable.Rows.Add(dr);
                }

                repositoryItemLookUpEdit = SetGridLookUpItem(dataTable);
                columnObject.ColumnEdit = repositoryItemLookUpEdit;
            }
        }

        protected void ApplyColumnStyle(aGridColumnStyle columnStyle, DataTable dataTable)
        {
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            if (columnStyle == aGridColumnStyle.LookUpEdit)
            {
                columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                repositoryItemLookUpEdit = SetGridLookUpItem(dataTable);
                columnObject.ColumnEdit = repositoryItemLookUpEdit;
            }
        }

        private static void edit_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            string text = Convert.ToString(e.Value);
            if (DateTime.TryParse(text, out var result))
            {
                e.DisplayText = result.ToString("yyyy\\/MM\\/dd");
            }
            else
            {
                e.DisplayText = ConvertToDateTimeType(text);
            }
        }

        private static string ConvertToDateTimeType(string value)
        {
            if (value != "" && value.Length == 8)
            {
                string text = value.Substring(0, 4);
                string text2 = value.Substring(4, 2);
                string text3 = value.Substring(6, 2);
                return text + "/" + text2 + "/" + text3;
            }

            return "";
        }

        private static void riItemDateEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DateEdit dateEdit = sender as DateEdit;
                dateEdit.EditValue = null;
                e.Handled = true;
            }
        }

        private void _repChkYN_CheckedChanged(object sender, EventArgs e)
        {
            gridView.SetFocusedRowCellValue(columnObject.ColumnEditName, ((CheckEdit)sender).Checked ? "Y" : "N");
        }

        public static RepositoryItemLookUpEdit SetGridLookUpItem(DataTable dt)
        {
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dt;
            repositoryItemLookUpEdit.ValueMember = dt.Columns[0].ToString();
            repositoryItemLookUpEdit.DisplayMember = dt.Columns[1].ToString();
            repositoryItemLookUpEdit.NullText = string.Empty;
            repositoryItemLookUpEdit.ShowNullValuePromptWhenFocused = true;
            repositoryItemLookUpEdit.ShowLines = false;
            repositoryItemLookUpEdit.ShowHeader = false;
            repositoryItemLookUpEdit.ShowFooter = false;
            repositoryItemLookUpEdit.UseDropDownRowsAsMaxCount = true;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.PopupFormMinSize = new Size(50, 50);
            repositoryItemLookUpEdit.PopupResizeMode = ResizeMode.LiveResize;
            repositoryItemLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            repositoryItemLookUpEdit.Columns.AddRange(new LookUpColumnInfo[1]
            {
            new LookUpColumnInfo(dt.Columns[1].ToString())
            });
            return repositoryItemLookUpEdit;
        }
    }


    public static void SetGridStyle(CHGrid gridControl, bool _allowSort, bool _showSum)
    {
        if (!(gridControl.MainView is GridView gridView))
        {
            return;
        }


        string[] array2 = new string[1] { "QT" };
        if (_showSum)
        {
            gridView.OptionsView.ShowFooter = true;
            gridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;
        }

        for (int i = 0; i < gridView.Columns.Count; i++)
        {
            gridView.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            if (Array.IndexOf(array2, gridView.Columns[i].FieldName.Substring(0, 2)) >= 0 && _showSum)
            {
                GridGroupSummaryItem gridGroupSummaryItem = new GridGroupSummaryItem();
                gridGroupSummaryItem.FieldName = gridView.Columns[i].FieldName;
                gridGroupSummaryItem.SummaryType = SummaryItemType.Sum;
                gridGroupSummaryItem.DisplayFormat = "{0:" + gridView.Columns[i].DisplayFormat.FormatString + "}";
                gridGroupSummaryItem.ShowInGroupColumnFooter = gridView.Columns[gridGroupSummaryItem.FieldName];
                gridView.GroupSummary.Add(gridGroupSummaryItem);
                gridView.Columns[i].Summary.Add(SummaryItemType.Sum, gridView.Columns[i].FieldName, "{0:" + gridView.Columns[i].DisplayFormat.FormatString + "}");
            }
        }

        if (gridView.GetType() == typeof(BandedGridView))
        {
            gridView.OptionsView.ShowColumnHeaders = false;
            gridView.OptionsView.ColumnAutoWidth = false;
        }

        gridView.OptionsView.ShowGroupPanel = false;
        gridView.OptionsView.ColumnAutoWidth = false;
        gridView.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
        gridView.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
        gridView.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        gridView.OptionsCustomization.AllowSort = true;
        gridView.OptionsClipboard.ClipboardMode = ClipboardMode.Formatted;
        gridView.OptionsSelection.MultiSelect = true;
        gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        gridView.OptionsView.ShowFooter = _showSum;
        gridView.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
        gridView.OptionsBehavior.AutoExpandAllGroups = true;
        gridView.IndicatorWidth = 29;
        if (gridView.GetType() == typeof(BandedGridView))
        {
            gridView.OptionsView.ShowColumnHeaders = false;
        }

        //DataTable dataTable = new DataTable();
        //dataTable.TableName = "Table";
        //for (int j = 0; j < gridView.Columns.Count; j++)
        //{
        //    if (!dataTable.Columns.Contains(gridView.Columns[j].FieldName))
        //    {
        //        DataColumn dataColumn = new DataColumn(gridView.Columns[j].FieldName);

        //        if (gridView.Columns[j].FieldName.Substring(0, 2) == "TM")
        //        {
        //            dataColumn.DataType = typeof(DateTime);
        //        }
        //        else
        //        {
        //            dataColumn.DataType = gridView.Columns[j].ColumnType;
        //        }

        //        dataTable.Columns.Add(dataColumn);

        //        if (gridView.Columns[j].Tag != null && !dataTable.Columns.Contains(gridView.Columns[j].FieldName.Replace("CD_", "NM_")))
        //        {
        //            dataColumn = new DataColumn(gridView.Columns[j].FieldName.Replace("CD_", "NM_"));
        //            dataColumn.DataType = gridView.Columns[j].ColumnType;
        //            dataTable.Columns.Add(dataColumn);
        //        }
        //    }
        //}

        //_DataBind(gridControl, dataTable);
        if (gridControl.Name.Length > 4 && int.TryParse(gridControl.Name.Substring(5, 1), out var _) && gridControl.SEQ != -1)
        {
            gridControl.SEQ = Convert.ToInt32(gridControl.Name.Substring(5, 1));
        }

        if (gridView.GetType() == typeof(BandedGridView))
        {
            BandedGridView bandedGridView = gridControl.MainView as BandedGridView;
            bandedGridView.Columns.Add(new BandedGridColumn
            {
                Caption = "CONDITION",
                FieldName = "CONDITION",
                UnboundType = UnboundColumnType.Boolean,
                UnboundExpression = "False",
                Visible = false
            });
        }
        else
        {
            gridView.Columns.Add(new GridColumn
            {
                Caption = "CONDITION",
                FieldName = "CONDITION",
                UnboundType = UnboundColumnType.Boolean,
                UnboundExpression = "False",
                Visible = false
            });
        }
    }

    public static void SetGridStyle(CHGrid gridControl, bool _showGroup, bool _allowSort, bool _showSum, GridMultiSelectMode multiSelectMode)
    {
        if (!(gridControl.MainView is GridView gridView))
        {
            return;
        }
        string[] array2 = new string[1] { "QT" };
        if (_showSum)
        {
            gridView.OptionsView.ShowFooter = true;
            gridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;
        }

        for (int i = 0; i < gridView.Columns.Count; i++)
        {
            gridView.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //if (gridView.Columns[i].AppearanceCell.TextOptions.HAlignment == HorzAlignment.Default)
            //{
            //    if (Array.IndexOf(array, gridView.Columns[i].FieldName.Substring(0, 2)) >= 0)
            //    {
            //        gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //    }
            //    else
            //    {
            //        gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            //    }
            //}

            if (Array.IndexOf(array2, gridView.Columns[i].FieldName.Substring(0, 2)) >= 0 && _showSum)
            {
                GridGroupSummaryItem gridGroupSummaryItem = new GridGroupSummaryItem();
                gridGroupSummaryItem.FieldName = gridView.Columns[i].FieldName;
                gridGroupSummaryItem.SummaryType = SummaryItemType.Sum;
                gridGroupSummaryItem.DisplayFormat = "{0:" + gridView.Columns[i].DisplayFormat.FormatString + "}";
                gridGroupSummaryItem.ShowInGroupColumnFooter = gridView.Columns[gridGroupSummaryItem.FieldName];
                gridView.GroupSummary.Add(gridGroupSummaryItem);
                gridView.Columns[i].Summary.Add(SummaryItemType.Sum, gridView.Columns[i].FieldName, "{0:" + gridView.Columns[i].DisplayFormat.FormatString + "}");
            }
        }

        gridView.OptionsView.ShowGroupPanel = _showGroup;
        gridView.OptionsView.ShowGroupedColumns = true;
        gridView.OptionsView.ColumnAutoWidth = false;
        gridView.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
        gridView.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
        gridView.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        gridView.OptionsCustomization.AllowSort = _allowSort;
        if (_showGroup)
        {
            gridView.OptionsCustomization.AllowSort = true;
        }

        gridView.OptionsView.ShowFooter = _showSum;
        gridView.IndicatorWidth = 29;
        gridView.OptionsNavigation.EnterMoveNextColumn = true;
        gridView.OptionsClipboard.ClipboardMode = ClipboardMode.Formatted;
        gridView.OptionsSelection.MultiSelect = true;
        gridView.OptionsSelection.MultiSelectMode = multiSelectMode;
        gridView.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = false;
        gridView.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
        gridView.VisibleColumns[0].OptionsColumn.AllowSize = false;
        gridView.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
        gridView.OptionsBehavior.AutoExpandAllGroups = true;
        if (gridView.GetType() == typeof(BandedGridView))
        {
            gridView.OptionsView.ShowColumnHeaders = false;
        }

        //DataTable dataTable = new DataTable();
        //dataTable.TableName = "Table";
        //for (int j = 0; j < gridView.Columns.Count; j++)
        //{
        //    if (!dataTable.Columns.Contains(gridView.Columns[j].FieldName))
        //    {
        //        DataColumn dataColumn = new DataColumn(gridView.Columns[j].FieldName);
        //      if (gridView.Columns[j].FieldName.Substring(0, 2) == "TM")
        //        {
        //            dataColumn.DataType = typeof(DateTime);
        //        }
        //        else
        //        {
        //            dataColumn.DataType = gridView.Columns[j].ColumnType;
        //        }

        //        dataTable.Columns.Add(dataColumn);
        //        if (gridView.Columns[j].Tag != null && !dataTable.Columns.Contains(gridView.Columns[j].FieldName.Replace("CD_", "NM_")))
        //        {
        //            dataColumn = new DataColumn(gridView.Columns[j].FieldName.Replace("CD_", "NM_"));
        //            dataColumn.DataType = gridView.Columns[j].ColumnType;
        //            dataTable.Columns.Add(dataColumn);
        //        }
        //    }
        //}

        //_DataBind(gridControl, dataTable);
        if (gridControl.Name.Length > 4 && int.TryParse(gridControl.Name.Substring(5, 1), out var _) && gridControl.SEQ != -1)
        {
            gridControl.SEQ = Convert.ToInt32(gridControl.Name.Substring(5, 1));
        }

        if (gridView.GetType() == typeof(BandedGridView))
        {
            BandedGridView bandedGridView = gridControl.MainView as BandedGridView;
            if (gridView.Columns.ColumnByFieldName("CONDITION") == null)
            {
                bandedGridView.Columns.Add(new BandedGridColumn
                {
                    Caption = "CONDITION",
                    FieldName = "CONDITION",
                    UnboundType = UnboundColumnType.Boolean,
                    UnboundExpression = "False",
                    Visible = false
                });
            }
        }
        else if (gridView.Columns.ColumnByFieldName("CONDITION") == null)
        {
            gridView.Columns.Add(new GridColumn
            {
                Caption = "CONDITION",
                FieldName = "CONDITION",
                UnboundType = UnboundColumnType.Boolean,
                UnboundExpression = "False",
                Visible = false
            });
        }
    }

    public static DataTable GetSelectedValues(GridView view)
    {
        DataTable dataTable = new DataTable();
        dataTable = (view.DataSource as DataView).ToTable().Clone();
        int[] selectedRows = view.GetSelectedRows();
        int[] array = selectedRows;
        foreach (int num in array)
        {
            int rowHandle = selectedRows[num];
            if (!view.IsGroupRow(rowHandle))
            {
                DataRow dataRow = view.GetDataRow(num);
                dataTable.Rows.Add(dataRow.ItemArray);
            }
        }

        return dataTable;
    }

    public static object[] GetSelectedValues(GridView view, string columnName)
    {
        int[] selectedRows = view.GetSelectedRows();
        object[] array = new object[selectedRows.Length];
        int[] array2 = selectedRows;
        foreach (int num in array2)
        {
            int rowHandle = selectedRows[num];
            if (!view.IsGroupRow(rowHandle))
            {
                array[num] = view.GetRowCellValue(rowHandle, columnName);
            }
            else
            {
                array[num] = -1;
            }
        }

        return array;
    }

    public static void SetDecimalPoint(GridControl aGrid, string column)
    {
        SetDecimalPoint(aGrid, column, 0m);
    }

    public static void SetDecimalPoint(GridControl aGrid, string column, decimal dPoint)
    {
        GridView gridView = aGrid.FocusedView as GridView;
        string text = "n" + dPoint;
        if (gridView.Columns[column].DisplayFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
        {
            gridView.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
            RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
            gridView.Columns[column].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns[column].DisplayFormat.FormatString = text;
            repositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemTextEdit.Mask.EditMask = text;
            gridView.Columns[column].ColumnEdit = repositoryItemTextEdit;
        }
    }

    public static void SetDecimalPoint(GridControl aGrid, string[] columns)
    {
        SetDecimalPoint(aGrid, columns, 0m);
    }

    public static void SetDecimalPoint(GridControl aGrid, string[] columns, decimal dPoint)
    {
        GridView gridView = aGrid.FocusedView as GridView;
        string text = "n" + dPoint;
        RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
        for (int i = 0; i < columns.Length; i++)
        {
            gridView.Columns[columns[i]].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns[columns[i]].DisplayFormat.FormatString = text;
            repositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemTextEdit.Mask.EditMask = text;
            gridView.Columns[columns[i]].ColumnEdit = repositoryItemTextEdit;
        }
    }

    public static void SetDecimalPoint(GridView gView, string[] columns, decimal dPoint)
    {
        string text = "n" + dPoint;
        RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
        for (int i = 0; i < columns.Length; i++)
        {
            gView.Columns[columns[i]].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gView.Columns[columns[i]].DisplayFormat.FormatString = text;
            repositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemTextEdit.Mask.EditMask = text;
            gView.Columns[columns[i]].ColumnEdit = repositoryItemTextEdit;
        }
    }

}
