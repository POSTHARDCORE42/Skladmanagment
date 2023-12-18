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
    public partial class EditTovarWindow : Window
    {
        private TovarInfo tovarInfo;
        private DatabaseContext dbContext;

        public EditTovarWindow(TovarInfo selectedTovar)
        {
            InitializeComponent();
            tovarInfo = selectedTovar;
            DataContext = tovarInfo;

            // Инициализация ComboBox для выбора поставщиков
            dbContext = new DatabaseContext();
            cmbSupplier.ItemsSource = dbContext.GetAllSuppliers();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Обновление данных товара
            var selectedSupplierID = (int)cmbSupplier.SelectedValue;
            dbContext.UpdateTovar(tovarInfo.ID, tovarInfo.Name, tovarInfo.Quantity, tovarInfo.Price, selectedSupplierID);

            // Закрытие окна после сохранения
            this.Close();
        }
    }
}
