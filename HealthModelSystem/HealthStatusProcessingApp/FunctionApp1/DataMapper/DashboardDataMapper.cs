using System;
using System.Collections.Generic;
using System.Text;
using WebComponentHealthSystem.DashboardDataModel;
using WebComponentHealthSystem.HealthDataModels;
using System.Linq;
using WebComponentHealthSystem.Common;
using System.Globalization;

namespace WebComponentHealthSystem.DataMapper
{
    internal class DashboardDataMapper
    {
        internal static DashboardModel MapHealthStatusDataToDashboardModel(string componentName, List<HealthStatusModel> dataItems)
        {
            // Validate data and return null if no valid data is found
            if (dataItems == null || dataItems.Count == 0)
                return null;          

            // Create the resource health model
            HealthStatusModel resourceHealthModel = dataItems.SingleOrDefault(item => item.ModelType == GlobalConstants.MODELTYPERESOURCEHEALTH);
            SubComponentModel resourceHealth = null;
            List<SubComponentModel> generalSubcomponentModels = new List<SubComponentModel>();
            if (resourceHealthModel != null)
            {
                resourceHealth = new SubComponentModel
                {
                    SubcomponentName = GlobalConstants.MODELTYPERESOURCEHEALTH,
                    AdditionalInfo = resourceHealthModel.CurrentHealthStatus == GlobalConstants.PLATFORMHEALTHYSTATUS ?
                                GlobalConstants.ComponentAvailableStatus :
                                GlobalConstants.ComponentUnavailableStatus,
                    CurrentStatus = resourceHealthModel.CurrentHealthStatus,
                    SubcomponentCategory = GlobalConstants.SUBCOMPGENERAL,
                    LastCheckTime = Utility.ParseEventDateTimeString(resourceHealthModel.EventDetails.EventTimestamp)
                };
            }
            if (resourceHealth != null)
                generalSubcomponentModels.Add(resourceHealth);


            // Create the service health model
            HealthStatusModel serviceHealthModel = dataItems.SingleOrDefault(item => item.ModelType == GlobalConstants.MODELTYPESERVICEHEALTH);
            SubComponentModel serviceHealth = null;
            if (serviceHealthModel != null)
            {
                serviceHealth = new SubComponentModel
                {
                    SubcomponentName = GlobalConstants.MODELTYPESERVICEHEALTH,
                    AdditionalInfo = serviceHealthModel.CurrentHealthStatus == GlobalConstants.PLATFORMHEALTHYSTATUS ?
                                GlobalConstants.ComponentAvailableStatus :
                                GlobalConstants.ComponentUnavailableStatus,
                    CurrentStatus = serviceHealthModel.CurrentHealthStatus,
                    SubcomponentCategory = GlobalConstants.SUBCOMPGENERAL,
                    LastCheckTime = Utility.ParseEventDateTimeString(serviceHealthModel.EventDetails.EventTimestamp)
                };
            }
            if (serviceHealth != null)
                generalSubcomponentModels.Add(serviceHealth);

            // build the General Subcomponent
            var generalSubcomponents = new SubComponent
            {
                SubcomponentCategory = GlobalConstants.SUBCOMPGENERAL,
                SubComponentStatus = generalSubcomponentModels
            };

            // Create the platform health models
            IEnumerable<HealthStatusModel> platformStatusModels = dataItems.Where(item => item.ModelType == GlobalConstants.MODELTYPEPLATFORMHEALTH);
            List<SubComponentModel> platformSubcomponents = new List<SubComponentModel>();
            foreach (var statusItem in platformStatusModels)
            {
                var platformMetricDataModel = new SubComponentModel
                {
                    SubcomponentName = statusItem.ModelSubtype,
                    AdditionalInfo = statusItem.CurrentHealthStatus == GlobalConstants.PLATFORMHEALTHYSTATUS ?
                                GlobalConstants.ComponentAvailableStatus :
                                GlobalConstants.ComponentUnavailableStatus,
                    CurrentStatus = statusItem.CurrentHealthStatus,
                    SubcomponentCategory = GlobalConstants.SUBCOMPPLATFORM,
                    LastCheckTime = Utility.ParseEventDateTimeString(statusItem.EventDetails.EventTimestamp)
                };
                platformSubcomponents.Add(platformMetricDataModel);
            }
            // build the Platform Subcomponent
            var platformSubcomponent = new SubComponent
            {
                SubcomponentCategory = GlobalConstants.SUBCOMPPLATFORM,
                SubComponentStatus = platformSubcomponents
            };

            List<SubComponent> healthStatusSubComponents = new List<SubComponent> { generalSubcomponents, platformSubcomponent };
            bool overallComponentUnHealthyStatus = healthStatusSubComponents.Any(component =>
                                                    component.SubComponentStatus.Any(subitem =>
                                                        subitem.AdditionalInfo.ToLower() == GlobalConstants.ComponentUnavailableStatus.ToLower()));
            DashboardModel componentHealthModel = new DashboardModel
            {
                ComponentName = componentName,
                LastCheckTime = DateTime.UtcNow,
                MainComponent = true,
                OverallHealthStatus = overallComponentUnHealthyStatus ? GlobalConstants.PLATFORMUNHEALTHYSTATUS: GlobalConstants.PLATFORMHEALTHYSTATUS,
                SubComponents = healthStatusSubComponents
            };
            return componentHealthModel;

        }
    }
}
