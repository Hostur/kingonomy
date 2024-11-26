using Kingonomy;
using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    internal sealed class AddPlayerQuery : KingSqlQuery
    {
        private const string QUERY = "INSERT INTO player (unity_id, role, metadata) VALUES (@0, @1, @2); SELECT LAST_INSERT_ID();";
        private readonly NpgsqlCommand _command;
        private readonly string _unityId;
        public AddPlayerQuery(string unityId, Role role, string metadata)
        {
            _unityId = unityId;
            _command = PrepareCommand(QUERY, unityId, (byte)role, metadata);
        }

        public async Task<PlayerModel?> Execute()
        {
            try
            {
                var id = await ExecuteScalarAsync(_command).ConfigureAwait(false);
                return await new GetPlayerQuery(_unityId).Execute().ConfigureAwait(false);
            }
            catch
            {
                return null;
            }
        }
        
        //public async Task<PlayerModel?> Execute2()
        //{
        //    var connection = await GetConnection().ConfigureAwait(false);
        //    var transaction = await connection.BeginTransactionAsync().ConfigureAwait(false);
        //    try
        //    {
        //    }
        //    catch
        //    {
        //        await transaction.RollbackAsync().ConfigureAwait(false);
        //    }
        //    finally
        //    {
        //        await PushConnectionBack(connection).ConfigureAwait(false);
        //    }
        //    return null;
        //}
    }
}
