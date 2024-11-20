using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public sealed class AddPlayerResourceQuery : KingSqlQuery
    {
        private const string QUERY = "INSERT INTO player_resource (player_id, resource_id, value) VALUES (@0, @1, @2);";

        private readonly NpgsqlCommand _command;

        public AddPlayerResourceQuery(int playerId, string resourceId, float value)
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
