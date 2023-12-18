using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;

namespace Skladmanagment
{
    public class DatabaseContext
    {
        private string connectionString = "Data Source=DESKTOP-OI0MKOB\\SQLEXPRESS;Initial Catalog=SkladManagement;User ID=admin;Password=admin"; // Строка подключения к бд

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        public void UpdateSklad(int id, string name, string address, string kladov)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Складские_помещения SET Наименование = @Name, Адрес = @Address, Кладовщик = @Kladov WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Kladov", kladov);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteSklad(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Складские_помещения WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddSklad(string name, string address, string kladov)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Складские_помещения (Наименование, Адрес, Кладовщик) VALUES (@Name, @Address, @Kladov)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Kladov", kladov);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddTovar(string name, string quantity, string price, int supplierID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Товары (Название, Количество, Цена, Поставщик_ID) VALUES (@Name, @Quantity, @Price, @SupplierID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateTovar(int id, string name, string quantity, string price, int supplierID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Товары SET Название = @Name, Количество = @Quantity, Цена = @Price, Поставщик_ID = @SupplierID WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTovar(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Товары WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool AuthenticateUser(string login, string password)
        {
            bool isAuthenticated = false;

            using (var connection = GetConnection())
            {
                connection.Open();

                // SQL-запрос для проверки логина и пароля
                string query = "SELECT COUNT(*) FROM Пользователи WHERE Логин = @Login AND Пароль = @Password";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        isAuthenticated = true;
                    }
                }
            }

            return isAuthenticated;
        }
        public List<TovarInfo> GetAllTovars()
        {
            List<TovarInfo> tovars = new List<TovarInfo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Товары"; // Замените на свой SQL-запрос

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TovarInfo tovar = new TovarInfo
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Название"].ToString(),
                            Quantity = reader["Количество"].ToString(),
                            Price = reader["Цена"].ToString(),
                            SupplierID = Convert.ToInt32(reader["Поставщик_ID"])
                            // и т.д., продолжайте для других полей
                        };

                        tovars.Add(tovar);
                    }

                    reader.Close();
                }
            }

            return tovars;
        }
        public int GetAllSklad()
        {
            int count = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Накладные"; // SQL-запрос для получения количества складов

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }
        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, Название FROM Поставщики"; // Предполагается, что у вас есть таблица 'Поставщики' с полями 'ID' и 'Name'

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Supplier supplier = new Supplier
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Название"].ToString()
                        };

                        suppliers.Add(supplier);
                    }

                    reader.Close();
                }
            }

            return suppliers;
        }
        public int GetAllTovar()
        {
            int count = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Товары"; // SQL-запрос для получения количества товаров

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }
        public List<Client> GetAllClientsA()
        {
            List<Client> clients = new List<Client>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Клиенты"; // SQL-запрос для получения всех клиентов

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Client client = new Client
                        {
                            Name = reader["Название"].ToString(),
                            Adress = reader["Адрес"].ToString(),
                            // Продолжайте для других полей клиента
                        };

                        clients.Add(client);
                    }

                    reader.Close();
                }
            }

            return clients;
        }
        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT * FROM Клиенты"; // Ваш запрос на получение клиентов из базы данных
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client client = new Client
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    // Здесь получите остальные данные о клиенте из базы данных
                                    // client.Property = reader["Column_Name"].ToString();
                                };
                                clients.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }

            return clients;
        }
        public void AddPost(string name, string address)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "INSERT INTO Поставщики (Название, Адрес) VALUES (@Name, @Address)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private decimal GetPriceOfProduct(int productId)
        {
            decimal price = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Цена FROM Товары WHERE ID = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            price = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }
            return price;
        }
        private string GetClientNameById(int clientId)
        {
            string clientName = string.Empty;
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Название FROM Клиенты WHERE ID = @ClientId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClientId", clientId);

                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            clientName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }
            return clientName;
        }
        public (List<DateTime> Dates, List<decimal> Costs) GetDateAndCostData()
        {
            var Dates = new List<DateTime>();
            var Costs = new List<decimal>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Дата, Стоимость FROM Накладные";

                    using (var command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var date = ((DateTime)reader["Дата"]).Date;
                            var cost = Convert.ToDecimal(reader["Стоимость"]);

                            Dates.Add(date);
                            Costs.Add(cost);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }

            return (Dates, Costs);
        }
            private string GetProductNameById(int productId)
        {
            string productName = string.Empty;
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Название FROM Товары WHERE ID = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            productName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }
            return productName;
        }
        public List<NakladnayaViewModel> GetAllNakladnayaData()
        {
            List<NakladnayaViewModel> nakladnayaList = new List<NakladnayaViewModel>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Накладные.ID, Накладные.Дата, Накладные.Количество, Товары.Название AS Товар, Клиенты.Название AS Клиент, Накладные.Стоимость FROM Накладные INNER JOIN Товары ON Накладные.Товар = Товары.ID INNER JOIN Клиенты ON Накладные.Клиент = Клиенты.ID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            NakladnayaViewModel nakladnaya = new NakladnayaViewModel
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Data = ((DateTime)reader["Дата"]).Date,
                                Kolvo = Convert.ToInt32(reader["Количество"]),
                                Client = reader["Клиент"].ToString(),
                                Cost = Convert.ToDecimal(reader["Стоимость"]),
                                Product = reader["Товар"].ToString(),
                                // Добавьте другие поля, если они есть в таблице Накладные
                            };

                            nakladnayaList.Add(nakladnaya);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }

            return nakladnayaList;
        }
        public bool AddNakladnaya(Nakladnaya nakladnaya, Client selectedClient)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Рассчитать стоимость по формуле: Цена товара * Количество товара
                    decimal price = GetPriceOfProduct(nakladnaya.ProductID);
                    decimal cost = price * nakladnaya.Quantity;

                    int selectedClientId = selectedClient != null ? selectedClient.ID : 0; // Предположим, что ID клиента - целое число

                    // Вставить данные в таблицу Накладные
                    string queryNakladnaya = "INSERT INTO Накладные (Дата, Товар, Клиент, Количество, Стоимость) VALUES (@Date, @Product, @Client, @Quantity, @Cost)";
                    using (var command = new SqlCommand(queryNakladnaya, connection))
                    {
                        command.Parameters.AddWithValue("@Date", nakladnaya.Date);
                        command.Parameters.AddWithValue("@Product", nakladnaya.ProductID);
                        command.Parameters.AddWithValue("@Client", selectedClientId);
                        command.Parameters.AddWithValue("@Quantity", nakladnaya.Quantity);
                        command.Parameters.AddWithValue("@Cost", cost); // Добавляем стоимость в запрос

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Возвращает true, если добавление прошло успешно
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
                return false; // Возвращаем false в случае ошибки
            }
        }

        public void AddClient(string name, string address)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Клиенты (Название, Адрес) VALUES (@Name, @Address)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Post> GetAllPostsA()
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Поставщики"; // SQL-запрос для получения всех поставщиков

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Post post = new Post
                        {
                            Name = reader["Название"].ToString(),
                            Adress = reader["Адрес"].ToString(),
                            // Продолжайте для других полей поставщика
                        };

                        posts.Add(post);
                    }

                    reader.Close();
                }
            }

            return posts;
        }
        public List<Client> GetAllClientNaklad()
        {
            List<Client> clients = new List<Client>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT ID, Название FROM Клиенты"; // запрос на получение клиентов из базы данных для накладных
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client client = new Client
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Название"].ToString()
                                };
                                clients.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }

            return clients;
        }
        public int GetAllClient()
        {
            int count = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Клиенты"; // SQL-запрос для получения количества клиентов

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }

        public int GetAllPostav()
        {
            int count = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Поставщики"; // SQL-запрос для получения количества поставщиков

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }

        public decimal GetAllPribyl()
        {
            decimal pribyl = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT SUM(Стоимость) FROM Накладные"; // SQL-запрос для получения общей прибыли через стоимость товаров в накладных

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        pribyl = Convert.ToDecimal(result);
                    }
                }
            }
            return pribyl;
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT * FROM Товары"; // Замените на свой SQL-запрос

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Название"].ToString(),
                            // и другие свойства продукта, если есть
                        };

                        products.Add(product);
                    }

                    reader.Close();
                }
            }

            return products;
        }
        public class CostByDay
        {
            public DateOnly Data { get; set; }
            public int Cost { get; set; }
        }
        public decimal GetProductPrice(int productId)
        {
            decimal price = 0;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Цена FROM Товары WHERE ID = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            price = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки или логирование ex
            }

            return price;
        }
        public List<SkladInfo> GetAllSklads()
        {
            List<SkladInfo> sklads = new List<SkladInfo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Складские_помещения"; // SQL-запрос для получения всех складов

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        SkladInfo sklad = new SkladInfo
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Наименование"].ToString(),
                            Address = reader["Адрес"].ToString(),
                            Kladov = reader["Кладовщик"].ToString(),
                            // и т.д., продолжайте для других полей
                        };

                        sklads.Add(sklad);
                    }

                    reader.Close();
                }
            }

            return sklads;
        }
    }

    public class SkladInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Kladov { get; set; }
        // и т.д., добавьте другие поля, если они есть в вашей таблице
    }
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class NakladnayaViewModel
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public int Kolvo { get; set; }
        public string Client { get; set; }
        public decimal Cost { get; set; }
        public string Product { get; set; } // Добавили свойство для названия товара
                                            // Другие свойства, если необходимо
    }


    public class TovarInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public int SupplierID { get; set; }
        // и другие поля товара, если есть
    }
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        // Другие свойства для клиента, если необходимо
    }

    public class Post
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        // Другие свойства для поставщика, если необходимо
    }
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // Другие свойства продукта
    }

    // Модель для накладной
    public class Nakladnaya
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        // Другие свойства накладной
    }
}

