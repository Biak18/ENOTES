using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Helper;

[SupportedOSPlatform("windows")]
public static class BorderlessHelper
{
    #region Dwm Dll import
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HT_CAPTION = 0x2;

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    public enum DwmWindowCornerPreference
    {
        Default = 0,
        DoNotRound = 1,
        Round = 2,
        RoundSmall = 3
    }

    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int DwmSetWindowAttribute(
        IntPtr hwnd,
        int attr,
        ref DwmWindowCornerPreference pref,
        int size);

    [DllImport("gdi32.dll")]
    static extern IntPtr CreateRoundRectRgn(
int left, int top, int right, int bottom, int width, int height);

    [DllImport("user32.dll")]
    static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);


    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    internal enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    /// <summary>
    /// Win 11 +
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="preference"></param>
    public static void SetWindowCorner(IntPtr hwnd, DwmWindowCornerPreference preference)
    {
        DwmSetWindowAttribute(hwnd, 33, ref preference, sizeof(int));
    }

    /// <summary>
    /// Win 10
    /// </summary>
    /// <param name="frm"></param>
    /// <param name="hwnd"></param>
    /// <param name="preference"></param>
    public static void SetWindowCorner(Form frm, int radius = 12)
    {
        IntPtr region = CreateRoundRectRgn(
           0, 0, frm.Width + 1, frm.Height + 1, radius, radius);

        SetWindowRgn(frm.Handle, region, true);
    }


    public static void ApplyBlurEffect(IntPtr hwnd)
    {
        var accent = new AccentPolicy();
        accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

        var accentStructSize = Marshal.SizeOf(accent);
        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData();
        data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
        data.SizeOfData = accentStructSize;
        data.Data = accentPtr;

        SetWindowCompositionAttribute(hwnd, ref data);
        Marshal.FreeHGlobal(accentPtr);
    }

    public static void MouseMove(IntPtr hwnd)
    {
        ReleaseCapture();
        SendMessage(hwnd, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
    }
    #endregion
}
