using Goosle.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Goosle.Views
{
    internal class DepSelection : SelectionObject
    {
        public int id_university { get; set; }
        public int On_Quarantine { get; set; }
        public DepSelection(int id, string name, int on_quarantine, int id_univer) : base(id, name)
        {
            On_Quarantine = on_quarantine;
            id_university = id_univer;
        }
    }
    struct DbDate
    {
        public DbDate(string date, string count)
        {
            Date = date;
            Count = count;
        }
        public string Date;
        public string Count;
    }
    /// <summary>
    /// Логика взаимодействия для StatsView.xaml
    /// </summary>
    public partial class StatsView : Page
    {
        private const int COLUMNS = 12;
        private DepSelection[] Departments;
        private DbDate[] ExistedDates;
        private bool isAdministator;
        private MySqlConnection conn;
        private UserModel user;

        public StatsView(MySqlConnection conn, UserModel user)
        {
            InitializeComponent();
            this.conn = conn;
            this.user = user;
            Departments = GetSelections();
            DepSelection.ItemsSource = Departments;
            DepSelection.SelectedIndex = 0;
            isAdministator = IsAdministrator(user.id_person);
            if (isAdministator)
            {
                Button_GoQuarantine.Visibility = Visibility.Visible;
                TB_Recomendation.Visibility = Visibility.Visible;
                TB_RecomendationTitle.Visibility = Visibility.Visible;
            }
        }

        private bool IsAdministrator(int id_person)
        {
            var cmd = new MySqlCommand($"SELECT * FROM Administration WHERE id_person={id_person} LIMIT 1", conn);
            var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count == 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private void Button_GoQuarantine_Click(object sender, RoutedEventArgs e)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var selectedDep = Departments[DepSelection.SelectedIndex];
            if (selectedDep.Id == -1)
            {
                var cmd = new MySqlCommand(
                    $"UPDATE Departments " +
                    $"JOIN University ON Departments.id_university=University.id_university " +
                    $"SET Departments.on_quarantine=0", conn);
                cmd.ExecuteNonQuery();
                var deps = Departments.Where(d => d.id_university == selectedDep.id_university).ToArray();
                for (var i = 0; i < deps.Count(); i++)
                {
                    deps[i].On_Quarantine = 1;
                }
                MessageBox.Show($"Весь университет отправлен на карантин!");
                return;
            }
            if (selectedDep.On_Quarantine == 1)
            {
                selectedDep.On_Quarantine = 0;
                var cmd = new MySqlCommand(
                    $"UPDATE Departments SET " +
                    $"on_quarantine=0 " +
                    $"WHERE id_department={selectedDep.Id};", conn);
                cmd.ExecuteNonQuery();
            }
            else if (selectedDep.On_Quarantine == 0)
            {
                selectedDep.On_Quarantine = 1;
                var cmd = new MySqlCommand(
                    $"UPDATE Departments SET " +
                    $"on_quarantine=1 " +
                    $"WHERE id_department={selectedDep.Id};", conn);
                cmd.ExecuteNonQuery();
            }
            ChangeCovidRecomendationStatus(selectedDep);
            MessageBox.Show("Сохранено!");
            return;
        }

        private void SetTodayStats(SelectionObject depObject)
        {
            MySqlCommand todayCommand;
            if (depObject.Id == -1)
            {
                todayCommand = new MySqlCommand(
                    "SELECT Count(is_sick) as is_sick_count FROM COVID " +
                    "JOIN People ON People.id_person=COVID.id_person " +
                    $"WHERE COVID.is_sick=1 AND COVID.DateStart=@date",
                    conn
                );
                todayCommand.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
            } else
            {
                todayCommand = new MySqlCommand(
                    "SELECT Count(is_sick) as is_sick_count FROM COVID " +
                    "JOIN People ON People.id_person=COVID.id_person " +
                    $"WHERE People.id_department=@id_department AND COVID.is_sick=1 AND COVID.DateStart=@date",
                    conn
                );
                todayCommand.Parameters.AddWithValue("@id_department", depObject.Id);
                todayCommand.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
            }
            var adapter = new MySqlDataAdapter(todayCommand);
            var table = new DataTable();
            adapter.Fill(table);
            Today.Text = $"Сегодня: {table.Rows[0]["is_sick_count"]}";
        }

        private DepSelection[] GetSelections()
        {
            var cmd = new MySqlCommand("SELECT * FROM Departments", conn);
            var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            var namesArr = new DepSelection[table.Rows.Count + 1];

            namesArr[0] = new DepSelection(-1, "Университет", 0, 1);
            for (int ind = 1, tableInd = 0; ind <= table.Rows.Count; ind++, tableInd++)
            {
                int on_quarantine = bool.Parse($"{table.Rows[tableInd]["on_quarantine"]}") ? 1 : 0;
                namesArr[ind] = new DepSelection(
                    int.Parse($"{table.Rows[tableInd]["id_department"]}"),
                    $"{table.Rows[tableInd]["dep_name"]}",
                    on_quarantine,
                    int.Parse($"{table.Rows[tableInd]["id_university"]}")
                );
            }
            return namesArr;
        }

        private void ChangeCovidRecomendationStatus(DepSelection department)
        {
            if (department.Id == -1)
            {
                Button_GoQuarantine.Content = "Отправить на карантин!";
            } 
            else
            {
                Button_GoQuarantine.Content = department.On_Quarantine == 1 ? "Отменить карантин!" : "Отправить на карантин!";
            }
            double peopleCount = GetPeoplesDepartmentCount(department.Id);
            int sickCount = GetSickedPeoplesDepartmentCount(department.Id);
            TB_Recomendation.Text = sickCount > peopleCount * 0.7 ? "Требуется карантин" : "Карантин не требуется";
            All_count.Text = $"Количество: {peopleCount}";
            Sicked_count.Text = $"Заболевших: {sickCount}";
        }

        private int GetSickedPeoplesDepartmentCount(int dep_id)
        {
            var cmd = new MySqlCommand(
                $"SELECT COUNT(People.id_person) AS people_count FROM People " +
                $"JOIN COVID ON People.id_person=COVID.id_person " +
                $"WHERE People.id_department={dep_id} AND COVID.is_sick=1 " +
                $"GROUP BY People.id_department", conn);
            if (dep_id == -1)
            {
                cmd = new MySqlCommand(
                    $"SELECT COUNT(People.id_person) AS people_count FROM People " +
                    $"JOIN COVID ON People.id_person=COVID.id_person " +
                    $"JOIN Departments ON Departments.id_department=People.id_department " +
                    $"JOIN University ON Departments.id_university=University.id_university " +
                    $"WHERE COVID.is_sick=1 AND University.id_university=1 " +
                    $"GROUP BY University.id_university", conn);
            }
            var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table.Rows.Count > 0 ? int.Parse($"{table.Rows[0]["people_count"]}") : 0;
        }

        private int GetPeoplesDepartmentCount(int dep_id)
        {
            var cmd = new MySqlCommand(
                $"SELECT COUNT(id_person) AS people_count FROM People " +
                $"WHERE id_department={dep_id} " +
                $"GROUP BY id_department", conn);
            if (dep_id == -1)
            {
                cmd = new MySqlCommand(
                    $"SELECT COUNT(People.id_person) AS people_count FROM People " +
                    $"JOIN Departments ON Departments.id_department=People.id_department " +
                    $"JOIN University ON Departments.id_university=University.id_university " +
                    $"GROUP BY University.id_university", conn);
            }
            var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table.Rows.Count > 0 ? int.Parse($"{table.Rows[0]["people_count"]}") : 0;
        }

        public void Dep_FunSelector(object sender, SelectionChangedEventArgs e)
        {
            StatsGrid.Children.Clear();
            var selectedDep = Departments[DepSelection.SelectedIndex];
            SetTodayStats(selectedDep);
            SetGraphHeighAndValue(GetGraphValues(DateTime.Now, selectedDep));
            SetDatesLine(DateTime.Now);
            ChangeCovidRecomendationStatus(selectedDep);
        }

        private void SetDatesLine(DateTime startTime)
        {
            for (int i = 0; i < COLUMNS; i++)
            {
                var text = new TextBlock() { HorizontalAlignment = System.Windows.HorizontalAlignment.Center };
                text.Text = (startTime - new TimeSpan(24*i, 0, 0)).ToString("dd");
                Grid.SetRow(text, 1);
                Grid.SetColumn(text, COLUMNS-1-i);
                StatsGrid.Children.Add(text);
            }
        }

        private int[] GetGraphValues(DateTime date, SelectionObject depObject)
        {
            int[] resultTableInts = new int[COLUMNS];
            var needUpperDateStr = (date - new TimeSpan(24 * COLUMNS, 0, 0)).ToString("yyyy-MM-dd");
            MySqlCommand cmd;
            if (depObject.Id == -1)
            {
                cmd = new MySqlCommand(
                "SELECT DateStart, SUM(is_sick) as sick_sum FROM COVID " +
                $"WHERE is_sick=1 AND DateStart > '{needUpperDateStr}' " +
                "GROUP BY DateStart " +
                "ORDER BY DateStart", conn);
            } else
            {
                cmd = new MySqlCommand(
                    "SELECT COVID.DateStart, SUM(COVID.is_sick) as sick_sum FROM COVID " +
                    "JOIN People ON People.id_person = COVID.id_person " +
                    $"WHERE COVID.is_sick=1 AND COVID.DateStart > '{needUpperDateStr}' AND People.id_department={depObject.Id} " +
                    "GROUP BY COVID.DateStart",
                    conn
                );
            }
            var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            ExistedDates = new DbDate[table.Rows.Count];
            for (int i = 0; i < ExistedDates.Length; i++)
            {
                ExistedDates[i] = new DbDate(
                $"{DateTime.Parse($"{table.Rows[i]["DateStart"]}").ToString("yyyy-MM-dd")}",
                $"{table.Rows[i]["sick_sum"]}"
                );
            }
            for (int i = 0; i < COLUMNS; i++)
            {
                var neededDate = (date - new TimeSpan(24*i, 0, 0)).ToString("yyyy-MM-dd");
                if (ExistedDates.Any(d => d.Date == neededDate))
                {
                    resultTableInts[COLUMNS - 1 - i] = Convert.ToInt32(ExistedDates.First(d => d.Date == neededDate).Count);
                } else
                {
                    resultTableInts[COLUMNS - 1 - i] = 0;
                }
            }
            return resultTableInts;
        }

        private void SetGraphHeighAndValue(int[] values)
        {
            int MAX_HEIGHT = 320;
            double maxValue = values.Max();
            double percent = maxValue / MAX_HEIGHT;
            for (int i = 0; i < values.Length; i++)
            {
                var text = new TextBlock() { 
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom, 
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Margin = new System.Windows.Thickness(0,0,0,10)
                };
                if (values[i] != 0)
                {
                    text.Text = $"{values[i]}";
                }
                var rect = new Rectangle() {
                    Fill = (Brush)new BrushConverter().ConvertFrom("#045A8D"),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    Height = (values[i] / percent)
                };
                Grid.SetColumn(text, i);
                Grid.SetColumn(rect, i);
                StatsGrid.Children.Add(rect);
                StatsGrid.Children.Add(text);
            }
        }
    }
}
