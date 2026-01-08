using CH.Framework.Win;
using CH.Helper;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace M_TEST_001
{
    public partial class M_TEST_001 : CHFormBase
    {
        M_TEST_001_D _D = null;
        public M_TEST_001()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _D = new M_TEST_001_D();
            InitializeControl();
        }

        private void InitializeControl()
        {
            //layoutControl2.Controls.Add(chTree1);

            //foreach (Control ctrl in layoutControl2.Controls)
            //{
            //    ctrl.BackColor = CHColor.Control_Normal;
            //}


        }

        void GetData()
        {
            List<MenuItemModel> menuData = new List<MenuItemModel>()
            {
                new MenuItemModel()
                {
                    CD_COM = "ERP",
                    CD_MENU = "M_TEST_001",
                    NM_MENU = "M_TEST_001",
                    NM_NETWINDOW = "M_TEST_REG_001.M_TEST_REG_001",
                    CD_MENU_PARENT = null,
                    FG_TYPE = "F"
                },
                new MenuItemModel()
                {
                    CD_COM = "ERP",
                    CD_MENU = "M_TEST_REG_002",
                    NM_MENU = "M_TEST_REG_002",
                    NM_NETWINDOW = "M_TEST_REG_002.M_TEST_REG_002",
                    CD_MENU_PARENT = "M_TEST_001",
                    FG_TYPE = "M"
                },
                new MenuItemModel()
                {
                    CD_COM = "ERP",
                    CD_MENU = "M_TEST_REG_003",
                    NM_MENU = "M_TEST_REG_003",
                    NM_NETWINDOW = "M_TEST_REG_003.M_TEST_REG_003",
                    CD_MENU_PARENT = "M_TEST_001",
                    FG_TYPE = "M"
                },
                new MenuItemModel()
                {
                    CD_COM = "ERP",
                    CD_MENU = "M_TEST_002",
                    NM_MENU = "M_TEST_002",
                    NM_NETWINDOW = "",
                    CD_MENU_PARENT = null,
                    FG_TYPE = "F"
                },
                new MenuItemModel()
                {
                    CD_COM = "ERP",
                    CD_MENU = "M_TEST_REG_004",
                    NM_MENU = "M_TEST_REG_004",
                    NM_NETWINDOW = "M_TEST_REG_004.M_TEST_REG_004",
                    CD_MENU_PARENT = "M_TEST_002",
                    FG_TYPE = "M"
                }
            };



            chTree1.DataSource = menuData;
            chTree1.KeyFieldName = "CD_MENU";
            chTree1.ParentFieldName = "CD_MENU_PARENT";
            chTree1.ColumnVislble(new string[] { "CD_COM", "NM_NETWINDOW", "FG_TYPE" });
            chTree1.ColumnReadOnly(new string[] { "NM_MENU" });
            chTree1.ExpandAll();
        }

        public override void OnSearch()
        {
            try
            {
                base.OnSearch();

                DataTable dataTable = _D.Search(new object[] { textEdit1.EditValue });

                chTree1.DataSource = dataTable;
                chTree1.KeyFieldName = "CD_MENU";
                chTree1.ParentFieldName = "CD_MENU_PARENT";
                chTree1.ColumnVislble(new string[] { "NM_NETWINDOW", "FG_TYPE", "CD_MODULE", "NO_POS" });
                chTree1.ColumnReadOnly(new string[] { "NM_MENU" });
                chTree1.ExpandAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override void OnAddrow()
        {
            base.OnAddrow();

            TreeListNode newChildNode = chTree1.AppendNode(new object[] { "M_TEST_REG_00" + (chTree1.Nodes.Count + 1), "M_TEST_REG_00" + chTree1.Nodes.Count + 1, "CHAN", "M_TEST_REG_005.dll", chTree1.FocusedNode.GetValue("CD_MENU"), "M" }, chTree1.FocusedNode);

            chTree1.ExpandAll();
        }

        public override void OnDeleteRow()
        {
            base.OnDeleteRow();

            if (chTree1.FocusedNode.HasChildren)
            {
                chTree1.DeleteChildRow(A.GetString(chTree1.FocusedNode.GetValue("CD_MENU")));
            }
            chTree1.DeleteNode(chTree1.FocusedNode);
        }

        public override void OnSave()
        {
            base.OnSave();

            try
            {
                DataTable dtChanges = chTree1.GetChanges();

                if (dtChanges == null || dtChanges.Rows.Count == 0)
                {
                    MessageBox.Show("NODATA");
                    return;
                }

                bool result = _D.Save(dtChanges);

                if (!result)
                {
                    throw new Exception("SAVEFAIL");
                }
                chTree1.AcceptChanges();
            }
            catch
            {

                throw;
            }
        }

        public override void OnPrint()
        {
            base.OnPrint();
            object obj = DBHelper.ExecuteScalar("select nm_menu from sys_menu where cd_menu = 'SN_CUS_REG_001';");
            MessageBox.Show(A.GetString(obj));
        }

        public class MenuItemModel
        {
            public string CD_COM { get; set; }
            public string CD_MENU { get; set; }
            public string NM_MENU { get; set; }
            public string NM_NETWINDOW { get; set; }
            public string CD_MENU_PARENT { get; set; }
            public string FG_TYPE { get; set; }  // M = Menu, F = Form
        }
    }
}
