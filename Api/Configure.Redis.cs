using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Redis;

namespace api
{
  public class ConfigureRedis : IConfigureServices, IConfigureAppHost
  {
    IConfiguration Configuration { get; }
    public ConfigureRedis(IConfiguration configuration) => Configuration = configuration;

    public void Configure(IServiceCollection services)
    {
      services.AddSingleton<IRedisClientsManager>(
          new RedisManagerPool(Configuration.GetConnectionString("Redis") ?? "localhost:6379"));
    }

    public void Configure(IAppHost appHost)
    {
      appHost.GetPlugin<SharpPagesFeature>()?.ScriptMethods.Add(new RedisScripts());
    }
  }
}
