using DevExpress.XtraWaitForm;
using System;
using System.Drawing;
using System.Runtime.Versioning;

namespace CH.Framework.Win;

[SupportedOSPlatform("windows")]
public partial class LoadingForm : WaitForm
{
    public LoadingForm()
    {
        /*  Skin skin = CommonSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel);
          SkinElement skinElement = skin["LoadingBig"];
          skinElement.Image.SetImage(Image.FromFile(@"D:\NET\ENOTES\Menu\CHF\CH.Framework.Win\rotate_right_24dp_00A2E8_FILL0_wght400_GRAD0_opsz24.png"), Color.Empty);*/
        InitializeComponent();
    }

    public override void SetCaption(string caption)
    {
        base.SetCaption(caption);
        progressPanel1.Caption = caption;
    }

    public override void SetDescription(string description)
    {
        base.SetDescription(description);
        progressPanel1.Description = description;

        using (Graphics g = progressPanel1.CreateGraphics())
        {
            SizeF size = g.MeasureString(description, progressPanel1.Appearance.Font);
            int width = (int)size.Width + 20;
            this.Width = (int)Math.Max(this.Width, width);
        }
    }
}
