using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;

namespace Api
{
  // Add any additional metadata properties you want to store in the Users Typed Session
  public class CustomUserSession : AuthUserSession
  {
  }

  public class ConfigureAuth : IConfigureAppHost, IConfigureServices
  {
    private NameValueCollection _config;
    public void Configure(IServiceCollection services)
    {
      services.AddSingleton<ICacheClient>(new MemoryCacheClient()); //Store User Sessions in Memory Cache (default)

      var userRepo = new InMemoryAuthRepository();
      services.AddSingleton<IAuthRepository>(userRepo);
    }

    public void Configure(IAppHost appHost)
    {
      _config = ConfigurationManager.AppSettings;
      var AppSettings = appHost.AppSettings;

      appHost.Plugins.Add(new AuthFeature(() => new CustomUserSession(),
        new IAuthProvider[] {
          new BasicAuthProvider(AppSettings)
        })
      {
        IncludeAssignRoleServices = false,
        IncludeRegistrationService = false,
        MaxLoginAttempts = AppSettings.Get("MaxLoginAttempts", 5)
      });

      var repo = HostContext.AppHost.GetAuthRepository(null);
      repo.CreateUserAuth(new UserAuth()
      {
        Email = _config["Email"],
        DisplayName = _config["DisplayName"],
        UserName = _config["DisplayName"]
      }, _config["Password"]);
    }
  }
}
