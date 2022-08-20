using Goosle.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Goosle.Views
{
    internal class SelectionObject
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public SelectionObject(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }


    /// <summary>
    /// Логика взаимодействия для LessonView.xaml
    /// </summary>
    public partial class LessonView : Page
    {
        private readonly MySqlConnection connection;
        /// <summary>
        /// Селекторы.
        /// </summary>
        private SelectionObject[] groupNames;
        private SelectionObject[] subjects;

        private Student[] students;

        private DataTable AttendsTable;

        public LessonView(MySqlConnection connection)
        {
            this.connection = connection;
            InitializeComponent();
            DateBox.SelectedDate = DateTime.Now;
            FillGroupList();
            FillSubjectsList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GroupList.SelectedItem == null || SubjectsBox.SelectedIndex == -1)
            {
                MessageBox.Show($"Требуется выбрать предмет и группу.");
                return;
            }
            var changedStudents = students.Where(s => s.isChanged).ToArray();
            MessageBox.Show($"Изменений: {changedStudents.Length}");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            for (int i = 0; i < changedStudents.Length; i++)
            {   
                var stud = changedStudents[i];
                if (stud.isFromData)
                {
                    var deleteCmd = new MySqlCommand(
                        $"UPDATE Attendance SET " +
                        $"Check_={stud.isAttend}, " +
                        $"points={stud.Points} " +
                        $"WHERE id_date='{DateBox.SelectedDate.Value.ToString("yyyy-MM-dd")}' " +
                        $"AND id_subject={subjects[SubjectsBox.SelectedIndex].Id} " +
                        $"AND id_student={stud.Id} " +
                        $"AND Check_={stud._isAttendFromData} " +
                        $"AND points={stud._pointsFromData} " +
                        $"LIMIT 1",
                        connection
                    );
                    deleteCmd.ExecuteNonQuery();
                } else
                {
                    var subjectsCmd = new MySqlCommand(
                        $"INSERT INTO " +
                        $"Attendance (id_date, id_lesson, id_subject, id_student, id_teacher, Check_, points) " +
                        $"VALUES ('{DateBox.SelectedDate.Value.ToString("yyyy-MM-dd")}', 1, {subjects[SubjectsBox.SelectedIndex].Id}, {stud.Id}, 1, {(stud.isAttend ? 1 : 0)}, {stud.Points})",
                        connection
                    );
                    subjectsCmd.ExecuteNonQuery();
                }
            }
            changedStudents.Select(s => s.isChanged = false);
        }

        private void FillSubjectsList()
        {
            var subjectsCmd = new MySqlCommand($"SELECT * FROM Subjects", connection);
            var adapter = new MySqlDataAdapter(subjectsCmd);
            var subjectsTable = new DataTable();
            adapter.Fill(subjectsTable);
            subjects = new SelectionObject[subjectsTable.Rows.Count];
            for (int i = 0; i < subjectsTable.Rows.Count; i++)
            {
                subjects[i] = new SelectionObject(int.Parse($"{subjectsTable.Rows[i]["id_subject"]}"), $"{subjectsTable.Rows[i]["Sub_Name"]}");
            }
            SubjectsBox.ItemsSource = subjects;
        }

        private void FillGroupList()
        {
            var groupCmd = new MySqlCommand($"SELECT * FROM `Group`", connection);
            var adapter = new MySqlDataAdapter(groupCmd);
            var groupTable = new DataTable();
            adapter.Fill(groupTable);
            groupNames = new SelectionObject[groupTable.Rows.Count];
            for (int i = 0; i < groupTable.Rows.Count; i++)
            {
                groupNames[i] = new SelectionObject(int.Parse($"{groupTable.Rows[i]["id_group"]}"), $"{groupTable.Rows[i]["name_group"]}");
            }
            GroupList.ItemsSource = groupNames;
        }

        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var groupName = groupNames[GroupList.SelectedIndex];

            var groupCmd = new MySqlCommand(
                $"SELECT * FROM Students " +
                $"JOIN People ON Students.id_person = People.id_person " +
                $"JOIN `Group` ON Students.id_group = `Group`.id_group " +
                $"LEFT JOIN COVID ON COVID.id_person = People.id_person " +
                $"WHERE `Group`.name_group = '{groupName.Name}'",
                connection
            );
            var adapter = new MySqlDataAdapter(groupCmd);
            var lessonsTable = new DataTable();
            adapter.Fill(lessonsTable);
            students = new Student[lessonsTable.Rows.Count];
            for (int i = 0; i < lessonsTable.Rows.Count; i++)
            {
                var name = $"{lessonsTable.Rows[i]["Surname"]} {lessonsTable.Rows[i]["Name"]} {lessonsTable.Rows[i]["Patronymic"]}";
                students[i] = new Student(int.Parse($"{lessonsTable.Rows[i]["id_student"]}"), name);
                if (bool.TryParse($"{lessonsTable.Rows[i]["is_sick"]}", out bool value))
                {
                    students[i].Color = (Brush) new BrushConverter().ConvertFrom("#FF3939");
                }
            }
            StudentsListView.ItemsSource = students;
            if (SubjectsBox.SelectedIndex != -1 && DateBox.SelectedDate.HasValue)
            {
                LessonList_SelectionChanged(sender, e);
            }
        }

        private void LessonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (students == null)
            {
                return;
            }
            if (students.Length != 0)
            {
                foreach (var s in students)
                {
                    s.Points = 0;
                    s.isAttend = false;
                }
            }
            if (GroupList.SelectedIndex == -1)
            {
                return;
            }
            var groupName = groupNames[GroupList.SelectedIndex];
            var dateDBformat = DateBox.SelectedDate.Value.ToString("yyyy-MM-dd");
            var selectedSub = subjects[SubjectsBox.SelectedIndex];
            var lessonsCmd = new MySqlCommand(
                $"SELECT * FROM Attendance " +
                $"JOIN Students ON Students.id_student=Attendance.id_student " +
                $"WHERE Attendance.id_date='{dateDBformat}' AND Attendance.id_subject={selectedSub.Id}",
                connection
            );
            var adapter = new MySqlDataAdapter(lessonsCmd);
            AttendsTable = new DataTable();
            adapter.Fill(AttendsTable);
            for (int i = 0; i < AttendsTable.Rows.Count; i++)
            {
                var studId = int.Parse($"{AttendsTable.Rows[i]["id_student"]}");
                var isCheck = bool.Parse($"{AttendsTable.Rows[i]["Check_"]}");
                var points = int.Parse($"{AttendsTable.Rows[i]["points"]}");
                var id = int.Parse($"{AttendsTable.Rows[i]["points"]}");
                if (students.Any(s => s.Id == studId))
                {
                    var student = students.First(s => s.Id == studId);
                    student.SetFromData(isCheck, points);
                }
            }
            StudentsListView.ItemsSource = students;
        }
    }
}
