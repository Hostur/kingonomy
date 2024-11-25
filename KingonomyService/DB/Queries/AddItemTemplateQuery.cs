using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    internal sealed class AddItemTemplateQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO item_template (custom_id, metadata) VALUES (@0, @1);";

        private readonly NpgsqlCommand _command;

        public AddItemTemplateQuery(string customId, string metadata)
        {
            _command = PrepareCommand(QUERY, customId, metadata);
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
