using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace HyperKey_Deregisterer
{
    internal static class Program
    {
        private const Byte vKEY_C = 0x43;
        private const Byte vKEY_W = 0x57;
        private const Byte vKEY_T = 0x54;
        private const Byte vKEY_Y = 0x59;
        private const Byte vKEY_O = 0x4F;
        private const Byte vKEY_P = 0x50;
        private const Byte vKEY_D = 0x44;
        private const Byte vKEY_L = 0x4C;
        private const Byte vKEY_X = 0x58;
        private const Byte vKEY_N = 0x4E;
        private const Byte vKEY_SPACE = 0x20;

        [DllImport("User32.dll")]
        private static extern void RegisterHotKey(IntPtr hwnd, Int32 id, UInt32 fsModifiers, UInt32 vk);

        [DllImport("User32.dll")]
        private static extern void UnregisterHotKey(IntPtr hwnd, Int32 id);

        private static void Main()
        {
            //all vkeys but teams
            Byte[] keys = [vKEY_W, vKEY_T, vKEY_Y, vKEY_O, vKEY_P, vKEY_D, vKEY_L, vKEY_X, vKEY_N, vKEY_SPACE];

            KillExplorer();

            //office keys
            for (Byte b = 0; b < keys.Length; b++)
            {
                RegisterHotKey(IntPtr.Zero, b, 0x1 + 0x2 + 0x4 + 0x8 | 0x4000, keys[b]);
            }

            //teams
            RegisterHotKey(IntPtr.Zero, 10, 0x8 | 0x4000, vKEY_C);

            StartExplorer();

            Thread.Sleep(1024);

            //overwrite
            // 11 is count of keys
            for (Byte id = 0; id < 11; ++id)
            {
                UnregisterHotKey(IntPtr.Zero, id);
            }
        }

        private static void KillExplorer()
        {
            Process process = new();

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "C:\\Windows\\System32\\taskkill.exe";
            process.StartInfo.Arguments = "/IM explorer.exe /F";

            process.Start();
            process.WaitForExit();
        }

        private static void StartExplorer()
        {
            Process process = new();

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "C:\\Windows\\explorer.exe";

            process.Start();
        }
    }
}