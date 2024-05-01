using System;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Installer
{
    internal static partial class Install
    {
        

        internal static void ApplyCustomUserInit()
        {
            Object rawUserInitString;
            
            try
            {
                rawUserInitString = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", null);
            
                String userInitString = (String)rawUserInitString;

                if (!Regex.Match(userInitString, InstallerSettings.UserInitRegexString, RegexOptions.IgnoreCase).Success)
                {
                    userInitString += InstallerSettings.UserInitString;

                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", userInitString, RegistryValueKind.String);

                    LogAppend("Added reference to user init\n");
                }

                // prevents office app from opening
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Classes\\ms-officeapp\\Shell\\Open\\Command", "", "rundll32", RegistryValueKind.String);

                // deactivate windows widgets (WIN + W)
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Dsh", "AllowNewsAndInterests", 0, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", "{2cc5ca98-6485-489a-920e-b3e88a6ccce3}", 1, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarViewMode", 2, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarOpenOnHover", 0, RegistryValueKind.DWord);

                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to configure user init:\n\n{ex.Message}");
            }
        }
    }
}