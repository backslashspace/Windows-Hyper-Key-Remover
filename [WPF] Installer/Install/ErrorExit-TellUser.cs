using System;
using System.Windows.Media;

namespace Installer
{
    internal static partial class Install
    {
        private static void ErrorExit(String message)
        {
            UI.Dispatcher.Invoke(() =>
            {
                UI.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                MessageBoxWindow.MessageBox messageBox = new("Error", message, MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");

                messageBox.ShowDialog();
            });

            Environment.Exit(-1);
        }
    }
}