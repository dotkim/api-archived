using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using System.Threading.Tasks;
using MongoDB.Entities;
using ServiceStack.Configuration;

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
      IAppSettings appSettings = new AppSettings();

      string connString = appSettings.Get<string>("MongoConnectionstring", "mongodb://localhost:27017");
      string db = appSettings.Get<string>("MongoDatabase", "chatbot");

      Init(db, connString).GetAwaiter().GetResult();
    }
  }
}
