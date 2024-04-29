namespace Uninstaller
{
    internal static partial class Uninstaller
    {
        private static void DeregisterWindowsApp()
        {
            if (InstalledComponents.WindowsApp_Deregister == null)
            {
                return;
            }

            try
            {
                InstalledComponents.WindowsApp_Deregister.DeleteSubKeyTree("Hyper Key Deregisterer");

                MainWindow.LogAppend("Deregistered Windows App\n");
                MainWindow.UpdateProgressBar();
            }
            catch
            {
                MainWindow.LogAppend("! Failed to remove from Windows App list\n");
            }
        }
    }
}