using CH.Framework.Win;
using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Threading;
using System.Windows.Forms;
namespace CH.Helper;


[SupportedOSPlatform("windows")]
public static class LoadingHelper2
{
    private static Thread _thread;
    private static CustomLoading _form;

    public static void StartLoading(Form parent, string caption, string description)
    {
        Rectangle parentBounds = parent.Bounds;
        var screen = Screen.FromControl(parent);
        Rectangle workingArea = screen.WorkingArea;

        _thread = new Thread(() =>
        {
            _form = new CustomLoading();
            _form.SetCaption(caption);
            _form.SetDescription(description);

            // Manual center over parent
            //int x = parentBounds.Left + (parentBounds.Width - _form.Width) / 2;
            //int y = parentBounds.Top + (parentBounds.Height - _form.Height) / 2;


            int x = workingArea.Left + (workingArea.Width - _form.Width) / 2;
            int y = workingArea.Top + (workingArea.Height - _form.Height) / 2;
            _form.StartPosition = FormStartPosition.Manual;
            _form.Location = new Point(x, y);

            Application.Run(_form);
        });

        _thread.IsBackground = true;
        _thread.SetApartmentState(ApartmentState.STA);
        _thread.Start();

        while (_form == null || !_form.IsHandleCreated)
            Thread.Sleep(10);
    }

    public static void EndLoading()
    {
        if (_form != null && _form.IsHandleCreated)
        {
            _form.Invoke(new Action(() =>
            {
                Application.ExitThread();
                _form.Dispose();
            }));
        }
        _thread = null;
        _form = null;
    }
}