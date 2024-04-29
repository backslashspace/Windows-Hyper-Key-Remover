namespace Enforcer
{
    partial class Service
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(System.Boolean disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "User-init Enforcer";
        }
    }
}