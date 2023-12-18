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
    /// Логика взаимодействия для tovar.xaml
    /// </summary>
    public partial class tovar : Window
    {
        public tovar()
        {
            InitializeComponent();
            LoadTovars();
        }
        private void LoadTovars()
        {
            DatabaseContext db = new DatabaseContext();
            List<TovarInfo> tovars = db.GetAllTovars();
            TovarDataGrid.ItemsSource = tovars;
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            homewindow homeWindow = new homewindow(); // Создание экземпляра класса HomeWindow
            homeWindow.Show(); // Отображение окна HomeWindow
            this.Close();
        }

        private void addTovar_Click(object sender, RoutedEventArgs e)
        {
            AddTovarWindow addTovarWindow = new AddTovarWindow(); // Создание экземпляра класса HomeWindow
            addTovarWindow.Show(); // Отображение окна HomeWindow
            
        }

        private void TovarDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Delbtn_Click(object sender, RoutedEventArgs e)
        {
            if (TovarDataGrid.SelectedItem != null)
            {
                TovarInfo selectedItem = (TovarInfo)TovarDataGrid.SelectedItem;
                DatabaseContext databaseContext = new DatabaseContext();
                databaseContext.DeleteTovar(selectedItem.ID);


                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    databaseContext.DeleteTovar(selectedItem.ID);
                    LoadTovars();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления.");
            }
        }

        private void Updbtn_Click(object sender, RoutedEventArgs e)
        {
            if (TovarDataGrid.SelectedItem != null)
            {
                TovarInfo selectedItem = (TovarInfo)TovarDataGrid.SelectedItem;

                // Создание экземпляра окна редактирования товара
                EditTovarWindow editTovarWindow = new EditTovarWindow(selectedItem);
                editTovarWindow.ShowDialog();

                // После закрытия окна обновляем данные в таблице
                LoadTovars();
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования.");
            }
        }
    }
}
