using System.Windows;

namespace Installer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UI.MainWindow = this;
            UI.Dispatcher = Dispatcher;

            ApplyAnimators();

            Loaded += InitializeApplicationThemeAwareness;

            Title += $"[{Program.AssemblyInformationalVersion} | Build {Program.AssemblyFileVersion.Revision}]";
        }

        private void ApplyAnimators()
        {
            ButtonAnimator.Initialize();
            
            ButtonAnimator.PrimaryButton.HookChild(ref IntroView.NextButton);
            ButtonAnimator.PrimaryButton.HookChild(ref SelectView.NextButton);

            ButtonAnimator.SecondaryButton.HookChild(ref IntroView.CancelButton);
            ButtonAnimator.SecondaryButton.HookChild(ref SelectView.BackButton);
            ButtonAnimator.SecondaryButton.HookChild(ref ResultView.FinishButton);

            CheckBoxAnimator.Initialize();
            CheckBoxAnimator.HookChild(ref SelectView.CheckBoxDisabled);
            CheckBoxAnimator.HookChild(ref SelectView.SelfHealingCheckBox);
        }

        private void InitializeApplicationThemeAwareness(object sender, RoutedEventArgs e)
        {
            UI.MainWindowHandle = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            DWMAPI.Initialize();

            ThemeAwareness.Initialize();
        }
    }
}