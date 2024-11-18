using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Development
{
    public sealed class CreateItemQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO item (custom_id, metadata) VALUES (@0, @1);";

        private readonly NpgsqlCommand _command;

        public CreateItemQuery(string itemId, string metadata)
        {
            _command = PrepareCommand(QUERY, itemId, metadata);
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
