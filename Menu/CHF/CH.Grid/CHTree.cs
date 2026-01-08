using CH.Grid.Properties;
using CH.Helper;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CH.Grid;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public class CHTree : TreeList
{
    private DataTable _dt = new DataTable();
    private DataTable data = null;
    private ImageCollection imageCollection_choose = new ImageCollection();
    private TreeList aTree1;
    SetControlBinding setControlBinding = null;
    public CHTree()
    {
        InitializeComponent();
    }
    protected override void InitLayout()
    {
        //if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        //    return;

        base.OptionsView.ShowColumns = false;
        base.OptionsView.ShowIndicator = false;
        base.OptionsView.ShowHorzLines = false;
        base.OptionsView.ShowVertLines = false;
        base.OptionsBehavior.Editable = false;
        base.OptionsSelection.KeepSelectedOnClick = false;
        base.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
        base.OptionsSelection.EnableAppearanceFocusedCell = false;
        base.OptionsSelection.MultiSelect = true;
        base.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
        base.OptionsNavigation.AutoFocusNewNode = true;
        base.Appearance.Row.Font = new Font("Segoe UI", 11f);
        base.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

        imageCollection_choose.AddImage(A.GetBitmap(Resources.Close_16x16));
        imageCollection_choose.AddImage(A.GetBitmap(Resources.Open_16x16));
        imageCollection_choose.AddImage(A.GetBitmap(Resources.Close_Choose_16x16));
        imageCollection_choose.AddImage(A.GetBitmap(Resources.Open_Choose_16x16));
        imageCollection_choose.AddImage(A.GetBitmap(Resources.registration_16x16));
        imageCollection_choose.AddImage(A.GetBitmap(Resources.registration_choose_16x16));
        imageCollection_choose.AddImage(A.GetBitmap(Resources.view_16x16));
        imageCollection_choose.AddImage(A.GetBitmap(Resources.view_choose_16x16));

        base.SelectImageList = imageCollection_choose;
        base.InitLayout();

        /* base.AfterExpand += TreeNode_AfterExpand;
         base.AfterCollapse += TreeNode_AfterCollapse;
         base.BeforeFocusNode += CHTree_BeforeFocusNode;
         base.FocusedNodeChanged += CHTree_FocusedNodeChanged;
        */
        base.GetSelectImage += CHTree_GetSelectImage;
    }

    private void CHTree_GetSelectImage(object sender, GetSelectImageEventArgs e)
    {
        if (e.Node.ParentNode != null)
        {
            //for child nodes

            string @string = A.GetString(e.Node.GetValue("CD_MENU"));

            if (@string.Contains("REG"))
            {
                e.NodeImageIndex = e.Node.IsSelected ? 5 : 4;
                return;
            }
            if (@string.Contains("REP"))
            {
                e.NodeImageIndex = e.Node.IsSelected ? 7 : 6;
                return;
            }
        }
        if (e.Node.Expanded)
        {
            e.NodeImageIndex = e.Node.IsSelected ? 3 : 1;
        }
        else
        {
            e.NodeImageIndex = e.Node.IsSelected ? 2 : 0;
        }
    }

    public void SetBinding(Control container, object[] EnableControlsIfAdded)
    {
        setControlBinding = new SetControlBinding(this, container, EnableControlsIfAdded);
    }

    protected override void InternalNodeChanged(TreeListNode node, TreeListNodes nodes, NodeChangeTypeEnum changeType)
    {
        if (changeType == NodeChangeTypeEnum.User1)
        {
            LayoutChanged();
        }

        base.InternalNodeChanged(node, nodes, changeType);
    }

    private void CHTree_BeforeFocusNode(object sender, NodeEventArgs e)
    {
        try
        {
            CHTree CHTree2 = sender as CHTree;
            if (CHTree2.FocusedNode == null)
            {
                return;
            }

            string @string = A.GetString(CHTree2.FocusedNode.GetValue("CD_MENU"));
            if (CHTree2.FocusedNode.Expanded)
            {
                CHTree2.FocusedNode.SelectImageIndex = 1;
                return;
            }

            CHTree2.FocusedNode.SelectImageIndex = 0;
            if (@string.Contains("REG"))
            {
                CHTree2.FocusedNode.SelectImageIndex = 5;
            }

            if (@string.Contains("REP"))
            {
                CHTree2.FocusedNode.SelectImageIndex = 7;
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void CHTree_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
    {
        try
        {
            if (e.Node == null)
            {
                return;
            }

            if (e.Node.Expanded)
            {
                e.Node.SelectImageIndex = 3;
            }
            else
            {
                string @string = A.GetString(e.Node.GetValue("CD_MENU"));
                e.Node.SelectImageIndex = 2;
                if (@string.Contains("REG"))
                {
                    e.Node.ImageIndex = 4;
                    e.Node.SelectImageIndex = 5;
                }
                else if (@string.Contains("REP"))
                {
                    e.Node.ImageIndex = 6;
                    e.Node.SelectImageIndex = 7;
                }
            }

            foreach (TreeListNode node in e.Node.Nodes)
            {
                if (node.HasChildren)
                {
                    SetNodeImage(node);
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void SetNodeImage(TreeListNode node)
    {
        string @string = A.GetString(node.GetValue("CD_MENU"));
        if (node.Selected && node.Expanded)
        {
            node.SelectImageIndex = 3;
        }

        if (node.Selected && !node.Expanded)
        {
            node.SelectImageIndex = 2;
        }

        if (!node.Selected && node.Expanded)
        {
            node.SelectImageIndex = 1;
        }

        if (!node.Selected && !node.Expanded)
        {
            node.SelectImageIndex = 0;
        }

        if (@string.Contains("REG"))
        {
            node.SelectImageIndex = 5;
            node.ImageIndex = 4;
        }

        if (@string.Contains("REP"))
        {
            node.SelectImageIndex = 7;
            node.ImageIndex = 6;
        }

        if (node.HasChildren && node.NextNode != null)
        {
            SetNodeImage(node.NextNode);
        }
    }

    private void TreeNode_AfterCollapse(object sender, NodeEventArgs e)
    {
        try
        {
            CHTree CHTree2 = sender as CHTree;
            if (e.Node.Selected)
            {
                e.Node.SelectImageIndex = 2;
            }
            else
            {
                e.Node.ImageIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void TreeNode_BeforeExpand(object sender, NodeEventArgs e)
    {
        try
        {
            CHTree CHTree2 = sender as CHTree;
            foreach (TreeListNode node in e.Node.Nodes)
            {
                if (node.Selected && node.Expanded)
                {
                    node.SelectImageIndex = 3;
                }

                if (node.Selected && !node.Expanded)
                {
                    node.SelectImageIndex = 2;
                }

                if (!node.Selected && node.Expanded)
                {
                    node.SelectImageIndex = 1;
                }

                if (!node.Selected && !node.Expanded)
                {
                    node.SelectImageIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void TreeNode_AfterExpand(object sender, NodeEventArgs e)
    {
        try
        {
            CHTree CHTree2 = sender as CHTree;
            if (e.Node.Selected)
            {
                e.Node.SelectImageIndex = 3;
            }
            else
            {
                e.Node.ImageIndex = 1;
            }

            foreach (TreeListNode node in e.Node.Nodes)
            {
                string @string = A.GetString(node.GetValue("CD_MENU"));
                if (node.Selected && node.Expanded)
                {
                    node.SelectImageIndex = 3;
                }

                if (node.Selected && !node.Expanded)
                {
                    node.SelectImageIndex = 2;
                }

                if (!node.Selected && node.Expanded)
                {
                    node.ImageIndex = 1;
                }

                if (!node.Selected && !node.Expanded)
                {
                    node.SelectImageIndex = 0;
                }

                if (@string.Contains("REG"))
                {
                    node.ImageIndex = 4;
                    node.SelectImageIndex = 5;
                }
                else if (@string.Contains("REP"))
                {
                    node.ImageIndex = 6;
                    node.SelectImageIndex = 7;
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public void Binding(object dataSource)
    {
        string name = dataSource.GetType().Name;
        string text = name;
        if (text == "DataTable")
        {
            DataTable dataTable = (DataTable)dataSource;
            data = dataTable.Copy();
            base.DataSource = data;
        }
    }

    public void AddRow(DataRow row)
    {
        data.Rows.Add(row.ItemArray);
        if (base.FocusedNode.ParentNode != null)
        {
            SetFocusedNode(base.FocusedNode.ParentNode);
        }
    }

    public void DeleteRow(int index)
    {
        data.Rows[index].Delete();
    }

    public void DeleteChildRow(string value)
    {
        List<string> list = new List<string>();
        List<DataRow> list2 = new List<DataRow>();
        try
        {
            DataView dataView = new DataView(data);
            dataView.RowFilter = base.ParentFieldName + " = '" + value + "'";
            for (int i = 0; i < dataView.Count; i++)
            {
                list2.Add(dataView[i].Row);
                if (A.GetString(dataView[i][base.KeyFieldName]) != string.Empty)
                {
                    list.Add(A.GetString(dataView[i][base.KeyFieldName]));
                }
            }

            for (int j = 0; j < list.Count; j++)
            {
                dataView.RowFilter = base.ParentFieldName + " = '" + list[j] + "'";
                for (int k = 0; k < dataView.Count; k++)
                {
                    list2.Add(dataView[k].Row);
                    if (A.GetString(dataView[k][base.KeyFieldName]) != string.Empty)
                    {
                        list.Add(A.GetString(dataView[k][base.KeyFieldName]));
                    }
                }
            }

            for (int l = 0; l < list2.Count; l++)
            {
                DeleteRow(data.Rows.IndexOf(list2[l]));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void SetNodeValue(int index, string fieldName, object value)
    {
        if (fieldName == base.KeyFieldName && A.GetString(data.Rows[index][fieldName]) != string.Empty)
        {
            DataView dataView = new DataView(data);
            dataView.RowFilter = base.ParentFieldName + " = '" + data.Rows[index][fieldName]?.ToString() + "'";
            data.Rows[index][fieldName] = value;
            while (dataView.Count > 0)
            {
                dataView[0][base.ParentFieldName] = value;
            }
        }
        else
        {
            data.Rows[index][fieldName] = value;
        }
    }

    public object GetNodeValue(int index, string fieldName)
    {
        return data.Rows[index][fieldName];
    }

    public DataTable GetChanges()
    {
        PostEditor();
        ShowEditor();
        HideEditor();
        _dt = base.DataSource as DataTable;
        DataTable changes = _dt.GetChanges();
        return changes;
    }

    public void AcceptChanges()
    {
        if (_dt != null)
        {
            _dt.AcceptChanges();
        }
    }


    public void ColumnVislble(string[] objColumn, bool vislbleColumn)
    {
        for (int i = 0; i < objColumn.Length; i++)
        {
            base.Columns[objColumn[i]].Visible = vislbleColumn;
        }
    }

    public void ColumnVislble(string[] objColumn)
    {
        ColumnVislble(objColumn, vislbleColumn: false);
    }

    public void ColumnReadOnly(string[] objColumn)
    {
        for (int i = 0; i < objColumn.Length; i++)
        {
            base.Columns[objColumn[i]].OptionsColumn.ReadOnly = false;
        }
    }


    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.aTree1 = new DevExpress.XtraTreeList.TreeList();
        ((System.ComponentModel.ISupportInitialize)this).BeginInit();
        base.SuspendLayout();
        base.Location = new System.Drawing.Point(0, 0);
        base.Name = "aTree1";
        base.Size = new System.Drawing.Size(200, 400);
        base.TabIndex = 0;
        base.LookAndFeel.UseDefaultLookAndFeel = false;
        base.LookAndFeel.SetSkinStyle(SkinStyle.Office2013LightGray);
        ((System.ComponentModel.ISupportInitialize)this).EndInit();
        base.ResumeLayout(false);
    }
}
