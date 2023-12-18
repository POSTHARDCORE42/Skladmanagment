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
    public partial class AddClientWindow : Window
    {
        private DatabaseContext dbContext;

        public AddClientWindow()
        {
            InitializeComponent();
            dbContext = new DatabaseContext();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;

            // Добавление клиента в базу данных
            dbContext.AddClient(name, address);

            MessageBox.Show("Клиент успешно добавлен в базу данных!");
            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtAddress.Text = "";
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
