using Npgsql;

namespace KingonomyService.DB.Base
{
    public class ConnectionProvider
    {
        public static string? _connectionString;
        public static void Initialize(Configuration configuration) => _connectionString = configuration.DbConnectionString;
        public static NpgsqlConnection Connection => new NpgsqlConnection(_connectionString);
    }
}
