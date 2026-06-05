using CH.Grid;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Helper;
[SupportedOSPlatform("windows")]
public static class TreeListHelper
{
    public enum aTreeListColumnStyle
    {
        Default,
        Text,
        Numeric,
        Date,
        DateFull,
        Time,
        Time2,
        CheckBox,
        SingleDropDown,
        SingleColumnDropDown,
        MultiColumnDropDown,
        DropDownCalendar,
        EditPopup,
        Button,
        Quantity,
        Price,
        Amount,
        Amount_K,
        Rate,
        Ym,
        Price_K,
        Res,
        Biz,
        LookUpEdit,
        MemoEdit,
        Qt,
        Am,
        Am_K,
        Um,
        Um_K,
        Rt_Exch,
        Rt_Discount,
        Rt_Statistics,
        Dc_item_spec,
        YY,
        Picture,
        Time3,
        D0,
        D1,
        D2,
        D3,
        D4
    }

    public class TreeListCheckHelper
    {
        public TreeListCheckHelper(TreeList treeList)
        {
            treeList.OptionsSelection.MultiSelect = true;
            treeList.OptionsView.ShowCheckBoxes = true;
            treeList.OptionsBehavior.AllowIndeterminateCheckState = false;
            treeList.BeforeCheckNode += treeList1_BeforeCheckNode;
        }

