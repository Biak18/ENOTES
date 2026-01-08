using CH.Framework.Common;
using CH.Framework.Win;
using CH.Helper;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Data;
using System.IO;
using System.Reflection;

namespace ENOTES;

public partial class ENOTES : XtraForm
{
    #region ▶ Initialize
    private const string filterText = "Search menu";
    ENOTES_D _D = null;
    public bool IsLogout { get; private set; } = false;

    public ENOTES()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _D = new ENOTES_D();


        if (Environment.OSVersion.Version.Build >= 22000) // Win11 check
        {
            BorderlessHelper.SetWindowCorner(this.Handle, BorderlessHelper.DwmWindowCornerPreference.Round);
        }
        else
        {
            BorderlessHelper.SetWindowCorner(this, 16); // custom radius for older Windows
        }
        InitializeTree();
        InitializeControl();
        InitializeEvent();
    }

    private void InitializeTree()
    {
        menuTree.OptionsBehavior.Editable = false;
        menuTree.OptionsSelection.KeepSelectedOnClick = false;
        menuTree.OptionsSelection.EnableAppearanceFocusedCell = false;
        menuTree.OptionsView.ShowButtons = false;
        menuTree.OptionsView.ShowIndentAsRowStyle = true;
        menuTree.OptionsView.ShowHorzLines = false;
        menuTree.OptionsView.ShowVertLines = false;
        menuTree.OptionsView.ShowColumns = false;
        menuTree.LookAndFeel.UseDefaultLookAndFeel = false;
        menuTree.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
        menuTree.OptionsSelection.EnableAppearanceFocusedRow = true;
        menuTree.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
        menuTree.OptionsSelection.InvertSelection = false;
        menuTree.OptionsSelection.MultiSelect = false;
        menuTree.OptionsSelection.EnableAppearanceHotTrackedRow = DevExpress.Utils.DefaultBoolean.True;

        //menuTree.OptionsView.ShowIndicator = false;
        //menuTree.StateImageList = imageCollection1;    // this can also be used for node images
        menuTree.SelectImageList = imageCollection1;   // used for node icons

        Color backColor = Color.FromArgb(31, 42, 56);
        Color hoverColor = Color.FromArgb(42, 56, 75);

        menuTree.Appearance.Empty.BackColor = backColor;
        menuTree.Appearance.Row.BackColor = backColor;
        menuTree.Appearance.FocusedRow.BackColor = backColor;
        menuTree.Appearance.HideSelectionRow.BackColor = backColor;
        menuTree.Appearance.TreeLine.BackColor = backColor;
        menuTree.Appearance.HotTrackedRow.BackColor = hoverColor;

        menuTree.Appearance.Row.ForeColor = Color.White;
        menuTree.Appearance.FocusedRow.ForeColor = Color.White;
        menuTree.Appearance.HideSelectionRow.ForeColor = Color.White;
        menuTree.Appearance.HotTrackedRow.ForeColor = Color.White;

        menuTree.Appearance.FocusedCell.BackColor = hoverColor;
        menuTree.Appearance.FocusedCell.ForeColor = Color.White;

        menuTree.OptionsView.ExpandButtonCentered = true;

        //menuTree.Appearance.Row.Font = new Font("Malgun gothic", 10);




        DataTable dataTable = _D.SearchMenu(new object[] { "", "" });
        menuTree.DataSource = dataTable;
        menuTree.KeyFieldName = "CD_MENU";
        menuTree.ParentFieldName = "CD_MENU_PARENT";

        foreach (var col in menuTree.Columns)
        {
            col.Visible = col.FieldName == "NM_MENU";
        }
        menuTree.ExpandToLevel(0);
    }

    private void InitializeControl()
    {
        btnFilterMenu.Text = filterText;
        btnFilterMenu.ForeColor = Color.White;
    }

    private void InitializeEvent()
    {

        topPanel.MouseDown += TopPanel_MouseDown;
        logo.DoubleClick += (s, e) => { leftPanel.Visible = !leftPanel.Visible; };

        btnSearch.Click += Btn_Click;
        btnAdd.Click += Btn_Click;
        btnDel.Click += Btn_Click;
        btnSave.Click += Btn_Click;
        btnPrint.Click += Btn_Click;

        btnMinimize.Click += BtnSup_Click;
        btnMaximize.Click += BtnSup_Click;
        btnClose.Click += BtnSup_Click;

        btnSearch.MouseEnter += Btn_MouseEnter;
        btnAdd.MouseEnter += Btn_MouseEnter;
        btnDel.MouseEnter += Btn_MouseEnter;
        btnSave.MouseEnter += Btn_MouseEnter;
        btnPrint.MouseEnter += Btn_MouseEnter;

        xtraTabbedMdiManager1.PageAdded += XtraTabbedMdiManager1_PageAdded;
        xtraTabbedMdiManager1.PageRemoved += XtraTabbedMdiManager1_PageRemoved;

        menuTree.GetSelectImage += MenuTree_GetSelectImage;
        menuTree.DoubleClick += MenuTree_DoubleClick;

        btnFilterMenu.GotFocus += BtnFilterMenu_GotFocus;
        btnFilterMenu.LostFocus += BtnFilterMenu_LostFocus;
        btnFilterMenu.KeyDown += BtnFilterMenu_KeyDown;
        btnFilterMenu.ButtonClick += BtnFilterMenu_ButtonClick;
    }
    #endregion

    #region ▶ Tree Events
    private void MenuTree_GetSelectImage(object sender, GetSelectImageEventArgs e)
    {
        //hide child
        if (e.Node.ParentNode != null)
        {
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
            e.NodeImageIndex = 1;     // expanded icon
        }
        else
            e.NodeImageIndex = 0;     // collapsed icon
    }

    private void MenuTree_DoubleClick(object sender, EventArgs e)
    {
        try
        {
            TreeListNode node = menuTree.FocusedNode;
            if (node == null) return;

            string tpType = node.GetValue("FG_TYPE").ToString();
            string cdMenu = node.GetValue("CD_MENU").ToString().Replace("SN", "M");
            string nmMenu = node.GetValue("NM_MENU").ToString();
            string nmNetWindow = node.GetValue("NM_NETWINDOW").ToString();
            string cdModule = node.GetValue("CD_MODULE").ToString();
            if (tpType != "M") return;

            OpenFormFromDll(cdMenu, nmNetWindow, nmMenu, cdModule);
        }
        catch (Exception ex)
        {
            using (var dlg = new MsgDialog(MessageType.Error, ex.Message))
            {
                dlg.ShowDialog(this);
            }
        }
    }
    #endregion

    #region ▶ Buttons Events
    private void Btn_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (ActiveMdiChild is not CHFormBase frm)
            return;
        try
        {
            switch (btn.Name)
            {
                case nameof(btnSearch):
                    LoadingHelper.StartLoading(frm, "Please wait.", "Searching...");
                    //Thread.Sleep(5000);
                    frm.OnSearch();
                    break;

                case nameof(btnAdd):
                    frm.OnAddrow();
                    break;

                case nameof(btnDel):
                    frm.OnDeleteRow();
                    break;

                case nameof(btnSave):
                    LoadingHelper.StartLoading(frm, "Please wait.", "Saving...");
                    frm.OnSave();
                    break;

                case nameof(btnPrint):
                    frm.OnPrint();
                    break;
            }
        }
        catch (Exception ex)
        {
            using (var dlg = new MsgDialog(MessageType.Error, ex.Message))
            {
                dlg.ShowDialog(this);
            }
        }
        finally
        {
            LoadingHelper.EndLoading();
        }
    }

    private void BtnSup_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        switch (btn.Name)
        {
            case nameof(btnMinimize):
                WindowState = FormWindowState.Minimized;
                break;
            case nameof(btnMaximize):
                if (WindowState == FormWindowState.Normal)
                    WindowState = FormWindowState.Maximized;
                else
                    WindowState = FormWindowState.Normal;
                break;
            case nameof(btnClose):
                this.Close();
                break;
        }
    }

    private void Btn_MouseEnter(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (btn == null) return;

        string txt = btn.Name switch
        {
            "btnSearch" => "Search",
            "btnAdd" => "Add",
            "btnDel" => "Delete",
            "btnSave" => "Save",
            "btnPrint" => "Print",
            _ => ""
        };
        myTooltip.SetToolTip(btn, txt);
    }
    #endregion

    #region ▶ Custom Events
    private void TopPanel_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            BorderlessHelper.MouseMove(this.Handle);
        }

        if (e.Clicks >= 2)
            WindowState = WindowState == FormWindowState.Normal
                ? FormWindowState.Maximized
                : FormWindowState.Normal;
    }

    private void XtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
    {
        mainPanel.Visible = (xtraTabbedMdiManager1.Pages.Count == 0);
    }

    private void XtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
    {
        mainPanel.Visible = !(xtraTabbedMdiManager1.Pages.Count > 0);
    }



    private void BtnFilterMenu_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            menuTree.ExpandAll();
            menuTree.ActiveFilterString = "NM_MENU like '%" + btnFilterMenu.Text.ToString() + "%'";
        }
    }

    private void BtnFilterMenu_LostFocus(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(btnFilterMenu.Text))
        {
            btnFilterMenu.Text = filterText;
        }
    }

    private void BtnFilterMenu_GotFocus(object sender, EventArgs e)
    {
        if (btnFilterMenu.Text == filterText)
        {
            btnFilterMenu.Text = "";
        }
    }

    private void BtnFilterMenu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
        menuTree.ExpandAll();
        menuTree.ActiveFilterString = "NM_MENU like '%" + btnFilterMenu.Text.ToString() + "%'";
    }
    #endregion

    #region ▶ Methods
    private void OpenFormFromDll(string cdMenu, string formName, string tabTitle, string cdModule)
    {
        string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{cdMenu}.dll");
        //string dllPath = @"C:\Users\GMSH-CHAN\Desktop\AllInOne\CHAN\M_TEST_001\bin\Debug\net8.0-windows\M_TEST_001.dll";
        if (!File.Exists(dllPath))
        {
            using (var dlg = new MsgDialog(MessageType.Error, $"Missing DLL: {dllPath}"))
            {
                dlg.ShowDialog(this);
            }
            return;
        }

        Assembly asm = Assembly.LoadFrom(dllPath);

        Type type = asm.GetType(cdModule + "." + cdMenu);
        if (type == null) return;

        // Prevent duplicate open
        foreach (Form f in MdiChildren)
        {
            if (f.GetType() == type)
            {
                f.Activate();
                return;
            }
        }

        CHFormBase form = Activator.CreateInstance(type) as CHFormBase;
        if (form == null) return;

        form.IsTopPanelVisible = false;
        form.MdiParent = this;
        form.Text = tabTitle;
        form.Show();
    }

    //private void SetNodeIcons(TreeListNode parentNode)
    //{
    //    foreach (TreeListNode node in parentNode.Nodes)
    //    {
    //        string tpType = node.GetValue("FG_TYPE").ToString();

    //        if (tpType == "M")
    //            node.SelectImageIndex = 2; // child/menu
    //        else
    //            node.SelectImageIndex = 0; // folder or parent

    //        // Recursively set icons for children
    //        if (node.HasChildren)
    //            SetNodeIcons(node);

    //        // Force redraw
    //        node.TreeList.RefreshNode(node);
    //    }
    //}
    #endregion

    #region ▶ Closing
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        if (!IsLogout)
        {
            Application.Exit();
        }
    }

    #endregion
}
