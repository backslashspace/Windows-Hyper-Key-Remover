using System.ServiceProcess;

namespace Enforcer
{
    internal static partial class Program
    {
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun =
            [
                new Service()
            ];
            ServiceBase.Run(ServicesToRun);
        }
    }
}