using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    internal sealed class ModifyPlayerItemQuery : KingSqlQuery
    {
        private const string QUERY =
            "UPDATE player_item SET metadata = @2 WHERE player_id = @0 AND item_id = @1;";

        private readonly NpgsqlCommand _command;

        public ModifyPlayerItemQuery(int playerId, int itemId, string metadata)
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
