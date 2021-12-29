using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using ServiceStack;
using ServiceStack.IO;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;

namespace Api
{
  public class ConfigureLogging : IConfigureAppHost
  {
    public void Configure(IAppHost appHost)
    {
      var builder = new ConfigurationBuilder().AddXmlFile($"./config/config.xml", true, true);
      AppConfig config = builder.Build().Get<AppConfig>();

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
      var logConfig = new LoggerConfiguration();

      // If we are debugging we set the level higher, else its just default.
      if (config.DebugMode) logConfig.MinimumLevel.Debug();

      switch (config.SerilogSink)
      {
        case "Elastic":
          // Will be changed for elastic support.
          logConfig.WriteTo.Console();
          break;

        case "File":
          // Will be changed for file support.
          logConfig.WriteTo.Console();
          break;

        default:
          logConfig.WriteTo.Console();
          break;
      }

      LogManager.LogFactory = new SerilogFactory(logConfig.CreateLogger());
    }
  }
}
