using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Data;
using Goosle.Models;
using Goosle.Views;

namespace Goosle
{
    /// <summary>
    /// Логика взаимодействия для DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public
        int flag;
        int ID;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlCommand cmd1;
        UserModel user;


        public DownloadWindow(int flag, int ID, MySqlConnection conn)
        {
            InitializeComponent();
            this.flag = flag;
            this.ID = ID;
            this.conn = conn;

            line1.Text = "Вы загружаете следующие документы:";
            if (flag == 1)
                line2.Text = "- Справка о здравии";
            else
                line2.Text = "- Справка о болезни";
            line3.Text = "- Дополнительные документы";
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void choose_Button(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == true)
            {
                // display image in picture box  
                string filename = open.FileName;
            }
            //DownloadWindow form_dow = new DownloadWindow(flag);
            //form_dow.ShowDialog();
            //Close();
            //this.Show();
        }

        private void send_Button(object sender, RoutedEventArgs e)
        {
            if(flag != 1)
            {
                DateTime begin = DateTime.Now;
                DateTime end = begin.AddDays(13);
                string sqlend = end.ToString("yyyy-MM-dd");
                string sqlbegin = begin.ToString("yyyy-MM-dd");
                cmd = new MySqlCommand("INSERT INTO COVID (id_person, is_sick, ShouldCheck, DateStart, DateEnd)" +
    "VALUES(" + ID.ToString() + ", 1, 0, '" + sqlbegin + "', '" + sqlend + "')", conn);
                //conn.Open();

                cmd.ExecuteNonQuery();

                cmd1 = new MySqlCommand("SELECT id_person FROM Attendance A INNER JOIN Students S2 on A.id_student = S2.id_student WHERE (A.id_student IN ( SELECT id_student FROM People P inner join Students S ON P.id_person = S.id_person WHERE S.id_group IN (SELECT id_group FROM Students WHERE Students.id_person = " + ID.ToString() + ")) AND (id_date >= '" + sqlbegin + "') AND (id_date<= '" + sqlend + "')) GROUP BY id_person", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new MySqlCommand("INSERT INTO COVID (id_person, ShouldCheck) " +
    "VALUES(" + dt.Rows[i].Field<int>("id_person").ToString() + ", 1)", conn);
                    //conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                cmd = new MySqlCommand("UPDATE COVID SET is_sick=0, ShouldCheck=0 WHERE id_person =" + ID.ToString(), conn);

                cmd.ExecuteNonQuery();
            }
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
