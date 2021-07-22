using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1.CosmosRepository;
using WebComponentHealthSystem.DashboardDataModel;
using System.Collections.Generic;
using WebComponentHealthSystem.HealthDataModels;
using WebComponentHealthSystem.DataMapper;

namespace WebComponentHealthSystem
{
    public class HealthSystemDashboardProcessor
    {
        private readonly ICosmosSqlRepository _cosmosSqlRepository;
        public HealthSystemDashboardProcessor(ICosmosSqlRepository cosmosSqlRepository)
        {
            _cosmosSqlRepository = cosmosSqlRepository;
        }
        [FunctionName("HealthSystemDashboardProcessor")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Dashboard function triggered");

            DashboardModel scalesetHealthData = await GetResourceHealthDashboardDataAsync(SCALESETCOMPONENTNAME);
            DashboardModel gatewayHealthData = await GetResourceHealthDashboardDataAsync(GATEWAYCOMPONENTNAME);
            DashboardModel webapptHealthData = await GetResourceHealthDashboardDataAsync(WEBCOMPONENTNAME);

            var healthDashboardData=  new List<DashboardModel> { scalesetHealthData, gatewayHealthData, webapptHealthData };

            return new OkObjectResult(healthDashboardData);
        }

        private async Task<DashboardModel> GetResourceHealthDashboardDataAsync(string componentName)
        {
            List<HealthStatusModel> healthStatusItems = await _cosmosSqlRepository.ReadItemsAsync<HealthStatusModel>(componentName);
            DashboardModel healthData = DashboardDataMapper.MapHealthStatusDataToDashboardModel(componentName,healthStatusItems);
            return healthData;
        }

        
        private const string GATEWAYCOMPONENTNAME = "applicationgateways";
        private const string SCALESETCOMPONENTNAME = "virtualmachinescalesets";
        private const string WEBCOMPONENTNAME = "WebApplication";
    }
}
