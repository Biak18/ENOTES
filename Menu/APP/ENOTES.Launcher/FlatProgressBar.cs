using System.Drawing.Drawing2D;

namespace ENOTES.Launcher;

public class FlatProgressBar : Control
{
    private int _value = 0;
    private int _maximum = 100;
    private Color _backColor = Color.FromArgb(55, 75, 100);
    private Color _fillColor = Color.FromArgb(40, 154, 221);
    private Color _textColor = Color.White;

    public int Value
    {
        get => _value;
        set
        {
            _value = Math.Clamp(value, 0, _maximum);
            Invalidate();
        }
    }

    public int Maximum
    {
        get => _maximum;
        set { _maximum = value; Invalidate(); }
    }

    public Color FillColor
    {
        get => _fillColor;
        set { _fillColor = value; Invalidate(); }
    }

    public FlatProgressBar()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint, true);
        Height = 16;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Background
        using (var brush = new SolidBrush(_backColor))
            g.FillRectangle(brush, ClientRectangle);

        // Fill
        if (_value > 0)
        {
            int fillWidth = (int)((double)_value / _maximum * Width);

            using (var brush = new LinearGradientBrush(
                new PointF(0, 0),
                new PointF(0, Height),
                Color.FromArgb(80, 180, 240),
                Color.FromArgb(30, 130, 200)))
                g.FillRectangle(brush, 0, 0, fillWidth, Height);

            // Highlight line at top of fill
            using (var pen = new Pen(Color.FromArgb(60, 255, 255, 255), 1f))
                g.DrawLine(pen, 0, 1, fillWidth, 1);
        }

        // Percent text
        string text = $"{_value}%";
        using (var font = new Font("Segoe UI", 8f, FontStyle.Bold))
        using (var brush = new SolidBrush(_textColor))
        {
            var size = g.MeasureString(text, font);
            var point = new PointF(
                (Width - size.Width) / 2,
                (Height - size.Height) / 2);
            g.DrawString(text, font, brush, point);
        }

        // Subtle outline border — drawn last so it's on top
        using (var pen = new Pen(Color.FromArgb(80, 255, 255, 255), 1f))
            g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
    }
}
