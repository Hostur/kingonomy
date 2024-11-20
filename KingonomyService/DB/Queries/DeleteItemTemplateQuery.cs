using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public sealed class DeleteItemTemplateQuery : KingSqlQuery
    {
        private const string QUERY = "DELETE FROM item_template WHERE id = @0;";
        private const string QUERY_2 = "DELETE FROM item_template WHERE custom_id = @0;";
        private readonly NpgsqlCommand _command;
        public DeleteItemTemplateQuery(int itemId)
        {
            _command = PrepareCommand(QUERY, itemId);
        }

        public DeleteItemTemplateQuery(string customId)
        {
            _command = PrepareCommand(QUERY_2, customId);
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
