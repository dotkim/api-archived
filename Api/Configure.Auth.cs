using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;

namespace Api
{
  // Add any additional metadata properties you want to store in the Users Typed Session
  public class CustomUserSession : AuthUserSession
  {
  }

  public class ConfigureAuth : IConfigureAppHost, IConfigureServices
  {
    public void Configure(IServiceCollection services)
    {
      services.AddSingleton<ICacheClient>(new MemoryCacheClient()); //Store User Sessions in Memory Cache (default)

      var userRepo = new InMemoryAuthRepository();
      services.AddSingleton<IAuthRepository>(userRepo);
    }

    public void Configure(IAppHost appHost)
    {
      IAppSettings appSettings = new AppSettings();

      appHost.Plugins.Add(new AuthFeature(() => new CustomUserSession(),
        new IAuthProvider[] {
          new BasicAuthProvider(appSettings)
        })
      {
        IncludeAssignRoleServices = false,
        IncludeRegistrationService = false,
        MaxLoginAttempts = appSettings.Get<int>("MaxLoginAttempts", 5)
      });

      var repo = HostContext.AppHost.GetAuthRepository(null);
      repo.CreateUserAuth(new UserAuth()
      {
        Email = appSettings.Get<string>("Email"),
        DisplayName = appSettings.Get<string>("DisplayName"),
        UserName = appSettings.Get<string>("DisplayName")
      }, appSettings.Get<string>("Password"));
    }
  }
}
