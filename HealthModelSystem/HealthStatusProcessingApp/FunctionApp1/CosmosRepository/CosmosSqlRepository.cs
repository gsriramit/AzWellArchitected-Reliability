using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureHealthAlertProcessingSystem.CosmosRepository
{
    public class CosmosSqlRepository :  ICosmosSqlRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosSqlRepository(CosmosClient cosmosClient, string databaseId, string containerId)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(databaseId, containerId);
        }

        public async Task<T> ReadItemAsync<T>(string documentId, string partitionKey)
        {
            ItemResponse<T> entityResponse = await _container.ReadItemAsync<T>(documentId, new Microsoft.Azure.Cosmos.PartitionKey(partitionKey));
            return entityResponse;
        }

        public async Task<List<T>> ReadItemsAsync<T>(string partitionKey)
        {
            string queryByPartition = $"select * from c where c.ComponentName = '{partitionKey}' ";
            QueryDefinition queryDefinition = new QueryDefinition(queryByPartition);

            List<T> healthStatusItems = new List<T>();

            using (FeedIterator<T> feedIterator = _container.GetItemQueryIterator<T>(queryDefinition))
            {
                while (feedIterator.HasMoreResults)
                {
                    foreach (var item in await feedIterator.ReadNextAsync())
                    {
                        healthStatusItems.Add(item);
                    }
                }
            }

            return healthStatusItems;
        }

        public async Task<bool> CreateItemAsync<T>(string documentId, T entity)
        {
            ItemResponse<T> itemResponse = await _container.CreateItemAsync(
                entity);
            return itemResponse.StatusCode == System.Net.HttpStatusCode.Created;
        }

        public async Task<bool> UpsertItemAsync<T>(string documentId, T entity)
        {
            try
            {
                ItemResponse<T> itemResponse = await _container.UpsertItemAsync<T>(entity);
                return itemResponse.StatusCode == System.Net.HttpStatusCode.Created ||
                    itemResponse.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ReplaceItemAsync<T>(string documentId, T entity)
        {
            ItemResponse<T> itemResponse = await _container.ReplaceItemAsync<T>(
                entity, documentId);
            return itemResponse.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> DeleteItemAsync<T>(string documentId, PartitionKey partitionKey)
        {
            ItemResponse<T> itemResponse = await _container.DeleteItemAsync<T>(
                documentId, partitionKey);
            return itemResponse.StatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
}
