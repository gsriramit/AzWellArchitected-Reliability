using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureRealtimeUpdatesProcessingSystem
{
    public static class HealthChangeFeedProcessor
    {

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Function)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "systemhealthmodelhub")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }


        [FunctionName("ProcessHealthChangeFeed")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "HealthModelDatabase",
            collectionName: "CloudComponentsHealth",
            ConnectionStringSetting = "CosmosConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> healthChangeMessages,
            [SignalR(HubName = "systemhealthmodelhub")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            if (healthChangeMessages != null && healthChangeMessages.Count > 0)
            {
                log.LogInformation("Documents modified " + healthChangeMessages.Count);
                log.LogInformation("First document Id " + healthChangeMessages[0].Id);
            }

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[] { "" }
                });
        }
    }
}
