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
    /// Логика взаимодействия для AddTovarWindow.xaml
    /// </summary>
    public partial class AddTovarWindow : Window
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public AddTovarWindow()
        {
            InitializeComponent();

            // Вызываем метод для загрузки данных поставщиков в ComboBox
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            // Получаем данные поставщиков из базы данных и загружаем в ComboBox
            var suppliers = db.GetAllSuppliers();

            // Предположим, что у вас есть класс Supplier и у него есть поля Name и ID
            foreach (var supplier in suppliers)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = supplier.Name;
                item.Tag = supplier.ID; // Используем ID поставщика как тег элемента ComboBoxItem
                cmbSuppliers.Items.Add(item);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Добавление товара с использованием выбранного поставщика из ComboBox
            string name = txtName.Text;
            string quantity = txtQuantity.Text;
            string price = txtPrice.Text;

            // Получение выбранного поставщика из ComboBox
            if (cmbSuppliers.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is int supplierID)
            {
                // Выбранный поставщик
                int selectedSupplierID = supplierID;

                // Добавление товара в базу данных
                db.AddTovar(name, quantity, price, selectedSupplierID);

                MessageBox.Show("Товар успешно добавлен!");
                this.Close(); // Закрытие окна после добавления товара
            }
            else
            {
                MessageBox.Show("Выберите поставщика.");
            }
        }
    }
}
