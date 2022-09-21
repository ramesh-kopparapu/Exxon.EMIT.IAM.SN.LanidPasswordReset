//using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using func_snpasswordreset_kamal.Entity;

namespace func_snpasswordreset.Helper
{
    public static class AzureTableHelper
    {
        public static async Task<T> GetEntity<T>(string connectionString, string tableName, string partitionKey, string rowKey) where T : class, ITableEntity, new()
        {
            var serviceClient = new TableServiceClient(connectionString);
            var tableClient = serviceClient.GetTableClient(tableName);
            await tableClient.CreateIfNotExistsAsync();
            var response = await tableClient.GetEntityAsync<T>(partitionKey, rowKey);
            return response;
        }

        public static async Task<Response> AddEntity<T>(string connectionString, string tableName, T entity) where T : ITableEntity
        {
            var serviceClient = new TableServiceClient(connectionString);
            var tableClient = serviceClient.GetTableClient(tableName);
            await tableClient.CreateIfNotExistsAsync();
            var response = await tableClient.AddEntityAsync<T>(entity);
            return response;
        }
    }
}
