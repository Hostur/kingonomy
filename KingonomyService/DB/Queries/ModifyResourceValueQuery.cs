using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public sealed class ModifyResourceValueQuery : KingSqlQuery
    {
        private const string QUERY =
            "UPDATE player_resource SET value = value + @2 WHERE player_id = @0 AND resource_id = @1;";

        private readonly NpgsqlCommand _command;

        public ModifyResourceValueQuery(int playerId, string resourceId, float value)
        {
            _command = PrepareCommand(QUERY, playerId, resourceId, value);
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
