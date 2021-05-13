using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ServiceInterface.Modules;
using Api.ServiceInterface.Storage;
using Api.ServiceModel;
using ServiceStack;
using ServiceStack.Logging;

namespace Api.ServiceInterface
{
  [Authenticate]
  public class VideoService : Service
  {
    private static ILog _Log = LogManager.GetLogger(typeof(VideoService));
    public async Task<GetVideoRandomResponse> GetAsync(GetVideoRandom request)
    {
      var query = await VideoModule.GetRandom(request.GuildId, request.Filter);
      return new GetVideoRandomResponse { Result = query };
    }

    public async Task<object> PostAsync(PostVideo request)
    {
      List<string> files = FileHandler.Process(base.Request.Files, "video");

      foreach (string file in files)
      {
        string ext = file.Split(".").Last();

        VideoModule video = new VideoModule
        {
          Name = file,
          GuildId = request.GuildId,
          Extension = ext,
          Tags = new List<string> { "tagme" }
        };

        bool check = await VideoModule.Exists(video.Name, request.GuildId);

        bool query;
        if (check)
        {
          query = await VideoModule.Update(video);
        }
        else
        {
          query = await VideoModule.Insert(video);
        }
      }

      return HttpResult.Redirect("/");
    }
  }
}
