using CH.Framework.Win.Controls;
using CH.Helper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Grid;

[SupportedOSPlatform("windows")]
internal class SetControlBinding
{
    private DataView _dv = new DataView();

    private DataTable _dt = new DataTable();

    private Dictionary<string, Control> _controls = null;

    private object[] _enableObjects;

    private GridView _view = null;

    private int Selected_Row = 0;

    #region GridBinding
    public SetControlBinding(GridView chGrid, Control container, object[] EnableControlsIfAdded)
    {
        _view = chGrid;
        CHGrid chGrid2 = chGrid.GridControl as CHGrid;
        chGrid2.GridMode = "FREEFORM";
        chGrid.DataSourceChanged += ChGrid_DataSourceChanged;
        chGrid.FocusedRowChanged += ChGrid_FocusedRowChanged;
        chGrid.RowCountChanged += ChGrid_RowCountChanged;

        _controls = new Dictionary<string, Control>();

        InitControls(container);
        chGrid.UpdateCurrentRow();

        if (chGrid.DataSource != null)
        {
            _dv = chGrid.DataSource as DataView;
            _dt = _dv.Table;
        }

        InitControlEvent();
        if (EnableControlsIfAdded != null)
        {
            _enableObjects = EnableControlsIfAdded;
        }
    }

    private void ChGrid_RowCountChanged(object sender, EventArgs e)
    {
        GridView gridView = sender as GridView;
        if (gridView.RowCount == 0)
        {
            DataRow valueToControl = null;
            InitControlEventDelete();
            SetValueToControl(valueToControl);
            InitControlEvent();
        }
        else
        {
            InitControlEventDelete();
            SetValueToControl(gridView.GetFocusedDataSourceRowIndex());
            InitControlEvent();
        }
    }

    private void ChGrid_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
        GridView gridView = sender as GridView;
        if (gridView.GetFocusedDataSourceRowIndex() < 0 || gridView.FocusedRowHandle < 0)
        {
            return;
        }

        Selected_Row = gridView.FocusedRowHandle;
        InitControlEventDelete();
        SetValueToControl(Selected_Row);
        InitControlEvent();

