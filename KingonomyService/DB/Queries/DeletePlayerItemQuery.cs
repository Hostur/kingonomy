using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    internal class DeletePlayerItemQuery : KingSqlQuery
    {
        private const string QUERY = "DELETE FROM player_item WHERE player_id = @0 AND id = @1;";
        private readonly NpgsqlCommand _command;
        public DeletePlayerItemQuery(int playerId, int itemId)
        {
            _command = PrepareCommand(QUERY, playerId, itemId);
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
