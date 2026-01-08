using CH.Framework.Win;
using DevExpress.XtraSplashScreen;
using System;
using System.Windows.Forms;

namespace CH.Helper;

public class LoadingHelper
{
    public static void StartLoading(Form _form, Type frmToShow)
    {
        if (SplashScreenManager.Default != null)
        {
            SplashScreenManager.CloseForm();
        }

        StartLoading(_form, frmToShow, "Please wait", "Loading...");
    }

    public static void StartLoading(Form form, string _Caption, string _Description)
    {
        if (SplashScreenManager.Default != null)
        {
            SplashScreenManager.CloseForm();
        }

        SplashScreenManager.ShowForm(form, typeof(LoadingForm), useFadeIn: true, useFadeOut: true, throwExceptionIfAlreadyOpened: true);
        SplashScreenManager.Default?.SetWaitFormCaption(_Caption);
        SplashScreenManager.Default?.SetWaitFormDescription(_Description);
    }

    public static void StartLoading(Form form, Type frmToShow, string _Caption, string _Description)
    {
        if (SplashScreenManager.Default != null)
        {
            SplashScreenManager.CloseForm();
        }

        SplashScreenManager.ShowForm(form, frmToShow, useFadeIn: true, useFadeOut: true, throwExceptionIfAlreadyOpened: true);
        SplashScreenManager.Default?.SetWaitFormCaption(_Caption);
        SplashScreenManager.Default?.SetWaitFormDescription(_Description);
    }

    public static void StartLoadingOverLay(Form form, string _Caption, string _Description)
    {
        if (SplashScreenManager.Default != null)
        {
            SplashScreenManager.CloseForm();
        }

        SplashScreenManager.ShowOverlayForm(form);
        SplashScreenManager.Default?.SetWaitFormCaption(_Caption);
        SplashScreenManager.Default?.SetWaitFormDescription(_Description);
    }

    public static void EndLoading()
    {
        if (SplashScreenManager.Default != null)
        {
            SplashScreenManager.CloseForm();
        }
    }

    public static void EndLoadingOverLay()
    {
        if (SplashScreenManager.Default != null)
        {
            SplashScreenManager.CloseOverlayForm(null);
        }
    }
}
