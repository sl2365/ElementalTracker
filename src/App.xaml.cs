using System;
using System.Windows;

namespace ElementalTracker
{
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += (s, e) =>
            {
                // Unwrap to find the real error
                Exception inner = e.Exception;
                while (inner.InnerException != null)
                    inner = inner.InnerException;

                MessageBox.Show(
                    "Error: " + inner.Message + 
                    "\n\nType: " + inner.GetType().Name +
                    "\n\n" + inner.StackTrace,
                    AppInfo.ShortName + " - Startup Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                e.Handled = true;
            };
        }
    }
}