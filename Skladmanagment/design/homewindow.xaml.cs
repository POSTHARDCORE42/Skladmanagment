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
using System.Windows.Shapes;

namespace Skladmanagment.design
{
    /// <summary>
    /// Логика взаимодействия для homewindow.xaml
    /// </summary>
    public partial class homewindow : Window
    {
        private DatabaseContext db = new DatabaseContext();
        public homewindow()
        {
            InitializeComponent();
            UpdateLabels();
        }
        private void UpdateLabels()
        {
            List<SkladInfo> sklads = db.GetAllSklads();
            int count = sklads.Count;
            allSklad.Content = count.ToString();
            allTovar.Content = db.GetAllTovar();
            allClient.Content = db.GetAllClient();
            allPostav.Content = db.GetAllPostav();
            allPribyl.Content = db.GetAllPribyl();
        }

        private void Sklad_Click(object sender, RoutedEventArgs e)
        {
            Sklad Sklad = new Sklad(); // Создание экземпляра класса HomeWindow
            Sklad.Show(); // Отображение окна HomeWindow
            this.Close();
        }

        private async void logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close(); // Закрыть текущее окно
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateLabels();
        }

        private void tovar_Click(object sender, RoutedEventArgs e)
        {
            tovar tovar = new tovar(); // Создание экземпляра класса HomeWindow
            tovar.Show(); // Отображение окна HomeWindow
            this.Close();
        }

        private void client_Click(object sender, RoutedEventArgs e)
        {
            clients clients = new clients(); // Создание экземпляра класса HomeWindow
            clients.Show(); // Отображение окна HomeWindow
            this.Close();
        }

        private void Nkladnaya_Click(object sender, RoutedEventArgs e)
        {
            nakladnaya nakladnaya = new nakladnaya(); // Создание экземпляра класса HomeWindow
            nakladnaya.Show(); // Отображение окна HomeWindow
            this.Close();
        }

        private void Otchyet_Click(object sender, RoutedEventArgs e)
        {
            otchet otchet = new otchet(); // Создание экземпляра класса HomeWindow
            otchet.Show(); // Отображение окна HomeWindow
            this.Close();
        }
    }
}