        private bool AnyChildIsChecked(TreeListNode node)
        {
            foreach (TreeListNode childNode in node.Nodes)
                if (childNode.CheckState == CheckState.Checked)
                    return true;
            return false;
        }
        private void treeList1_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node.Checked)
                node.UncheckAll();
            else node.CheckAll();
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                node.CheckState = AnyChildIsChecked(node) ? CheckState.Checked : CheckState.Unchecked;
            }
        }
    }

    public class SetTreeColumn
    {
        protected TreeList treeList;

        protected TreeListColumn columnObject;

        private bool _AutoSearch;

        private string _str_Figure;

        private string _str_Table;

        private int _Width;

        private bool _Editable = false;

        private bool _Visible = true;

        private string _MaskInput;

        private string _FormatString;

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
                if (!value)
                {
                    columnObject.AppearanceCell.ForeColor = ColorTranslator.FromHtml("#808080");
                }
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

        public string MaskInput
        {
            get
            {
                return _MaskInput;
            }
            set
            {
                _MaskInput = value;
                columnObject.Format.FormatType = FormatType.Custom;
                columnObject.Format.FormatString = value;
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
                columnObject.Format.FormatType = FormatType.Custom;
                columnObject.Format.FormatString = value;
            }
        }

        public SetTreeColumn(TreeList treeList, string columnField, bool visible)
        {
            this.treeList = treeList;
            columnField = columnField.Replace(" ", "");
            columnObject = treeList.Columns.AddVisible(columnField);
            Visible = visible;
            treeList.Columns.Add(columnObject);
        }

        public SetTreeColumn(TreeList treeList, string columnField, string columnTitle, int columnWidth, bool editable)
        {
            this.treeList = treeList;
            columnField = columnField.Replace(" ", "");
            columnObject = treeList.Columns.AddVisible(columnField);
            columnObject.Caption = columnTitle;
            Width = columnWidth;
            Editable = editable;
        }

        public SetTreeColumn(TreeList treeList, string columnField, string columnTitle, int columnWidth, bool editable, bool visible)
        {
            this.treeList = treeList;
            columnField = columnField.Replace(" ", "");
            columnObject = treeList.Columns.AddVisible(columnField);
            columnObject.Caption = columnTitle;
            Width = columnWidth;
            Editable = editable;
            Visible = visible;
        }

        public SetTreeColumn(TreeList treeList, string columnField, string columnTitle, aTreeListColumnStyle columnStyle, int columnWidth, bool editable)
        {
            this.treeList = treeList;
            columnField = columnField.Replace(" ", "");
            columnObject = treeList.Columns.AddVisible(columnField);
            columnObject.Caption = columnTitle;
            Width = columnWidth;
            Editable = editable;
            ApplyColumnStyle(columnStyle);
        }

        public SetTreeColumn(TreeList treeList, string columnField, string columnTitle, aTreeListColumnStyle columnStyle, int columnWidth, bool editable, bool visible)
        {
            this.treeList = treeList;
            columnField = columnField.Replace(" ", "");
            columnObject = treeList.Columns.AddVisible(columnField);
            columnObject.Caption = columnTitle;
            Width = columnWidth;
            Editable = editable;
            Visible = visible;
            ApplyColumnStyle(columnStyle);
        }

        //public SetTreeColumn(TreeList treeList, string columnField, string columnTitle, aTreeListColumnStyle columnStyle, int columnWidth, bool editable, string str_Figure)
        //{
        //    this.treeList = treeList;
        //    columnField = columnField.Replace(" ", "");
        //    columnObject = treeList.Columns.AddVisible(columnField);
        //    columnObject.Caption = columnTitle;
        //    Width = columnWidth;
        //    Editable = editable;
        //    columnObject.OptionsColumn.FixedWidth = true;
        //    ApplyColumnStyle(columnStyle, str_Figure);
        //}

        public SetTreeColumn(TreeList treeList, string columnField, string columnTitle, aTreeListColumnStyle columnStyle, int columnWidth, bool editable, DataTable dataTable)
        {
            this.treeList = treeList;
            columnField = columnField.Replace(" ", "");
            columnObject = treeList.Columns.AddVisible(columnField);
            columnObject.Caption = columnTitle;
            Width = columnWidth;
            Editable = editable;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(columnStyle, dataTable);
        }


        public SetTreeColumn(TreeList treeList, string columnField, string columnTitle, aTreeListColumnStyle columnStyle, int columnWidth, bool editable, string popUpID, string str_Table)
        {
            this.treeList = treeList;
            columnField = columnField.Replace(" ", "");
            columnObject = treeList.Columns.AddVisible(columnField);
            Width = columnWidth;
            Editable = editable;
            columnObject.OptionsColumn.FixedWidth = true;
            ApplyColumnStyle(columnStyle, popUpID, (str_Table == "") ? "" : str_Table, null, AutoSearch: true);
        }

        protected void ApplyColumnStyle(aTreeListColumnStyle columnStyle)
        {
            RepositoryItemButtonEdit repositoryItemButtonEdit = new RepositoryItemButtonEdit();
            RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
            RepositoryItemMemoEdit columnEdit = new RepositoryItemMemoEdit();
            RepositoryItemDateEdit repositoryItemDateEdit = new RepositoryItemDateEdit();
            RepositoryItemTimeSpanEdit repositoryItemTimeSpanEdit = new RepositoryItemTimeSpanEdit();
            RepositoryItemPictureEdit repositoryItemPictureEdit = new RepositoryItemPictureEdit();
            switch (columnStyle)
            {
                case aTreeListColumnStyle.Default:
                    columnObject.Format.FormatType = FormatType.None;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    break;
                case aTreeListColumnStyle.Text:
                    columnObject.Format.FormatType = FormatType.None;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    break;
                case aTreeListColumnStyle.MemoEdit:
                    columnObject.Format.FormatType = FormatType.None;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.ColumnEdit = columnEdit;
                    break;
                case aTreeListColumnStyle.Numeric:
                    columnObject.Format.FormatType = FormatType.Custom;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.Format.FormatString = FormatString;
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = FormatString;
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.Date:
                    {
                        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        string editMask3 = "([0-9][0-9][0-9][0-9])/(0[0-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])";
                        repositoryItemDateEdit.Mask.EditMask = editMask3;
                        repositoryItemDateEdit.Mask.UseMaskAsDisplayFormat = true;
                        repositoryItemDateEdit.Mask.MaskType = MaskType.RegEx;
                        columnObject.ColumnEdit = repositoryItemDateEdit;
                        columnObject.SortMode = ColumnSortMode.Custom;
                        repositoryItemDateEdit.CustomDisplayText += edit_CustomDisplayText;
                        break;
                    }
                case aTreeListColumnStyle.DateFull:
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    repositoryItemTextEdit.DisplayFormat.FormatType = FormatType.DateTime;
                    repositoryItemTextEdit.DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss";
                    repositoryItemTextEdit.Mask.UseMaskAsDisplayFormat = true;
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.Time:
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
                case aTreeListColumnStyle.Time2:
                    columnObject.Format.FormatType = FormatType.Custom;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    MaskInput = "hh:mm:ss";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Simple;
                    repositoryItemTextEdit.Mask.EditMask = MaskInput;
                    repositoryItemTextEdit.Mask.UseMaskAsDisplayFormat = true;
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.Time3:
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
                case aTreeListColumnStyle.D0:
                    columnObject.Format.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.Format.FormatString = "n0";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n0";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.D1:
                    columnObject.Format.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.Format.FormatString = "n1";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n1";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.D2:
                    columnObject.Format.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.Format.FormatString = "n2";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n2";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.D3:
                    columnObject.Format.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.Format.FormatString = "n3";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n3";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.D4:
                    columnObject.Format.FormatType = FormatType.Numeric;
                    columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    columnObject.Format.FormatString = "n4";
                    repositoryItemTextEdit.Mask.MaskType = MaskType.Numeric;
                    repositoryItemTextEdit.Mask.EditMask = "n4";
                    columnObject.ColumnEdit = repositoryItemTextEdit;
                    break;
                case aTreeListColumnStyle.CheckBox:
                case aTreeListColumnStyle.SingleDropDown:
                case aTreeListColumnStyle.SingleColumnDropDown:
                case aTreeListColumnStyle.MultiColumnDropDown:
                case aTreeListColumnStyle.DropDownCalendar:
                case aTreeListColumnStyle.EditPopup:
                case aTreeListColumnStyle.Button:
                case aTreeListColumnStyle.Ym:
                case aTreeListColumnStyle.Res:
                case aTreeListColumnStyle.Biz:
                case aTreeListColumnStyle.LookUpEdit:
                case aTreeListColumnStyle.YY:
                case aTreeListColumnStyle.Picture:
                    break;
            }
        }

        //protected void ApplyColumnStyle(aTreeListColumnStyle columnStyle, string str_Figure)
        //{
        //    RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
        //    _str_Figure = str_Figure;
        //    if (columnStyle == aTreeListColumnStyle.LookUpEdit)
        //    {
        //        columnObject.Format.FormatType = FormatType.None;
        //        columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
        //        columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
        //        repositoryItemLookUpEdit = SetTreeListLookUpItem(CH.GetCode(str_Figure, addEmptyLine: true));
        //        columnObject.ColumnEdit = repositoryItemLookUpEdit;
        //    }
        //}

        protected void ApplyColumnStyle(aTreeListColumnStyle columnStyle, DataTable dataTable)
        {
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            if (columnStyle == aTreeListColumnStyle.LookUpEdit)
            {
                columnObject.Format.FormatType = DevExpress.Utils.FormatType.None;
                columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                repositoryItemLookUpEdit = SetTreeListLookUpItem(dataTable);
                columnObject.ColumnEdit = repositoryItemLookUpEdit;
            }
        }

        protected void ApplyColumnStyle(aTreeListColumnStyle columnStyle, string popUpID, string str_Table, Hashtable SearchCondition, bool AutoSearch)
        {
            _AutoSearch = AutoSearch;
            _str_Table = str_Table;
            if (columnStyle == aTreeListColumnStyle.EditPopup)
            {
                columnObject.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                columnObject.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                columnObject.Tag = "User|" + popUpID + "|" + str_Table;
            }
        }
    }

    public static void SetTreeListStyle(CHTreelist treeControl, bool _allowSort, bool _showSum)
    {
        if (treeControl == null)
        {
            return;
        }

        string[] array = new string[] { "QT" };
        string[] array2 = new string[] { "QT" };
        for (int i = 0; i < treeControl.Columns.Count; i++)
        {
            treeControl.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            if (treeControl.Columns[i].AppearanceCell.TextOptions.HAlignment == HorzAlignment.Default)
            {
                if (Array.IndexOf(array, treeControl.Columns[i].FieldName.Substring(0, 2)) >= 0)
                {
                    treeControl.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                }
                else
                {
                    treeControl.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                }
            }

            if (Array.IndexOf(array2, treeControl.Columns[i].FieldName.Substring(0, 2)) >= 0 && _showSum)
            {
                TreeListColumn treeListColumn = treeControl.Columns[treeControl.Columns[i].FieldName];
                treeListColumn.AllNodesSummary = true;
                treeListColumn.SummaryFooterStrFormat = "{0:" + treeControl.Columns[i].Format.FormatString + "}";
                treeListColumn.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            }
        }

        treeControl.OptionsView.AutoWidth = false;
        treeControl.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
        treeControl.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        treeControl.OptionsCustomization.AllowSort = true;
        treeControl.OptionsClipboard.ClipboardMode = ClipboardMode.Formatted;
        treeControl.OptionsSelection.MultiSelect = true;
        treeControl.OptionsView.ShowSummaryFooter = _showSum;
        treeControl.IndicatorWidth = 29;
        DataTable dataTable = new DataTable();
        dataTable.TableName = "Table";
        for (int j = 0; j < treeControl.Columns.Count; j++)
        {
            if (!dataTable.Columns.Contains(treeControl.Columns[j].FieldName))
            {
                DataColumn dataColumn = new DataColumn(treeControl.Columns[j].FieldName);
                if (Array.IndexOf(array, treeControl.Columns[j].FieldName.Substring(0, 2)) >= 0)
                {
                    dataColumn.DataType = typeof(decimal);
                }
                else
                {
                    dataColumn.DataType = treeControl.Columns[j].ColumnType;
                }

                if (Array.IndexOf(array, dataColumn.ColumnName.Substring(0, 2)) >= 0)
                {
                    dataColumn.DefaultValue = 0;
                }

                dataTable.Columns.Add(dataColumn);
                if (treeControl.Columns[j].Tag != null && !dataTable.Columns.Contains(treeControl.Columns[j].FieldName.Replace("CD_", "NM_")))
                {
                    dataColumn = new DataColumn(treeControl.Columns[j].FieldName.Replace("CD_", "NM_"));
                    dataColumn.DataType = treeControl.Columns[j].ColumnType;
                    dataTable.Columns.Add(dataColumn);
                }
            }
        }

        _DataBind(treeControl, dataTable);
        if (treeControl.Name.Length > 4 && int.TryParse(treeControl.Name.Substring(5, 1), out var _) && treeControl.SEQ != -1)
        {
            treeControl.SEQ = Convert.ToInt32(treeControl.Name.Substring(5, 1));
        }

        treeControl.Columns.Add(new TreeListColumn
        {
            Caption = "CONDITION",
            FieldName = "CONDITION",
            UnboundType = UnboundColumnType.Boolean,
            UnboundExpression = "False",
            Visible = false
        });
    }

    private static void _DataBind(CHTreelist treeControl, object dtData)
    {
        _DataBind(treeControl, dtData, "Table");
    }

    private static void _DataBind(CHTreelist treeControl, object dtData, string dataMember)
    {
        TreeListColumnCollection columns = treeControl.Columns;
        Hashtable hashtable = new Hashtable();
        for (int i = 0; i < columns.Count; i++)
        {
            if (!treeControl.Columns[i].Visible)
            {
                hashtable.Add(treeControl.Columns[i].FieldName, null);
            }
        }

        treeControl.DataSource = dtData;
        if (!(dtData is DataTable))
        {
            treeControl.DataMember = dataMember;
        }

        DataTable dataTable = _GetGridDataTable(dtData, dataMember);
        columns = treeControl.Columns;
        for (int j = 0; j < columns.Count; j++)
        {
            TreeListColumn treeListColumn = columns[j];
            if (!hashtable.Contains(treeListColumn.FieldName))
            {
                treeListColumn.Visible = true;
            }

            if (dataTable != null && dataTable.Columns.Contains(treeListColumn.FieldName))
            {
                dataTable.Columns[treeListColumn.FieldName].AllowDBNull = true;
                dataTable.Columns[treeListColumn.FieldName].ReadOnly = false;
            }
        }

        hashtable = null;
        columns = null;
        TreeList treeList = new TreeList();
        treeList = treeControl;
        InitEvent(treeList);
        treeList.Update();
    }

    private static void InitEvent(TreeList tList)
    {
        tList.KeyDown += treeList_KeyDown;
    }

    private static void treeList_KeyDown(object sender, KeyEventArgs e)
    {
        TreeList treeList = sender as TreeList;
        if (e.KeyCode != Keys.V || !e.Control)
        {
            return;
        }

        foreach (TreeListCell selectedCell in treeList.GetSelectedCells())
        {
            treeList.SetRowCellValue(selectedCell.Node, treeList.FocusedColumn, treeList.GetRowCellValue(selectedCell.Node, selectedCell.Column));
        }

        e.Handled = true;
        e.SuppressKeyPress = true;
    }

    private static DataTable _GetGridDataTable(object dataSource, string dataMember)
    {
        if (dataSource is DataTable)
        {
            return (DataTable)dataSource;
        }

        if (dataSource is DataViewManager)
        {
            if (dataMember.Contains("."))
            {
                return ((DataViewManager)dataSource).DataSet.Relations[dataMember.Substring(dataMember.LastIndexOf(".") + 1)].ChildTable;
            }

            return ((DataViewManager)dataSource).DataSet.Tables[dataMember];
        }

        if (dataSource is DataSet)
        {
            return ((DataSet)dataSource).Tables[dataMember];
        }

        if (dataSource is DataView)
        {
            return ((DataView)dataSource).Table;
        }

        return null;
    }

    public static RepositoryItemLookUpEdit SetTreeListLookUpItem(DataTable dt)
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
}
