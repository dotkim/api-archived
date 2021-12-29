using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.ServiceInterface;
using Funq;
using ServiceStack;
using ServiceStack.VirtualPath;

namespace Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = new ConfigurationBuilder().AddXmlFile($"./config/config.xml", true, true);
      AppConfig config = builder.Build().Get<AppConfig>();

      if (config.DebugMode)
      {
        System.Console.WriteLine("Running with config:");
        System.Console.WriteLine(Newtonsoft.Json.JsonConvert
          .SerializeObject(config, Newtonsoft.Json.Formatting.Indented));
      }

      var host = new WebHostBuilder()
          .UseKestrel(options =>
          {
            if (config.UseHTTPS)
            {
              options.Listen(IPAddress.Any, config.WebPort, listenOptions =>
              {
                listenOptions.UseHttps(config.CertificatePath,
                  config.CertificateSecret);
              });
            }
            else
            {
              options.Listen(IPAddress.Any, config.WebPort);
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
      var builder = new ConfigurationBuilder().AddXmlFile($"./config/config.xml", true, true);
      AppConfig config = builder.Build().Get<AppConfig>();

      bool debugMode = config.DebugMode;

      string staticDir = config.UploadsDir;
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
