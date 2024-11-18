using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    /// <summary>
    /// Add to given player all the resources that this player doesn't have yet. New resources will be assigned with value 0 by default.
    /// </summary>
    public sealed class AddPlayerResourcesQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO " +
            "   player_resource (player_id, resource_id, value) " +
            "   SELECT p.id, r.id, 0 " +
            "   FROM player p " +
            "   CROSS JOIN resource r " +
            "   LEFT JOIN player_resource pr ON pr.player_id = p.id AND pr.resource_id = r.id " +
            "   WHERE p.id = @0 AND pr.player_id IS NULL;";

        private readonly NpgsqlCommand _command;

        public AddPlayerResourcesQuery(int playerIda)
        {
            _command = PrepareCommand(QUERY, playerIda);
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
