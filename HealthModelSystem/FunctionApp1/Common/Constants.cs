using System;
using System.Collections.Generic;
using System.Text;

namespace WebComponentHealthSystem.Common
{
    public static class GlobalConstants
    {
        public const string DATABASEID = "HealthModelDatabase";
        public const string CONTAINERID = "CloudComponentsHealth";
        public const string PRIMARYREGION = "East US";
        public const string SECONDARYREGION = "East US2";
        public const string GLOBAL = "Global";
        public const string PLATFORMMETRICS = "PlatformMetrics";
        public const string APPINSIGHTSMETRICS = "AppInsightsMetrics";
        public const string PLATFORMINITIATED = "PlatformInitiated";
        public const string PLATFORMHEALTHYSTATUS = "Healthy";
        public const string PLATFORMUNHEALTHYSTATUS = "UnHealthy";
        public const string PLATFORMALERTFIREDSTATUS = "Fired";
        public const string PLATFORMALERTRESOLVEDSTATUS = "Resolved";
        public const string CRITICALALERTSEVERITY = "Critical";
        public const string ERRORALERTSEVERITY = "Error";
        public const string WARNINGALERTSEVERITY = "Warning";
        public const string MODELTYPERESOURCEHEALTH = "Resource Health";
        public const string MODELTYPESERVICEHEALTH = "Service Health";
        public const string MODELTYPEPLATFORMHEALTH = "Platform Health";
        public const string MODELTYPEAPPLICATIONHEALTH = "Application Health";

        public const string SUBCOMPGENERAL = "General";
        public const string SUBCOMPPLATFORM = "Platform";
        public const string ComponentAvailableStatus = "Available";
        public const string ComponentUnavailableStatus = "Unavailable";
    }
}
