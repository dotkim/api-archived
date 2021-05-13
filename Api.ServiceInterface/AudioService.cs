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
  public class AudioService : Service
  {
    private static ILog _Log = LogManager.GetLogger(typeof(AudioService));
    public async Task<GetAudioRandomResponse> GetAsync(GetAudioRandom request)
    {
      var query = await AudioModule.GetRandom(request.GuildId, request.Filter);
      return new GetAudioRandomResponse { Result = query };
    }

    public async Task<object> PostAsync(PostAudio request)
    {
      List<string> files = FileHandler.Process(base.Request.Files, "audio");

      foreach (string file in files)
      {
        string ext = file.Split(".").Last();

        AudioModule audio = new AudioModule
        {
          Name = file,
          GuildId = request.GuildId,
          Extension = ext,
          Tags = new List<string> { "tagme" }
        };

        bool check = await AudioModule.Exists(audio.Name, request.GuildId);

        bool query;
        if (check)
        {
          query = await AudioModule.Update(audio);
        }
        else
        {
          query = await AudioModule.Insert(audio);
        }
      }

      return HttpResult.Redirect("/");
    }
  }
}
