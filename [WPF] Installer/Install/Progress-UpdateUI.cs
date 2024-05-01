using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Installer
{
    internal static partial class Install
    {
        private static Double CurrentProgressBarValue;

        private static void UpdateProgressBar()
        {
            UI.Dispatcher.Invoke(() =>
            {
                DoubleAnimation doubleAnimation = new();
                doubleAnimation.Duration = UISettings.AnimationDuration;
                doubleAnimation.To = ++CurrentProgressBarValue;
                doubleAnimation.DecelerationRatio = 1;

                UI.MainWindow.ResultView.InstallProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
            });
        }

        internal static void LogAppend(String newLine)
        {
            UI.Dispatcher.Invoke(() =>
            {
                UI.MainWindow.ResultView.LogBox.Text += newLine;
            });
        }
    }
}