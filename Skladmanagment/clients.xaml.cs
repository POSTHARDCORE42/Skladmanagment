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
using System.Windows.Shapes;

namespace Skladmanagment
{
    /// <summary>
    /// Логика взаимодействия для clients.xaml
    /// </summary>
    public partial class clients : Window
    {
        public clients()
        {
            InitializeComponent();
            LoadClient();
            LoadPost();
        }
        private void LoadClient()
        {
            DatabaseContext db = new DatabaseContext();
            List<Client> clients = db.GetAllClientsA();
            ClientDataGrid.ItemsSource = clients;
        }
        private void LoadPost()
        {
            DatabaseContext db = new DatabaseContext();
            List<Post> post = db.GetAllPostsA();
            PostDataGrid.ItemsSource = post;
        }
        private void home_Click(object sender, RoutedEventArgs e)
        {
            homewindow homeWindow = new homewindow(); // Создание экземпляра класса HomeWindow
            homeWindow.Show(); // Отображение окна HomeWindow
            this.Close();
        }

        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PostDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow(); // Создание экземпляра класса HomeWindow
            addClientWindow.Show(); // Отображение окна HomeWindow
            
        }

        private void addPostav_Click(object sender, RoutedEventArgs e)
        {
            AddPostWindow addPostWindow = new AddPostWindow(); // Создание экземпляра класса HomeWindow
            addPostWindow.Show(); // Отображение окна HomeWindow
        }
    }
}
