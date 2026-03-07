using CH.Helper;
using System.IO;


namespace ENOTES
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            /* ApplicationConfiguration.Initialize(); does all the things
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            */

            //DevExpress.UserSkins.BonusSkins.Register();

            //Assembly asm = typeof(DevExpress.UserSkins.ENOTESSKIN).Assembly;
            //DevExpress.XtraEditors.WindowsFormsSettings.RegisterUserSkins(asm);

            //UserLookAndFeel.Default.SetSkinStyle("ENOTES_SKIN");
            ApplicationConfiguration.Initialize();
            Bootstrap();
            Application.Run(new AppContextController());
        }

        private static void Bootstrap()
        {
            string appIni = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "AppSettings.ini"
            );

            string mode = IniFile.IniReadValue("App", "ConnectionMode", appIni);

            if (string.IsNullOrEmpty(mode) || mode == "DbDirect")
            {
                CH.AppContext.Configure(CH.ConnectionMode.DbDirect);
            }
            else
            {
                CH.AppContext.Configure(CH.ConnectionMode.Web);
            }
        }

    }
}