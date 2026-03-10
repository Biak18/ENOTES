using System;
using System.Windows.Forms;

namespace ENOTES.Deploy
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.          
            Application.Run(new DeployForm());
        }
    }
}
