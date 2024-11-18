using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public class SelectPlayerResourcesQuery : KingSqlQuery
    {
        private const string QUERY =
            "SELECT " +
            "r.custom_id, " +
            "pr.value " +
            "FROM player p " +
            "JOIN player_resource pr ON pr.player_id = p.id " +
            "JOIN resource r ON r.id = pr.resource_id " +
            "WHERE p.unity_id = @0";

        private readonly NpgsqlCommand _command;

        public SelectPlayerResourcesQuery(string playerUnityId)
        {
            _command = PrepareCommand(QUERY, playerUnityId);
        }

        public async Task<List<ResourceModel>> Execute()
        {
            try
            {
                List<ResourceModel> result = new List<ResourceModel>();
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    while (reader.HasRows && await reader.ReadAsync())
                    {
                        string id = reader.GetString(0);
                        float value = reader.GetFloat(1);
                        result.Add(new ResourceModel(id, value));
                    }

                    await reader.CloseAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occur executing SelectPlayerResourcesQuery: {e}");
                return null;
            }
            finally
            {
                await PushConnectionBack(_command.Connection).ConfigureAwait(false);
            }
        }
    }
}
