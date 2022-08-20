using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Goosle
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        // Вызывается при нажатии крестика. Закрывает окно регистрации.
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

        

        //Проверка полей регистрации на выполнние требований:
        //ФИО только русские буквы, Пасспорт только цифры, Номер пропуска только из s,t,a и набора цифр, пароли совпадают
        public bool checkInputs(string Surname, string UserName, string Patronymic, string Passport, string EntryCard, string Pass1, string Pass2)
        {
            if (Surname == "" || UserName == "" || Patronymic == "" || Passport == "" || EntryCard == "" || Pass1 == "" || Pass2 == "")
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
                return false;
            }
            List<string> values = new List<string>();
            if (CheckRegProp.checkStringInputs(Surname))
                values.Add("Фамилия (возможны только рус. буквы)\n");
            if (CheckRegProp.checkStringInputs(UserName))
                values.Add("Имя (возможны только рус. буквы)\n");
            if (CheckRegProp.checkStringInputs(Patronymic))
                values.Add("Отчество (возможны только рус. буквы)\n");
            if (CheckRegProp.checkNumInputs(Passport))
                values.Add("Пасспорт (должны быть только цифры)\n");
            if (CheckRegProp.checkEntryCard(EntryCard))
                values.Add("Номер пропуска ( должен содержать в начале s,t,a и потом набор цифр)\n");
            if (Pass1 != Pass2)
                values.Add("Пароли не совпадают");

            if (values.Count != 0)
            {
                MessageBox.Show("Ошибки в данных: \n" + String.Join("", values));
                return false;
            }
            else
                return true;
        }

        //Функция при нажатии кнопки "Зарегистрироваться", проверяет корректность ввода и отправляет запрос на регистрацию в БД
        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            string Surname = textBoxSurname.Text;
            string UserName = textBoxName.Text;
            string Patronymic = textBoxPatronymic.Text;
            string BirthDate = dataSelector.Text;
            string Passport = textBoxPassport.Text;
            string EntryCard = textBoxEntryCard.Text;
            string Pass1 = passwordBox1.Password;
            string Pass2 = passwordBox1.Password;
            
            if (checkInputs(Surname, UserName, Patronymic, Passport, EntryCard, Pass1, Pass2))
            {
                string connString = "Server=" + "db-goosle.ck2ojbqqmm8s.eu-central-1.rds.amazonaws.com" +
                ";Database=" + "goosleDB"
                + ";port=" + "3306" + ";User Id=" + "a100001" + ";password=" + "12345";
                MySqlConnection conn = new MySqlConnection(connString);
                try
                {
                    conn.Open();
                    //Проверяем есть ли в БД строка с такими данными
                    MySqlCommand command = new MySqlCommand($"SELECT Surname, Name,  Patronymic, Birthday, Passport " +
                        $"FROM People WHERE Entry_Card='{EntryCard}'", conn);
                    MySqlDataAdapter userAdapter = new MySqlDataAdapter(command);
                    DataTable CacheData = new DataTable();
                    userAdapter.Fill(CacheData);
                    //Если все совпало, то можно установить пароль
                    if (CacheData.Rows[0][0].ToString() == Surname && CacheData.Rows[0][1].ToString() == UserName
                        && CacheData.Rows[0][2].ToString() == Patronymic && CacheData.Rows[0][4].ToString() == Passport)
                    {
                        command = new MySqlCommand($"UPDATE People SET Pass='{Pass1}' WHERE Entry_Card='{EntryCard}'", conn);
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                    MessageBox.Show("Регистрация прошла успешно!\nЛогин: "+EntryCard+"\nПароль: " +Pass1);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Данные не совпадают с информацией в системе.");
                }
            }
            
        }

        // Исправляет ввод в полях фамилия, имя, отчество: первую букву делает заглавной, остальные строчными
        // Для составных фио также делает заглавной букву в каждой части
        private void FirstUpperCase(object sender, RoutedEventArgs e)
        {
            try
            {
                textBoxSurname.Text = textBoxSurname.Text.Replace(" ", "");
                string[] strParts = textBoxSurname.Text.Split('-');
                textBoxSurname.Text = "";
                for (int i = 0; i < strParts.Length; i++)
                {
                    textBoxSurname.Text = textBoxSurname.Text + strParts[i].First().ToString().ToUpper() + strParts[i].ToLower().Substring(1);
                    if (i < strParts.Length - 1)
                        textBoxSurname.Text = textBoxSurname.Text + "-";
                }

                textBoxName.Text = textBoxName.Text.Replace(" ", "");
                strParts = textBoxName.Text.Split('-');
                textBoxName.Text = "";
                for (int i = 0; i < strParts.Length; i++)
                {
                    textBoxName.Text = textBoxName.Text + strParts[i].First().ToString().ToUpper() + strParts[i].ToLower().Substring(1);
                    if (i < strParts.Length - 1)
                        textBoxName.Text = textBoxName.Text + "-";
                }

                textBoxPatronymic.Text = textBoxPatronymic.Text.Replace(" ", "");
                strParts = textBoxPatronymic.Text.Split('-');
                textBoxPatronymic.Text = "";
                for (int i = 0; i < strParts.Length; i++)
                {
                    textBoxPatronymic.Text = textBoxPatronymic.Text + strParts[i].First().ToString().ToUpper() + strParts[i].ToLower().Substring(1);
                    if (i < strParts.Length - 1)
                        textBoxPatronymic.Text = textBoxPatronymic.Text + "-";
                }
            }
            catch
            {

            }
        }

        private void DelSpaces(object sender, RoutedEventArgs e)
        {
            try
            {
                textBoxPassport.Text = textBoxPassport.Text.Replace(" ", "");
                textBoxEntryCard.Text = textBoxEntryCard.Text.Replace(" ", "");
                textBoxEntryCard.Text = textBoxEntryCard.Text.First().ToString().ToLower() + textBoxEntryCard.Text.Substring(1);
            }
            catch
            {

            }
        }
    }


}
