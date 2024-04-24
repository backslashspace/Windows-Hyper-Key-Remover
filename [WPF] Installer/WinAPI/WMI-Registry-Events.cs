using System;
using System.Management;
using System.Security.Principal;

namespace Installer
{
    internal sealed class RegistryEvents_CurrentUser
    {
        internal ManagementEventWatcher Watcher { get; private set; }

        internal RegistryEvents_CurrentUser(String doubleEscapedSecondLevelString, String value, Action<object, EventArrivedEventArgs> action)
        {
            //sample keyPath = @"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Accent";
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            WqlEventQuery query = new($@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{currentUser.User.Value}\\{doubleEscapedSecondLevelString}' AND ValueName='{value}'");

            Watcher = new ManagementEventWatcher(query);

            Watcher.EventArrived += action.Invoke;

            Watcher.Start();
        }

        internal void Stop()
        {
            Watcher.Stop();
            Watcher.Dispose();
        }
    }
}