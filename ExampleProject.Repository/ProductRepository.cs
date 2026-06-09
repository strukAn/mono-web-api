using System.Text;
using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Repository.Common;
using Npgsql;

namespace ExampleProject.Repository
{
    public class ProductRepository : IProductRepository
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=admin;" +
            "Database=db2";

        public async Task<List<Product>> GetAllAsync(ProductFilter filter)
        {
            string command = "select p.*, c.\"Name\" as \"CategoryName\" from \"Product\" as p left join \"Category\" as c on (p.\"CategoryId\" = c.\"Id\") where (1=1";
            NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            List<Product> products = new List<Product>();
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
            StringBuilder builder = new StringBuilder();
            builder.Append(command);

            if (filter.Name != "")
            {
                builder.Append(" and p.\"Name\" = @Name");
                cmd.Parameters.AddWithValue("@Name", filter.Name);
            }
            if (filter.Description != "")
            {
                builder.Append(" and p.\"Description\" LIKE @Description");
                string desc = $"%{filter.Description}%";
                cmd.Parameters.AddWithValue("@Description", desc);
            }
            if (filter.Stock != -1)
            {
                builder.Append(" and p.\"Stock\" = @Stock");
                cmd.Parameters.AddWithValue("@Stock", filter.Stock);
            }
            builder.Append(")");

            cmd.CommandText = builder.ToString();

                connection.Open();
            try
            {
                using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(
                            (int)reader["Id"],
                            (string)reader["Name"],
                            reader.IsDBNull(2) ? "" : (string)reader["Description"],
                            (int)reader["Stock"],
                            reader.IsDBNull(4) || reader.IsDBNull(5) ? null : new Category((int)reader["CategoryId"], (string)reader["CategoryName"])
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }

            return products;
        }
        
        public async Task<Product> GetAsync(int id)
        {
            string command = "select p.*, c.\"Name\" as \"CategoryName\" from \"Product\" as p left join \"Category\" as c on (p.\"CategoryId\" = c.\"Id\") where (p.\"Id\" = @Id)";
            NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            Product product = new Product();
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();

                    product.Id = (int)reader["Id"];
                    product.Name = (string)reader["Name"];
                    product.Description = reader.IsDBNull(2) ? "" : (string)reader["Description"];
                    product.Stock = (int)reader["Stock"];
                    product.Category = reader.IsDBNull(4) || reader.IsDBNull(5) ? null : new Category((int)reader["CategoryId"], (string)reader["CategoryName"]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }

            return product;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result;
            string command = "delete from \"Product\" where (\"Id\" = @Id)";
            NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            cmd.Parameters.AddWithValue("Id", id);

            try
            {
                connection.Open();
                result = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public async Task<int> AddAsync(Product product)
        {
            int result = 0;
            string command = "insert into \"Product\" (\"Id\", \"Name\", \"Description\", \"Stock\", \"CategoryId\") values(DEFAULT, @Name, @Description, @Stock, @CategoryId)";
            NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            if(product == null || product.Category == null)
            {
                return -1;
            }

            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);
            cmd.Parameters.AddWithValue("@CategoryId", product.Category.Id);

            try
            {
                connection.Open();
                result = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        public async Task<int> UpdateAsync(int id, Product product)
        {
            int result = 0;
            string command = "update \"Product\" set \"Name\" = @Name, \"Description\" = @Description, \"Stock\" = @Stock, \"CategoryId\" = @CategoryId where (\"Id\" = @Id)";
            NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            if (product == null || product.Category == null)
            {
                return -1;
            }

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);
            cmd.Parameters.AddWithValue("@CategoryId", product.Category.Id);

            try
            {
                connection.Open();
                result = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
