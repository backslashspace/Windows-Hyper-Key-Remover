using System;
using System.Text.RegularExpressions;

namespace Enforcer
{
    internal static partial class Program
    {
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
                userInitString += ", \"C:\\Program Files\\Hyper Key Remover\\HyperKey-Deregisterer.exe\",";

                Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", userInitString);
            }
            catch
            {
                Environment.Exit(-3);
            }
        }
    }
}