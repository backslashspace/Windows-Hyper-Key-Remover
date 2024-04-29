using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;

namespace Uninstaller
{
    internal static partial class Uninstaller
    {
        private static void ClearUserInit()
        {
            if (!InstalledComponents.UserInit)
            {
                return;
            }

            try
            {
                Object rawUserInitString = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", null);

                String newUserInitString = Regex.Replace((String)rawUserInitString, UserInitRegex, ",");

                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", newUserInitString);

                MainWindow.LogAppend("Removed user init reference\n");
                MainWindow.UpdateProgressBar();
            }
            catch
            {
                MainWindow.LogAppend("! Failed to remove user init reference\n");
            }
        }
    }
}