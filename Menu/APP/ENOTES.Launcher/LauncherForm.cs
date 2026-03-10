
using System.Diagnostics;

namespace ENOTES.Launcher;

public partial class LauncherForm : Form
{
    public LauncherForm()
    {
        InitializeComponent();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await RunUpdateAsync();
    }

    private async Task RunUpdateAsync()
    {
        var progress = new Progress<(string message, int percent)>(report =>
        {
            lblStatus.Text = report.message;
            flatProgressBar1.Value = report.percent;
        });

        await AppUpdater.CheckAndUpdate(progress);

        await Task.Delay(300);

        // Launch main app
        Process.Start(new ProcessStartInfo
        {
            FileName = "ENOTES_App.exe",
            UseShellExecute = true,
            WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
        });

        Application.Exit();
    }
}