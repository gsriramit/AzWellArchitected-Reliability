using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureHealthAlertProcessingSystem.DashboardDataModel;
using AzureHealthAlertProcessingSystem.DataMapper;
using AzureHealthAlertProcessingSystem.HealthDataModels;


namespace AzureHealthAlertProcessingSystem
{
    public class HealthChangeFeedProcessor
    {

        [FunctionName("negotiate")]
        public SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "systemhealthmodelhub")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }


        [FunctionName("ProcessHealthChangeFeed")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "HealthModelDatabase",
            collectionName: "CloudComponentsHealth",
            ConnectionStringSetting = "CosmosConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> healthChangeMessages,
            [SignalR(HubName = "systemhealthmodelhub")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            var healthDashboardData = new List<DashboardModel>();   
            if (healthChangeMessages != null && healthChangeMessages.Count > 0)
            {
                log.LogInformation("Documents modified " + healthChangeMessages.Count);
                log.LogInformation("First document Id " + healthChangeMessages[0].Id);

                //Serialize the document updates before type-casting to the healthstatusmodel type
                string healthUpdateSerializedVal = JsonConvert.SerializeObject(healthChangeMessages);
                // Create the list of health updates from the received change feed
                List<HealthStatusModel> healthUpdates = JsonConvert.DeserializeObject<List<HealthStatusModel>>(healthUpdateSerializedVal);

                
                if(healthUpdates!=null & healthUpdates.Count > 0)
                {
                    //If multiple updates are received at the same time, the signalR message sent to the client should still be
                    // in the DashboardDataModel format. The objects should be grouped by the Component property as the category
                    var updateGroups = from update in healthUpdates
                                       group update by update.ComponentName;
                    // Iterate over the categories and create one dashboard model object per component category
                    foreach(var componentCategory in updateGroups)
                    {
                        // Use the same Dashboard data mapper class to create the model
                        DashboardModel healthData = DashboardDataMapper.MapHealthStatusDataToDashboardModel(componentCategory.Key, componentCategory.ToList());
                        healthDashboardData.Add(healthData);
                    }
                }
            }

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "healthstatuschange",
                    Arguments = new[] { healthDashboardData }
                });
        }
    }
}
