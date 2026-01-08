using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
public partial class CHRoundButton : Button
{
    private int borderSize = 0;
    private int borderRadius = 40;
    private Color borderColor = Color.FromArgb(147, 112, 147);

    [Category("ENOTES" +
        "")]
    public int BorderSize
    {
        get
        {
            return borderSize;
        }
        set
        {
            borderSize = value; Invalidate();
        }
    }

    [Category("ENOTES")]
    public int BorderRadius
    {
        get
        {
            return borderRadius;
        }
        set
        {
            if (value <= base.Height)
                borderRadius = value;
            else
                borderRadius = base.Height;
            Invalidate();
        }
    }

    [Category("ENOTES")]
    public Color BorderColor
    {
        get
        {
            return borderColor;
        }
        set
        {
            borderColor = value; Invalidate();
        }
    }

    [Category("ENOTES")]
    public Color BackgroundColor
    {
        get
        {
            return base.BackColor;
        }
        set
        {
            base.BackColor = value;
        }
    }

    [Category("ENOTES")]
    public Color TextColor
    {
        get
        {
            return base.ForeColor;
        }
        set
        {
            base.ForeColor = value;
        }
    }

    //Constructor
    public CHRoundButton()
    {
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        base.FlatStyle = FlatStyle.Flat;
        base.FlatAppearance.BorderSize = 0;
        base.Size = new Size(150, 40);
        base.BackColor = Color.MediumSlateBlue;
        base.ForeColor = Color.White;
        base.Resize += RoundButton_Resize;

        //FlatAppearance.MouseDownBackColor = Color.Transparent;
        //FlatAppearance.MouseOverBackColor = Color.Transparent;
        //BackColor = Color.Transparent;
    }

    private void RoundButton_Resize(object sender, EventArgs e)
    {
        if (borderRadius > base.Height)
            borderRadius = base.Height;
    }

    private GraphicsPath GetFigurePath(Rectangle rect, float radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
        path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
        path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
        path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
        path.CloseFigure();
        return path;
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        Rectangle rctSurface = base.ClientRectangle;
        Rectangle rctBorder = Rectangle.Inflate(rctSurface, -borderSize, -borderSize);
        int num = 2;
        if (borderSize > 0)
            num = borderSize;

        if (borderRadius > 2) //round button
        {
            using (GraphicsPath gpSurface = GetFigurePath(rctSurface, borderRadius))
            using (GraphicsPath gpBorder = GetFigurePath(rctBorder, borderRadius - borderSize))
            using (Pen pSurface = new Pen(base.Parent.BackColor, num))
            using (Pen pBorder = new Pen(borderColor, borderSize))
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                pBorder.Alignment = PenAlignment.Inset;
                base.Region = new Region(gpSurface);

                pevent.Graphics.DrawPath(pSurface, gpSurface);

                if (borderSize >= 1)
                {
                    pevent.Graphics.DrawPath(pBorder, gpBorder);
                }
            }
        }
        else //normal button
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.None;
            base.Region = new Region(rctSurface);
            if (borderSize >= 1)
            {
                using (Pen pBorder = new Pen(borderColor, borderSize))
                {
                    pBorder.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawRectangle(pBorder, 0, 0, base.Width - 1, base.Height - 1);
                }
            }
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        base.Parent.BackColorChanged += Parent_BackColorChanged;
    }

    private void Parent_BackColorChanged(object sender, EventArgs e)
    {
        if (base.DesignMode)
            Invalidate();
    }

    protected override bool ShowFocusCues
    {
        get
        {
            return false;
        }
    }
    public override void NotifyDefault(bool value)
    {
        base.NotifyDefault(false);
    }

}
