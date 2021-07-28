using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using System.Threading.Tasks;
using MongoDB.Entities;
using ServiceStack.Configuration;

namespace api
{
  public class ConfigureMongoDb : IConfigureServices
  {
    IConfiguration Configuration { get; }
    public ConfigureMongoDb(IConfiguration configuration) => Configuration = configuration;

    private async Task Init(string database, string host)
    {
      await DB.InitAsync(database, host);
    }
    public void Configure(IServiceCollection services)
    {
      IAppSettings appSettings = new AppSettings();

      string host = appSettings.Get<string>("MongoHost", "database");
      string db = appSettings.Get<string>("MongoDatabase", "chatbot");

      Init(db, host).GetAwaiter().GetResult();
    }
  }
}
