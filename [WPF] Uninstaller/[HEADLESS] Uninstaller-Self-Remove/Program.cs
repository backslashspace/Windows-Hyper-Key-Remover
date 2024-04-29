using System;
using System.IO;
using System.Threading;

namespace Uninstaller_Self_Remove
{
    internal static class Program
    {
        private static void Main()
        {
            Thread.Sleep(5120);

            for (Byte b = 0; b < 10; ++b)
            {
                try
                {
                    Directory.Delete("C:\\Program Files\\Hyper Key Remover", true);

                    Environment.Exit(0);
                }
                catch
                {
                    if (b == 9)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(1024);
                    }
                }
            }
        }
    }
}