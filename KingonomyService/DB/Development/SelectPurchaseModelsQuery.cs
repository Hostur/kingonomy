using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Development
{
    public class SelectPurchaseModelsQuery : KingSqlQuery
    {
        private const string QUERY = "SELECT id, reward, price FROM purchase_model;";

        private readonly NpgsqlCommand _command;

        public SelectPurchaseModelsQuery()
        {
            _command = PrepareCommand(QUERY);
        }

        public async Task<List<PurchaseModel>> Execute()
        {
            try
            {
                List<PurchaseModel> result = new List<PurchaseModel>();
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    while (reader.HasRows && await reader.ReadAsync())
                    {
                        string id = reader.GetString(0);
                        string reward = reader.GetString(1);
                        string price = reader.GetString(2);
                        result.Add(new PurchaseModel(id, reward, price));
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
