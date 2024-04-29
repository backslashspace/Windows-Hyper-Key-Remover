using System.Diagnostics;

namespace Uninstaller
{
    internal static partial class Uninstaller
    {
        private static void DeregisterService()
        {
            if (!InstalledComponents.Service_deregister)
            {
                return;
            }

            Process process = new();
            process.StartInfo.FileName = "C:\\Windows\\System32\\sc.exe";
            process.StartInfo.Verb = "runas";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = "delete \"Hyper Key User-Init Enforcer\"";
            process.Start();
            process.WaitForExit();

            MainWindow.LogAppend("Deregistered self-healing service\n");
            MainWindow.UpdateProgressBar();
        }
    }
}