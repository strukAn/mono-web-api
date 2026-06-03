using Npgsql;

namespace ExampleProject.WebApi
{
    public class ClothingStoreRepository
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=admin;" +
            "Database=postgres";

        private NpgsqlConnection connection;

        public ClothingStoreRepository()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();
        }
    }
}
