using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Installer
{
    internal static class Program
    {
        internal static Version AssemblyVersion;
        internal static Version AssemblyFileVersion;
        internal static String AssemblyInformationalVersion;

        //this 'overrides' Main() in App.g.cs
        [STAThread()]
        private static void Main()
        {
            try
            {
                GetVersionInfos();

                App app = new();
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception ex)
            {
                AllocConsole();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Error message: ");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(ex.Message);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nStackTrace: ");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(ex.StackTrace);

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nAn unknown error occurred in the application.");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\nPress return to exit: ");

                Console.ReadLine();

                Environment.Exit(1);
            }
        }

        private static void GetVersionInfos()
        {
            AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            AssemblyFileVersion = new(fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
            AssemblyInformationalVersion = fileVersionInfo.ProductVersion;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern void AllocConsole();

    }
}