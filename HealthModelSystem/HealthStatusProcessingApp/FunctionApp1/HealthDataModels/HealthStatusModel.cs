using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureHealthAlertProcessingSystem.HealthDataModels
{
    public class HealthStatusModel
    {
        public string ComponentName { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string  ModelType { get; set; }
        public string ModelSubtype { get; set; }
        public string CurrentHealthStatus { get; set; }
        public string PreviousHealthStatus { get; set; }
        public AdditionalEventDetails EventDetails { get; set; }

    }

    public class AdditionalEventDetails
    {
        public string EventDataId { get; set; }

        public string alertLevel { get; set; }
        public string CorrelationId { get; set; }
        public string  EventTimestamp { get; set; }
        public string OperationName { get; set; }
        public string OperationId { get; set; }
        public string EventType { get; set; }
        public string EventCause { get; set; }
    }
}
