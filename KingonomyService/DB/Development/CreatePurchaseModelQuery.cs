using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Development
{
    public class CreatePurchaseModelQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO purchase_model (id, reward, price) VALUES (@0, @1, @2);";

        private readonly NpgsqlCommand _command;

        public CreatePurchaseModelQuery(string id, string reward, string price)
        {
            _command = PrepareCommand(QUERY, id, reward, price);
        }

        public async Task<bool> Execute()
        {
            // nA LOGOWAniu gracza
            // Pobierz przedmiot o id X
            // Pobierz metadane o id Y
            // Porownaj z CurrentDay 
            // 
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
