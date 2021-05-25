using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.ServiceInterface;
using Funq;
using MongoDB.Entities;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;
using System.Net;
using Serilog;
using ServiceStack.IO;
using System;

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
            options.Listen(IPAddress.Loopback, 8080);
            if (appSettings.Exists("UseHTTPS"))
            {
              options.Listen(IPAddress.Loopback, 8443, listenOptions =>
              {
                listenOptions.UseHttps(appSettings.Get<string>("CertificatePath"),
                  appSettings.Get<string>("CertificateSecret"));
              });
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
      bool debugMode = AppSettings.Get("DebugMode", false);

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

      Plugins.Add(new RequestLogsFeature
      {
        RequestLogger = new CsvRequestLogger(
        files: new FileSystemVirtualFiles(HostContext.Config.WebHostPhysicalPath),
        requestLogsPattern: "requestlogs/{year}-{month}/{year}-{month}-{day}.csv",
        errorLogsPattern: "requestlogs/{year}-{month}/{year}-{month}-{day}-errors.csv",
        appendEvery: TimeSpan.FromSeconds(1)
        ),
      });


      //TODO: Logger will later be able to write to file or elastic.
      LogManager.LogFactory = new SerilogFactory(
        new LoggerConfiguration()
        .WriteTo.Console()
        .MinimumLevel.Debug()
        .CreateLogger()
      );

      Task.Run(async () =>
            {
              await DB.InitAsync(appSettings.Get<string>("MongoDatabase", "chatbot"),
                appSettings.Get<string>("MongoHost", "localhost"));
            })
            .GetAwaiter()
            .GetResult();
    }
  }
}
