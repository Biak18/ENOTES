using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
public class CHLoadingPanel : Panel
{
    private readonly Timer _animationTimer;
    private int _angle;
    private int _dotCount;
    private int _dotTick;

    private string _caption = "Please wait";
    private string _description = "Loading...";

    private readonly Color _accentColor = Color.FromArgb(40, 154, 221);
    private readonly Color _textPrimary = Color.White;
    private readonly Color _textSecondary = Color.FromArgb(180, 200, 220);

    #region Public Properties

    [Category("Appearance")]
    public string Caption
    {
        get => _caption;
        set
        {
            if (_caption == value) return;
            _caption = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    public string Description
    {
        get => _description;
        set
        {
            if (_description == value) return;
            _description = value;
            Invalidate();
        }
    }

    #endregion

    public CHLoadingPanel()
    {
        Size = new Size(280, 140);
        BackColor = Color.FromArgb(31, 42, 56);

        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);

        DoubleBuffered = true;

        _animationTimer = new Timer { Interval = 20 }; // 50 FPS
        _animationTimer.Tick += AnimationTick;
    }

    #region Animation Logic

    private void AnimationTick(object sender, EventArgs e)
    {
        _angle = (_angle + 6) % 360;

        _dotTick++;
        if (_dotTick >= 20) // ~400ms (20ms * 20)
        {
            _dotTick = 0;
            _dotCount = (_dotCount + 1) % 4;
        }

        Invalidate();
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);

        if (!DesignMode && Visible)
            _animationTimer.Start();
        else
            _animationTimer.Stop();
    }

    public void Start()
    {
        if (!DesignMode && !_animationTimer.Enabled)
            _animationTimer.Start();
    }

    public void Stop()
    {
        if (_animationTimer.Enabled)
            _animationTimer.Stop();
    }

    #endregion

    #region Painting

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        int centerX = Width / 2;

        DrawSpinner(g, centerX);
        DrawText(g, centerX);
    }

    private void DrawSpinner(Graphics g, int centerX)
    {
        int size = 36;
        int top = 20;

        Rectangle rect = new(
            centerX - size / 2,
            top,
            size,
            size);

        using var bgPen = new Pen(Color.FromArgb(60, 255, 255, 255), 3.5f)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };

        using var fgPen = new Pen(_accentColor, 3.5f)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };

        g.DrawArc(bgPen, rect, 0, 360);
        g.DrawArc(fgPen, rect, _angle, 280);
    }

    private void DrawText(Graphics g, int centerX)
    {
        int baseTop = 20 + 36;

        string dots = new('.', _dotCount);
        string captionText = _caption + dots;

        using var captionFont = new Font("Segoe UI", 11f, FontStyle.Bold);
        using var captionBrush = new SolidBrush(_textPrimary);

        SizeF captionSize = g.MeasureString(captionText, captionFont);
        g.DrawString(
            captionText,
            captionFont,
            captionBrush,
            centerX - captionSize.Width / 2,
            baseTop + 10);

        using var descFont = new Font("Segoe UI", 8.5f);
        using var descBrush = new SolidBrush(_textSecondary);

        SizeF descSize = g.MeasureString(_description, descFont);
        g.DrawString(
            _description,
            descFont,
            descBrush,
            centerX - descSize.Width / 2,
            baseTop + 34);
    }

    #endregion

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _animationTimer.Stop();
            _animationTimer.Dispose();
        }

        base.Dispose(disposing);
    }
}