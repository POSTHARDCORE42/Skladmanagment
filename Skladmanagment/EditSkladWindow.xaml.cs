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
    /// Логика взаимодействия для EditSkladWindow.xaml
    /// </summary>
    public partial class EditSkladWindow : Window
    {
        private SkladInfo selectedSklad;

        public EditSkladWindow(SkladInfo sklad)
        {
            InitializeComponent();
            selectedSklad = sklad;

            // Заполняем поля данными выбранного склада
            textBoxName.Text = selectedSklad.Name;
            textBoxAddress.Text = selectedSklad.Address;
            textBoxKladov.Text = selectedSklad.Kladov;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем выбранный склад данными из полей
            selectedSklad.Name = textBoxName.Text;
            selectedSklad.Address = textBoxAddress.Text;
            selectedSklad.Kladov = textBoxKladov.Text;

            // Вызываем метод обновления информации о складе в базе данных
            DatabaseContext databaseContext = new DatabaseContext();
            databaseContext.UpdateSklad(selectedSklad.ID, selectedSklad.Name, selectedSklad.Address, selectedSklad.Kladov);

            // Закрываем окно после успешного обновления
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем окно без сохранения изменений
            this.Close();
        }
    }
}
