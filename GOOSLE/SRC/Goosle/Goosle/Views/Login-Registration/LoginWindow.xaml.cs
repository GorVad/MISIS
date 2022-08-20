using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Goosle.Models;
using MySql.Data.MySqlClient;

namespace Goosle
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// В окне авторизации пользователь вводит свои логин и пароль в соответствующие текстовые поля. Текст пароля скрыт точками.
    /// Затем нажимается кнопка Войти. Если подлкючение к БД пройдет успешно, то окно закрывается и открывается главное окно.
    /// Если подключение не удалось, то появится уведомление.
    /// Если пользватель не имеет учетной записи, то он нажимает конпку Регистрация, открывающую соответсвующее окно.
    /// При нажатии на крестик программа завершает работву.
    /// </summary>
    public partial class LoginWindow : Window
    {
        bool conn_status = false;
        MySqlConnection conn;
        string Login_name;
        private UserModel user;

        public LoginWindow()
        {
            InitializeComponent();
        }

        internal UserModel GetUser()
        {
            return user;
        }

        // Функция при нажатия кнопки Закрыть (крестик)
        // При нажатии закрывает приложение
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.Count > 1)
                for (int i = 0; i < Application.Current.Windows.Count; i++)
                    Application.Current.Windows[i].Close();
            Close();
            //Application.Current.MainWindow.Close();
        }

        // Функция возращает значение переменноц conn_failed
        // Если подключение к БД не вышло - true, иначе - false
        // Используется в главном окне, чтобы закрыть приложение при неудачном подключении к БД
        internal bool GetStatus()
        {
            return conn_status;
        }


        // Функция возращает соединение с БД
        // Нужна для передачи доступа к БД в другие окна приложения
        internal MySqlConnection GetConnection()
        {
            return conn;
        }

        internal string GetLogin()
        {
            return Login_name;
        }

        // Функция при нажатия кнопки Войти
        // При нажатии производится попытка подключения к БД
        // Если удается подключение, то форма закрывается и открывается главное окно
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            String connString = "Server=" + "db-goosle.ck2ojbqqmm8s.eu-central-1.rds.amazonaws.com" + 
                ";Database=" + "goosleDB"
                + ";port=" + "3306" + ";User Id=" + "a100001" + ";password=" + "12345";
            conn = new MySqlConnection(connString);
            Login_name = textBoxLogin.Text.Trim();

            try
            {
                conn.Open();
                user = getUser(Login_name, passwordBox.Password);
                conn_status = true;
            }
            catch (Exception ex) when (ex is MySqlException || ex is IndexOutOfRangeException) 
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            conn.Close();
            if (conn_status)
            {
                Close();
            }
        }

        // Функция при нажатия кнопки Регистрация
        // При нажатии откроет форму регистрации
        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow form_reg = new RegistrationWindow();
            form_reg.ShowDialog();
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

        // Функция срабатывает при получении фокуса на textBoxLogin (поле для логина)
        // удалает "Имя пользователя", освобождая место для ввода логина
        private void textBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "Имя пользователя")
                textBoxLogin.Text = "";
        }

        // Функция срабатывает при потере фокуса на textBoxLogin (поле для логина)
        // Если поле было пустое (т.е. пользователь получал фокус, но не вводил логин), то возвращается надпись "Имя пользователя"
        private void textBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "")
                textBoxLogin.Text = "Имя пользователя";
        }

        // Функция срабатывает при получении фокуса на passowrdBox (поле для пароля)
        // удалает дефолтные звездочки, освобождая место для ввода пароля
        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "Пароль")
                passwordBox.Password = "";
        }

        // Функция срабатывает при потере фокуса на passowrdBox (поле для пароля)
        // Если поле было пустое (т.е. пользователь получал фокус, но не вводил пароль), то возвращаются дефолтные звездочки
        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
                passwordBox.Password = "Пароль";
        }

        /// <summary>
        /// Возвращает UserModel с инициализированными полями из базы данных.
        /// Не вызывать при отсутствии пользователей.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="pass">Пароль позователя.</param>
        /// <exception cref="IndexOutOfRangeException">Выбрасывается при отсутствии пользователя с таким логином и паролем.</exception>
        private UserModel getUser(string login, string pass)
        {
            var user = new MySqlCommand($"SELECT * FROM People WHERE Entry_Card='{login}' AND Pass='{pass}' LIMIT 1", conn);
            var userAdapter = new MySqlDataAdapter(user);
            var userTable = new DataTable();
            userAdapter.Fill(userTable);
            return new UserModel()
            {
                id_person = int.Parse(userTable.Rows[0]["id_person"].ToString()),
                id_department = int.Parse(userTable.Rows[0]["id_department"].ToString()),
                Name = $"{userTable.Rows[0]["Name"]}",
                Surname = $"{userTable.Rows[0]["Surname"]}",
                Patronymic = $"{userTable.Rows[0]["Patronymic"]}",
                Birthday = $"{userTable.Rows[0]["Birthday"]}",
                Age = $"{userTable.Rows[0]["Age"]}",
                Passport = $"{userTable.Rows[0]["Passport"]}",
                TelephoneNumber = $"{userTable.Rows[0]["TelephoneNumber"]}",
                Photo = $"{userTable.Rows[0]["Photo"]}",
                Entry_Card = $"{userTable.Rows[0]["Entry_Card"]}",
                Pass = $"{userTable.Rows[0]["Pass"]}",
                Role = $"{userTable.Rows[0]["Role"]}",
            };
        }
    }
}
