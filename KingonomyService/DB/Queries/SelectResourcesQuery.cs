﻿using Kingonomy.Models;
using KingonomyService.DB.Base;
using Npgsql;

namespace KingonomyService.DB.Queries
{
    public sealed class SelectResourcesQuery : KingSqlQuery
    {
        private const string QUERY = "SELECT custom_id FROM resource;";

        private readonly NpgsqlCommand _command;

        public SelectResourcesQuery()
        {
            _command = PrepareCommand(QUERY);
        }

        public async Task<List<ResourceModel>> Execute()
        {
            try
            {
                List<ResourceModel> result = new List<ResourceModel>();
                await using (var reader = await GetDataReaderAsync(_command).ConfigureAwait(false))
                {
                    while (reader.HasRows && await reader.ReadAsync())
                    {
                        string resourceId = reader.GetString(0);
                        result.Add(new ResourceModel(resourceId, 0));
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