using System;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MessageBoxWindow
{
    public partial class MessageBox
    {
        private void BuildWindow(String body, Icons icon, Int16 minWidth)
        {
            if (Width < minWidth)
            {
                Width = minWidth;
            }

            Int16 position = 0;
            Byte maxLineLength = body.Length < 350 ? (Byte)100 : (Byte)255;

            for (Int16 i = 0; i < body.Length; ++i)
            {
                if (body[i] == '\n')
                {
                    position = 0;
                }
                else if (position == maxLineLength)
                {
                    position = 0;

                    MessageBlock.Text += '\n';
                }

                MessageBlock.Text += body[i];

                ++position;
            }

            MessageBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            MessageBlock.Arrange(new Rect(0, 0, DesiredSize.Width, DesiredSize.Height));

            Size textBlockSize = MessageBlock.RenderSize;

            // left text body margin, some random wpf offset??, the text width and right margin
            Double targetWidth = 64 + 14 + textBlockSize.Width + 34;

            if (targetWidth > Width)
            {
                Width = targetWidth;
            }

            // height padding
            Double windowHeight;
            if (textBlockSize.Height > MessageBlock.FontSize && textBlockSize.Height < MessageBlock.FontSize * 2)
            {
                // one line
                windowHeight = 40 + textBlockSize.Height + 96;
            }
            else
            {
                // more than one line
                MessageBlock.Margin = new Thickness(64, 24, 30, 0);

                windowHeight = 30 + textBlockSize.Height + 128;
            }

            if (windowHeight > Height)
            {
                Height = windowHeight;
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
    }
}