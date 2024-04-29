using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace Installer
{
    internal static partial class InstallerWorker
    {
        internal static void RegisterWindowAsApp()
        {
            UInt32 estimatedSize = 0;

            try
            {
                DirectoryInfo directoryInfo = new(Config.InstallPath);

                UInt64 size = 0;

                FileInfo[] fileInfos = directoryInfo.GetFiles();

                for (Int32 i = 0; i < fileInfos.Length; ++i)
                {
                    size += (UInt64)fileInfos[i].Length;
                }

                estimatedSize = unchecked((UInt32)(size / 1024));
            }
            catch (Exception ex)
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    Pin.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                    MessageBoxWindow.MessageBox messageBox = new("Error", $"Unable to calculate size of install directory:\n{ex.Message}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");

                    messageBox.ShowDialog();
                });

                Environment.Exit(-1);
            }

            String REGPATH = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Hyper Key Deregisterer";

            String displayIcon = $"{Config.InstallPath}\\HyperKey-Deregisterer.exe";
            String displayName = "Windows Hyper Key Remover";
            String displayVersion = $"{Assembly.GetExecutingAssembly().GetName().Version}";
            String publisher = "https://github.com/backslashspace";
            String uninstallString = $"{Config.InstallPath}\\HyperKey Deregisterer Uninstaller.exe";

            //

            try
            {
                Registry.SetValue(REGPATH, "DisplayIcon", displayIcon, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "DisplayName", displayName, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "DisplayVersion", displayVersion, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "EstimatedSize", unchecked((Int32)estimatedSize), RegistryValueKind.DWord);
                Registry.SetValue(REGPATH, "NoModify", 1, RegistryValueKind.DWord);
                Registry.SetValue(REGPATH, "NoRepair", 1, RegistryValueKind.DWord);
                Registry.SetValue(REGPATH, "Publisher", publisher, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "UninstallString", uninstallString, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    Pin.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                    MessageBoxWindow.MessageBox messageBox = new("Error", $"Unable to set registry values:\n{ex.Message}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");

                    messageBox.ShowDialog();
                });

                Environment.Exit(-1);
            }

            LogAppend("Registered as Windows App\n");
            UpdateProgressBar();
        }
    }
}