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
    private DataTable _dt = new DataTable();

    private Dictionary<string, Control> _controls = null;

    private object[] _enableObjects;

    private GridView _view = null;

    private int Selected_Row = 0;


    #region TreeBinding
    public SetControlBinding(GridView chGrid, Control container, object[] EnableControlsIfAdded)
    {

    }
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
        if (_dt.Rows.Count <= 0)
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
            }
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
        if (_controls == null || row == null || (row != null && row.RowState == DataRowState.Deleted))
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
    #endregion
}
