using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Uninstaller
{
    internal static partial class Uninstaller
    {
        internal struct InstalledComponents
        {
            internal static Boolean Directory = false;

            internal static Boolean Remover_exe = false;
            internal static Boolean Remover_exe_config = false;
            internal static Boolean Service_exe = false;
            internal static Boolean Service_exe_config = false;

            internal static Boolean UserInit = false;

            internal static Boolean Service_deregister = false;

            internal static RegistryKey WindowsApp_Deregister = null;
        }

        private const String InstallDirectory = "C:\\Program Files\\Hyper Key Remover";
        private const String UserInitRegex = ",\\s*\"C:\\\\Program Files\\\\Hyper Key Remover\\\\HyperKey-Deregisterer\\.exe\"(\\s*,|\\s*\\z)";

        internal static void Worker()
        {
            GetInstalledComponentInfo();

            String buttonText = ""; 

            if (!PrepareProgressBar())
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    Pin.MainWindow.LogBox.Text = "Directories clean - nothing to do";
                    Pin.MainWindow.UninstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));
                });

                buttonText = "OK";
            }
            else
            {
                buttonText = "Finish";

                RemovePrograms();

                ClearUserInit();

                DeregisterService();

                DeregisterWindowsApp();
            }

            Pin.MainWindow.Dispatcher.Invoke(async () =>
            {
                await Task.Delay(1200).ConfigureAwait(true);

                Pin.MainWindow.UninstallButton.Click += Pin.MainWindow.FinishUninstall;
                Pin.MainWindow.UninstallButton.Content = buttonText;

                //

                ThicknessAnimation button = new();
                button.Duration = MainWindow.Duration;
                button.To = new(0, 0, 0, 32);
                button.DecelerationRatio = 1;

                DoubleAnimation buttonOpacity = new();
                buttonOpacity.Duration = MainWindow.Duration;
                buttonOpacity.To = 1;
                buttonOpacity.DecelerationRatio = 1;

                Pin.MainWindow.UninstallButton.BeginAnimation(Button.MarginProperty, button);
                Pin.MainWindow.UninstallButton.BeginAnimation(Button.OpacityProperty, buttonOpacity);

                //

                ThicknessAnimation bar = new();
                bar.Duration = MainWindow.Duration;
                bar.To = new(120, 0, 120, 140);
                bar.DecelerationRatio = 1;

                DoubleAnimation barOpacity = new();
                barOpacity.Duration = MainWindow.Duration;
                barOpacity.To = 0;
                barOpacity.DecelerationRatio = 1;

                Pin.MainWindow.UninstallProgressBar.BeginAnimation(ProgressBar.MarginProperty, bar);
                Pin.MainWindow.UninstallProgressBar.BeginAnimation(ProgressBar.OpacityProperty, barOpacity);
            });
        }

        //

        private static Boolean PrepareProgressBar()
        {
            Byte maximum = 0;

            if (InstalledComponents.Remover_exe)
            {
                ++maximum;
            }

            if (InstalledComponents.Remover_exe_config)
            {
                ++maximum;
            }

            if (InstalledComponents.Service_exe)
            {
                ++maximum;
            }

            if (InstalledComponents.Service_exe_config)
            {
                ++maximum;
            }

            if (InstalledComponents.UserInit)
            {
                ++maximum;
            }

            if (InstalledComponents.Service_deregister)
            {
                ++maximum;
            }

            if (InstalledComponents.WindowsApp_Deregister != null)
            {
                ++maximum;
            }

            Pin.MainWindow.Dispatcher.Invoke(new Action(() => Pin.MainWindow.UninstallProgressBar.Maximum = maximum));

            if (maximum == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}