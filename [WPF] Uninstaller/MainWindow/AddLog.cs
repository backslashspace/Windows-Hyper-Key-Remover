using System;

namespace Uninstaller
{
    public partial class MainWindow
    {
        internal static void LogAppend(String newLine)
        {
            Pin.MainWindow.Dispatcher.Invoke(() =>
            {
                Pin.MainWindow.LogBox.Text += newLine;
            });
        }
    }
}