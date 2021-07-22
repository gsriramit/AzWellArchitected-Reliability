using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.HealthAlertModels.ResourceHealth
{

    public class ResourceHealthAlert
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
        public string channels { get; set; }
        public string correlationId { get; set; }
        public string eventSource { get; set; }
        public DateTime eventTimestamp { get; set; }
        public string eventDataId { get; set; }
        public string level { get; set; }
        public string operationName { get; set; }
        public string operationId { get; set; }
        public Properties properties { get; set; }
        public string status { get; set; }
        public DateTime submissionTimestamp { get; set; }
    }

    public class Properties
    {
        public string title { get; set; }
        public object details { get; set; }
        public string currentHealthStatus { get; set; }
        public string previousHealthStatus { get; set; }
        public string type { get; set; }
        public string cause { get; set; }
    }

}
