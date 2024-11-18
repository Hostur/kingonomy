using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public sealed class AddPlayerItemQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO player_item (player_id, item_id, metadata) VALUES (@0, @1, @2);";

        private readonly NpgsqlCommand _command;

        public AddPlayerItemQuery(int playerId, int itemId, string metadata)
        {
            _command = PrepareCommand(QUERY, playerId, itemId, metadata);
        }

        public async Task<bool> Execute()
        {
            try
            {
                await ExecuteNoQueryAsync(_command).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
