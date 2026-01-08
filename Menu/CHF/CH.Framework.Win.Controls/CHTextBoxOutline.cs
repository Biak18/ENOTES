using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;


[SupportedOSPlatform("windows")]
[ToolboxItem(true), DesignerCategory("code")]
public class CHTextBoxOutline : ButtonEdit
{
    int outlineHeight = 1;
    Color outlineColor = SystemColors.Highlight;

    public CHTextBoxOutline()
    {
        SetStyle(ControlStyles.ResizeRedraw, true);
        base.Properties.AutoHeight = false;
        base.Properties.Buttons?.Clear();

        //base.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton()
        //{
        //    Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph,
        //    IsLeft = true,
        //    Width = 25
        //});
    }

    [Category("ENOTES")]
    [DefaultValue(1)]
    public int OutlineHeight
    {
        get => outlineHeight;
        set
        {
            if (value != outlineHeight)
            {
                outlineHeight = Math.Max(Math.Min(value, 4), 1);
                InvokeNcCalcSize();
            }
        }
    }

    [Category("ENOTES")]
    [DefaultValue(typeof(Color), "Highlight")]
    public Color OutlineColor
    {
        get => outlineColor;
        set
        {
            if (value != outlineColor)
            {
                outlineColor = value;
                Invalidate();
                InvokeNcCalcSize();
            }
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        SetWindowTheme(Handle, "", "");
        base.OnHandleCreated(e);
        //SendMessage(Handle, EM_SETCUEBANNER, 1, "Placeholder Text");
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case WM_NCCALCSIZE:
                WmNcCalcSize(ref m);
                break;
            case WM_NCPAINT:
                WmNcPaint(ref m);
                break;
            default:
                base.WndProc(ref m);
                break;
        }
    }

    protected virtual void WmNcCalcSize(ref Message m)
    {
        if (nint.Zero == m.WParam)
        {
            var client = Marshal.PtrToStructure<RECT>(m.LParam);
            client.Bottom -= outlineHeight;
            Marshal.StructureToPtr(client, m.LParam, true);
            m.Result = nint.Zero;
        }
        else
        {
            var calcParams = Marshal.PtrToStructure<NCCALCSIZE_PARAMS>(m.LParam);
            calcParams.rgrc[0].Bottom -= outlineHeight;
            Marshal.StructureToPtr(calcParams, m.LParam, true);
            m.Result = 0x0010 | 0x0020 | 0x0300;
        }
    }

    protected virtual void WmNcPaint(ref Message m)
    {
        nint hDC = nint.Zero;
        bool deleteDC = false;
        Rectangle clipRegion = Rectangle.Empty;

        if (1 == m.WParam)
        {
            deleteDC = true;
            hDC = GetWindowDC(m.HWnd);
            clipRegion = new Rectangle(0, 0, Width, Height - outlineHeight);
        }
        else
        {
            hDC = GetDCEx(Handle, m.WParam, DCX_WINDOW | DCX_USESTYLE);
        }

        if (hDC != nint.Zero)
        {
            using var g = Graphics.FromHdc(hDC);
            using var pen = new Pen(outlineColor, outlineHeight);
            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (clipRegion != Rectangle.Empty)
            {
                g.ExcludeClip(clipRegion);
            }

            g.DrawLine(pen, 0,
                g.VisibleClipBounds.Bottom - 1,
                g.VisibleClipBounds.Width,
                g.VisibleClipBounds.Bottom - 1
            );

            if (deleteDC) ReleaseDC(Handle, hDC);
        }
        m.Result = nint.Zero;
    }

    private void InvokeNcCalcSize()
    {
        SetWindowPos(Handle, nint.Zero, 0, 0, 0, 0,
            SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOSENDCHANGING);
    }

    const int EM_SETCUEBANNER = 0x1501;
    const int WM_NCCALCSIZE = 0x0083;
    const int WM_NCPAINT = 0x0085;
    const uint DCX_WINDOW = 0x00000001;
    const uint DCX_EXCLUDERGN = 0x00000040;
    const uint DCX_INTERSECTRGN = 0x00000080;
    const uint DCX_USESTYLE = 0x00010000;
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOZORDER = 0x0004;
    const uint SWP_FRAMECHANGED = 0x0020;
    const uint SWP_SHOWWINDOW = 0x0040;
    const uint SWP_NOSENDCHANGING = 0x0400;

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern int SendMessage(nint hWnd, int msg, int wParam, string lParam);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("UxTheme.dll", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern nint SetWindowTheme(nint hwnd, string pszSubAppName, string pszSubIdList);

    [DllImport("user32.dll")]
    internal static extern nint GetDCEx(nint hWnd, nint hrgnClip, uint flags);

    [DllImport("user32.dll")]
    internal static extern nint GetWindowDC(nint hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool ReleaseDC(nint hWnd, nint hDc);

    [StructLayout(LayoutKind.Sequential)]
    internal struct NCCALCSIZE_PARAMS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public RECT[] rgrc;
        public WINDOWPOS lppos;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPOS
    {
        public nint hwnd;
        public nint hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public uint flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left; Top = top; Right = right; Bottom = bottom;
        }
        public static RECT FromRectangle(Rectangle r) => new(r.Left, r.Top, r.Bottom, r.Right);

        public Rectangle ToRectangle() => Rectangle.FromLTRB(Left, Top, Right, Bottom);
        public Size Size => new(Right - Left, Bottom - Top);
    }
}
