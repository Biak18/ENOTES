using CH.Framework.Win;
using CH.Helper;
using M_SYS_BAS_REG_001;
using System;
using System.Data;
using static CH.Helper.aGridHelper;

namespace SYS;

// User registration form
public partial class M_SYS_BAS_REG_001 : CHFormBase
{
    #region ▶ Initialize ----------
    M_SYS_BAS_REG_001_D _D = null;
    DataTable dtActive = null;
    DataTable dtRole = null;
    public M_SYS_BAS_REG_001()
    {
        InitializeComponent();
        chLayoutPanel1.SetPanelType = CH.Framework.Win.Controls.CHLayoutPanel.PanelType.MAINFORM;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _D = new M_SYS_BAS_REG_001_D();
        dtActive = CreateDataTable(true);
        dtRole = CreateDataTable();
        InitializeGrid();
        InitializeEvent();

    }

    private void InitializeEvent()
    {
        gridView1.DoubleClick += GridView1_DoubleClick;
        gridView1.CustomColumnDisplayText += GridView1_CustomColumnDisplayText;
    }

    private DataTable CreateDataTable(bool YN = false)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CODE", typeof(string));
        dt.Columns.Add("NAME", typeof(string));
        DataRow dr = null;
        if (YN)
        {
            for (int i = 0; i < 2; i++)
            {
                dr = dt.NewRow();
                dr["CODE"] = i == 0 ? "Y" : "N";
                dr["NAME"] = i == 0 ? "Yes" : "No";
                dt.Rows.Add(dr);
            }
        }
        else
        {
            string[,] str = new string[,] { { "0", "Employee" }, { "1", "Team Lead" }, { "2", "Manager" }, { "3", "Boss" } };

            for (int i = 0; i < str.GetLength(0); i++)
            {
                dr = dt.NewRow();
                dr["CODE"] = str[i, 0];
                dr["NAME"] = str[i, 1];
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    #endregion

    #region ▶ GridView ------------
    private void InitializeGrid()
    {
        SetColumn CD_COM = new SetColumn(gridView1, "CD_COM", "Company Code", 150, true);
        SetColumn CD_USER = new SetColumn(gridView1, "CD_USER", "User Code", 150, true);
        SetColumn NM_USER = new SetColumn(gridView1, "NM_USER", "User Name", 150, true);
        SetColumn DC_PASSWORD = new SetColumn(gridView1, "DC_PASSWORD", "Password", 150, false);
        SetColumn DT_REG = new SetColumn(gridView1, "DT_REG", "Register Date", CH.Helper.aGridColumnStyle.Date, 95, true);
        SetColumn DC_EMAIL = new SetColumn(gridView1, "DC_EMAIL", "Email", CH.Helper.aGridColumnStyle.Text, 110, true);

        SetColumn DC_ADDRESS1 = new SetColumn(gridView1, "DC_ADDRESS1", "Address1", CH.Helper.aGridColumnStyle.Text, 110, true);
        SetColumn DC_ADDRESS2 = new SetColumn(gridView1, "DC_ADDRESS2", "Address1", CH.Helper.aGridColumnStyle.Text, 110, true);
        SetColumn NO_TEL = new SetColumn(gridView1, "NO_TEL", "Tel", CH.Helper.aGridColumnStyle.Text, 110, true);
        SetColumn YN_ACTIVE = new SetColumn(gridView1, "YN_ACTIVE", "Active", CH.Helper.aGridColumnStyle.LookUpEdit, 110, true, dtActive);
        SetColumn FG_ROLE = new SetColumn(gridView1, "FG_ROLE", "Role", CH.Helper.aGridColumnStyle.LookUpEdit, 110, true, dtRole);
        chGrid1.VerifyNotNull = new string[] { "CD_COM", "CD_USER", "NM_USER", "DC_PASSWORD", "DT_REG", "YN_ACTIVE", "FG_ROLE" };
        SetGridStyle(chGrid1, false, false);
    }
    #endregion

    #region ▶ MainButton ----------

    public override void OnSearch()
    {
        try
        {
            base.OnSearch();

            DataTable dataTable = _D.Search(new object[] { "", txt_Search.Text });
            chGrid1.Binding(dataTable);

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public override void OnAddrow()
    {
        try
        {
            base.OnAddrow();
            gridView1.AddNewRow();
            gridView1.UpdateCurrentRow();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public override void OnDeleteRow()
    {
        try
        {
            base.OnDeleteRow();
            gridView1.DeleteRow(gridView1.FocusedRowHandle);

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public override void OnSave()
    {
        try
        {
            base.OnSave();
            int cnt = gridView1.RowCount;
            DataTable dtSave = chGrid1.GetChanges();

            bool isSave = _D.Save(dtSave);

            if (!isSave)
            {
                ShowMessageBox("Save Failed!", CH.Framework.Common.MessageType.Error);
                return;
            }
            chGrid1.AcceptChanges();
            ShowMessageBox("Save successfully", CH.Framework.Common.MessageType.Information);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    #endregion

    #region ▶ Event ---------------

    private void GridView1_DoubleClick(object sender, EventArgs e)
    {
        if (gridView1.FocusedColumn != null)
        {
            if (gridView1.FocusedColumn.FieldName == "DC_PASSWORD")
            {
                //Show password popup
                string dcPassword = A.GetString(gridView1.GetFocusedRowCellValue("DC_PASSWORD"));
                M_SYS_BAS_REG_001_POPUP_01 POPUP_PWD = new M_SYS_BAS_REG_001_POPUP_01();
                POPUP_PWD.OldPassword = dcPassword;
                POPUP_PWD.IsNewUser = string.IsNullOrWhiteSpace(dcPassword);
                if (POPUP_PWD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string rtnNewPwd = POPUP_PWD.ReturnData["newPwd"] as string;
                    if (!string.IsNullOrWhiteSpace(rtnNewPwd))
                    {
                        gridView1.SetFocusedRowCellValue("DC_PASSWORD", rtnNewPwd);
                    }
                }
            }
        }
    }

    private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == "DC_PASSWORD")
        {
            if (string.IsNullOrWhiteSpace(e.DisplayText)) return;
            e.DisplayText = "***********";

        }
    }
    #endregion

    #region ▶ Method --------------

    #endregion
}
