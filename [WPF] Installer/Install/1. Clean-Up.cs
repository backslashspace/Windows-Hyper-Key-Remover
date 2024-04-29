using System;
using System.IO;

namespace Installer
{
    internal static partial class InstallerWorker
    {
        internal static void CleanUp()
        {
            try
            {
                Directory.Delete(Config.InstallPath, true);

                LogAppend("Performed post cleanup\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    MessageBoxWindow.MessageBox messageBox = new("Error", $"Unable to perform post install clean up:\n\n{ex.Message}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");
            
                    messageBox.ShowDialog();
                });

                Environment.Exit(-1);
            }
        }
    }
}