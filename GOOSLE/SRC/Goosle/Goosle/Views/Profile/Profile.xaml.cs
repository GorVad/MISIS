using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;
using Goosle.Models;
using System.Data;
using Goosle.Navigations;

namespace Goosle.Views
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        MySqlCommand cmd;
        MySqlConnection conn;
        UserModel user;
        private readonly IUserNavigation nav;

        public Profile(MySqlConnection connection, UserModel user, IUserNavigation nav)
        {
            InitializeComponent();

            this.user = user;
            this.nav = nav;
            conn = connection;

            Photo.Source = new BitmapImage(new Uri("/Goosle;component/boy.png", UriKind.Relative));

            Questionnaire.Text = "\n" + user.Surname + " " + user.Name + " " + user.Patronymic + "\n" + "Группа:  БПМ-16-2\n" + "Дата рождения:  " + user.Birthday.Substring(0, user.Birthday.Length - 8) + "\n" + "Номер телефона:  " + user.TelephoneNumber + "\n" + "Студенческий билет:  " + user.Entry_Card;
            line1.Text = "Паспортные данные:";
            line2.Text = "Серия|Номер:  " + user.Passport;
            line3.Text = "Дата выдачи:  25.06.2018";

            Covid(conn, user);

        }

        public void Covid (MySqlConnection conn, UserModel user)
        {
            cmd = new MySqlCommand("select ShouldCheck, is_sick, DateStart, DateEnd from People P inner join COVID C on P.id_person = C.id_person where Entry_Card = '" + user.Entry_Card + "'", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count != 0) 
            {
                DataRow row = dt.Rows[0];
                if (row["DateStart"] != DBNull.Value)
                {
                    line4.Visibility = Visibility.Visible;
                    string DateStart = dt.Rows[0].Field<DateTime>("DateStart").ToString();
                    line4.Text = "Дата заболевания: " + DateStart.Substring(0, DateStart.Length - 8);
                    string DateEnd = " ";
                    if (row["DateEnd"] != DBNull.Value)
                    {
                        DateEnd = dt.Rows[0].Field<DateTime>("DateEnd").ToString();
                        line5.Text = "Дата выхода: " + DateEnd.Substring(0, DateEnd.Length - 8);
                    }
                    else
                    {
                        line5.Text = "На больничном";
                    }

                }
                if (Convert.ToInt32(row["ShouldCheck"]) == 1)
                {
                    line4.Visibility = Visibility.Collapsed;
                    line5.Text = "COVID: необходима проверка";
                }
                else if (Convert.ToInt32(row["is_sick"]) == 0)
                {
                    line4.Visibility = Visibility.Collapsed;
                    line5.Text = "COVID: здоров";
                }

            }

            else
            {
                line4.Visibility = Visibility.Collapsed;
                line5.Text = "COVID: не болел";
            }
        }


        private void Update(object sender, RoutedEventArgs e)
        {

            conn.Open();
            UpdateStatus form = new UpdateStatus(user.id_person, conn, nav);
            form.ShowDialog();
            conn.Close();
            Covid(conn, user);
        }
    }
}
