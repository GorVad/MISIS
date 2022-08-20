using System;
using System.Windows;
using System.Windows.Input;

namespace Goosle
{
    /// <summary>
    /// Логика взаимодействия для AdditionalInformation.xaml
    /// </summary>
    public partial class AdditionalInformation : Window
    {
        public AdditionalInformation(string text)
        {
            InitializeComponent();
            textBox.Text = text;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Позволяет перетаскивать окно программы по экрану зажатой левой кнопкой мыши
        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (InvalidOperationException) { }
        }
    }
}
