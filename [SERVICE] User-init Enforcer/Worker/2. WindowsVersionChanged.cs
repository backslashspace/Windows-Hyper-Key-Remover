using System;

namespace Enforcer
{
    internal static partial class Program
    {
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
                if (!UInt32.TryParse((String)rawCurrentBuildNumber, out currentBuildNumber))
                {
                    Environment.Exit(-2);
                }
            }

            if (rawUBR is Int32)
            {
                UBR = unchecked((UInt32)((Int32)rawUBR));
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
                    Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\HyperKey-Deregisterer", "PreviousVersionInfo", $"{currentBuildNumber}.{UBR}");
                }
                catch
                {
                    Environment.Exit(-3);
                }

                return true;
            }

            return false;
        }
    }
}