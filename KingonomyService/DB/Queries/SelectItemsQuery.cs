using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public sealed class SelectItemsQuery : KingSqlQuery
    {
        private const string QUERY = "SELECT id, custom_id, metadata FROM item;";

        private readonly NpgsqlCommand _command;

        public SelectItemsQuery()
        {
            _command = PrepareCommand(QUERY);
        }

        public async Task<List<ItemModel>> Execute()
        {
            try
            {
                List<ItemModel> result = new List<ItemModel>();
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    while (reader.HasRows && await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string customId = reader.GetString(1);
                        string metadata = reader.GetString(2);
                        result.Add(new ItemModel(customId, metadata));
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
