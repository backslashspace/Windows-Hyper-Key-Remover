using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Installer
{
    internal static partial class Install
    {
        internal static void RegisterWindowAsApp()
        {
            UInt32 estimatedSize = 0;

            try
            {
                DirectoryInfo directoryInfo = new(InstallerSettings.InstallPath);

                UInt64 size = 0;

                FileInfo[] fileInfos = directoryInfo.GetFiles();

                for (Int32 i = 0; i < fileInfos.Length; ++i)
                {
                    size += (UInt64)fileInfos[i].Length;
                }

                estimatedSize = unchecked((UInt32)(size / 1024));
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to calculate size of install directory:\n{ex.Message}");
            }

            String REGPATH = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Hyper Key Deregisterer";

            String displayIcon = $"{InstallerSettings.InstallPath}\\HyperKey-Deregisterer.exe";
            String displayName = "Windows Hyper Key Remover";
            String displayVersion = $"{Program.AssemblyFileVersion.Major}.{Program.AssemblyFileVersion.Minor}.{Program.AssemblyFileVersion.Build}.{Program.AssemblyFileVersion.Revision}";
            String publisher = "https://github.com/backslashspace";
            String uninstallString = $"{InstallerSettings.InstallPath}\\HyperKey Deregisterer Uninstaller.exe";

            //

            try
            {
                Registry.SetValue(REGPATH, "DisplayIcon", displayIcon, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "DisplayName", displayName, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "DisplayVersion", displayVersion, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "EstimatedSize", unchecked((Int32)estimatedSize), RegistryValueKind.DWord);
                Registry.SetValue(REGPATH, "NoModify", 1, RegistryValueKind.DWord);
                Registry.SetValue(REGPATH, "NoRepair", 1, RegistryValueKind.DWord);
                Registry.SetValue(REGPATH, "Publisher", publisher, RegistryValueKind.String);
                Registry.SetValue(REGPATH, "UninstallString", uninstallString, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to set registry values:\n{ex.Message}");
            }

            LogAppend("Registered as Windows App\n");
            UpdateProgressBar();
        }
    }
}