using System.Text;
using ExampleProject.Repository.Common;
using ExampleProject.Common;
using ExampleProject.Model;
using Npgsql;

namespace ExampleProject.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=admin;" +
            "Database=db2";

        public async Task<List<Category>> GetAllAsync(CategoryFilter filter)
        {
            string command = "select * from \"Category\" where (1=1";
            List<Category> categories = new List<Category>();
            using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
            StringBuilder builder = new StringBuilder();

            builder.Append(command);

            if (filter.Name != "")
            {
                builder.Append(" and p.\"Name\" = @Name");
                cmd.Parameters.AddWithValue("@Name", filter.Name);
            }
            builder.Append(")");

            cmd.CommandText = builder.ToString();

            try
            {
                connection.Open();
                using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category(
                            (int)reader["Id"],
                            (string)reader["Name"]
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

            return categories;
        }

        public async Task<Category> GetAsync(int id)
        {
            string command = "select * from \"Category\" as c where (c.\"Id\" = @Id)";
            Category category = new Category();
            using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();

                    category.Id = (int)reader["Id"];
                    category.Name = (string)reader["Name"];
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

            return category;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result;
            string command = "delete from \"Category\" where (\"Id\" = @Id)";
            using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
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

        public async Task<int> AddAsync(Category category)
        {
            int result = 0;
            string command = "insert into \"Category\" (\"Id\", \"Name\") values(DEFAULT, @Name)";
            using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            if (category == null)
            {
                return -1;
            }

            cmd.Parameters.AddWithValue("@Id", category.Id);
            cmd.Parameters.AddWithValue("@Name", category.Name);

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
        public async Task<int> UpdateAsync(int id, Category category)
        {
            int result = 0;
            string command = "update \"Category\" set \"Name\" = @Name where (\"Id\" = @Id)";
            using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            if (category == null)
            {
                return -1;
            }

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", category.Name);

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
