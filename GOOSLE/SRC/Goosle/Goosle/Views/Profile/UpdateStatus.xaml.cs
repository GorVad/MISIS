using System;
using System.Windows;
using System.Windows.Input;
using Goosle.Navigations;
using MySql.Data.MySqlClient;

namespace Goosle
{
    /// <summary>
    /// Логика взаимодействия для UpdateStatus.xaml
    /// </summary>
    public partial class UpdateStatus : Window
    {
        int ID;
        MySqlConnection conn;
        private readonly IUserNavigation nav;

        public UpdateStatus(int ID, MySqlConnection conn, IUserNavigation nav)
        {
            InitializeComponent();
            this.ID = ID;
            this.conn = conn;
            this.nav = nav;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void healthy_Button(object sender, RoutedEventArgs e)
        {
            nav.HideProfileCOVID_Status();
            this.Hide();
            int document_flag = 1;
            DownloadWindow form_dow = new DownloadWindow(document_flag, ID, conn);
            form_dow.ShowDialog();
            Close();
        }

        private void sick_Button(object sender, RoutedEventArgs e)
        {
            nav.ShowProfileCOVID_Status();
            this.Hide();
            int document_flag = 2;
            DownloadWindow form_dow = new DownloadWindow(document_flag, ID, conn);
            form_dow.ShowDialog();


            //this.Show();
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
