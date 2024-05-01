using System;
using System.IO;

namespace Installer
{
    internal static partial class Install
    {
        internal static void CleanUp()
        {
            try
            {
                Directory.Delete(InstallerSettings.InstallPath, true);

                LogAppend("Performed post cleanup\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to perform post install clean up:\n\n{ex.Message}");
            }
        }
    }
}