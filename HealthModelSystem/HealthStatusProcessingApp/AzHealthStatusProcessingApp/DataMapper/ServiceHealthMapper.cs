using AzHealthStatusProcessingApp.HealthAlertModels.ServiceHealth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using AzHealthStatusProcessingApp.Common;
using AzHealthStatusProcessingApp.HealthDataModels;

namespace AzHealthStatusProcessingApp.DataMapper
{
    internal class ServiceHealthMapper
    {
        /// <summary>
        /// a simple function that is meant to create the target status model given the resource health alert object
        /// </summary>
        /// <remarks>
        /// Use an adapter pattern if needed
        /// also test the null conditions carefully
        /// </remarks>
        /// <param name="serviceHealthAlert"></param>
        /// <returns></returns>
        internal static HealthStatusModel MapServiceHealthToStatusModel(ServiceHealthAlert serviceHealthAlert)
        {
            if (serviceHealthAlert == null || serviceHealthAlert.data ==null ||
                serviceHealthAlert.data.essentials ==null || serviceHealthAlert.data.alertContext ==null)
                return null;

            Essentials resourceAlertEssentials = serviceHealthAlert.data.essentials;
            Alertcontext resourceAlertContext = serviceHealthAlert.data.alertContext;

            //Perform the prelim validation before further processing
            //Case1 : Global Service Issue (required)
            //Case2: Regional Service Issue & our region(s) of deployments are affected
            //Case3: Regional Service Issue in the regions of interest & services of interest are affected
            string impactedServicesValue = resourceAlertContext.properties.impactedServices.Replace('\\',' ');
            List<ImpactedServices> impactedServices = JsonConvert.DeserializeObject<List<ImpactedServices>>(impactedServicesValue);

            bool bprocessAlert = false;
            impactedServices.ForEach(service =>
            {
                bprocessAlert = service.ImpactedRegions.ToList().Any(region => region.RegionName == GlobalConstants.PRIMARYREGION
                                                                                        || region.RegionName == GlobalConstants.SECONDARYREGION
                                                                                        || region.RegionName == GlobalConstants.GLOBAL);
            });

            if (!bprocessAlert)
                return null;    

            // Get the component name and the resource name/id
            List<string> parsedResourceIdValues = Utility.GetResourceDetails(resourceAlertEssentials.alertTargetIDs[0]);

            string resourceType = parsedResourceIdValues.ElementAt(1);
            string resourceId = parsedResourceIdValues.ElementAt(2);
            // Get the event source- In this context this is expected to be service health
            //ToDo - this value is provided as an INT in the common alert schema documentation. Check with any actual alert payload and change code accordingly
            string eventSource = "ServiceHealth";

            // build the health status model that will be persisted in the DB
            HealthStatusModel statusData = new HealthStatusModel
            {
                ComponentName = resourceType, // PartitionKey
                CurrentHealthStatus = resourceAlertContext.properties.incidentType,               
                Id = $"{resourceId}-{eventSource}", // docId
                ModelType = GlobalConstants.MODELTYPESERVICEHEALTH,
                ModelSubtype = string.Empty,
                EventDetails = new AdditionalEventDetails
                {
                    CorrelationId = resourceAlertContext.correlationId,
                    EventCause = resourceAlertContext.properties.incidentType,
                    EventTimestamp = resourceAlertContext.eventTimestamp.ToString(),
                    EventType = resourceAlertContext.properties.incidentType,
                    OperationId = resourceAlertContext.operationId,
                    alertLevel = Utility.GetAlertSeverityLevel(resourceAlertContext.level),
                    EventDataId = resourceAlertContext.eventDataId,
                    OperationName = resourceAlertContext.operationName
                }

            };

            return statusData;           

        }
    }
}
