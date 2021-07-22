using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionApp1.CosmosRepository
{
    public interface ICosmosSqlRepository
    {
        Task<bool> CreateItemAsync<T>(string documentId, T entity);
        Task<bool> DeleteItemAsync<T>(string documentId, PartitionKey partitionKey);
        Task<T> ReadItemAsync<T>(string documentId, string partitionKey);
        Task<List<T>> ReadItemsAsync<T>(string partitionKey);
        Task<bool> ReplaceItemAsync<T>(string documentId, T entity);
        Task<bool> UpsertItemAsync<T>(string documentId, T entity);
    }
}