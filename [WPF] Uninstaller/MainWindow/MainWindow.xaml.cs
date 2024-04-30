using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Uninstaller
{
    public partial class MainWindow : Window
    {
        internal static Duration Duration = new(TimeSpan.FromSeconds(1));

        public MainWindow()
        {
            InitializeComponent();

            Pin.MainWindow = this;
        }

        private async void UninstallButton_Click(object sender, RoutedEventArgs e)
        {
            UninstallButton.Click -= UninstallButton_Click;

            ThicknessAnimation bar = new();
            bar.Duration = Duration;
            bar.To = new(48, 0, 48, 48);
            bar.DecelerationRatio = 1;

            UninstallProgressBar.BeginAnimation(ProgressBar.MarginProperty, bar);

            ThicknessAnimation button = new();
            button.Duration = Duration;
            button.To = new(0, 0, 0, -32);
            button.AccelerationRatio = 1;

            DoubleAnimation opacity = new();
            opacity.Duration = Duration;
            opacity.To = 0;
            opacity.AccelerationRatio = 1;

            UninstallButton.BeginAnimation(Button.MarginProperty, button);
            UninstallButton.BeginAnimation(Button.OpacityProperty, opacity);

            ThicknessAnimation logBox = new();
            logBox.Duration = Duration;
            logBox.To = new(0, 32, 0, 0);
            logBox.DecelerationRatio = 1;

            LogBoxBorder.BeginAnimation(Border.MarginProperty, logBox);

            await Task.Delay(1100).ConfigureAwait(false);

            Thread worker = new(() => Uninstaller.Worker());
            worker.Start();
        }

        internal void FinishUninstall(object sender, RoutedEventArgs e)
        {
            if (Uninstaller.InstalledComponents.Directory)
            {
                try
                {
                    String tempPath = Path.GetTempPath() + "hyperkeydereguninstallhelper";

                    Directory.CreateDirectory(tempPath);

                    Assembly assembly = Assembly.GetExecutingAssembly();

                    using (FileStream exeFile = File.Create($"{tempPath}\\Hyper Key Uninstall Helper.exe"))
                    {
                        assembly.GetManifestResourceStream("Uninstaller.Hyper Key Uninstall Helper.exe").CopyTo(exeFile);
                    }

                    using (FileStream confFile = File.Create($"{tempPath}\\Hyper Key Uninstall Helper.exe.config"))
                    {
                        assembly.GetManifestResourceStream("Uninstaller.Hyper Key Uninstall Helper.exe.config").CopyTo(confFile);
                    }

                    Process process = new();
                    process.StartInfo.FileName = $"{tempPath}\\Hyper Key Uninstall Helper.exe";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
                catch { }
            }

            Close();
        }
    }
}