﻿using KingonomyService.DB.Base;
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
            "   unity_id VARCHAR(30) NOT NULL UNIQUE " +
            ");";

        private const string CREATE_RESOURCE =
            "CREATE TABLE resource " +
            "( " +
            "   id SERIAL PRIMARY KEY, " +
            "   custom_id VARCHAR(30) NOT NULL UNIQUE" +
            ");";

        private const string CREATE_PLAYER_RESOURCE =
            "CREATE TABLE player_resource " +
            "( " +
            "   player_id INT NOT NULL, " +
            "   resource_id INT NOT NULL, " +
            "   value NUMERIC DEFAULT 0, " +
            "   PRIMARY KEY (player_id, resource_id), " +
            "   FOREIGN KEY (player_id) REFERENCES player (id), " +
            "   FOREIGN KEY (resource_id) REFERENCES resource (id)" +
            ");";

        private const string CREATE_ITEM =
            "CREATE TABLE item " +
            "( " +
            "   id SERIAL PRIMARY KEY, " +
            "   custom_id VARCHAR(30) NOT NULL UNIQUE, " +
            "   metadata JSONB" +
            ");";

        private const string CREATE_PLAYER_ITEM =
            "CREATE TABLE player_item " +
            "( " +
            "   id SERIAL PRIMARY KEY, " +
            "   player_id INT NOT NULL, " +
            "   item_id INT NOT NULL, " +
            "   metadata JSONB, " +
            "   FOREIGN KEY (player_id) REFERENCES player (id), " +
            "   FOREIGN KEY (item_id) REFERENCES item (id)" +
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
                                         $"{CREATE_RESOURCE}\n" +
                                         $"{CREATE_PLAYER_RESOURCE}\n" +
                                         $"{CREATE_ITEM}\n" +
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