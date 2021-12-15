using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Redis;

namespace api
{
  public class ConfigureRedis : IConfigureServices, IConfigureAppHost
  {
    IConfiguration Configuration { get; }
    public ConfigureRedis(IConfiguration configuration) => Configuration = configuration;

    public void Configure(IServiceCollection services)
    {
      IAppSettings appSettings = new AppSettings();

      services.AddSingleton<IRedisClientsManager>(
          new RedisManagerPool(appSettings.Get<string>("RedisConnectionstring", "localhost:6379")));
    }

    public void Configure(IAppHost appHost)
    {
      appHost.GetPlugin<SharpPagesFeature>()?.ScriptMethods.Add(new RedisScripts());
    }
  }
}
