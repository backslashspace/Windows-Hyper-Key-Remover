using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace Installer
{
    internal static partial class InstallerWorker
    {
        internal static void ExtractUninstallerFiles()
        {
            try
            {
                using FileStream exeFile = File.Create($"{Config.InstallPath}\\HyperKey Deregisterer Uninstaller.exe");
                using FileStream confFile = File.Create($"{Config.InstallPath}\\HyperKey Deregisterer Uninstaller.exe.config");

                Assembly assembly = Assembly.GetExecutingAssembly();
                assembly.GetManifestResourceStream("Installer.resources.HyperKey Deregisterer Uninstaller.exe").CopyTo(exeFile);
                assembly.GetManifestResourceStream("Installer.resources.HyperKey Deregisterer Uninstaller.exe.config").CopyTo(confFile);

                LogAppend("Extracted uninstaller\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    Pin.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                    MessageBoxWindow.MessageBox messageBox = new("Error", $"Unable to extract uninstaller:\n\n{ex.Message}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");

                    messageBox.ShowDialog();
                });

                Environment.Exit(-1);
            }
        }
    }
}