using FunctionApp1.HealthAlertModels.PlatfromMetrics;
using System.Collections.Generic;
using System.Linq;
using WebComponentHealthSystem.Common;
using WebComponentHealthSystem.HealthDataModels;

namespace WebComponentHealthSystem.DataMapper
{
    internal class PlatformMetricsMapper
    {
        /// <summary>
        /// a simple function that is meant to create the target status model given the resource health alert object
        /// </summary>
        /// <remarks>
        /// Use an adapter pattern if needed
        /// also test the null conditions carefully
        /// </remarks>
        /// <param name="platformMetricsAlert"></param>
        /// <returns></returns>
        internal static HealthStatusModel MapPlatformHealthToStatusModel(PlatformMetricAlerts platformMetricsAlert)
        {
            if (platformMetricsAlert == null || platformMetricsAlert.data ==null ||
                platformMetricsAlert.data.essentials ==null || platformMetricsAlert.data.alertContext ==null)
                return null;

            Essentials resourceAlertEssentials = platformMetricsAlert.data.essentials;
            Alertcontext resourceAlertContext = platformMetricsAlert.data.alertContext;

            // Get the component name and the resource name/id
            List<string> parsedResourceIdValues = Utility.GetResourceDetails(resourceAlertEssentials.alertTargetIDs[0]);

            string resourceType = parsedResourceIdValues.ElementAt(1);
            string resourceId = parsedResourceIdValues.ElementAt(2);
            // Event source in this context is Platform Metrics
            string eventSource = GlobalConstants.PLATFORMMETRICS;
            // Specifics of the Metric
            string eventMetric = resourceAlertContext.condition?.allOf[0].metricName.Replace(" ", string.Empty);

            // build the health status model that will be persisted in the DB
            HealthStatusModel statusData = new HealthStatusModel
            {
                // PartitionKey
                ComponentName = resourceType, 
                // the alert will be configured with a severity level of "Warning/Error/Critical". We willnot have alerts for Information and Verbose
                CurrentHealthStatus = Utility.ResolvePlatformHealthStatus(resourceAlertEssentials.monitorCondition, resourceAlertEssentials.severity),
                Id = $"{resourceId}-{eventSource}-{eventMetric}", // docId
                ModelType = GlobalConstants.MODELTYPEPLATFORMHEALTH,
                ModelSubtype = eventMetric,
                EventDetails = new AdditionalEventDetails
                {                    
                    alertLevel = resourceAlertEssentials.severity,                   
                    EventCause = GlobalConstants.PLATFORMINITIATED,
                    EventTimestamp = resourceAlertEssentials.firedDateTime.ToString(),
                    EventType = resourceAlertEssentials.signalType                    
                }

            };

            return statusData;   
           

        }
       
    }
}
