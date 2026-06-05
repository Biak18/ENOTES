using CH.Framework.Win;
using CH.Helper;
using System;
using System.Data;
using static CH.Helper.TreeListHelper;

namespace SYS;

// User registration form
public partial class M_SYS_AUT_REG_002 : CHFormBase
{
    #region ▶ Initialize ----------
    M_SYS_AUT_REG_002_D _D = null;
    DataTable dtRole;
    public M_SYS_AUT_REG_002()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _D = new M_SYS_AUT_REG_002_D();
        InitializeTree();
        InitializeEvent();

        // For saving
        dtRole = new DataTable();
        dtRole.Columns.Add("FG_ROLE", typeof(string));
        dtRole.Columns.Add("CD_MENU", typeof(string));
        dtRole.Columns.Add("CRUD_TYPE", typeof(string));
    }

    private void InitializeEvent()
    {

    }


    #endregion

    #region ▶ TreeView ------------

    private void InitializeTree()
    {
        SetTreeColumn NM_MENU = new SetTreeColumn(chTreelist1, "NM_MENU", "Menu Name", 250, false);
        SetTreeColumn EMPLOYEE = new SetTreeColumn(chTreelist1, "EMPLOYEE", "Employee", aTreeListColumnStyle.CheckBox, 100, true);
        SetTreeColumn TEAMLEAD = new SetTreeColumn(chTreelist1, "TEAMLEAD", "TeamLead", aTreeListColumnStyle.CheckBox, 100, true);
        SetTreeColumn MANAGER = new SetTreeColumn(chTreelist1, "MANAGER", "Manager", aTreeListColumnStyle.CheckBox, 100, true);
        SetTreeColumn BOSS = new SetTreeColumn(chTreelist1, "BOSS", "Boss", aTreeListColumnStyle.CheckBox, 100, true);

        SetTreeListStyle(chTreelist1, false, false);
    }

    #endregion

    #region ▶ MainButton ----------

    public override void OnSearch()
    {
        try
        {
            base.OnSearch();

            DataTable dt = _D.SearchMenus();
            chTreelist1.Binding(dt);
            chTreelist1.ParentFieldName = "CD_MENU_PARENT";
            chTreelist1.KeyFieldName = "CD_MENU";
            chTreelist1.ExpandAll();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public override void OnAddrow()
    {
        return;
    }

    public override void OnDeleteRow()
    {
        return;
    }

    public override void OnSave()
    {
        try
        {
            base.OnSave();
            DataTable dtSave = chTreelist1.GetChanges();

            if (dtSave == null)
            {
                ShowMessageBox("There are no changes to save.", CH.Framework.Common.MessageType.Warning);
                return;
            }

            dtRole.Rows.Clear();

            foreach (DataRow dataRow in dtSave.Rows)
            {
                string cdMenu = A.GetString(dataRow["CD_MENU"]);

                ProcessRoleChange(dataRow, "EMPLOYEE", "0", cdMenu);
                ProcessRoleChange(dataRow, "TEAMLEAD", "1", cdMenu);
                ProcessRoleChange(dataRow, "MANAGER", "2", cdMenu);
                ProcessRoleChange(dataRow, "BOSS", "3", cdMenu);
            }

            if (dtRole.Rows.Count == 0)
            {
                chTreelist1.AcceptChanges();
                ShowMessageBox("Save successfully", CH.Framework.Common.MessageType.Information);
                return;
            }

            bool isSave = _D.Save(dtRole);

            if (!isSave)
            {
                ShowMessageBox("Save Failed!", CH.Framework.Common.MessageType.Error);
                return;
            }

            chTreelist1.AcceptChanges();
            dtRole.AcceptChanges();
            ShowMessageBox("Save successfully", CH.Framework.Common.MessageType.Information);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private void ProcessRoleChange(DataRow row, string columnName, string role, string cdMenu)
    {
        bool isCurrentChecked = A.GetBool(row[columnName]);
        bool isOriginalChecked = false;

        if (row.RowState == DataRowState.Modified)
        {
            isOriginalChecked = A.GetBool(row[columnName, DataRowVersion.Original]);
        }

        if (!isOriginalChecked && isCurrentChecked)
        {
            AddActionRow("I", role, cdMenu);
        }
        else if (isOriginalChecked && !isCurrentChecked)
        {
            AddActionRow("D", role, cdMenu);
        }
    }

    private void AddActionRow(string crudType, string fgRole, string cdMenu)
    {
        DataRow drNew = dtRole.NewRow();
        drNew["CRUD_TYPE"] = crudType;
        drNew["FG_ROLE"] = fgRole;
        drNew["CD_MENU"] = cdMenu;
        dtRole.Rows.Add(drNew);
    }
    #endregion

    #region ▶ Event ---------------
    #endregion

    #region ▶ Button --------------
    #endregion

    #region ▶ Tree-Logic -------------- 
    #endregion
}
