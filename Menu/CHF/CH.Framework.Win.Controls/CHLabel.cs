using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Framework.Win.Controls;

[SupportedOSPlatform("windows")]
public class CHLabel : LabelControl
{
    private int FIXED_HEIGHT = 23;
    public CHLabel()
    {
        base.AutoSizeMode = LabelAutoSizeMode.None;
        base.Height = FIXED_HEIGHT;
        base.Width = 80;
        base.ForeColor = CHColor.Label_Normal;
        base.Font = new Font("Segoe UI", 9f);
    }

    protected override void OnPropertiesChanged()
    {
        base.OnPropertiesChanged();
        base.Height = FIXED_HEIGHT;
    }

    private void InitializeComponent()
    {
        base.SuspendLayout();
        base.ResumeLayout(false);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        LabelControlViewInfo labelControlViewInfo = base.ViewInfo;
        using (GraphicsCache cache = new GraphicsCache(e.Graphics))
        {
            ControlGraphicsInfoArgs infoArgs = new ControlGraphicsInfoArgs(labelControlViewInfo, cache, base.ViewInfo.Bounds);

            Painter.Draw(infoArgs);

            if (!base.Enabled)
            {
                ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, Color.Black, 0, ButtonBorderStyle.Inset, Color.Black, 0, ButtonBorderStyle.Inset, Color.Black, 0, ButtonBorderStyle.Inset, CHColor.Label_Disable, 1, ButtonBorderStyle.Inset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, Color.Black, 0, ButtonBorderStyle.Inset, Color.Black, 0, ButtonBorderStyle.Inset, Color.Black, 0, ButtonBorderStyle.Inset, CHColor.Control_Required_Under, 1, ButtonBorderStyle.Inset);
            }
        }
    }
}
