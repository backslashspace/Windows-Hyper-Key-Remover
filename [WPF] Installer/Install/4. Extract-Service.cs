using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace Installer
{
    internal static partial class InstallerWorker
    {
        internal static void ExtractServiceFiles()
        {
            try
            {
                using FileStream exeFile = File.Create($"{Config.InstallPath}\\User-init Enforcer.exe");
                using FileStream confFile = File.Create($"{Config.InstallPath}\\User-init Enforcer.exe.config");

                Assembly assembly = Assembly.GetExecutingAssembly();
                assembly.GetManifestResourceStream("Installer.resources.User-init Enforcer.exe").CopyTo(exeFile);
                assembly.GetManifestResourceStream("Installer.resources.User-init Enforcer.exe.config").CopyTo(confFile);

                LogAppend("Extracted Self-Healing service\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    Pin.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                    MessageBoxWindow.MessageBox messageBox = new("Error", $"Unable to extract self healing service:\n\n{ex.Message}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");

                    messageBox.ShowDialog();
                });

                Environment.Exit(-1);
            }
        }
    }
}