        if (gridView.GetDataRow(Selected_Row).RowState == DataRowState.Added)
        {
            if (_enableObjects == null) return;

            object[] enableObjects = _enableObjects;

            for (int i = 0; i < enableObjects.Length; i++)
            {
                Control ctrl = enableObjects[i] as Control;

                switch (ctrl.GetType().Name)
                {
                    case "CHLTextEdit":
                        ((CHLTextEdit)ctrl).ReadOnly = false;
                        break;


                    case "LookUpEdit":
                        ((LookUpEdit)ctrl).ReadOnly = false;
                        break;

                    case "CHLLookupEdit":
                        ((CHLLookupEdit)ctrl).ReadOnly = false;
                        break;

                    case "CHLNumericText":
                        ((CHLNumericText)ctrl).ReadOnly = false;
                        break;

                    default:
                        ctrl.Enabled = true;
                        break;
                }
            }
        }
        else
        {
            if (_enableObjects == null)
                return;

            object[] enableObjects = _enableObjects;

            for (int i = 0; i < enableObjects.Length; i++)
            {
                Control ctrl = enableObjects[i] as Control;

                switch (ctrl.GetType().Name)
                {
                    case "CHLTextEdit":
                        ((CHLTextEdit)ctrl).ReadOnly = true;
                        break;

                    case "LookUpEdit":
                        ((LookUpEdit)ctrl).ReadOnly = true;
                        break;

                    case "CHLLookupEdit":
                        ((CHLLookupEdit)ctrl).ReadOnly = true;
                        break;

                    case "CHLNumericText":
                        ((CHLNumericText)ctrl).ReadOnly = true;
                        break;

                    default:
                        ctrl.Enabled = false;
                        break;
                }
            }
        }
    }

    private void ChGrid_DataSourceChanged(object sender, EventArgs e)
    {
        GridView gridView = sender as GridView;
        Selected_Row = 0;
        InitControlEventDelete();
        _dv = gridView.DataSource as DataView;
        _dt = _dv.Table;
        SetValueToControl(gridView.GetFocusedDataSourceRowIndex());
        InitControlEvent();
    }
    #endregion

    #region TreeBinding
    public SetControlBinding(CHTree chTree, Control container, object[] EnableControlsIfAdded)
    {
        chTree.DataSourceChanged += ChTree_DataSourceChanged;
        chTree.FocusedNodeChanged += ChTree_FocusedNodeChanged;

        _controls = new Dictionary<string, Control>();


        InitControls(container);

        InitControlEvent();
        if (EnableControlsIfAdded != null)
        {
            _enableObjects = EnableControlsIfAdded;
        }
    }

    private void ChTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        _dt = ((CHTree)sender).DataSource as DataTable;
        string keyfield = ((CHTree)sender).KeyFieldName;
        if (_dt.Rows.Count <= 0 || keyfield == "ID")
        {
            return;
        }

        InitControlEventDelete();
        DataRow dataRow = null;
        if (e.Node != null)
        {
            Selected_Row = e.Node.Id;
            string keyvalue = A.GetString(e.Node.GetValue(keyfield));

            dataRow = _dt.AsEnumerable().FirstOrDefault((DataRow r) => r.RowState != DataRowState.Deleted && r[keyfield]?.ToString() == keyvalue);
            if (dataRow != null)
            {
                SetValueToControl(dataRow);
            }
        }

        InitControlEvent();

        /*
        if (dataRow == null) return;

        if (dataRow.RowState == DataRowState.Added)
        {

            if (_enableObjects == null)
                return;

            object[] enableObjects = _enableObjects;

            for (int i = 0; i < enableObjects.Length; i++)
            {
                Control ctrl = enableObjects[i] as Control;

                switch (ctrl.GetType().Name)
                {
                    case "CHLTextEdit":
                        ((CHLTextEdit)ctrl).ReadOnly = false;
                        break;


                    case "LookUpEdit":
                        ((LookUpEdit)ctrl).ReadOnly = false;
                        break;

                    case "CHLLookupEdit":
                        ((CHLLookupEdit)ctrl).ReadOnly = false;
                        break;

                    case "CHLNumericText":
                        ((CHLNumericText)ctrl).ReadOnly = false;
                        break;

                    default:
                        ctrl.Enabled = true;
                        break;
                }
            }
        }
        else
        {
            if (_enableObjects == null)
                return;

            object[] enableObjects = _enableObjects;

            for (int i = 0; i < enableObjects.Length; i++)
            {
                Control ctrl = enableObjects[i] as Control;

                switch (ctrl.GetType().Name)
                {
                    case "CHLTextEdit":
                        ((CHLTextEdit)ctrl).ReadOnly = true;
                        break;

                    case "LookUpEdit":
                        ((LookUpEdit)ctrl).ReadOnly = true;
                        break;

                    case "CHLLookupEdit":
                        ((CHLLookupEdit)ctrl).ReadOnly = true;
                        break;

                    case "CHLNumericText":
                        ((CHLNumericText)ctrl).ReadOnly = true;
                        break;

                    default:
                        ctrl.Enabled = false;
                        break;
                }
            }
        }
        */
    }

    private void ChTree_DataSourceChanged(object sender, System.EventArgs e)
    {
    }
    #endregion

    #region ControlEvent
    private void InitControls(Control container)
    {
        foreach (Control control in container.Controls)
        {
            Control control2 = control;
            if (control.GetType().FullName.Contains("CH.Framework.Win.Controls"))
            {
                control2 = control;
                control2.Tag = control.Tag;
            }

            if (control2 is Panel || control2 is XtraScrollableControl || control2 is PanelControl || control2 is TabControl || control2 is TabPage || control2 is XtraTabControl || control2 is XtraTabPage || /*control2 is aTabControl ||*/ control2 is SplitContainer || control2 is CHLayoutPanel)
            {
                InitControls(control2);
            }
            else
            {
                if (control2.Tag == null || !(control2.Tag.ToString() != "") || control2 is Label || control2 is Button || control2 is TableLayoutPanel)
                {
                    continue;
                }

                if (_controls.ContainsKey(control2.Tag.ToString()))
                {
                    throw new Exception(control2.Name + " The Tag set in the control is the value already set in the other control.\nThe Tag of the controls to bind cannot be duplicated.");
                }

                _controls.Add(control2.Tag.ToString(), control2);

            }
        }
    }


    public void InitControlEvent()
    {
        foreach (Control ctrl in _controls.Values)
        {
            switch (ctrl.GetType().Name)
            {
                case "TextEdit":
                    ((TextEdit)ctrl).TextChanged += Control_Validated;
                    break;

                case "CHLTextEdit":
                    ((CHLTextEdit)ctrl).TextChangedByUser += Control_Validated;
                    ((CHLTextEdit)ctrl).EditValueChangedByUser += Control_Validated;
                    break;


                case "LookUpEdit":
                    ((LookUpEdit)ctrl).EditValueChanged += Control_Validated;
                    break;

                case "CHLLookupEdit":
                    ((CHLLookupEdit)ctrl).EditValueChangedByUser += Control_Validated;
                    break;

                case "CHLNumericText":
                    ((CHLNumericText)ctrl).EditValueChangedByUser += Control_Validated;
                    ((CHLNumericText)ctrl).DecimalValueChangedByUser += Control_Validated;
                    break;

                case "CHLPeriodEdit":
                    string[] array = ((CHPeriodEdit)ctrl).Tag.ToString().Split(';');
                    if (array == null || array.Length != 2)
                    {
                        throw new Exception("Tag properties must be specified in the form of <Start Date Data Column Name; To Date Data Column Name>.");
                    }
                    ((CHPeriodEdit)ctrl).txtDtFrom.Tag = array[0];
                    ((CHPeriodEdit)ctrl).txtDtTo.Tag = array[1];
                    ((CHPeriodEdit)ctrl).txtDtFrom.TextChanged += Control_CodeChanged;
                    ((CHPeriodEdit)ctrl).txtDtTo.TextChanged += Control_CodeChanged;
                    ((CHPeriodEdit)ctrl).txtDtFrom.EditValueChanged += Control_Validated;
                    ((CHPeriodEdit)ctrl).txtDtTo.EditValueChanged += Control_Validated;
                    break;
            }
        }
    }

    public void InitControlEventDelete()
    {
        foreach (Control ctrl in _controls.Values)
        {
            switch (ctrl.GetType().Name)
            {
                case "TextEdit":
                    ((TextEdit)ctrl).TextChanged -= Control_Validated;
                    break;

                case "CHLTextEdit":
                    ((CHLTextEdit)ctrl).TextChangedByUser -= Control_Validated;
                    ((CHLTextEdit)ctrl).EditValueChangedByUser -= Control_Validated;
                    break;


                case "LookUpEdit":
                    ((LookUpEdit)ctrl).EditValueChanged -= Control_Validated;
                    break;

                case "CHLLookupEdit":
                    ((CHLLookupEdit)ctrl).EditValueChangedByUser -= Control_Validated;
                    break;

                case "CHLNumericText":
                    ((CHLNumericText)ctrl).EditValueChangedByUser -= Control_Validated;
                    ((CHLNumericText)ctrl).DecimalValueChangedByUser -= Control_Validated;
                    break;

                case "CHPeriodEdit":
                    ((CHPeriodEdit)ctrl).txtDtFrom.TextChanged -= Control_CodeChanged;
                    ((CHPeriodEdit)ctrl).txtDtTo.TextChanged -= Control_CodeChanged;
                    ((CHPeriodEdit)ctrl).txtDtFrom.EditValueChanged -= Control_Validated;
                    ((CHPeriodEdit)ctrl).txtDtTo.EditValueChanged -= Control_Validated;
                    break;
            }
        }
    }

    private void Control_CodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (!(((TextEdit)sender).Parent.GetType().Name == "CHPeriodEdit") || _dt.Rows.Count <= 0)
            {
                return;
            }

            if (((CHPeriodEdit)((TextEdit)sender).Parent).DtStart == string.Empty)
            {
                string[] array = ((CHPeriodEdit)((TextEdit)sender).Parent).Tag.ToString().Split(';');
                if (array == null || array.Length != 2)
                {
                    throw new Exception("Tag properties must be specified in the form of <Start Date Data Column Name; To Date Data Column Name>.");
                }

                _dt.Rows[Selected_Row][array[0].ToString()] = string.Empty;
                _dt.Rows[Selected_Row][array[1].ToString()] = string.Empty;
                SetValueToDataRow(((TextEdit)sender).Parent);
            }
            else
            {
                string[] array2 = ((CHPeriodEdit)((TextEdit)sender).Parent).Tag.ToString().Split(';');
                if (array2 == null || array2.Length != 2)
                {
                    throw new Exception("Tag properties must be specified in the form of <Start Date Data Column Name; To Date Data Column Name>.");
                }

                _dt.Rows[Selected_Row][array2[0].ToString()] = ((CHPeriodEdit)((TextEdit)sender).Parent).DtStart;
                _dt.Rows[Selected_Row][array2[1].ToString()] = ((CHPeriodEdit)((TextEdit)sender).Parent).DtEnd;
                SetValueToDataRow(((TextEdit)sender).Parent);
            }
        }
        catch
        {

            throw;
        }
    }

    private void Control_Validated(object sender, EventArgs e)
    {
        try
        {
            if (_dt == null || _dt.Rows.Count == 0)
            {
                return;
            }

            switch (((Control)sender).GetType().Name)
            {
                case "TextEdit":
                    if (!((TextEdit)sender).IsModified)
                    {
                        return;
                    }
                    break;

                case "CHLTextEdit":
                    if (!((CHLTextEdit)sender).CHTextEdit.IsModified)
                    {
                        return;
                    }
                    break;


                case "LookUpEdit":
                    if (!((LookUpEdit)sender).IsModified)
                    {
                        return;
                    }
                    break;

                case "CHLLookupEdit":
                    if (!((CHLLookupEdit)sender).CHLookupedit.IsModified)
                    {
                        return;
                    }
                    break;

                case "CHLNumericText":
                    if (!((CHLNumericText)sender).CHNumericText.IsModified)
                    {
                        return;
                    }
                    break;
            }

            SetValueToDataRow(sender);
        }
        catch
        {
            throw;
        }
    }

    private void SetValueToDataRow(object sender)
    {
        if (_dt == null || _dt.Rows.Count == 0)
        {
            return;
        }

        DataRow dataRow = null;
        dataRow = ((_view == null) ? _dt.Rows[Selected_Row] : _view.GetDataRow(Selected_Row));
        if (dataRow == null)
        {
            return;
        }

        if (((Control)sender).Tag == null)
        {
            throw new Exception(((Control)sender).Name + " You must specify the DataTable column name to map to the Control Tag property.");
        }

        switch (((Control)sender).GetType().Name)
        {
            case "TextEdit":
                if (dataRow[((Control)sender).Tag.ToString()].GetType().Name == "Decimal")
                {
                    dataRow[((Control)sender).Tag.ToString()] = ((((Control)sender).Text != "") ? ((Control)sender).Text : "0");
                }
                else
                {
                    dataRow[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
                }
                break;

            case "CHLTextEdit":
                if (dataRow[((Control)sender).Tag.ToString()].GetType().Name == "Decimal")
                {
                    dataRow[((Control)sender).Tag.ToString()] = ((((Control)sender).Text != "") ? ((Control)sender).Text : "0");
                }
                else
                {
                    dataRow[((Control)sender).Tag.ToString()] = ((CHLTextEdit)(Control)sender).EditValue;
                }
                break;

            case "LookUpEdit":
            case "CHLLookupEdit":
                dataRow[((Control)sender).Tag.ToString()] = ((CHLLookupEdit)sender).EditValue;
                break;
        }
    }

    private void SetValueToControl(DataRow row)
    {
        Control control = null;
        if (_controls == null || /*row == null ||*/ (row != null && row.RowState == DataRowState.Deleted))
        {
            return;
        }

        foreach (string key in _controls.Keys)
        {
            control = _controls[key];
            switch (control.GetType().Name)
            {
                case "TextEdit":
                    control.Text = ((row == null) ? string.Empty : A.GetString(row[key]));
                    break;

                case "CHLTextEdit":
                    control.Text = ((row == null) ? string.Empty : A.GetString(row[key]));
                    break;

                case "LookUpEdit":
                    ((LookUpEdit)control).EditValue = ((row == null) ? null : A.GetString(row[key]));
                    break;

                case "CHLLookupEdit":
                    ((CHLLookupEdit)control).EditValue = ((row == null) ? null : A.GetString(row[key]));
                    break;

                case "CHLNumericText":
                    ((CHLNumericText)control).EditValue = ((row == null) ? string.Empty : A.GetString(row[key]));
                    break;
            }
        }
    }

    private void SetValueToControl(int selectedRowNo)
    {
        if (_dt.Rows.Count != 0 && _dt.Rows.Count > selectedRowNo && selectedRowNo >= 0)
        {
            if (_view != null)
            {
                SetValueToControl(_view.GetDataRow(Selected_Row));
            }
            else
            {
                SetValueToControl(_dt.Rows[selectedRowNo]);
            }
        }
    }
    #endregion
}
