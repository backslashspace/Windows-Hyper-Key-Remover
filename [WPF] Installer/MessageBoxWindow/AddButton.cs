using System;
using System.Windows.Input;
using System.Windows;

namespace MessageBoxWindow
{
    public partial class MessageBox
    {
        private System.Windows.Controls.Button[] Buttons;

        private void AddButton(Byte index, String content, Double xMargin, Boolean isPrimaryButton)
        {
            Buttons[index] = new()
            {
                Content = content,
                Margin = new Thickness(0, 0, xMargin, 16),
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
    }
}