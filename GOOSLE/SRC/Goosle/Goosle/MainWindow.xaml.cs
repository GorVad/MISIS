using System;
using System.Windows;
using Goosle.Models;
using Goosle.Navigations;



namespace Goosle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();

            // Открывается форма Авторизации
            LoginWindow form = new LoginWindow();
            form.ShowDialog();

            //Тут будет проверка удалось ли подключение к БД, а потом уже открытие основного окна
            if (form.GetStatus())
            {
                UserModel user = form.GetUser();
                if (user.Role == "Студент") // если пользователь == student
                {
                    MainFrame.Navigate(new StudentNavigation(form.GetConnection(), user));
                }
                else if (user.Role == "Преподаватель" || user.Role == "Администрация")  // если преподаватель
                {
                    MainFrame.Navigate(new TeacherNavigation(form.GetConnection(), user));
                }
                else
                {
                    throw new Exception("Unexpected role user.");
                }
                Show();
            } 
            else
            {
                // Если подключение не удалось, завершаем работу программы 
                Close();
            }
            

        }
        // Позволяет перетаскивать окно программы по экрану зажатой левой кнопкой мыши
        private void grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (InvalidOperationException) { }
        }
    }
}
