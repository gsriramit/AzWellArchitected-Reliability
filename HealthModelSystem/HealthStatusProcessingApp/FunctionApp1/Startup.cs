using AzureHealthAlertProcessingSystem.CosmosRepository;
using AzureHealthAlertProcessingSystem.Factories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AzureHealthAlertProcessingSystem.Common;
using AzureHealthAlertProcessingSystem;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureHealthAlertProcessingSystem
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)       
        {
            builder.Services.AddSingleton<ICosmosSqlRepository>((s) =>
            {
                var cosmosClient = CosmosFactory.CreateCosmosClientForHealthSystem();
                return new CosmosSqlRepository(cosmosClient, GlobalConstants.DATABASEID, GlobalConstants.CONTAINERID);
            });
        }
    }
}
