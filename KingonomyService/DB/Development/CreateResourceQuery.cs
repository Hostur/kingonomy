using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Development
{
    public sealed class CreateResourceQuery : KingSqlQuery
    {
        private const string QUERY =
            "INSERT INTO resource (custom_id) VALUES (@0);";

        private readonly NpgsqlCommand _command;

        public CreateResourceQuery(string resourceId)
        {
            _command = PrepareCommand(QUERY, resourceId);
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
