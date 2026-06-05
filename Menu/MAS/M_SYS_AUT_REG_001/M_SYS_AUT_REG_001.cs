using CH.Framework.Win;
using CH.Framework.Win.Controls;
using CH.Grid;
using CH.Helper;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static CH.Helper.aGridHelper;

namespace SYS;

// User registration form
public partial class M_SYS_AUT_REG_001 : CHFormBase
{
    #region ▶ Initialize ----------
    M_SYS_AUT_REG_001_D _D = null;
    public M_SYS_AUT_REG_001()
    {
        InitializeComponent();
        chLayoutPanel1.SetPanelType = CH.Framework.Win.Controls.CHLayoutPanel.PanelType.MAINFORM;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _D = new M_SYS_AUT_REG_001_D();
        InitializeGrid();
        InitializeTree();
        InitializeEvent();

    }

    private void InitializeEvent()
    {
        gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
        Btn_Assign.Click += Btn_Click;
        Btn_Remove.Click += Btn_Click;
    }
    #endregion

    #region ▶ GridView ------------
    private void InitializeGrid()
    {
        SetColumn CD_COMPANY = new SetColumn(gridView1, "CD_COMPANY", "Company Code", 100, false);
        SetColumn NM_COMPANY = new SetColumn(gridView1, "NM_COMPANY", "Company Name", 150, false);
        SetGridStyle(chGrid1, false, false);

        SetColumn CD_USER = new SetColumn(gridView2, "CD_USER", "User Code", 100, false);
        SetColumn NM_USER = new SetColumn(gridView2, "NM_USER", "User Name", 150, false);
        SetColumn FG_ROLE = new SetColumn(gridView2, "FG_ROLE", "Role", CH.Helper.aGridColumnStyle.LookUpEdit, 100, false);
        SetColumn CD_COM = new SetColumn(gridView2, "CD_COM", false);
        SetGridStyle(chGrid2, false, false);
    }
    #endregion

    #region ▶ TreeView ------------

    private void InitializeTree()
    {
        DataTable dt = _D.SearchAvailableMenus(new object[] { "xxxx" });

        // Load both trees
        LoadTree(treeAvailable, dt);
        LoadTree(treeAssigned, dt);

        // 1. Enable Drag on Source Treeb
        treeAvailable.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;

        // 2. Enable Drop on Target Tree
        treeAssigned.AllowDrop = true;
        treeAssigned.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;

        // 3. CRITICAL: Subscribe to the Drag-and-Drop events on the Target Tree
        treeAssigned.DragOver += TreeAssigned_DragOver;
        treeAssigned.DragDrop += TreeAssigned_DragDrop;
        treeAssigned.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
    }

    private void LoadTree(CHTree treeList, DataTable dt)
    {
        treeList.DataSource = dt.Copy().Clone(); // Empty Data
        treeList.KeyFieldName = "CD_MENU";
        treeList.ParentFieldName = "CD_MENU_PARENT";
        treeList.ColumnVislble(new string[] { "FG_TYPE", "CD_MODULE", "NO_POS", "IS_ASSIGNED", "CD_COMPANY" });
        treeList.ColumnReadOnly(new string[] { "NM_MENU" });

        //treeList.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.None;
        treeList.ExpandAll();

    }
    #endregion

    #region ▶ MainButton ----------

