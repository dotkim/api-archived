using System.Threading.Tasks;
using MongoDB.Entities;
using ServiceStack;
using ServiceStack.Configuration;

namespace Api
{
  public class ConfigureMongo : IConfigureAppHost
  {
    private async Task Init(string database, string host)
    {
      await DB.InitAsync(database, host);
    }

    public void Configure(IAppHost appHost)
    {
      IAppSettings appSettings = new AppSettings();
      string db = appSettings.Get<string>("MongoDatabase", "chatbot");
      string host = appSettings.Get<string>("MongoHost", "localhost");

      Init(db, host).GetAwaiter().GetResult();
    }
  }
}
