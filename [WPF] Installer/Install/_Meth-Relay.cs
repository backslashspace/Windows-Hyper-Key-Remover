using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Installer
{
    internal static partial class InstallerWorker
    {
        private static Double CurrentProgressState;

        private static void UpdateProgressBar()
        {
            Pin.MainWindow.Dispatcher.Invoke(() =>
            {
                DoubleAnimation doubleAnimation = new();
                doubleAnimation.Duration = MainWindow.Duration;
                doubleAnimation.To = ++CurrentProgressState;
                doubleAnimation.DecelerationRatio = 1;

                Pin.MainWindow.ResultView.InstallProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
            });
        }

        private static void LogAppend(String newLine)
        {
            Pin.MainWindow.Dispatcher.Invoke(() =>
            {
                Pin.MainWindow.ResultView.LogBox.Text += newLine;
            });
        }
    }
}