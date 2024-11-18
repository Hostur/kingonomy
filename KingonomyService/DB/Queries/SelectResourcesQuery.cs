using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public sealed class SelectResourcesQuery : KingSqlQuery
    {
        private const string QUERY = "SELECT id, custom_id FROM resource;";

        private readonly NpgsqlCommand _command;

        public SelectResourcesQuery()
        {
            _command = PrepareCommand(QUERY);
        }

        public async Task<ResourcesModel> Execute()
        {
            try
            {
                List<ResourcesModel> result = new List<ResourcesModel>();
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    while (reader.HasRows && await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string resourceId = reader.GetString(1);
                        result.Add(new ResourcesModel(id, resourceId));
                    }

                    await reader.CloseAsync();
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
