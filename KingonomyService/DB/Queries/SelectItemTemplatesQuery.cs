using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    internal sealed class SelectItemTemplatesQuery : KingSqlQuery
    {
        private const string QUERY = "SELECT id, metadata FROM item_template;";

        private readonly NpgsqlCommand _command;

        public SelectItemTemplatesQuery()
        {
            _command = PrepareCommand(QUERY);
        }

        public async Task<List<ItemTemplateModel>> Execute()
        {
            try
            {
                List<ItemTemplateModel> result = new List<ItemTemplateModel>();
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    while (reader.HasRows && await reader.ReadAsync())
                    {
                        string id = reader.GetString(0);
                        string metadata = reader.GetString(1);
                        result.Add(new ItemTemplateModel(id, metadata));
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