    public override void OnSearch()
    {
        try
        {
            base.OnSearch();

            DataTable dtCompanies = _D.SearchCompanies(new object[] { "" });
            DataTable dtUsers = _D.SearchUsers(new object[] { "", "" });
            DataTable dt = _D.SearchAvailableMenus(new object[] { CH.AppContext.User.CompanyCode });

            DataTable dtAvailable = dt.Copy().Clone();
            DataTable dtAssigned = dt.Copy().Clone();


            foreach (DataRow row in dt.Rows)
            {
                // Always show all in available tree
                dtAvailable.ImportRow(row);

                // Only assigned in right tree
                if (Convert.ToBoolean(row["IS_ASSIGNED"]) || A.GetString(row["CD_MENU"]) == "BAS")
                    dtAssigned.ImportRow(row);
            }

            chGrid1.Binding(dtCompanies);
            chGrid2.Binding(dtUsers);
            treeAvailable.Binding(dtAvailable);
            treeAvailable.ExpandAll();
            treeAssigned.Binding(dtAssigned);
            treeAssigned.ExpandAll();

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
            DataTable dtSave = treeAssigned.GetChanges();

            bool isSave = _D.Save(dtSave);

            if (!isSave)
            {
                ShowMessageBox("Save Failed!", CH.Framework.Common.MessageType.Error);
                return;
            }
            treeAssigned.AcceptChanges();
            ShowMessageBox("Save successfully", CH.Framework.Common.MessageType.Information);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    #endregion

    #region ▶ Event ---------------

    private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
        gridView2.ActiveFilterString = "CD_COM = '" + A.GetString(gridView1.GetRowCellValue(e.FocusedRowHandle, "CD_COMPANY")) + "'";

        treeAssigned.ActiveFilterString = "CD_COMPANY = '" + A.GetString(gridView1.GetRowCellValue(e.FocusedRowHandle, "CD_COMPANY")) + "' " +
            "OR CD_MENU = 'BAS'";
    }
    #endregion

    #region ▶ Button --------------

    private void Btn_Click(object sender, EventArgs e)
    {
        CHRoundButton btn = sender as CHRoundButton;
        TreeListNode rootNode;
        TreeListNode endNode;
        try
        {
            switch (btn.Name)
            {
                case "Btn_Assign":
                    rootNode = treeAvailable.FocusedNode;
                    endNode = treeAssigned.FocusedNode;

                    if (rootNode == null || endNode == null) return;



                    foreach (TreeListNode node in treeAvailable.GetNodeList())
                    {
                        if (A.GetString(node.GetValue("CD_MENU")) == "BAS") continue;
                        if ((bool)node.GetValue("IS_ASSIGNED") && node == rootNode)
                        {
                            ShowMessageBox("['" + A.GetString(node.GetValue("NM_MENU")) + "'] already assigned", CH.Framework.Common.MessageType.Information);
                            return;
                        }
                    }

                    //if ((bool)rootNode.GetValue("IS_ASSIGNED"))
                    //{
                    //    ShowMessageBox("['" + A.GetString(rootNode.GetValue("NM_MENU")) + "'] already assigned", CH.Framework.Common.MessageType.Information);
                    //    return;
                    //}


                    treeAssigned.BeginUpdate();
                    if (treeAvailable.IsRootNode(rootNode))
                    {
                        AddTreeListNodeAll(rootNode, endNode);
                        treeAssigned.CollapseAll();
                        treeAssigned.ExpandAll();
                    }
                    else
                    {
                        AddSelectedTreeListNode(treeAvailable, endNode);
                        treeAssigned.CollapseAll();
                        treeAssigned.ExpandAll();
                    }
                    treeAssigned.EndUpdate();
                    treeAssigned.FocusedNode = endNode;
                    break;

                case "Btn_Remove":
                    List<TreeListNode> deleteNode = new List<TreeListNode>();

                    TreeListNode parentNode = treeAssigned.FocusedNode;
                    TreeListNode focusedNode = treeAssigned.FocusedNode;

                    if (treeAssigned.IsRootNode(parentNode))
                    {
                        if (ShowMessageBox("Do you want to delete all menus?", CH.Framework.Common.MessageType.Question) == DialogResult.Yes)
                        {
                            foreach (TreeListNode node in parentNode.Nodes)
                            {
                                deleteNode.Add(node);
                            }
                        }
                    }
                    else
                    {
                        if (focusedNode.HasChildren && ShowMessageBox("Submenu exists.\nAre you sure you want to delete it?", CH.Framework.Common.MessageType.Question) != DialogResult.Yes) return;
                        deleteNode.Add(focusedNode);
                    }

                    if (!deleteNode.Any()) return;

                    treeAssigned.BeginUpdate();
                    treeAvailable.BeginUpdate();
                    try
                    {
                        foreach (TreeListNode nodeToDelete in deleteNode)
                        {
                            // 1. If this node has sub-children, we must unassign them in treeAvailable first
                            ResetAvailableTreeStatus(nodeToDelete);

                            // 2. Safely delete from the assigned tree
                            treeAssigned.DeleteNode(nodeToDelete);
                        }
                    }
                    finally
                    {
                        treeAvailable.EndUpdate();
                        treeAssigned.EndUpdate();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            treeAssigned.EndUpdate();
            HandleException(ex);
        }
    }
    #endregion

    #region ▶ Tree-Logic --------------
    private void TreeAssigned_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
        TreeListNode draggedNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
        if (draggedNode == null) return;

        if (Convert.ToBoolean(draggedNode.GetValue("IS_ASSIGNED")))
        {
            ShowMessageBox("['" + A.GetString(draggedNode.GetValue("NM_MENU")) + "'] already assigned", CH.Framework.Common.MessageType.Information);
            return;
        }

        CHTree targetTree = sender as CHTree;
        System.Drawing.Point clientPoint = targetTree.PointToClient(new System.Drawing.Point(e.X, e.Y));
        TreeListHitInfo hitInfo = targetTree.CalcHitInfo(clientPoint);


        TreeListNode targetParentNode = hitInfo.Node;

        targetTree.BeginUpdate();
        try
        {
            TreeListNode newNode = targetTree.AppendNode(null, targetParentNode);

            newNode.SetValue("CD_COMPANY", A.GetString(gridView1.GetFocusedRowCellValue("CD_COMPANY")));
            newNode.SetValue("CD_MENU", draggedNode.GetValue("CD_MENU"));
            newNode.SetValue("NM_MENU", draggedNode.GetValue("NM_MENU"));
            newNode.SetValue("CD_MENU_PARENT", targetParentNode != null ? targetParentNode.GetValue("CD_MENU") : "");
            newNode.SetValue("CD_MODULE", draggedNode.GetValue("CD_MODULE"));
            newNode.SetValue("FG_TYPE", draggedNode.GetValue("FG_TYPE"));
            newNode.SetValue("NO_POS", draggedNode.GetValue("NO_POS"));
            newNode.SetValue("IS_ASSIGNED", true);

            draggedNode.SetValue("IS_ASSIGNED", true);
            if (targetParentNode != null) targetParentNode.HasChildren = true;
            treeAssigned.FocusedNode = newNode;
            newNode.Expanded = true;

            AddChild(draggedNode, newNode);

        }
        finally
        {
            targetTree.EndUpdate();
        }
    }

    private void TreeAssigned_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(TreeListNode)))
        {
            TreeListNode draggedNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;

            if (draggedNode != null)
            {
                CHTree targetTree = sender as CHTree;

                System.Drawing.Point clientPoint = targetTree.PointToClient(new System.Drawing.Point(e.X, e.Y));
                TreeListHitInfo hitInfo = targetTree.CalcHitInfo(clientPoint);

                TreeListNode targetHoverNode = hitInfo.Node;

                bool isSourceRoot = draggedNode.Level == 0 || A.GetString(draggedNode.GetValue("CD_MENU")) == "BAS";
                bool isAlreadyAssigned = Convert.ToBoolean(draggedNode.GetValue("IS_ASSIGNED"));
                bool targetHasRootAlready = targetTree.Nodes.Count > 0;
                bool droppingIntoEmptySpace = (targetHoverNode == null);

                if (isSourceRoot || isAlreadyAssigned || (targetHasRootAlready && droppingIntoEmptySpace))
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void AddTreeListNodeAll(TreeListNode rootNode, TreeListNode endNode)
    {
        try
        {
            if (ShowMessageBox("Do you want to add all menus?", CH.Framework.Common.MessageType.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                LoadingHelper.StartLoading(this, "Menu adding", "All menu is adding.");

                foreach (TreeListNode node in rootNode.Nodes)
                {
                    if (A.GetString(node.GetValue("CD_MENU")) == "BAS") continue;

                    TreeListNode newChinmdNode = treeAssigned.AppendNode(null, endNode);

                    newChinmdNode.SetValue("CD_COMPANY", A.GetString(gridView1.GetFocusedRowCellValue("CD_COMPANY")));
                    newChinmdNode.SetValue("CD_MENU", node.GetValue("CD_MENU"));
                    newChinmdNode.SetValue("NM_MENU", node.GetValue("NM_MENU"));
                    newChinmdNode.SetValue("CD_MENU_PARENT", endNode.GetValue("CD_MENU"));
                    newChinmdNode.SetValue("CD_MODULE", node.GetValue("CD_MODULE"));
                    newChinmdNode.SetValue("FG_TYPE", node.GetValue("FG_TYPE"));
                    newChinmdNode.SetValue("NO_POS", node.GetValue("NO_POS"));
                    newChinmdNode.SetValue("IS_ASSIGNED", true);

                    node.SetValue("IS_ASSIGNED", true);
                    endNode.HasChildren = true;
                    treeAssigned.FocusedNode = newChinmdNode;
                    newChinmdNode.Expanded = false;

                    AddChild(node, newChinmdNode);
                }
                LoadingHelper.EndLoading();
            }
        }
        catch (Exception Ex)
        {
            LoadingHelper.EndLoading();
            HandleException(Ex);
        }
    }

    private void AddSelectedTreeListNode(TreeList treeList, TreeListNode endNode)
    {
        foreach (TreeListNode node in treeList.GetNodeList())
        {
            if (node.Selected)
            {
                TreeListNode newChinmdNode = treeAssigned.AppendNode(null, endNode);

                newChinmdNode.SetValue("CD_COMPANY", A.GetString(gridView1.GetFocusedRowCellValue("CD_COMPANY")));
                newChinmdNode.SetValue("CD_MENU", node.GetValue("CD_MENU"));
                newChinmdNode.SetValue("NM_MENU", node.GetValue("NM_MENU"));
                newChinmdNode.SetValue("CD_MENU_PARENT", endNode.GetValue("CD_MENU"));
                newChinmdNode.SetValue("CD_MODULE", node.GetValue("CD_MODULE"));
                newChinmdNode.SetValue("FG_TYPE", node.GetValue("FG_TYPE"));
                newChinmdNode.SetValue("NO_POS", node.GetValue("NO_POS"));
                newChinmdNode.SetValue("IS_ASSIGNED", true);

                node.SetValue("IS_ASSIGNED", true);

                endNode.HasChildren = true;
                treeAssigned.FocusedNode = newChinmdNode;
                endNode.Expanded = false;

                AddChild(node, newChinmdNode);
            }
        }
    }

    private void AddChild(TreeListNode node, TreeListNode childNode)
    {
        if (node.HasChildren)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                TreeListNode parentNode = node.Nodes[i];

                TreeListNode newChinmdNode = treeAssigned.AppendNode(null, childNode);

                newChinmdNode.SetValue("CD_COMPANY", A.GetString(gridView1.GetFocusedRowCellValue("CD_COMPANY")));
                newChinmdNode.SetValue("CD_MENU", parentNode.GetValue("CD_MENU"));
                newChinmdNode.SetValue("NM_MENU", parentNode.GetValue("NM_MENU"));
                newChinmdNode.SetValue("CD_MENU_PARENT", childNode.GetValue("CD_MENU"));
                newChinmdNode.SetValue("CD_MODULE", parentNode.GetValue("CD_MODULE"));
                newChinmdNode.SetValue("FG_TYPE", parentNode.GetValue("FG_TYPE"));
                newChinmdNode.SetValue("NO_POS", parentNode.GetValue("NO_POS"));
                newChinmdNode.SetValue("IS_ASSIGNED", true);

                parentNode.SetValue("IS_ASSIGNED", true);
                childNode.HasChildren = true;
                treeAssigned.FocusedNode = newChinmdNode;

                AddChild(parentNode, newChinmdNode);
            }
        }
    }

    private void ResetAvailableTreeStatus(TreeListNode assignedNode)
    {
        // Find the matching node in treeAvailable using the unique CD_MENU key
        object menuCode = assignedNode.GetValue("CD_MENU");
        TreeListNode availableNode = treeAvailable.FindNodeByFieldValue("CD_MENU", menuCode);

        if (availableNode != null)
        {
            // Reset the assignment state back to false
            availableNode.SetValue("IS_ASSIGNED", false);
        }

        // Recursively handle all nested child nodes down this branch
        foreach (TreeListNode childNode in assignedNode.Nodes)
        {
            ResetAvailableTreeStatus(childNode);
        }
    }
    #endregion
}
