using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Enforcer
{
    internal static partial class Program
    {
        private static Int32 ExitCode = 0;
        private const String ExeRegex = ",\\s*\"C:\\\\Program Files\\\\HyperKey-Deregisterer\\\\HyperKey-Deregisterer\\.exe\"(\\s*,|\\s*\\z)";

        internal static void Worker()
        {
            Boolean versionChanged = WindowsVersionChanged();
            Boolean userInitIsValid = UserInitIsValid(); // if invalid -> fix

            if (!versionChanged && userInitIsValid)
            {
                Environment.Exit(ExitCode);
            }

            if (versionChanged)
            {
                SetHelpPaneAttributes();
            }

            Environment.Exit(ExitCode);
        }

        private static Boolean WindowsVersionChanged()
        {
            Object rawCurrentBuildNumber = null;
            Object rawUBR = null;
            Object rawPreviousVersionInfo = null;

            UInt32 currentBuildNumber = 0;
            UInt32 UBR = 0;
            String previousVersionInfo = "";

            try
            {
                rawCurrentBuildNumber = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentBuildNumber", null);
                rawUBR = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "UBR", null);
                rawPreviousVersionInfo = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\HyperKey-Deregisterer", "PreviousVersionInfo", null);
            }
            catch
            {
                Environment.Exit(-1);
            }

            if (rawCurrentBuildNumber is String)
            {
                if (UInt32.TryParse((String)rawCurrentBuildNumber, out currentBuildNumber))
                {
                    Environment.Exit(-2);
                }
            }

            if (rawUBR is UInt32)
            {
                UBR = (UInt32)rawUBR;                
            }
            else
            {
                Environment.Exit(-2);
            }

            if (rawPreviousVersionInfo is String)
            {
                previousVersionInfo = (String)rawPreviousVersionInfo;
            }
            else
            {
                previousVersionInfo = "not found";
            }

            //

            if ($"{currentBuildNumber}.{UBR}" != previousVersionInfo)
            {
                //was updated

                try
                {
                    Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\HyperKey - Deregisterer", "PreviousVersionInfo", $"{currentBuildNumber}.{UBR}");
                }
                catch
                {
                    Environment.Exit(-3);
                }

                return true;
            }

            return false;
        }

        private static Boolean UserInitIsValid()
        {
            Object rawUserInitString = null;
            String userInitString = null;

            try
            {
                rawUserInitString = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", null);
            }
            catch
            {
                Environment.Exit(-1);
            }

            if (rawUserInitString is String)
            {
                userInitString = (String)rawUserInitString;
            }
            else 
            {
                Environment.Exit(-2);
            }

            Match match = Regex.Match(userInitString, ExeRegex);

            if (match.Success)
            {
                return true;
            }

            FixUserInit();

            return false;
        }

        private static void FixUserInit()
        {
            Object rawUserInitString = null;
            String userInitString = null;

            try
            {
                rawUserInitString = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", null);
            }
            catch
            {
                Environment.Exit(-1);
            }

            if (rawUserInitString is String)
            {
                userInitString = (String)rawUserInitString;
            }
            else
            {
                Environment.Exit(-2);
            }

            try
            {
                userInitString += ", \"C:\\Program Files\\HyperKey-Deregisterer\\HyperKey-Deregisterer.exe\",";

                Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", userInitString);
            }
            catch
            {
                Environment.Exit(-3);
            }
        }

        private static void SetHelpPaneAttributes()
        {
            try
            {
                String helpPanePath = "C:\\Windows\\HelpPane.exe";

                Process("takeown.exe", $"/F {helpPanePath}");
                Process("icacls.exe", $"{helpPanePath} /grant 'S-1-5-32-544':(F)");

                //explorer F1
                FileInfo file = new("C:\\Windows\\HelpPane.exe");
                AuthorizationRuleCollection accessRules = file.GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));

                FileSecurity fileSecurity = file.GetAccessControl();
                List<FileSystemAccessRule> existsList = new();

                foreach (FileSystemAccessRule rule in accessRules)
                {
                    existsList.Add(rule);
                }

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