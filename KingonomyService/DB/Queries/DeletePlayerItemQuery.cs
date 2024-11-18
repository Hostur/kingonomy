using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public class DeletePlayerItemQuery : KingSqlQuery
    {
        private const string QUERY =
            "DELETE FROM player_item WHERE player_id = @0 AND id = @1;";

        private readonly NpgsqlCommand _command;

        /// <summary>
        /// Delete player item entity.
        /// </summary>
        /// <param name="playerId">Player db id.</param>
        /// <param name="itemId">Player unique item serial id.</param>
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
