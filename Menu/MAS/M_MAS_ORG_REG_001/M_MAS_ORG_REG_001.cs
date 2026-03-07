using CH.Framework.Win;
using System;
using System.Data;
using static CH.Helper.aGridHelper;

namespace MAS;

// User registration form
public partial class M_MAS_ORG_REG_001 : CHFormBase
{
    #region ▶ Initialize ----------
    M_MAS_ORG_REG_001_D _D = null;
    public M_MAS_ORG_REG_001()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _D = new M_MAS_ORG_REG_001_D();
        InitializeGrid();
        InitializeEvent();

    }

    private void InitializeEvent()
    {

    }
    #endregion

    #region ▶ GridView ------------
    private void InitializeGrid()
    {
        SetColumn CD_COM = new SetColumn(gridView1, "CD_MENU", "Company Code", 150, true);
        SetColumn CD_USER = new SetColumn(gridView1, "CD_USER", "User Code", 150, true);
        SetColumn NM_USER = new SetColumn(gridView1, "NM_USER", "User Name", 150, true);
        SetColumn DT_REG = new SetColumn(gridView1, "DT_REG", false);
        SetColumn DC_EMAIL = new SetColumn(gridView1, "DC_EMAIL", false);

        SetColumn DC_ADDRESS1 = new SetColumn(gridView1, "DC_ADDRESS1", false);
        SetColumn DC_ADDRESS2 = new SetColumn(gridView1, "DC_ADDRESS2", false);
        SetColumn NO_TEL = new SetColumn(gridView1, "NO_TEL", false);
        SetColumn YN_ACTIVE = new SetColumn(gridView1, "YN_ACTIVE", false);
        SetColumn FG_ROLE = new SetColumn(gridView1, "FG_ROLE", false);
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
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    #endregion

    #region ▶ Event ---------------

    #endregion

    #region ▶ Method --------------

    #endregion
}
