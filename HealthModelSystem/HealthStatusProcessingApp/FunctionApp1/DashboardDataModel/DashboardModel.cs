using System;
using System.Collections.Generic;
using System.Text;

namespace AzureHealthAlertProcessingSystem.DashboardDataModel
{
    internal class DashboardModel
    {
        public string ComponentName { get; set; }
        public bool MainComponent { get; set; }
        public DateTime LastCheckTime { get; set; }
        public string OverallHealthStatus { get; set; }
        public List<SubComponent> SubComponents { get; set; }
    }

    internal class SubComponent
    {
        public string SubcomponentCategory { get; set; }
        public List<SubComponentModel> SubComponentStatus { get; set; }
    }

    internal class SubComponentModel
    {
        public string SubcomponentName { get; set; }
        public string CurrentStatus { get; set; }
        public string AdditionalInfo { get; set; }
        public string SubcomponentCategory { get; set; }
        public DateTime LastCheckTime { get; set; }
    }
}
