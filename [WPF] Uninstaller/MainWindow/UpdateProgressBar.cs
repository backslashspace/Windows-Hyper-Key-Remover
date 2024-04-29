using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Uninstaller
{
    public partial class MainWindow
    {
        private static Double CurrentProgressState;

        internal static void UpdateProgressBar()
        {
            Pin.MainWindow.Dispatcher.Invoke(() =>
            {
                DoubleAnimation doubleAnimation = new();
                doubleAnimation.Duration = MainWindow.Duration;
                doubleAnimation.To = ++CurrentProgressState;
                doubleAnimation.DecelerationRatio = 1;

                Pin.MainWindow.UninstallProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
            });
        }
    }
}