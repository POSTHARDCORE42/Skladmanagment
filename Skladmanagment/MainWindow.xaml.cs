using Skladmanagment.design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Skladmanagment
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string pass = password.Text;

            DatabaseContext db = new DatabaseContext();

            bool isAuthenticated = db.AuthenticateUser(login, pass);

            if (isAuthenticated)
            {
                // Если аутентификация прошла успешно, можно осуществить переход на другое окно или выполнить другие действия
                MessageBox.Show("Аутентификация прошла успешно!");
                homewindow homeWindow = new homewindow(); // Создание экземпляра класса HomeWindow
                homeWindow.Show(); // Отображение окна HomeWindow
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка аутентификации! Проверьте логин и пароль.");
            }
        }
        private void Button_Click_FAQ(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Это тестовая сборка");
        }
    }
}
