namespace ENOTES;

public class AppContextController : ApplicationContext
{
    private ENOTES_LOGIN _loginForm;

    public AppContextController()
    {
        ShowLogin();
    }

    private void ShowLogin()
    {
        _loginForm = new ENOTES_LOGIN();
        _loginForm.LoginSuccess += OnLoginSuccess;
        _loginForm.FormClosed += (s, e) => ExitThread();

        _loginForm.Show();
    }

    private void OnLoginSuccess(object sender, EventArgs e)
    {
        _loginForm.Hide();

        ENOTES mainForm = new ENOTES();
        mainForm.FormClosed += (s, e) =>
        {
            if (mainForm.IsLogout)
            {
                ShowLogin();
            }
            else
            {
                ExitThread(); // close entire app
            }
        };

        mainForm.Show();
    }
}
