using CH.Framework.Common;
using CH.Framework.Win;
using CH.Helper;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Data;

namespace SYS
{
    public partial class M_SYS_MENU_REG_001 : CHFormBase
    {
        M_SYS_MENU_REG_001_D _D = null;
        public M_SYS_MENU_REG_001()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _D = new M_SYS_MENU_REG_001_D();

            string[,] data = new string[,] { { "1", "Test" }, { "2", "Test2" } };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                string code = data[i, 0];
                string name = data[i, 1];
            }

            InitializeTree();
            InitializeControl();

            chTree1.SetBinding(chLayoutPanel2, new object[] { Txt_CdMenu });
        }

        private void InitializeTree()
        {
            DataTable dataTable = _D.Search(new object[] { "xxxx" });

            chTree1.DataSource = dataTable;
            chTree1.KeyFieldName = "CD_MENU";
            chTree1.ParentFieldName = "CD_MENU_PARENT";
            chTree1.ColumnVislble(new string[] { "NM_NETWINDOW", "FG_TYPE", "CD_MODULE", "NO_POS" });
            chTree1.ColumnReadOnly(new string[] { "NM_MENU" });
        }

        private void InitializeControl()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("CODE", typeof(string)),
                new DataColumn("NAME", typeof(string))
            });

            dt.Rows.Add(new object[] { "M", "Menu" });
            dt.Rows.Add(new object[] { "F", "Folder" });

            SetControl ctr = new SetControl();
            ctr.SetCombobox(LookUp_Type, dt);
        }

        public override void OnSearch()
        {
            try
            {
                base.OnSearch();

                DataTable dataTable = _D.Search(new object[] { Txt_MenuName.EditValue });

                chTree1.DataSource = dataTable;

                chTree1.ClearSelection();
                if (chTree1.Nodes.Count > 0)
                {
                    chTree1.SelectNode(chTree1.Nodes[0]);
                    chTree1.FocusedNode = chTree1.Nodes[0];
                }
                chTree1.ExpandAll();
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

                TreeListNode newChildNode = chTree1.AppendNode(new object[] { "", "", "", "", chTree1.FocusedNode.GetValue("CD_MENU"), "F" }, chTree1.FocusedNode);

                chTree1.ExpandAll();
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

                if (chTree1.FocusedNode == chTree1.FocusedNode.RootNode)
                    return;

                if (chTree1.FocusedNode.HasChildren)
                {
                    chTree1.DeleteChildRow(A.GetString(chTree1.FocusedNode.GetValue("CD_MENU")));
                }
                chTree1.DeleteNode(chTree1.FocusedNode);
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
                DataTable dtChanges = chTree1.GetChanges();

                if (dtChanges == null || dtChanges.Rows.Count == 0)
                {
                    ShowMessageBox("NODATA", MessageType.Warning);
                    return;
                }

                bool result = _D.Save(dtChanges);

                if (!result)
                {
                    ShowMessageBox("SAVEFAIL", MessageType.Warning);
                    return;
                }
                ShowMessageBox("SAVESUCCESS", MessageType.Information);
                chTree1.AcceptChanges();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
