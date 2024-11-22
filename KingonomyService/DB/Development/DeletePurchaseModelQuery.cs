using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Development
{
    public sealed class DeletePurchaseModelQuery : KingSqlQuery
    {
        private const string QUERY =
            "DELETE FROM purchase_model WHERE id = @0;";

        private readonly NpgsqlCommand _command;

        public DeletePurchaseModelQuery(string id)
        {
            _command = PrepareCommand(QUERY, id);
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
