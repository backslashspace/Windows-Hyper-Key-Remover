using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Installer
{
    public partial class MessageBox : Window
    {
        /// <summary>Index of the pressed button from RTL</summary>
        internal Byte? Result;

        #region Main
        internal MessageBox(String title, String message, Icons icon, String buttonText0)
        {
            InitializeComponent();

            Title = title;

            BuildWindow(message, icon, 187);

            Buttons = new System.Windows.Controls.Button[1];

            AddButton(0, buttonText0, 16, false);

            Loaded += SetWindowTheme;
        }

        private void SetWindowTheme(object sender, RoutedEventArgs e)
        {
            DWMAPI.SetTheme(new System.Windows.Interop.WindowInteropHelper(this).Handle, !(Boolean)ThemeAwareness.AppsUseLightTheme);
        }

        //internal MessageBox(String Title, String Body, Icons Icon, String Button_0_Text, Byte Default_Button)
        //{
        //    if (Default_Button != 0) { throw new ArgumentException("Byte Default_Button: out of range\nOverload max = 0"); }
        //
        //    BuildWindow(Title, Body, Icon, 187);
        //
        //    Buttons = new System.Windows.Controls.Button[1];
        //
        //    PushButton(0, Button_0_Text, 14, Button_Style.Blue);
        //}
        //
        //internal MessageBox(String Title, String Body, Icons Icon, String Button_0_Text, String Button_1_Text)
        //{
        //
        //    BuildWindow(Title, Body, Icon, 211);
        //
        //    Buttons = new System.Windows.Controls.Button[2];
        //
        //    PushButton(0, Button_0_Text, 16, Button_Style.Dark);
        //
        //    PushButton(1, Button_1_Text, 103, Button_Style.Dark);
        //}
        //
        //internal MessageBox(String Title, String Body, Icons Icon, String Button_0_Text, String Button_1_Text, Byte Default_Button)
        //{
        //    BuildWindow(Title, Body, Icon, 211);
        //
        //    Buttons = new System.Windows.Controls.Button[2];
        //
        //    switch (Default_Button)
        //    {
        //        case 0:
        //            PushButton(0, Button_0_Text, 16, Button_Style.Blue);
        //            PushButton(1, Button_1_Text, 103, Button_Style.Dark);
        //            break;
        //
        //        case 1:
        //            PushButton(0, Button_0_Text, 16, Button_Style.Dark);
        //            PushButton(1, Button_1_Text, 103, Button_Style.Blue);
        //            break;
        //
        //        default:
        //            throw new ArgumentException("Byte Default_Button: out of range\nOverload max = 1 (0, 1)");
        //    }
        //}
        //
        //internal MessageBox(String Title, String Body, Icons Icon, String Button_0_Text, String Button_1_Text, String Button_2_Text)
        //{
        //    BuildWindow(Title, Body, Icon, 298);
        //
        //    Buttons = new System.Windows.Controls.Button[3];
        //
        //    PushButton(0, Button_0_Text, 16, Button_Style.Dark);
        //    PushButton(1, Button_1_Text, 103, Button_Style.Dark);
        //    PushButton(2, Button_2_Text, 190, Button_Style.Dark);
        //}
        //
        //internal MessageBox(String Title, String Body, Icons Icon, String Button_0_Text, String Button_1_Text, String Button_2_Text, Byte Default_Button)
        //{
        //    BuildWindow(Title, Body, Icon, 298);
        //
        //    Buttons = new System.Windows.Controls.Button[3];
        //
        //    switch (Default_Button)
        //    {
        //        case 0:
        //            PushButton(0, Button_0_Text, 16, Button_Style.Blue);
        //            PushButton(1, Button_1_Text, 103, Button_Style.Dark);
        //            PushButton(2, Button_2_Text, 190, Button_Style.Dark);
        //            break;
        //
        //        case 1:
        //            PushButton(0, Button_0_Text, 16, Button_Style.Dark);
        //            PushButton(1, Button_1_Text, 103, Button_Style.Blue);
        //            PushButton(2, Button_2_Text, 190, Button_Style.Dark);
        //            break;
        //
        //        case 2:
        //            PushButton(0, Button_0_Text, 16, Button_Style.Dark);
        //            PushButton(1, Button_1_Text, 103, Button_Style.Dark);
        //            PushButton(2, Button_2_Text, 190, Button_Style.Blue);
        //            break;
        //
        //        default:
        //            throw new ArgumentException("Byte Default_Button: out of range\nOverload max = 2 (0, 1, 2)");
        //    }
        //}
        #endregion


        internal enum Icons
        {
            Gear = 0,
            Gear_Tick = 1,
            Shield_Exclamation_Mark = 2,
            Triangle_Exclamation_Mark = 3,
            Circle_Error = 4,
            Shield_Error = 5,
            Tick = 6,
            Admin_Shield = 7,
            Shield_Tick = 8,
            Shield_Question = 9,
            Circle_Question = 10,
            Globe = 11,
            Lock = 12,
            Performance = 13,
        }


        private System.Windows.Controls.Button[] Buttons;

        //# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #

        private void AddButton(Byte index, String content, Double x, Boolean isPrimaryButton)
        {
            Buttons[index] = new()
            {
                Content = content,
                Margin = new Thickness(0, 0, x, 16),
                Height = 32,
                Width = 120,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            if (isPrimaryButton)
            {
                Buttons[index].Style = (Style)FindResource("Button_Primary");
            }
            else
            {
                Buttons[index].Style = (Style)FindResource("Button_Secondary");
            }

            Button_Grid.Children.Add(Buttons[index]);


            Buttons[index].Click += index switch
            {
                0 => new RoutedEventHandler(Button_0),
                1 => new RoutedEventHandler(Button_1),
                2 => new RoutedEventHandler(Button_2),
                _ => throw new NotImplementedException("MessageBox -> AddButton -> Buttons[index].Click += index switch")
            };

            if (isPrimaryButton)
            {
                Buttons[index].Focus();
                Keyboard.Focus(Buttons[index]);
            }
        }

        private void BuildWindow(String body, Icons icon, Int16 minWidth)
        {
            if (Width < MinWidth)
            {
                Width = MinWidth; 
            }

            MessageBlock.Text = "";

            Int16 position = 0;
            Byte maxLineLength =  body.Length < 350 ? (Byte)100 : (Byte)255;
            Double windowHeight;
            Double windowWidth;

            for (Int16 I = 0; I < body.Length; ++I)
            {
                if (body[I] == '\n')
                {
                    position = 0;
                }
                else if (position == maxLineLength)
                {
                    position = 0;

                    MessageBlock.Text += '\n';
                }

                MessageBlock.Text += body[I];

                ++position;
            }

            //

            MessageBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            MessageBlock.Arrange(new Rect(0, 0, DesiredSize.Width, DesiredSize.Height));

            Size textBlockSize = MessageBlock.RenderSize;

            windowWidth = textBlockSize.Width + 62 + 33;

            //padding
            if (textBlockSize.Height > MessageBlock.FontSize && textBlockSize.Height < MessageBlock.FontSize * 2)
            {
                //when one line 
                windowHeight = 26 + textBlockSize.Height + 96;
            }
            else
            {
                //when two line 
                MessageBlock.Margin = new Thickness(64, 24, 30, 0);

                windowHeight = 30 + textBlockSize.Height + 96;
            }

            //

            if (windowHeight > Height)
            {
                Height = windowHeight;
            }

            if (windowWidth > Width)
            {
                Width = windowWidth;
            }

            Dialogue_Icon.Source = icon switch
            {
                Icons.Admin_Shield => new BitmapImage(new Uri(@"Icons\imageres_78.ico", UriKind.Relative)),
                Icons.Circle_Error => new BitmapImage(new Uri(@"Icons\imageres_98.ico", UriKind.Relative)),
                Icons.Circle_Question => new BitmapImage(new Uri(@"Icons\imageres_99.ico", UriKind.Relative)),
                Icons.Gear => new BitmapImage(new Uri(@"Icons\shell32_16826.ico", UriKind.Relative)),
                Icons.Gear_Tick => new BitmapImage(new Uri(@"Icons\imageres_114.ico", UriKind.Relative)),
                Icons.Globe => new BitmapImage(new Uri(@"Icons\shell32_14.ico", UriKind.Relative)),
                Icons.Lock => new BitmapImage(new Uri(@"Icons\shell32_48.ico", UriKind.Relative)),
                Icons.Performance => new BitmapImage(new Uri(@"Icons\imageres_150.ico", UriKind.Relative)),
                Icons.Shield_Error => new BitmapImage(new Uri(@"Icons\imageres_105.ico", UriKind.Relative)),
                Icons.Shield_Exclamation_Mark => new BitmapImage(new Uri(@"Icons\imageres_107.ico", UriKind.Relative)),
                Icons.Shield_Question => new BitmapImage(new Uri(@"Icons\imageres_104.ico", UriKind.Relative)),
                Icons.Shield_Tick => new BitmapImage(new Uri(@"Icons\imageres_106.ico", UriKind.Relative)),
                Icons.Tick => new BitmapImage(new Uri(@"Icons\shell32_1_16802.ico", UriKind.Relative)),
                Icons.Triangle_Exclamation_Mark => new BitmapImage(new Uri(@"Icons\imageres_84.ico", UriKind.Relative)),
                _ => throw new NotImplementedException("MessageBox -> BuildWindow -> Dialogue_Icon.Source = icon switch")
            };
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