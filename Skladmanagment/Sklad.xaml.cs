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
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Window
    {
        private readonly DatabaseContext databaseContext;


        public Sklad()
        {
            InitializeComponent();
            databaseContext = new DatabaseContext();

            LoadSkladsData();
        }

        private void LoadSkladsData()
        {
            // Получаем данные о складах из базы данных
            List<SkladInfo> sklads = databaseContext.GetAllSklads();

            // Привязываем данные к DataGrid
            skladsDataGrid.ItemsSource = sklads;
        }

        private void skladsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка изменений выбора, если нужно
        }

        private void addSklad_Click(object sender, RoutedEventArgs e)
        {
            AddSkladWindow addSkladWindow = new AddSkladWindow();
            addSkladWindow.Show();
             
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            homewindow homeWindow = new homewindow(); // Создание экземпляра класса HomeWindow
            homeWindow.Show(); // Отображение окна HomeWindow
            this.Close();
        }

        private void Delbtn_Click(object sender, RoutedEventArgs e)
        {
            if (skladsDataGrid.SelectedItem != null)
            {
                SkladInfo selectedItem = (SkladInfo)skladsDataGrid.SelectedItem;

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    databaseContext.DeleteSklad(selectedItem.ID);
                    LoadSkladsData();
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления.");
            }
        }

        private void Updbtn_Click(object sender, RoutedEventArgs e)
        {
            if (skladsDataGrid.SelectedItem != null)
            {
                SkladInfo selectedItem = (SkladInfo)skladsDataGrid.SelectedItem;

                // Открываем окно для редактирования, передавая информацию о выбранном элементе
                EditSkladWindow editSkladWindow = new EditSkladWindow(selectedItem);
                editSkladWindow.Show();

                // Обновляем данные после закрытия окна редактирования
                LoadSkladsData();
            }
            else
            {
                MessageBox.Show("Выберите элемент для изменения.");
            }
        }
    }
}
