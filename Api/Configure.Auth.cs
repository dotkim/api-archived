using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;

namespace api
{
  // Add any additional metadata properties you want to store in the Users Typed Session
  public class CustomUserSession : AuthUserSession
  {
  }

  // Custom Validator to add custom validators to built-in /register Service requiring DisplayName and ConfirmPassword
  public class CustomRegistrationValidator : RegistrationValidator
  {
    public CustomRegistrationValidator()
    {
      RuleSet(ApplyTo.Post, () =>
      {
        RuleFor(x => x.DisplayName).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty();
      });
    }
  }

  public class ConfigureAuth : IConfigureAppHost
  {
    public void Configure(IAppHost appHost)
    {
      var AppSettings = appHost.AppSettings;
      appHost.Plugins.Add(new AuthFeature(() => new CustomUserSession(),
          new IAuthProvider[] {
                    new CredentialsAuthProvider(AppSettings),     /* Sign In with Username / Password credentials */
                    new ApiKeyAuthProvider(AppSettings)
          }));

      appHost.Plugins.Add(new RegistrationFeature()); //Enable /register Service

      //override the default registration validation with your own custom implementation
      appHost.RegisterAs<CustomRegistrationValidator, IValidator<Register>>();
    }
  }
}
