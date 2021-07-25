using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureHealthAlertProcessingSystem.HealthAlertModels.ResourceHealth;
using AzureHealthAlertProcessingSystem.CosmosRepository;
using AzureHealthAlertProcessingSystem.HealthDataModels;
using AzureHealthAlertProcessingSystem.DataMapper;
namespace AzureHealthAlertProcessingSystem
{
    public class ResourceHealthAlertProcessor
    {        
        private readonly ICosmosSqlRepository _cosmosSqlRepository;

        public ResourceHealthAlertProcessor(ICosmosSqlRepository cosmosSqlRepository)
        {
            _cosmosSqlRepository = cosmosSqlRepository;
        }       

        [FunctionName("UpdateComponentResourceHealth")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP trigger function invoked for Resource Health Update");            

            //TODO: handle exceptions
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("Request body:" + requestBody);
            // extract the Resource Health Alert matching the common alert schema from the request body
            ResourceHealthAlert alertData = JsonConvert.DeserializeObject<ResourceHealthAlert>(requestBody);
            // create the data to be persisted
            HealthStatusModel statusData = ResourceHealthMapper.MapResourceHealthToStatusModel(alertData);
            // persist the update to the DB
            bool updateStatus = statusData == null ? false: await _cosmosSqlRepository.UpsertItemAsync<HealthStatusModel>(statusData.Id, statusData);

            string responseMessage = "This HTTP triggered function executed successfully";

            return new OkObjectResult(responseMessage);
        }       
    }
}
