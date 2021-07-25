using AzHealthStatusProcessingApp.CosmosRepository;
using AzHealthStatusProcessingApp.Factories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AzHealthStatusProcessingApp.Common;
using AzHealthStatusProcessingApp;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzHealthStatusProcessingApp
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
