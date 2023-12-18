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
    public partial class nakladnaya : Window
    {
        private DatabaseContext dbContext;

        public nakladnaya()
        {
            InitializeComponent();
            dbContext = new DatabaseContext();
            LoadProducts();
            LoadClients();// Загрузить продукты в ComboBox
        }

        private void LoadProducts()
        {
            // Загрузить список продуктов в ComboBox из базы данных
            List<Product> products = dbContext.GetAllProducts();
            cmbProducts.ItemsSource = products;
            cmbProducts.DisplayMemberPath = "Name"; // Предположим, что у вас есть метод GetAllProducts
        }
        private void LoadClients()
        {
            // Загрузить список клиентов в cmbClients из базы данных
            List<Client> clientsForNaklad = dbContext.GetAllClientNaklad();
            cmbClients.ItemsSource = clientsForNaklad;
            cmbClients.DisplayMemberPath = "Name"; // Предположим, что у класса Client есть свойство Name для отображения имени клиента

        }

        private void btnAddNaklad_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = cmbProducts.SelectedItem as Product;
            Client selectedClient = cmbClients.SelectedItem as Client; // Получение выбранного клиента

            if (selectedProduct != null && selectedClient != null)
            {
                // Получить количество и дату из элементов интерфейса
                if (int.TryParse(txtQuantity.Text, out int quantity)) // Предполагается, что введённое количество - число
                {
                    DateTime date = datePicker.SelectedDate ?? DateTime.Now; // Предполагается, что дата выбрана или берётся текущая дата

                    // Создать накладную
                    Nakladnaya nakladnaya = new Nakladnaya
                    {
                        Date = date,
                        ProductID = selectedProduct.ID,
                        Quantity = quantity
                    };

                    // Формирование накладной через метод в DatabaseContext
                    bool success = dbContext.AddNakladnaya(nakladnaya, selectedClient);

                    if (success)
                    {
                        MessageBox.Show("Накладная успешно добавлена!");
                        // Очистить элементы интерфейса после добавления
                        cmbProducts.SelectedIndex = -1;
                        txtQuantity.Text = "";
                        datePicker.SelectedDate = null;
                        cmbClients.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить накладную.");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректное количество товара.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт и клиента.");
            }
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            homewindow homeWindow = new homewindow(); // Создание экземпляра класса HomeWindow
            homeWindow.Show(); // Отображение окна HomeWindow
            this.Close();
        }
    }
}