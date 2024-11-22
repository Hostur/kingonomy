using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Development
{
    public class AddPurchaseModelQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO purchase_model (id, reward, price) VALUES (@0, @1, @2);";

        private readonly NpgsqlCommand _command;

        public AddPurchaseModelQuery(string id, string reward, string price)
        {
            _command = PrepareCommand(QUERY, id, reward, price);
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
