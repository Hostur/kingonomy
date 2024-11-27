using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    internal sealed class AddPlayerItemQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO player_item (player_id, custom_id, stackable, quantity, metadata) VALUES (@0, @1, @2, @3, @4);";

        private readonly NpgsqlCommand _command;

        public AddPlayerItemQuery(int playerId, string customId, bool stackable, float quantity, string? metadata)
        {
            _command = PrepareCommand(QUERY, playerId, customId, stackable, quantity, metadata ?? string.Empty);
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
