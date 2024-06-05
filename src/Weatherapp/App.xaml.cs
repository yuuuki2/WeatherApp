using System.Windows;

namespace Weatherapp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Logging.Log("Application started.");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logging.Log("Application exiting.");
            base.OnExit(e);
        }
    }
}
