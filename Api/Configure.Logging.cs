using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.IO;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;

namespace Api
{
  public class ConfigureLogging : IConfigureAppHost
  {
    public void Configure(IAppHost appHost)
    {
      IAppSettings appSettings = new AppSettings();

      appHost.Plugins.Add(new RequestLogsFeature
      {
        RequestLogger = new CsvRequestLogger(
        files: new FileSystemVirtualFiles(HostContext.Config.WebHostPhysicalPath),
        requestLogsPattern: "requestlogs/{year}-{month}/{year}-{month}-{day}.csv",
        errorLogsPattern: "requestlogs/{year}-{month}/{year}-{month}-{day}-errors.csv",
        appendEvery: TimeSpan.FromSeconds(1)
        ),
      });

      //TODO: Write to file or elastic.
      var config = new LoggerConfiguration();

      // If we are debugging we set the level higher, else its just default.
      if (appSettings.Get<bool>("DebugMode", false)) config.MinimumLevel.Debug();

      switch (appSettings.Get<string>("SerilogSink"))
      {
          case "Elastic":
            // Will be changed for elastic support.
            config.WriteTo.Console();
            break;

          case "File":
            // Will be changed for file support.
            config.WriteTo.Console();
            break;

          default:
            config.WriteTo.Console();
            break;
      }

      LogManager.LogFactory = new SerilogFactory(config.CreateLogger());
    }
  }
}
