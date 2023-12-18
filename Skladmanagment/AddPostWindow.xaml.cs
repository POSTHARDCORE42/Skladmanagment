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
    public partial class AddPostWindow : Window
    {
        private DatabaseContext dbContext;

        public AddPostWindow()
        {
            InitializeComponent();
            dbContext = new DatabaseContext(); // Инициализация объекта базы данных
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
            {
                // Добавление поставщика в базу данных
                dbContext.AddPost(name, address);

                MessageBox.Show("Поставщик успешно добавлен.");
                Close(); // Закрыть окно после добавления поставщика
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
            }
        }
    }
}
