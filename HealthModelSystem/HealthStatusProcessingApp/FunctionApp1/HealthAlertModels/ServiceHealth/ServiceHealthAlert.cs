using System;
using System.Collections.Generic;
using System.Text;

namespace AzureHealthAlertProcessingSystem.HealthAlertModels.ServiceHealth
{
    public class ServiceHealthAlert
    {
        public string schemaId { get; set; }
        public Data data { get; set; }
    }


    public class Data
    {
        public Essentials essentials { get; set; }
        public Alertcontext alertContext { get; set; }
    }

    public class Essentials
    {
        public string alertId { get; set; }
        public string alertRule { get; set; }
        public string severity { get; set; }
        public string signalType { get; set; }
        public string monitorCondition { get; set; }
        public string monitoringService { get; set; }
        public string[] alertTargetIDs { get; set; }
        public string originAlertId { get; set; }
        public DateTime firedDateTime { get; set; }
        public DateTime resolvedDateTime { get; set; }
        public string description { get; set; }
        public string essentialsVersion { get; set; }
        public string alertContextVersion { get; set; }
    }

    public class Alertcontext
    {
        public object authorization { get; set; }
        public int channels { get; set; }
        public object claims { get; set; }
        public object caller { get; set; }
        public string correlationId { get; set; }
        public int eventSource { get; set; }
        public DateTime eventTimestamp { get; set; }
        public object httpRequest { get; set; }
        public string eventDataId { get; set; }
        public int level { get; set; }
        public string operationName { get; set; }
        public string operationId { get; set; }
        public Properties properties { get; set; }
        public string status { get; set; }
        public object subStatus { get; set; }
        public DateTime submissionTimestamp { get; set; }
        public object ResourceType { get; set; }
    }

    public class Properties
    {
        public string title { get; set; }
        public string service { get; set; }
        public string region { get; set; }
        public string communication { get; set; }
        public string incidentType { get; set; }
        public string trackingId { get; set; }
        public DateTime impactStartTime { get; set; }
        public DateTime impactMitigationTime { get; set; }
        public string impactedServices { get; set; }
        public string impactedServicesTableRows { get; set; }
        public string defaultLanguageTitle { get; set; }
        public string defaultLanguageContent { get; set; }
        public string stage { get; set; }
        public string communicationId { get; set; }
        public string maintenanceId { get; set; }
        public string isHIR { get; set; }
        public string version { get; set; }
    }

  

    public class ImpactedServices
    {
        public Impactedregion[] ImpactedRegions { get; set; }
        public string ServiceName { get; set; }
    }

    public class Impactedregion
    {
        public string RegionName { get; set; }
    }


}
