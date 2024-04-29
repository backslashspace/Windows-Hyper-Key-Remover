using System.ServiceProcess;
using System;
using System.Threading;

namespace Enforcer
{
    public partial class Service : ServiceBase
    {
        private static Thread Worker;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(String[] args)
        {
            Worker = new(() => Program.Worker());
            Worker.Name = "Worker Thread";
            Worker.Start();
        }

        protected override void OnStop()
        {
            Worker.Join();
        }
    }
}