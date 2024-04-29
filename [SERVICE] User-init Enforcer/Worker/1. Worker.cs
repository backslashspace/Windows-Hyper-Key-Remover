using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace Enforcer
{
    internal static partial class Program
    {
        private static Int32 ExitCode = 0;
        private const String ExeRegex = ",\\s*\"C:\\\\Program Files\\\\Hyper Key Remover\\\\HyperKey-Deregisterer\\.exe\"(\\s*,|\\s*\\z)";

        internal static void Worker()
        {
            Boolean versionChanged = WindowsVersionChanged();
            Boolean userInitIsValid = UserInitIsValid(); // if invalid -> fix

            if (!versionChanged && userInitIsValid)
            {
                Thread.Sleep(5120);

                Environment.Exit(ExitCode);
            }

            if (versionChanged)
            {
                // prevents office app from opening
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Classes\\ms-officeapp\\Shell\\Open\\Command", "", "rundll32", RegistryValueKind.String);

                // deactivate windows widgets (WIN + W)
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Dsh", "AllowNewsAndInterests", 0, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", "{2cc5ca98-6485-489a-920e-b3e88a6ccce3}", 1, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarViewMode", 2, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarOpenOnHover", 0, RegistryValueKind.DWord);

                SetHelpPaneAttributes();
            }

            Thread.Sleep(5120);

            Environment.Exit(ExitCode);
        }

        //

        private static void SetHelpPaneAttributes()
        {
            try
            {
                String helpPanePath = "C:\\Windows\\HelpPane.exe";

                Process("takeown.exe", $"/F {helpPanePath}");
                Process("icacls.exe", $"{helpPanePath} /grant 'S-1-5-32-544':(F)");

                // explorer F1
                FileInfo file = new("C:\\Windows\\HelpPane.exe");
                AuthorizationRuleCollection accessRules = file.GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));

                FileSecurity fileSecurity = file.GetAccessControl();
                List<FileSystemAccessRule> existsList = new();

                foreach (FileSystemAccessRule rule in existsList)
                {
                    fileSecurity.RemoveAccessRuleAll(rule);
                }

                file.SetAccessControl(fileSecurity);
            }
            catch
            {
                ExitCode = -4;
            }   
        }

        private static void Process(String Path, String Args)
        {
            Process process = new();

            process.StartInfo.FileName = Path;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;

            process.StartInfo.Arguments = Args;

            process.Start();
            process.WaitForExit();
        }
    }
}