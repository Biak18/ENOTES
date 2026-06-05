using CH.Framework.Win;
using CH.Helper;
using System;
using System.Data;
using static CH.Helper.aGridHelper;
using static CH.Helper.TreeListHelper;

namespace SYS;

// User registration form
public partial class M_SYS_AUT_REG_003 : CHFormBase
{
    #region ▶ Initialize ----------
    M_SYS_AUT_REG_003_D _D = null;
    DataTable dtUserMenu;
    public M_SYS_AUT_REG_003()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _D = new M_SYS_AUT_REG_003_D();
        InitializeGrid();
        InitializeTree();
        InitializeEvent();

        // For saving
        dtUserMenu = new DataTable();
        dtUserMenu.Columns.Add("CD_COMPANY", typeof(string));
        dtUserMenu.Columns.Add("CD_USER", typeof(string));
        dtUserMenu.Columns.Add("CD_MENU", typeof(string));
        dtUserMenu.Columns.Add("FG_ACTION", typeof(string));
        dtUserMenu.Columns.Add("CRUD_TYPE", typeof(string));
    }



    private void InitializeEvent()
    {
        gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
        chTreelist1.CellValueChanged += ChTreelist1_CellValueChanged; ;
    }
    #endregion

    #region ▶ GridView ------------
    private void InitializeGrid()
    {
        SetColumn CD_USER = new SetColumn(gridView1, "CD_USER", "User Code", 100, false);
        SetColumn NM_USER = new SetColumn(gridView1, "NM_USER", "User Name", 150, false);
        SetColumn FG_ROLE = new SetColumn(gridView1, "FG_ROLE", "Role", aGridColumnStyle.LookUpEdit, 100, false, new string[,] { { "0", "Employee" }, { "1", "Team Lead" }, { "2", "Manager" }, { "3", "Boss" } });
        SetGridStyle(chGrid1, false, false);
    }
    #endregion

    #region ▶ TreeView ------------

    private void InitializeTree()
    {
        SetTreeColumn NM_MENU = new SetTreeColumn(chTreelist1, "NM_MENU", "Menu Name", 250, false);
        SetTreeColumn IS_ALLOW = new SetTreeColumn(chTreelist1, "IS_ALLOW", "Allow", aTreeListColumnStyle.CheckBox, 100, true);
        SetTreeColumn IS_DENY = new SetTreeColumn(chTreelist1, "IS_DENY", "Not Allow", aTreeListColumnStyle.CheckBox, 80, true);

        SetTreeListStyle(chTreelist1, false, false);
    }

    #endregion

    #region ▶ MainButton ----------

    public override void OnSearch()
    {
        try
        {
            base.OnSearch();

            DataTable dt = _D.SearchMenus(new object[] { CH.AppContext.User.CompanyCode });
            DataTable dtuser = _D.SearchUsers(new object[] { CH.AppContext.User.CompanyCode, "" });

            chGrid1.Binding(dtuser);

            chTreelist1.Binding(dt);
            chTreelist1.ParentFieldName = "CD_MENU_P";
            chTreelist1.KeyFieldName = "CD_MENU_C";
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
            dtUserMenu.Rows.Clear();

            DataTable dataTable = chTreelist1.GetChanges();
            if (dataTable == null) return;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRow.RowState == DataRowState.Deleted) continue;

                string cdUser = A.GetString(dataRow["CD_USER"]);
                string cdMenu = A.GetString(dataRow["CD_MENU"]);

                ProcessTwoColumns(dataRow, cdMenu, cdUser);
            }

            if (dtUserMenu.Rows.Count == 0)
            {
                ShowMessageBox("There are no changes to save.", CH.Framework.Common.MessageType.Warning);
                return;
            }

            bool isSave = _D.Save(dtUserMenu);
            if (!isSave)
            {
                ShowMessageBox("Save Failed!", CH.Framework.Common.MessageType.Error);
                return;
            }

            chTreelist1.AcceptChanges();
            dtUserMenu.AcceptChanges();
            ShowMessageBox("Save successfully", CH.Framework.Common.MessageType.Information);
            OnSearch();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private void ProcessTwoColumns(DataRow row, string cdMenu, string cdUser)
    {
        // Grab UI state current values
        bool currentAllow = A.GetBool(row["IS_ALLOW"]);
        bool currentDeny = A.GetBool(row["IS_DENY"]);

        // Track original states safely using DataRowVersion
        bool originalAllow = false;
        bool originalDeny = false;

        if (row.RowState == DataRowState.Modified)
        {
            originalAllow = A.GetBool(row["IS_ALLOW", DataRowVersion.Original]);
            originalDeny = A.GetBool(row["IS_DENY", DataRowVersion.Original]);
        }
        else
        {
            originalAllow = currentAllow;
            originalDeny = currentDeny;
        }

        bool wasRecordInDb = A.GetBool(row["IS_EXIST"]);

        // If neither checkbox changed, skip out early!
        if (currentAllow == originalAllow && currentDeny == originalDeny) return;

        // --- DECISION TREE MAP ---
        if (currentAllow)
        {
            // User wants to explicitly Allow ('A')
            string crudType = wasRecordInDb ? "U" : "I";
            AddActionRow(crudType, cdMenu, cdUser, "A");
        }
        else if (currentDeny)
        {
            // User wants to explicitly Block ('N')
            string crudType = wasRecordInDb ? "U" : "I";
            AddActionRow(crudType, cdMenu, cdUser, "N");
        }
        else
        {
            // Both boxes are empty now! This means the user wants to remove the override entirely and inherit from role
            if (wasRecordInDb)
            {
                AddActionRow("D", cdMenu, cdUser, ""); // If it's in the DB, DELETE it
            }
        }
    }

    private void AddActionRow(string crudType, string cdMenu, string cdUser, string fgAction)
    {
        DataRow drNew = dtUserMenu.NewRow();
        drNew["CRUD_TYPE"] = crudType;
        drNew["CD_COMPANY"] = CH.AppContext.User.CompanyCode;
        drNew["CD_USER"] = cdUser;
        drNew["CD_MENU"] = cdMenu;
        drNew["FG_ACTION"] = fgAction;
        dtUserMenu.Rows.Add(drNew);
    }
    #endregion

    #region ▶ Event ---------------
    private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
        chTreelist1.ActiveFilterString = "CD_USER = '" + A.GetString(gridView1.GetRowCellValue(e.FocusedRowHandle, "CD_USER")) + "'";
    }

    private void ChTreelist1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
    {
        if (e.Column.FieldName == "IS_ALLOW" && A.GetBool(e.Value) == true)
        {
            chTreelist1.FocusedNode.SetValue("IS_DENY", false);
        }
        else if (e.Column.FieldName == "IS_DENY" && A.GetBool(e.Value) == true)
        {
            chTreelist1.FocusedNode.SetValue("IS_ALLOW", false);
        }
    }
    #endregion

    #region ▶ Button --------------
    #endregion

    #region ▶ Tree-Logic -------------- 
    #endregion
}
