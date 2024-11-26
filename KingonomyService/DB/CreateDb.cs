using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB
{
    public class CreateDb : KingSqlQuery
    {
        #region SQL
        private const string CREATE_PLAYER =
            "CREATE TABLE player " +
            "( " +
            "   id SERIAL PRIMARY KEY, " +
            "   unity_id VARCHAR(50) NOT NULL UNIQUE, " +
            "   role BYTE, " +
            "   metadata JSONB" +
            ");";

        private const string CREATE_PLAYER_RESOURCE =
            "CREATE TABLE player_resource " +
            "( " +
            "   player_id INT NOT NULL, " +
            "   resource_id VARCHAR(50) NOT NULL, " +
            "   value NUMERIC DEFAULT 0, " +
            "   PRIMARY KEY (player_id, resource_id), " +
            "   FOREIGN KEY (player_id) REFERENCES player (id) " +
            ");";

        private const string CREATE_PLAYER_ITEM =
            "CREATE TABLE player_item " +
            "( " +
            "   id SERIAL PRIMARY KEY, " +
            "   player_id INT NOT NULL, " +
            "   custom_id VARCHAR(50) NOT NULL, " +
            "   metadata JSONB, " + 
            "   FOREIGN KEY (player_id) REFERENCES player (id) " +
            ");";

        private const string CREATE_PURCHASE_MODEL =
            "CREATE TABLE purchase_model " +
            "(" +
            "   id VARCHAR(30) PRIMARY KEY, " +
            "   reward JSONB NOT NULL, " +
            "   price JSONB DEFAULT NULL " +
            ");";

        private const string CREATE_DB = "CREATE DATABASE {0});" +
                                         $"{CREATE_PLAYER}\n" +
                                         $"{CREATE_PLAYER_RESOURCE}\n" +
                                         $"{CREATE_PLAYER_ITEM}\n" +
                                         $"{CREATE_PURCHASE_MODEL}\n";
        #endregion

        private readonly NpgsqlCommand _command;
        public CreateDb(string dbName)
        {
            _command = PrepareCommand(CREATE_DB, dbName);
        }

        public async Task Execute()
        {
            try
            {
                await ExecuteNoQueryAsync(_command).ConfigureAwait(false);
            }
            finally
            {
                await PushConnectionBack(_command.Connection).ConfigureAwait(false);
            }
        }
    }
}
