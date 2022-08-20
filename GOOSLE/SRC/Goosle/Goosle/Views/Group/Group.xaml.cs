using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using System.Data;



namespace Goosle.Views
{
    /// <summary>
    /// Логика взаимодействия для Group1.xaml
    /// </summary>
    public partial class Group : Page
    {
        MySqlConnection conn;
        string LoginName = "s123456";
        MySqlCommand cmd;


        public Group(MySqlConnection connection, string login)
        {
            InitializeComponent();
            LoginName = login;
            conn = connection;
            cmd = new MySqlCommand("SELECT Name, Surname, Patronymic, Birthday, Age from (SELECT Name, Surname, Patronymic, Birthday, Age, id_group from Students S Inner JOIN People P on S.id_person = P.id_person) as T where(T.id_group IN(SELECT T1.id_group from(SELECT id_group, Entry_Card from Students S Inner JOIN People P on S.id_person = P.id_person) as T1 where T1.Entry_Card = '" + 
                LoginName + "')); ", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            List<MyTable> studentList = new List<MyTable>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MyTable student = new MyTable();
                //student.StudentId = Convert.ToInt32(dt.Rows[i]["StudentId"]);
                student.Name = dt.Rows[i]["Name"].ToString();
                student.Surname = dt.Rows[i]["Surname"].ToString();
                student.Patronymic = dt.Rows[i]["Patronymic"].ToString();
                student.Birthday = dt.Rows[i]["Birthday"].ToString().Substring(0, 10);
                student.Age = dt.Rows[i]["Age"].ToString();
                studentList.Add(student);
            }
            //System.Console.WriteLine(studentList.);
            groupGrid.ItemsSource = studentList;




            //Поменяю на запросы гогда будут заполнены таблицы
            //cmd1 = new MySqlCommand("SELECT * FROM Students", conn);
            //MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
            //da1 = new MySqlDataAdapter(cmd1);
            //DataTable dt1 = new DataTable();
            //da1.Fill(dt1);
            GroupNumber.Text = "Номер группы: МПИ-20-4-2";

            Quantity.Text = "Количество студентов: " + studentList.Count;
            Sensei.Text = "Куратор: Широков Андрей Иванович";
            ////Gop.Text = dt.Rows[0].Field<string>("Name");

        }
        //Получаем данные из таблицы
        private void grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MyTable path = groupGrid.SelectedItem as MyTable;
            AdditionalInformation form = new AdditionalInformation("ФИО: " + path.Name + " " + path.Surname + " " + path.Patronymic +
                "\nТелефон: +79268735239" + "\nEmail: stdem@mail.ru");
            form.ShowDialog();
            //MessageBox.Show("Информация о выбранном студенте/преподавателе\n" + "ФИО: " + path.Name + " " + path.Surname + " " + path.Patronymic + 
            //    "\nТелефон: +79268735239" + "\nEmail: stdem@mail.ru");

        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Contains("ID") || e.PropertyName.Contains("id_t") || e.PropertyName.Contains("id_kid") || e.PropertyName.Contains("id_bab") || e.PropertyName.Contains("id_pa") || e.PropertyName.Contains("id_adm") || e.PropertyName == "id_Schedule")
                e.Column.Visibility = Visibility.Hidden;

            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";

            e.Column.Width = DataGridLength.Auto;
            //e.Column.Width = (dataGrid.Width - 19) / 8;

            if (e.PropertyName == "Name")
                e.Column.Header = "Имя";
            if (e.PropertyName.Contains("Surname"))
                e.Column.Header = "Отчество";
            if (e.PropertyName.Contains("Pytronamic"))
                e.Column.Header = "Отчество";
            if (e.PropertyName.Contains("Age"))
                e.Column.Header = "Возраст";
            if (e.PropertyName.Contains("Passport"))
                e.Column.Header = "Пасспорт";
            if (e.PropertyName.Contains("Birthday"))
                e.Column.Header = "Дата рождения";
            if (e.PropertyName.Contains("TelephoneNumber"))
                e.Column.Header = "Телефон";
            if (e.PropertyName.Contains("name_tutor"))
                e.Column.Header = "Имя";
            if (e.PropertyName.Contains("name_baby"))
                e.Column.Header = "Имя";
            if (e.PropertyName.Contains("name_kid"))
                e.Column.Header = "Имя";
            if (e.PropertyName == "name_administrator")
                e.Column.Header = "Имя";
            if (e.PropertyName.Contains("surname"))
                e.Column.Header = "Фамилия";
            if (e.PropertyName.Contains("group"))
                e.Column.Header = "Группа";
            if (e.PropertyName.Contains("work"))
                e.Column.Header = "Должность";
            e.Column.Width = DataGridLength.Auto;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        class MyTable
        {
            //public MyTable(string Name, string Surname,  string Patronymic, string Birthday, string Age)
            //{
            //    this.Name = Name;
            //    this.Surname = Surname;
            //    this.Patronymic = Patronymic;
            //    this.Birthday = Birthday;
            //    this.Age = Age;
            //}
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string Birthday { get; set; }
            public string Age { get; set; }
        }
    }
}
