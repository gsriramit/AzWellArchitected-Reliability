using FunctionApp1.HealthAlertModels.AppAvailability;
using System.Collections.Generic;
using System.Linq;
using WebComponentHealthSystem.Common;
using WebComponentHealthSystem.HealthDataModels;

namespace WebComponentHealthSystem.DataMapper
{
    internal class ApplicationMetricsMapper
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
        internal static HealthStatusModel MapAppHealthToStatusModel(WebappAvailabilityAlert platformMetricsAlert)
        {
            if (platformMetricsAlert == null || platformMetricsAlert.data ==null ||
                platformMetricsAlert.data.essentials ==null || platformMetricsAlert.data.alertContext ==null)
                return null;

            Essentials resourceAlertEssentials = platformMetricsAlert.data.essentials;
            Alertcontext resourceAlertContext = platformMetricsAlert.data.alertContext;

            // Event source in this context is Platform Metrics
            string eventSource = GlobalConstants.APPINSIGHTSMETRICS;
            // Specifics of the Metric
            string eventMetric = resourceAlertContext.condition?.allOf[0].metricName.Replace("/", string.Empty);

            // build the health status model that will be persisted in the DB
            HealthStatusModel statusData = new HealthStatusModel
            {
                // PartitionKey
                ComponentName = RESOURCETYPE,                
                CurrentHealthStatus = Utility.ResolvePlatformHealthStatus(resourceAlertEssentials.monitorCondition, resourceAlertEssentials.severity),
                Id = $"{applicationName}-{eventSource}-{eventMetric}", // docId
                ModelType = GlobalConstants.MODELTYPEAPPLICATIONHEALTH,
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

        private const string RESOURCETYPE = "WebApplication";
        private static string applicationName = "Webapp-ECommerce";

    }
}
