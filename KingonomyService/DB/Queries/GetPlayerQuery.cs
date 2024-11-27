using Kingonomy;
using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    internal sealed class GetPlayerQuery : KingSqlQuery
    {
        private const string QUERY =
            "SELECT " +
            "id, " +
            "unity_id, " +
            "role, " +
            "metadata " +
            "FROM player WHERE unity_id = @0;";

        private readonly NpgsqlCommand _command;
        public GetPlayerQuery(string unityId)
        {
            _command = PrepareCommand(QUERY, unityId);
        }

        public async Task<PlayerModel?> Execute()
        {
            try
            {
                PlayerModel result = null;
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    if (reader.HasRows && await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string unityId = reader.GetString(1);
                        byte role = reader.GetByte(2);
                        string metadata = reader.GetString(3);

                        result = new PlayerModel(id, unityId, (Role)role, metadata, null);
                    }

                    await reader.CloseAsync().ConfigureAwait(false);
                }

                // If it is a player get his items and resources through the same connection.
                if (result?.Role == Role.Player && _command.Connection != null)
                {
                    result.Items = (await new SelectPlayerItemsQuery(result.Id).Execute(_command.Connection)).ToArray();
                }

                return result;
            }
            finally
            {
                await PushConnectionBack(_command.Connection).ConfigureAwait(false);
            }
        }
    }
}
