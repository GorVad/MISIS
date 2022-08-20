using Goosle.Views;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using Goosle.Navigations;
using Goosle.Models;
using System.Data;

namespace Goosle
{
    /// <summary>
    /// Логика взаимодействия для StudentNavigation.xaml
    /// </summary>
    public partial class StudentNavigation : Page, IUserNavigation
    {
        MySqlConnection conn;
        private readonly UserModel user;

        public StudentNavigation(MySqlConnection connection, UserModel user)
        {
            InitializeComponent();
            conn = connection;
            this.user = user;
            if (RED_BUTTON(conn, user) == false)
                this.RED.Visibility = Visibility.Hidden;
            ChangeHeaderName($"{user.Surname} {user.Name[0]}. {user.Patronymic[0]}.");
            StudentFrame.Navigate(new Profile(conn, user, this));
        }

        private void Group(object sender, RoutedEventArgs e)
        {
            StudentFrame.Navigate(new Group(conn, user.Entry_Card));
        }

        public void ChangeHeaderName(string value)
        {
            NameField.Text = value;
        }

        public void Profile_Click(object sender, RoutedEventArgs e)
        {
            StudentFrame.Navigate(new Profile(conn, user, this));
        }

        public void Shedule_Click(object sender, RoutedEventArgs e)
        {
            StudentFrame.Navigate(new SheduleView(conn, user.id_person));
        }

        public void CovidStats_Click(object sender, RoutedEventArgs e)
        {
            StudentFrame.Navigate(new StatsView(conn, user));
        }

        public bool RED_BUTTON(MySqlConnection conn, UserModel user)
        {
            MySqlCommand cmd = new MySqlCommand("select ShouldCheck from COVID where id_person = " + user.id_person, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows[0].Field<bool>("ShouldCheck") != true)
                return false;
            else
                return true;
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Hide();
            Application.Current.MainWindow = new MainWindow();
        }

        public void HideProfileCOVID_Status()
        {
            RED.Visibility = Visibility.Hidden;
        }

        public void ShowProfileCOVID_Status()
        {
            RED.Visibility = Visibility.Visible;
        }
    }
}
