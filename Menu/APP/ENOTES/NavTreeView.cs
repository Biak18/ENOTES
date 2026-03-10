using System.Runtime.InteropServices;
using static ENOTES.ENOTES;

namespace ENOTES;

internal class NavTreeView : TreeView
{
    private Color _backNav = Color.FromArgb(31, 42, 56);
    private Color _hoverColor = Color.FromArgb(42, 56, 75);
    private Color _activeColor = Color.FromArgb(40, 154, 221);
    private Color _textColor = Color.FromArgb(220, 230, 240);
    private TreeNode _activeNode;
    private TreeNode _hoveredNode;

    public NavTreeView()
    {
        DrawMode = TreeViewDrawMode.OwnerDrawAll;
        FullRowSelect = true;
        ShowLines = false;
        ShowPlusMinus = false;
        ShowRootLines = false;
        BorderStyle = BorderStyle.None;
        BackColor = _backNav;
        Indent = 16;
        ItemHeight = 36;
        Font = new Font("Segoe UI", 9.5f);
        Scrollable = true;

    }

    public void SetActiveNode(TreeNode node)
    {
        _activeNode = node;
        Invalidate();
    }

    protected override void OnDrawNode(DrawTreeNodeEventArgs e)
    {
        var g = e.Graphics;
        var node = e.Node;
        var bounds = new Rectangle(0, e.Bounds.Y, Width, e.Bounds.Height);

        bool isHovered = node == _hoveredNode;
        bool isActive = node == _activeNode;
        bool isParent = node.Nodes.Count > 0;
        int depth = node.Level;

        // Background
        Color backColor = isActive ? Color.FromArgb(38, 50, 68)
                        : isHovered ? _hoverColor
                        : _backNav;

        using (var brush = new SolidBrush(backColor))
            g.FillRectangle(brush, bounds);

        // Active left accent bar
        if (isActive)
        {
            using (var brush = new SolidBrush(_activeColor))
                g.FillRectangle(brush, 0, bounds.Y, 3, bounds.Height);
        }

        int x = 16 + depth * Indent;

        if (ImageList != null && ImageList.Images.Count > 0)
        {
            int imgIndex = isParent
                ? (node.IsExpanded ? 1 : 0)  // 0=folder, 1=folder_open
                : (node.ImageIndex >= 0 && node.ImageIndex < ImageList.Images.Count
                    ? node.ImageIndex : 0);

            MenuNodeData data = node.Tag as MenuNodeData;
            if (data.FgType != "M")
            {
                var img = ImageList.Images[imgIndex];
                int imgY = bounds.Y + (bounds.Height - img.Height) / 2;
                g.DrawImage(img, x, imgY, img.Width, img.Height);
                x += img.Width + 6; // icon width + gap → move x right
            }

        }

        // Node text
        int textX = 38 + depth * Indent;
        Color textColor = isActive ? Color.White
                        : isParent ? Color.FromArgb(200, 215, 230)
                        : _textColor;

        FontStyle style = isParent ? FontStyle.Regular : FontStyle.Regular;
        using (var font = new Font("Segoe UI", isParent ? 9f : 9f, style))
        using (var brush = new SolidBrush(textColor))
        {
            float textY = bounds.Y + (bounds.Height - font.Height) / 2f;
            g.DrawString(node.Text, font, brush, textX, textY);
        }



        e.DrawDefault = false;
    }

    protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
    {
        base.OnNodeMouseClick(e);

        if (e.Node.Nodes.Count > 0)
        {
            // Toggle expand
            if (e.Node.IsExpanded) e.Node.Collapse();
            else e.Node.Expand();
        }
        else
        {
            // Leaf node — set as active
            SetActiveNode(e.Node);
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        var node = GetNodeAt(e.Location);

        // Only redraw if hovered node actually changed
        if (node != _hoveredNode)
        {
            _hoveredNode = node;
            Invalidate();
        }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (_hoveredNode != null)
        {
            _hoveredNode = null;
            Invalidate();
        }
    }
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
    }
    // Pinvoke:
    private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
    private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
    private const int TVS_EX_DOUBLEBUFFER = 0x0004;
    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
}