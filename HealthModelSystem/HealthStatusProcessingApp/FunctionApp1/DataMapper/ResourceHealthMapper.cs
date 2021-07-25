using AzureHealthAlertProcessingSystem.HealthAlertModels.ResourceHealth;
using System.Collections.Generic;
using System.Linq;
using AzureHealthAlertProcessingSystem.Common;
using AzureHealthAlertProcessingSystem.HealthDataModels;

namespace AzureHealthAlertProcessingSystem.DataMapper
{
    internal class ResourceHealthMapper
    {
        /// <summary>
        /// a simple function that is meant to create the target status model given the resource health alert object
        /// </summary>
        /// <remarks>
        /// Use an adapter pattern if needed
        /// also test the null conditions carefully
        /// </remarks>
        /// <param name="resourceHealthAlert"></param>
        /// <returns></returns>
        internal static HealthStatusModel MapResourceHealthToStatusModel(ResourceHealthAlert resourceHealthAlert)
        {
            if (resourceHealthAlert == null || resourceHealthAlert.data ==null ||
                resourceHealthAlert.data.essentials ==null || resourceHealthAlert.data.alertContext ==null)
                return null;

            Essentials resourceAlertEssentials = resourceHealthAlert.data.essentials;
            Alertcontext resourceAlertContext = resourceHealthAlert.data.alertContext;

            // Get the component name and the resource name/id
            List<string> parsedResourceIdValues = Utility.GetResourceDetails(resourceAlertEssentials.alertTargetIDs[0]);

            string resourceType = parsedResourceIdValues.ElementAt(1);
            string resourceId = parsedResourceIdValues.ElementAt(2);
            // Get the event source- In this context this is expected to be ResourceHealth
            string eventSource = resourceAlertContext.eventSource.ToLower();

            // build the health status model that will be persisted in the DB
            HealthStatusModel statusData = new HealthStatusModel
            {
                ComponentName = resourceType, // PartitionKey
                CurrentHealthStatus = resourceAlertContext.properties.currentHealthStatus,
                PreviousHealthStatus = resourceAlertContext.properties.previousHealthStatus,
                Id = $"{resourceId}-{eventSource}", // docId
                ModelType = GlobalConstants.MODELTYPERESOURCEHEALTH,
                ModelSubtype = string.Empty,
                EventDetails = new AdditionalEventDetails
                {
                    alertLevel = resourceAlertContext.level,
                    EventDataId = resourceAlertContext.eventDataId,
                    CorrelationId = resourceAlertContext.correlationId,
                    EventCause = resourceAlertContext.properties.cause,
                    EventTimestamp = resourceAlertContext.eventTimestamp.ToString(),
                    EventType = resourceAlertContext.properties.type,
                    OperationId = resourceAlertContext.operationId,
                    OperationName = resourceAlertContext.operationName
                }

            };

            return statusData;           

        }
    }
}
