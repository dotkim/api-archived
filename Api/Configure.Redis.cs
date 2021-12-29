using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Redis;

namespace Api
{
  public class ConfigureRedis : IConfigureServices, IConfigureAppHost
  {
    IConfiguration Configuration { get; }
    public ConfigureRedis(IConfiguration configuration) => Configuration = configuration;

    public void Configure(IServiceCollection services)
    {
      var builder = new ConfigurationBuilder().AddXmlFile($"./config/config.xml", true, true);
      AppConfig config = builder.Build().Get<AppConfig>();

      services.AddSingleton<IRedisClientsManager>(
          new RedisManagerPool(config.RedisConnectionstring));
    }

    public void Configure(IAppHost appHost)
    {
      appHost.GetPlugin<SharpPagesFeature>()?.ScriptMethods.Add(new RedisScripts());
    }
  }
}
