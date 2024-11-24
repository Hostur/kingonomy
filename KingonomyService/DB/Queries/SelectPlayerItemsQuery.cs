using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public class SelectPlayerItemsQuery : KingSqlQuery
    {
        private const string QUERY =
            "SELECT " +
            "pi.id, " +
            "pi.custom_id, " +
            "pi.metadata " +
            "FROM player p " +
            "JOIN player_item pi ON pi.player_id = p.id " +
            "WHERE p.unity_id = @0";

        private const string QUERY_2 =
            "SELECT " +
            "pi.id, " +
            "pi.custom_id, " +
            "pi.metadata " +
            "FROM player p " +
            "JOIN player_item pi ON pi.player_id = p.id " +
            "WHERE p.id = @0";

        private readonly NpgsqlCommand _command;

        public SelectPlayerItemsQuery(string playerUnityId)
        {
            _command = PrepareCommand(QUERY, playerUnityId);
        }

        public SelectPlayerItemsQuery(int playerId)
        {
            _command = PrepareCommand(QUERY_2, playerId);
        }

        public async Task<List<PlayerItemModel>> Execute()
        {
            try
            {
                List<PlayerItemModel> result = new List<PlayerItemModel>();
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    while (reader.HasRows && await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string itemId = reader.GetString(1);
                        string metadata = reader.GetString(2);
                        result.Add(new PlayerItemModel(id, itemId, metadata));
                    }

                    await reader.CloseAsync();
                }

                return result;
            }
            finally
            {
                await PushConnectionBack(_command.Connection).ConfigureAwait(false);
            }
        }

        public async Task<List<PlayerItemModel>> Execute(NpgsqlConnection connection)
        {
            List<PlayerItemModel> result = new List<PlayerItemModel>();
            await using (var reader = await GetDataReaderAsync(_command, connection).ConfigureAwait(false))
            {
                while (reader.HasRows && await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    string itemId = reader.GetString(1);
                    string metadata = reader.GetString(2);
                    result.Add(new PlayerItemModel(id, itemId, metadata));
                }

                await reader.CloseAsync();
            }

            return result;
        }
    }
}
