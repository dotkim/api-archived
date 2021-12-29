using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using System.Threading.Tasks;
using MongoDB.Entities;

namespace Api
{
  public class ConfigureMongoDb : IConfigureServices
  {
    IConfiguration Configuration { get; }
    public ConfigureMongoDb(IConfiguration configuration) => Configuration = configuration;

    private async Task Init(string database, string connString)
    {
      await DB.InitAsync(database,
        MongoDB.Driver.MongoClientSettings.FromConnectionString(connString));
    }
    public void Configure(IServiceCollection services)
    {
      var builder = new ConfigurationBuilder().AddXmlFile($"./config/config.xml", true, true);
      AppConfig config = builder.Build().Get<AppConfig>();

      string connString = config.MongoConnectionstring;
      string db = config.MongoDatabase;

      Init(db, connString).GetAwaiter().GetResult();
    }
  }
}
