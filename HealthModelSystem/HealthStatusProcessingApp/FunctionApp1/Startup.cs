using FunctionApp1.CosmosRepository;
using FunctionApp1.Factories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WebComponentHealthSystem.Common;

[assembly: FunctionsStartup(typeof(WebComponentHealthSystem.Startup))]
namespace WebComponentHealthSystem
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
