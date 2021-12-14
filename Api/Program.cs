using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.ServiceInterface;
using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.VirtualPath;

namespace Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      IAppSettings appSettings = new AppSettings();

      var host = new WebHostBuilder()
          .UseKestrel(options =>
          {
            if (appSettings.Exists("UseHTTPS"))
            {
              options.Listen(IPAddress.Any, 8443, listenOptions =>
              {
                listenOptions.UseHttps(appSettings.Get<string>("CertificatePath"),
                  appSettings.Get<string>("CertificateSecret"));
              });
            }
            else
            {
              options.Listen(IPAddress.Any, 8080);
            }
          })
          .UseContentRoot(Directory.GetCurrentDirectory())
          .UseModularStartup<Startup>()
          .Build();

      host.Run();
    }
  }

  public class Startup : ModularStartup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public new void ConfigureServices(IServiceCollection services)
    {
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseServiceStack(new AppHost());

      app.Run(context =>
      {
        return Task.FromResult(0);
      });
    }
  }

  public class AppHost : AppHostBase
  {
    public AppHost()
        : base(
            "api",
            typeof(ImageService).Assembly,
            typeof(VideoService).Assembly,
            typeof(KeywordService).Assembly,
            typeof(AudioService).Assembly
        )
    { }

    public override void Configure(Container container)
    {
      IAppSettings appSettings = new AppSettings();
      bool debugMode = appSettings.Get<bool>("DebugMode", false);
      
      string staticDir = appSettings.Get<string>("UploadsDir");
      if (!Directory.Exists(staticDir)) Directory.CreateDirectory(staticDir);
      AddVirtualFileSources.Add(new FileSystemMapping("assets", staticDir));

      if (debugMode)
      {
        base.SetConfig(new HostConfig
        {
          DebugMode = debugMode
        });
      }
      else
      {
        base.SetConfig(new HostConfig
        {
          EnableFeatures = Feature.All.Remove(Feature.Metadata)
        });
      }
    }
  }
}
