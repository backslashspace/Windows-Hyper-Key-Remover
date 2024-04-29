using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace Installer
{
    internal static partial class InstallerWorker
    {
        internal static void ExtractHyperKeyRemover()
        {
            try
            {
                Directory.CreateDirectory(Config.InstallPath);

                using FileStream exeFile = File.Create($"{Config.InstallPath}\\HyperKey-Deregisterer.exe");
                using FileStream confFile = File.Create($"{Config.InstallPath}\\HyperKey-Deregisterer.exe.config");

                Assembly assembly = Assembly.GetExecutingAssembly();
                assembly.GetManifestResourceStream("Installer.resources.HyperKey-Deregisterer.exe").CopyTo(exeFile);
                assembly.GetManifestResourceStream("Installer.resources.HyperKey-Deregisterer.exe.config").CopyTo(confFile);

                LogAppend("Extracted HyperKey Deregisterer\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    Pin.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                    MessageBoxWindow.MessageBox messageBox = new("Error", $"Unable to extract HyperKey Deregisterer application:\n\n{ex.Message}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");

                    messageBox.ShowDialog();
                });

                Environment.Exit(-1);
            }
        }
    }
}