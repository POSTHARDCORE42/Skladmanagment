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
    /// Логика взаимодействия для AddSkladWindow.xaml
    /// </summary>
    public partial class AddSkladWindow : Window
    {
        private DatabaseContext db = new DatabaseContext();

        public AddSkladWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;
            string kladov = txtKladov.Text;

            // Проверка наличия данных в полях
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(kladov))
            {
                // Добавление записи в базу данных
                db.AddSklad(name, address, kladov);
                MessageBox.Show("Запись добавлена успешно!");
                this.Close(); // Закрытие окна после добавления записи
                
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
            }
        }
    }
}
