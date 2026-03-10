using CH.Framework.Common;
using CH.Framework.Win;
using CH.Framework.Win.Controls;
using CH.Helper;
using DevExpress.XtraEditors;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

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

        foreach (Control ctl in this.Controls)
        {
            if (ctl is MdiClient client)
            {
                client.Dock = DockStyle.Fill;
                //client.BackColor = this.BackColor;
                client.BackColor = Color.White;
                client.Padding = new Padding(8, 0, 0, 0);
            }
        }

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

    public class MenuNodeData
    {
        public string CdMenu { get; set; }
        public string NmMenu { get; set; }
        public string NmNetWindow { get; set; }
        public string FgType { get; set; }
        public string CdModule { get; set; }
    }

    private void InitializeTree()
    {
        menuTree.ImageList = imageList1;
        DataTable dataTable = _D.SearchMenu(new object[] { "", "" });
        LoadNavTree(dataTable);
    }

    private void LoadNavTree(DataTable dt)
    {
        menuTree.BeginUpdate();
        menuTree.Nodes.Clear();
        var nodeMap = new Dictionary<string, TreeNode>();

        foreach (DataRow row in dt.Rows)
        {
            string cd = row["CD_MENU"].ToString();
            string nm = row["NM_MENU"].ToString();
            string fgType = row["FG_TYPE"]?.ToString();
            string cdMenu = cd.Replace("SN", "M");

            var node = new TreeNode(nm)
            {
                Tag = new MenuNodeData
                {
                    CdMenu = cdMenu,
                    NmMenu = nm,
                    NmNetWindow = row["NM_NETWINDOW"]?.ToString(),
                    FgType = fgType,
                    CdModule = row["CD_MODULE"]?.ToString()
                }
            };

            nodeMap[cd] = node;
        }

        foreach (DataRow row in dt.Rows)
        {
            string cd = row["CD_MENU"].ToString();
            string parent = row["CD_MENU_PARENT"]?.ToString();

            if (string.IsNullOrEmpty(parent) || !nodeMap.ContainsKey(parent))
                menuTree.Nodes.Add(nodeMap[cd]);  // root
            else
                nodeMap[parent].Nodes.Add(nodeMap[cd]); // child
        }
        menuTree.EndUpdate();
        menuTree.ExpandAll();
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
        //menuTree.GetSelectImage += MenuTree_GetSelectImage;
        menuTree.DoubleClick += MenuTree_DoubleClick;

        btnFilterMenu.GotFocus += BtnFilterMenu_GotFocus;
        btnFilterMenu.LostFocus += BtnFilterMenu_LostFocus;
        btnFilterMenu.KeyDown += BtnFilterMenu_KeyDown;
        btnFilterMenu.ButtonClick += BtnFilterMenu_ButtonClick;

        //xtraTabbedMdiManager1.CustomDrawTabHeader += XtraTabbedMdiManager1_CustomDrawTabHeader;
    }

    private void TabbedView1_CustomDrawTabHeader1(object sender, DevExpress.XtraTab.TabHeaderCustomDrawEventArgs e)
    {
        bool isActive = e.TabHeaderInfo.IsActiveState;

        if (isActive)
        {

            using (var brush = new SolidBrush(CHColor.Control_Normal_Tab))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            using (var brush = new SolidBrush(Color.FromArgb(32, 130, 188)))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X, e.Bounds.Y, 5, e.Bounds.Height));
            }
        }
        else
        {
            using (var brush = new SolidBrush(SystemColors.Control))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
        }
        e.TabHeaderInfo.PaintAppearance.ForeColor = SystemColors.ControlDark;

        e.DefaultDrawText();
        e.DefaultDrawImage();
        e.DefaultDrawButtons();

        e.Handled = true;
    }

    private void XtraTabbedMdiManager1_CustomDrawTabHeader(object sender, DevExpress.XtraTab.TabHeaderCustomDrawEventArgs e)
    {
        bool isActive = e.TabHeaderInfo.IsActiveState;

        if (isActive)
        {

            using (var brush = new SolidBrush(CHColor.Control_Normal_Tab))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            using (var brush = new SolidBrush(Color.FromArgb(32, 130, 188)))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X, e.Bounds.Y, 5, e.Bounds.Height));
            }
        }
        else
        {
            using (var brush = new SolidBrush(SystemColors.Control))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
        }
        e.TabHeaderInfo.PaintAppearance.ForeColor = SystemColors.ControlDark;

        e.DefaultDrawText();
        e.DefaultDrawImage();
        e.DefaultDrawButtons();

        e.Handled = true;
    }
    #endregion

    #region ▶ Tree Events  
    private void MenuTree_DoubleClick(object sender, EventArgs e)
    {
        try
        {
            if (menuTree.SelectedNode?.Tag is not MenuNodeData data) return;
            if (data.FgType != "M") return;

            OpenFormFromDll(data.CdMenu, data.NmNetWindow, data.NmMenu, data.CdModule);
        }
        catch (Exception ex)
        {
            using (var dlg = new MsgDialog(MessageType.Error, ex.Message))
                dlg.ShowDialog(this);
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
                    //LoadingHelper2.StartLoading(frm, "Please wait.", "Searching...");
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

    //private void BtnFilterMenu_KeyDown(object sender, KeyEventArgs e)
    //{
    //    if (e.KeyCode == Keys.Enter)
    //    {
    //        menuTree.ExpandAll();
    //        menuTree.ActiveFilterString = "NM_MENU like '%" + btnFilterMenu.Text.ToString() + "%'";
    //    }
    //}

    private void BtnFilterMenu_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            FilterTree(btnFilterMenu.Text);
    }


    private void FilterTree(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword) || keyword == filterText)
        {
            // Restore all nodes
            menuTree.Nodes.Clear();
            LoadNavTree(_D.SearchMenu(new object[] { "", "" }));
            return;
        }

        // Show only matching nodes + their parents
        menuTree.Nodes.Clear();
        DataTable dt = _D.SearchMenu(new object[] { "", "" });
        var allRows = dt.AsEnumerable().ToList();

        // Find matching rows
        var matched = allRows
            .Where(r => r["NM_MENU"].ToString()
                .Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .Select(r => r["CD_MENU"].ToString())
            .ToHashSet();

        // Include all parents of matched nodes
        foreach (var cd in matched.ToList())
        {
            var row = allRows.FirstOrDefault(r => r["CD_MENU"].ToString() == cd);
            string parent = row?["CD_MENU_PARENT"]?.ToString();
            while (!string.IsNullOrEmpty(parent))
            {
                matched.Add(parent);
                var parentRow = allRows.FirstOrDefault(r => r["CD_MENU"].ToString() == parent);
                parent = parentRow?["CD_MENU_PARENT"]?.ToString();
            }
        }

        // Rebuild tree with only matched nodes
        var filtered = dt.AsEnumerable()
            .Where(r => matched.Contains(r["CD_MENU"].ToString()));
        var filteredDt = filtered.Any() ? filtered.CopyToDataTable() : dt.Clone();

        LoadNavTree(filteredDt);
        menuTree.ExpandAll();
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

    //private void BtnFilterMenu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    //{
    //    menuTree.ExpandAll();
    //    menuTree.ActiveFilterString = "NM_MENU like '%" + btnFilterMenu.Text.ToString() + "%'";
    //}

    private void BtnFilterMenu_ButtonClick(object sender,
    DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
        FilterTree(btnFilterMenu.Text);
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
        if (type == null)
        {
            using (var dlg = new MsgDialog(MessageType.Error, $"Form class not found: {cdModule}.{cdMenu}"))
            {
                dlg.ShowDialog(this);
            }
            return;
        }

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
    #region Borderless Aero Snap
    const int WM_NCCALCSIZE = 0x83;
    const int WM_NCHITTEST = 0x84;
    const int HTCAPTION = 0x2; // Title bar hit test
    const int WS_THICKFRAME = 0x40000; // for Aero Snap
    const int WS_CAPTION = 0xC00000; // for title bar functionality

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.Style |= WS_THICKFRAME | WS_CAPTION;
            return cp;
        }
    }
    [DllImport("user32.dll")]
    static extern bool IsZoomed(IntPtr hWnd);

    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct NCCALCSIZE_PARAMS
    {
        public RECT rgrc0, rgrc1, rgrc2;
        public IntPtr lppos;
    }
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
        {
            if (IsZoomed(this.Handle))
            {
                Screen screen = Screen.FromHandle(this.Handle);
                Rectangle workingArea = screen.WorkingArea;

                NCCALCSIZE_PARAMS ncp = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(
                    m.LParam, typeof(NCCALCSIZE_PARAMS));

                ncp.rgrc0.Left = workingArea.Left;
                ncp.rgrc0.Top = workingArea.Top;
                ncp.rgrc0.Right = workingArea.Right;
                ncp.rgrc0.Bottom = workingArea.Bottom;

                Marshal.StructureToPtr(ncp, m.LParam, false);
            }

            m.Result = IntPtr.Zero;
            return;
        }

        if (m.Msg == WM_NCHITTEST)
        {
            base.WndProc(ref m);
            if (m.Result.ToInt32() == HTCAPTION)
            {
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = PointToClient(screenPoint);

                if (topPanel.Bounds.Contains(clientPoint))
                {
                    m.Result = new IntPtr(HTCAPTION);
                    return;
                }
            }
            return;
        }

        base.WndProc(ref m);
    }
    #endregion

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
