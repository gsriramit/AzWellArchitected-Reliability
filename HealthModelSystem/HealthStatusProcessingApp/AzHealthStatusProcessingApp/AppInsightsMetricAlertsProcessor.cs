using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzHealthStatusProcessingApp.CosmosRepository;
using AzHealthStatusProcessingApp.HealthDataModels;
using AzHealthStatusProcessingApp.DataMapper;
using AzHealthStatusProcessingApp.HealthAlertModels.AppAvailability;

namespace AzHealthStatusProcessingApp
{
    public class AppInsightsMetricAlertsProcessor
    {
        private readonly ICosmosSqlRepository _cosmosSqlRepository;
        public AppInsightsMetricAlertsProcessor(ICosmosSqlRepository cosmosSqlRepository)
        {
            _cosmosSqlRepository = cosmosSqlRepository;
        }

        [FunctionName("UpdateAppHealth")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP trigger function invoked for App-Insights Metrics Update");

            //TODO: handle exceptions
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("Request body:"+ requestBody);

            // extract the Resource Health Alert matching the common alert schema from the request body
            WebappAvailabilityAlert alertData = JsonConvert.DeserializeObject<WebappAvailabilityAlert>(requestBody);
            // create the data to be persisted
            HealthStatusModel statusData = ApplicationMetricsMapper.MapAppHealthToStatusModel(alertData);
            // persist the update to the DB
            bool updateStatus = statusData == null ? false: await _cosmosSqlRepository.UpsertItemAsync(statusData.Id, statusData);

            string responseMessage = "This HTTP triggered function executed successfully";

            return new OkObjectResult(responseMessage);
        }
    }
}
