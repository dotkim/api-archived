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
  public class ImageService : Service
  {
    private static ILog _Log = LogManager.GetLogger(typeof(ImageService));

    public async Task<GetImagePageResponse> GetAsync(GetImagePage request)
    {
      var query = await ImageModule.GetPage(request.Page, true);
      return new GetImagePageResponse { Result = query };
    }

    public async Task<GetImageRandomResponse> GetAsync(GetImageRandom request)
    {
      var query = await ImageModule.GetRandom(request.GuildId, request.Filter);
      return new GetImageRandomResponse { Result = query };
    }

    public async Task<object> PostAsync(PostImage request)
    {
      List<string> files = FileHandler.Process(base.Request.Files, "image");

      foreach (string file in files)
      {
        string ext = file.Split(".").Last();

        ImageModule image = new ImageModule
        {
          Name = file,
          GuildId = request.GuildId,
          Extension = ext,
          Tags = new List<string> { "tagme" }
        };

        bool check = await ImageModule.Exists(image.Name, request.GuildId);

        bool query;
        if (check)
        {
          query = await ImageModule.Update(image);
        }
        else
        {
          query = await ImageModule.Insert(image);
        }
      }

      return HttpResult.Redirect("/");
    }
  }
}
