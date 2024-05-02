using System;
using System.Windows;

namespace MessageBoxWindow
{
    public partial class MessageBox : Window
    {
        /// <summary>Index of the pressed button from RTL</summary>
        internal Byte? Result;

        internal MessageBox(String title, String message, Icons icon, String buttonText)
        {
            InitializeComponent();

            Title = title;

            BuildWindow(message, icon, 0);

            Buttons = new System.Windows.Controls.Button[1];

            AddButton(0, buttonText, 16, false);

            Loaded += SetWindowTheme;
        }

        internal MessageBox(String title, String message, Icons icon, String buttonText, Byte rtlDefaultButtonIndex)
        {
            if (rtlDefaultButtonIndex != 0) { throw new ArgumentException("rtlDefaultButtonIndex: out of range\nOverload max = 0"); }

            InitializeComponent();

            Title = title;
        
            BuildWindow(message, icon, 0);
        
            Buttons = new System.Windows.Controls.Button[1];

            AddButton(0, buttonText, 16, true);

            Loaded += SetWindowTheme;
        }
        
        internal MessageBox(String title, String message, Icons icon, String buttonTextLeft, String buttonTextRight)
        {
            InitializeComponent();

            Title = title;

            BuildWindow(message, icon, 314);
        
            Buttons = new System.Windows.Controls.Button[2];

            AddButton(0, buttonTextRight, 16, false);
            AddButton(1, buttonTextLeft, 146, false);

            Loaded += SetWindowTheme;
        }

        internal MessageBox(String title, String message, Icons icon, String buttonTextLeft, String buttonTextRight, Byte rtlDefaultButtonIndex)
        {
            InitializeComponent();

            Title = title;

            BuildWindow(message, icon, 314);

            Buttons = new System.Windows.Controls.Button[2];

            switch (rtlDefaultButtonIndex)
            {
                case 0:
                    AddButton(0, buttonTextRight, 16, true);
                    AddButton(1, buttonTextLeft, 146, false);
                    break;

                case 1:
                    AddButton(0, buttonTextRight, 16, false);
                    AddButton(1, buttonTextLeft, 146, true);
                    break;

                default:
                    throw new ArgumentException("rtlDefaultButtonIndex: out of range\nOverload max = 1");
            }

            Loaded += SetWindowTheme;
        }

        internal MessageBox(String title, String message, Icons icon, String buttonTextLeft, String buttonTextMiddle, String buttonTextRight)
        {
            InitializeComponent();

            Title = title;

            BuildWindow(message, icon, 444);

            Buttons = new System.Windows.Controls.Button[3];

            AddButton(0, buttonTextRight, 16, false);
            AddButton(1, buttonTextMiddle, 146, false);
            AddButton(2, buttonTextLeft, 276, false);

            Loaded += SetWindowTheme;
        }

        internal MessageBox(String title, String message, Icons icon, String buttonTextLeft, String buttonTextMiddle, String buttonTextRight, Byte rtlDefaultButtonIndex)
        {
            InitializeComponent();

            Title = title;

            BuildWindow(message, icon, 444);

            Buttons = new System.Windows.Controls.Button[3];

            switch (rtlDefaultButtonIndex)
            {
                case 0:
                    AddButton(0, buttonTextRight, 16, true);
                    AddButton(1, buttonTextMiddle, 146, false);
                    AddButton(2, buttonTextLeft, 276, false);
                    break;

                case 1:
                    AddButton(0, buttonTextRight, 16, false);
                    AddButton(1, buttonTextMiddle, 146, true);
                    AddButton(2, buttonTextLeft, 276, false);
                    break;

                case 2:
                    AddButton(0, buttonTextRight, 16, false);
                    AddButton(1, buttonTextMiddle, 146, false);
                    AddButton(2, buttonTextLeft, 276, true);
                    break;

                default:
                    throw new ArgumentException("rtlDefaultButtonIndex: out of range\nOverload max = 2");
            }

            Loaded += SetWindowTheme;
        }
     
        //# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #

        private void SetWindowTheme(object sender, RoutedEventArgs e)
        {
            try
            {
                Installer.DWMAPI.SetTheme(new System.Windows.Interop.WindowInteropHelper(this).Handle, !Installer.ThemeAwareness.AppsUseLightTheme);
            }
            catch { }
        }

        //# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #

        #region Button_Handler
        private void Button_0(object sender, RoutedEventArgs e)
        {
            Result = 0;

            Close();
        }

        private void Button_1(object sender, RoutedEventArgs e)
        {
            Result = 1;

            Close();
        }

        private void Button_2(object sender, RoutedEventArgs e)
        {
            Result = 2;

            Close();
        }
        #endregion
    }
}