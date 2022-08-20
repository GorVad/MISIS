using Goosle.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Goosle.Views
{
    /// <summary>
    /// Логика взаимодействия для SheduleView.xaml
    /// </summary>
    public partial class SheduleView : Page
    {
        private readonly MySqlConnection connection;
        private readonly static string[] WeekNames = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
        private readonly List<LessonModel> lessonList;
        private BrushConverter colorConverter = new BrushConverter();
        private bool isTeacher = false;
        private DataTable CacheData;

        public SheduleView(MySqlConnection connection, int userId)
        {
            this.connection = connection;
            InitializeComponent();
            var user = new MySqlCommand($"SELECT * FROM People WHERE id_person = {userId} LIMIT 1", connection);
            var userAdapter = new MySqlDataAdapter(user);
            var userTable = new DataTable();
            userAdapter.Fill(userTable);
            MySqlCommand cmd;
            if ($"{userTable.Rows[0]["Role"]}" == "Студент")
            {
                cmd = GetStudentCommand(Convert.ToInt32($"{userTable.Rows[0]["id_person"]}"));
            } else
            {
                isTeacher = true;
                cmd = GetTeacherCommand(Convert.ToInt32($"{userTable.Rows[0]["id_person"]}"));
            }
            var da = new MySqlDataAdapter(cmd);
            CacheData = new DataTable();
            lessonList = new List<LessonModel>();
            da.Fill(CacheData);
            foreach (DataRow row in CacheData.Rows)
            {
                lessonList.Add(new LessonModel()
                {
                    Group = $"{row["name_group"]}",
                    Name = $"{row["Sub_Name"]}",
                    Teacher = $"{row["Name"]} {row["Surname"]} {row["Patronymic"]}",
                    Office = $"{row["Lesson_office"]}",
                    WeekType = $"{row["day_of_week"]}",
                    StartedAt = $"{row["Start"]}"
                });
            }
            FillViewWithSheduleBlocks();
        }

        public void FillViewWithSheduleBlocks()
        {
            for (int indX = 0, dayInd = 0; indX < 2; indX++)
            {
                for (var indY = 0; indY < 3; indY++, dayInd++)
                {
                    var weekField = new TextBlock();
                    weekField.Margin = new Thickness(5, 5, 0, 0);
                    weekField.FontSize = 14;
                    weekField.Text = WeekNames[dayInd];
                    var grid = CreateSheduleGrid();
                    var list = lessonList.Where(l => l.WeekType == WeekNames[dayInd]);
                    Grid.SetRow(weekField, indY);
                    Grid.SetColumn(weekField, indX);
                    grid.ItemsSource = list;
                    Grid.SetRow(grid, indY);
                    Grid.SetColumn(grid, indX);
                    SheduleGrid.Children.Add(weekField);
                    SheduleGrid.Children.Add(grid);
                }
            }
        }

        private GridViewColumn CreateColumn(string bindingName, string columnName, int width)
        {
            return new GridViewColumn()
            {
                Header = columnName,
                DisplayMemberBinding = new Binding(bindingName),
                Width = width
            };
        }

        private ListView CreateSheduleGrid()
        {
            var grid = new ListView()
            {
                Padding = new Thickness(-1),
                Margin = new Thickness(5, 25, 5, 5),
                BorderThickness = new Thickness(2),
                Background = Brushes.White,
                BorderBrush = (Brush)colorConverter.ConvertFrom("#3F9EA0"),
            };
            grid.View = AddColumns();
            grid.ItemContainerStyle = Application.Current.FindResource("ListViewItemStyle") as Style;
            return grid;
        }

        private GridView AddColumns()
        {
            var grid = new GridView();
            grid.ColumnHeaderContainerStyle = Application.Current.FindResource("ListViewHeaderStyle") as Style;
            grid.Columns.Add(CreateColumn("Name", "Предмет", 120));
            if (isTeacher)
            {
                grid.Columns.Add(CreateColumn("Group", "Группа", 100));
            } else
            {
                grid.Columns.Add(CreateColumn("Teacher", "Препод.", 100));
            }
            grid.Columns.Add(CreateColumn("StartedAt", "Время", 100));
            grid.Columns.Add(CreateColumn("Office", "Кабинет", 100));
            return grid;
        }

        private MySqlCommand GetTeacherCommand(int userId)
        {
            return new MySqlCommand(
            "SELECT " +
                "Schedule.day_of_week, " +
                "`Group`.name_group, " +
                "People.Name, " +
                "People.Surname, " +
                "People.Patronymic, " +
                "Offices.Lesson_office, " +
                "Subjects.Sub_Name, " +
                "Lesson_duration.Start, " +
                "Lesson_duration.End " +
            "FROM Schedule " +
                "JOIN `Group` ON Schedule.id_group = `Group`.id_group " +
                "JOIN Teachers ON Teachers.id_teacher = Schedule.id_teacher " +
                "JOIN People ON Teachers.id_person = People.id_person " +
                "JOIN Lesson_duration ON Lesson_duration.id_lesson = Schedule.id_lesson " +
                "JOIN Subjects ON Schedule.id_subject = Subjects.id_subject " +
                "JOIN Offices ON Schedule.id_office = Offices.id_office " +
            $"WHERE People.id_person = {userId}",
            connection);
        }

        private MySqlCommand GetStudentCommand(int userId)
        {
            var studentCmd = new MySqlCommand(
                $"SELECT * FROM Students JOIN `Group` ON `Group`.id_group = Students.id_group " +
                $"WHERE Students.id_person = {userId}",
                connection
            );
            var adapter = new MySqlDataAdapter(studentCmd);
            var studentTable = new DataTable();
            adapter.Fill(studentTable);
            var groupId = $"{studentTable.Rows[0]["id_group"]}";
            return new MySqlCommand(
            "SELECT " +
                "Schedule.day_of_week, " +
                "`Group`.name_group, " +
                "People.Name, " +
                "People.Surname, " +
                "People.Patronymic, " +
                "Offices.Lesson_office, " +
                "Subjects.Sub_Name, " +
                "Lesson_duration.Start, " +
                "Lesson_duration.End " +
            "FROM Schedule " +
                "JOIN `Group` ON Schedule.id_group = `Group`.id_group " +
                "JOIN Teachers ON Teachers.id_teacher = Schedule.id_teacher " +
                "JOIN People ON Teachers.id_person = People.id_person " +
                "JOIN Lesson_duration ON Lesson_duration.id_lesson = Schedule.id_lesson " +
                "JOIN Subjects ON Schedule.id_subject = Subjects.id_subject " +
                "JOIN Offices ON Schedule.id_office = Offices.id_office " +
            $"WHERE Schedule.id_group = {groupId}",
            connection);
        }
    }
}
