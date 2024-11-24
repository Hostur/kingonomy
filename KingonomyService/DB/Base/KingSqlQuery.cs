using System.Data.Common;
using System.Data;
using Npgsql;

namespace KingonomyService.DB.Base
{
    public class KingSqlQuery
    {
        private const int ONE_32 = 1;
        private const long ONE_64 = 1;

        protected string AsQueryArray(int[] values)
        {
            string value = string.Join(',', values);
            return $"({value})";
        }

        protected async Task<bool> ExecuteExists(NpgsqlCommand command)
        {
            NpgsqlConnection connection = null;
            try
            {
                connection = await GetConnection().ConfigureAwait(false);
                command.Connection = connection;
                var exists = await ExecuteScalarAsync(command).ConfigureAwait(false);
                return exists.Equals(ONE_32) || exists.Equals(ONE_64);
            }
            finally
            {
                await PushConnectionBack(connection).ConfigureAwait(false);
            }
        }

        protected async Task<DbDataReader> GetDataReaderAsync(NpgsqlCommand command)
        {
            command.Connection = await GetConnection().ConfigureAwait(false);
            return await command.ExecuteReaderAsync().ConfigureAwait(false);
        }

        protected async Task<DbDataReader> GetDataReaderAsync(NpgsqlCommand command, NpgsqlConnection connection)
        {
            command.Connection = connection;
            return await command.ExecuteReaderAsync().ConfigureAwait(false);
        }

        protected async Task<object> ExecuteScalarAsync(NpgsqlCommand command)
        {
            NpgsqlConnection connection = null;
            try
            {
                connection = await GetConnection().ConfigureAwait(false);
                command.Connection = connection;
                var result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                return result;
            }
            finally
            {
                await PushConnectionBack(command.Connection).ConfigureAwait(false);
            }
        }


        protected async Task<int> ExecuteNoQueryAsync(NpgsqlCommand command)
        {
            NpgsqlConnection connection = null;
            try
            {
                connection = await GetConnection().ConfigureAwait(false);
                command.Connection = connection;
                var result = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return result;
            }
            finally
            {
                await PushConnectionBack(connection).ConfigureAwait(false);
            }
        }

        protected async Task<int> ExecuteNoQueryAsync(NpgsqlCommand command, NpgsqlConnection connection,
            bool closeConnection = false)
        {
            command.Connection = connection;
            var result = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            if (closeConnection)
                command.Connection?.Close();
            return result;
        }

        protected NpgsqlCommand PrepareCommand(string command, params object[] parameters)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = command;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + i, parameters[i]);
                }
            }

            return cmd;
        }

        private static async Task<NpgsqlConnection> GetConnectionInternal()
        {
            var connection = ConnectionProvider.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync().ConfigureAwait(false);
            }

            return connection;
        }

        public static async Task<NpgsqlConnection> GetConnection() =>
            await GetConnectionInternal().ConfigureAwait(false);

        public static async Task PushConnectionBack(NpgsqlConnection? connection)
        {
            if (connection == null) return;
            await connection.CloseAsync().ConfigureAwait(false);
            await connection.DisposeAsync().ConfigureAwait(false);
        }
    }
}
