using Goosle.Models;
using Goosle.Views;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Goosle.Navigations
{
    /// <summary>
    /// Логика взаимодействия для TeacherNavigation.xaml
    /// </summary>
    public partial class TeacherNavigation : Page, IUserNavigation
    {
        MySqlConnection connection;
        private readonly UserModel user;

        public TeacherNavigation(MySqlConnection mySqlConnection, UserModel user)
        {
            connection = mySqlConnection;
            this.user = user;
            InitializeComponent();
            ChangeHeaderName($"{user.Surname} {user.Name[0]}. {user.Patronymic[0]}.");
            TeacherFrame.Navigate(new Profile(connection, user, this));
        }

        private void Lesson_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new LessonView(connection));
        }

        public void ChangeHeaderName(string value)
        {
            NameField.Text = value;
        }

        public void Profile_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new Profile(connection, user, this));
        }

        public void Shedule_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new SheduleView(connection, user.id_person));
        }

        public void CovidStats_Click(object sender, RoutedEventArgs e)
        {
            TeacherFrame.Navigate(new StatsView(connection, user));
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
            
        }

        public void ShowProfileCOVID_Status()
        {
            
        }
    }
}
