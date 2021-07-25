using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureHealthAlertProcessingSystem.Factories
{
    /// <summary>
    /// This implementation creates a singleton object to be shared among the different functions in this app
    /// Implement a Factory Pattern if multiple versions of the cosmos client need to be created for different consumers
    /// Abstract using an interface when we have the need to use this for multiple consumers
    /// </summary>
    internal class CosmosFactory
    {
        private static string EndpointUrl = Environment.GetEnvironmentVariable("EndpointUrl");
        private static string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
        private static CosmosClient cosmosClient;

        private CosmosFactory() { }

        // The lock object will be used to synchronize threads during the first access to the Singleton.
        private static readonly object _lock = new object();

        /// <summary>
        /// Singleton creation method
        /// </summary>
        /// <returns></returns>
        internal static CosmosClient CreateCosmosClientForHealthSystem()
        {
            string cosmosConnectionString = $"AccountEndpoint={EndpointUrl};AccountKey={PrimaryKey};";
            if (cosmosClient == null)
            {
                // create a lock to create a singleton object in a thread safe way
                lock (_lock)
                {                    
                    if (cosmosClient == null)
                    {
                        cosmosClient = new CosmosClient(cosmosConnectionString);                        
                    }
                }
            }

                
            return cosmosClient;
        }
    }
}
