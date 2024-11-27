using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB
{
    public sealed class CreateDb : KingSqlQuery
    {
        #region SQL
        private const string CREATE_PLAYER =
            "CREATE TABLE IF NOT EXISTS player " +
            "( " +
            "   id SERIAL PRIMARY KEY, " +
            "   unity_id VARCHAR(50) NOT NULL UNIQUE, " +
            "   role BYTE, " +
            "   metadata JSONB" +
            ");";

        private const string CREATE_PLAYER_ITEM =
            "CREATE TABLE IF NOT EXISTS player_item " +
            "( " +
            "   id SERIAL PRIMARY KEY, " +
            "   player_id INT NOT NULL, " +
            "   custom_id VARCHAR(50) NOT NULL, " +
            "   stackable BOOLEAN NOT NULL, " +
            "   quantity NUMERIC NOT NULL, " + 
            "   metadata JSONB, " + 
            "   FOREIGN KEY (player_id) REFERENCES player (id) " +
            ");";

        private const string CREATE_PURCHASE_MODEL =
            "CREATE TABLE IF NOT EXISTS purchase_model " +
            "(" +
            "   id VARCHAR(30) PRIMARY KEY, " +
            "   reward JSONB NOT NULL, " +
            "   price JSONB DEFAULT NULL " +
            ");";

        #endregion

        private readonly NpgsqlCommand _command1;
        private readonly NpgsqlCommand _command2;
        private readonly NpgsqlCommand _command3;
        public CreateDb()
        {
            _command1 = PrepareCommand(CREATE_PLAYER);
            _command2 = PrepareCommand(CREATE_PLAYER_ITEM);
            _command3 = PrepareCommand(CREATE_PURCHASE_MODEL);
        }

        public async Task Execute()
        {
            var connection = await GetConnection().ConfigureAwait(false);
            try
            {
                await ExecuteNoQueryAsync(_command1, connection).ConfigureAwait(false);
                await ExecuteNoQueryAsync(_command2, connection).ConfigureAwait(false);
                await ExecuteNoQueryAsync(_command3, connection).ConfigureAwait(false);
            }
            finally
            {
                await PushConnectionBack(connection).ConfigureAwait(false);
            }
        }
    }
}
