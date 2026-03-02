using CH.Helper;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win;

[SupportedOSPlatform("windows")]
public partial class CustomLoading : Form
{
    private System.Windows.Forms.Timer _spinTimer;
    private System.Windows.Forms.Timer _dotTimer;
    private int _angle = 0;
    private int _dotCount = 0;
    private string _caption = "Please wait.";
    private string _description = "Loading...";

    private readonly Color _backColor = Color.FromArgb(31, 42, 56);   // 60%
    private readonly Color _accentColor = Color.FromArgb(40, 154, 221); // spinner/text
    private readonly Color _textPrimary = Color.White;                   // 30%
    private readonly Color _textSecondary = Color.FromArgb(180, 200, 220); // 10%

    public CustomLoading()
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;
        this.Size = new Size(280, 140);
        this.BackColor = _backColor;
        this.ShowInTaskbar = false;
        this.TopMost = true;
        this.DoubleBuffered = true;
        //this.Region = RoundedRegion(this.Width, this.Height, 16);

        _spinTimer = new System.Windows.Forms.Timer { Interval = 16 };
        _spinTimer.Tick += (s, e) =>
        {
            _angle = (_angle + 6) % 360;
            Invalidate();
        };

        _dotTimer = new System.Windows.Forms.Timer { Interval = 400 };
        _dotTimer.Tick += (s, e) =>
        {
            _dotCount = (_dotCount + 1) % 4;
            Invalidate();
        };

        this.HandleCreated += (s, e) =>
        {
            _spinTimer.Start();
            _dotTimer.Start();
        };
    }

    public void SetCaption(string caption)
    {
        _caption = caption;
        Invalidate();
    }

    public void SetDescription(string description)
    {
        _description = description;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        int cx = this.Width / 2;

        // ── Spinner ──────────────────────────────────────
        int spinnerSize = 36;
        int spinnerTop = 20;
        Rectangle spinRect = new Rectangle(
            cx - spinnerSize / 2,
            spinnerTop,
            spinnerSize,
            spinnerSize
        );

        // Background arc (dim)
        using (var bgPen = new Pen(Color.FromArgb(60, 255, 255, 255), 3.5f))
        {
            bgPen.StartCap = LineCap.Round;
            bgPen.EndCap = LineCap.Round;
            g.DrawArc(bgPen, spinRect, 0, 360);
        }

        // Foreground arc (animated)
        using (var fgPen = new Pen(_accentColor, 3.5f))
        {
            fgPen.StartCap = LineCap.Round;
            fgPen.EndCap = LineCap.Round;
            g.DrawArc(fgPen, spinRect, _angle, 280);
        }

        // ── Caption ──────────────────────────────────────
        using (var captionFont = new Font("Segoe UI", 11f, FontStyle.Bold, GraphicsUnit.Point))
        using (var captionBrush = new SolidBrush(_textPrimary))
        {
            string dots = new string('.', _dotCount);
            string captionText = _caption + dots;
            SizeF captionSize = g.MeasureString(captionText, captionFont);
            g.DrawString(
                captionText,
                captionFont,
                captionBrush,
                new PointF(cx - captionSize.Width / 2, spinnerTop + spinnerSize + 10)
            );
        }

        // ── Description ──────────────────────────────────
        using (var descFont = new Font("Segoe UI", 8.5f, FontStyle.Regular, GraphicsUnit.Point))
        using (var descBrush = new SolidBrush(_textSecondary))
        {
            SizeF descSize = g.MeasureString(_description, descFont);
            g.DrawString(
                _description,
                descFont,
                descBrush,
                new PointF(cx - descSize.Width / 2, spinnerTop + spinnerSize + 34)
            );
        }

        // ── Border ───────────────────────────────────────
        //using (var borderPen = new Pen(Color.FromArgb(60, 255, 255, 255), 1f))
        //{
        //    g.DrawPath(borderPen, RoundedPath(this.Width - 1, this.Height - 1, 16));
        //}
    }

    private Region RoundedRegion(int w, int h, int r)
    {
        return new Region(RoundedPath(w, h, r));
    }

    #region Win
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (Environment.OSVersion.Version.Build >= 22000) // Win11 check
        {
            BorderlessHelper.SetWindowCorner(this.Handle, BorderlessHelper.DwmWindowCornerPreference.Round);
        }
        else
        {
            BorderlessHelper.SetWindowCorner(this, 16); // custom radius for older Windows
        }
    }
    #endregion

    private GraphicsPath RoundedPath(int w, int h, int r)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddArc(0, 0, r * 2, r * 2, 180, 90);
        path.AddArc(w - r * 2, 0, r * 2, r * 2, 270, 90);
        path.AddArc(w - r * 2, h - r * 2, r * 2, r * 2, 0, 90);
        path.AddArc(0, h - r * 2, r * 2, r * 2, 90, 90);
        path.CloseFigure();
        return path;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _spinTimer?.Stop();
            _spinTimer?.Dispose();
            _dotTimer?.Stop();
            _dotTimer?.Dispose();
        }
        base.Dispose(disposing);
    }
}