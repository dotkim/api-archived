using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ServiceModel;
using Api.ServiceModel.Entities;
using ServiceStack;
using ServiceStack.Logging;

namespace Api.ServiceInterface
{
  [Authenticate]
  [RequiredRole("Admin")]
  public class KeywordService : Service
  {
    private static ILog _Log = LogManager.GetLogger(typeof(KeywordService));

    public async Task<GetKeywordResponse> GetAsync(GetKeyword request)
    {
      string name = request.Name.ToUpperInvariant();
      var query = await Keyword.Get(name, request.GuildId);

      int min = query.Messages.Min(x => x.Count);
      Message message = query.Messages.Where(x => x.Count == min).First();

      await message.IncrementCount();

      return new GetKeywordResponse { Result = message };
    }

    public async Task<GetKeywordNamesResponse> GetAsync(GetKeywordNames request)
    {
      var query = await Keyword.GetAllNames(request.GuildId);
      List<string> response = query.Select(x => x.Name).ToList();

      return new GetKeywordNamesResponse { Result = response };
    }

    public async Task<GetKeywordResponse> PostAsync(PostKeyword request)
    {
      string name = request.Name.ToUpperInvariant();
      var exist = await new Keyword{ Name = name, GuildId = request.GuildId }.Exists();

      if (exist)
      {
        var keyword = await Keyword.Get(name, request.GuildId);
        int count = keyword.Messages.Min(x => x.Count);
        
        var message = new Message();
        message.Text = request.Message;
        message.Count = count;
        message.UploaderId = request.UploaderId;
        await keyword.Messages.AddAsync(message);

        return new GetKeywordResponse { Result = message };
      }
      else
      {
        var schema = new Keyword();
        schema.Name = name;
        schema.GuildId = request.GuildId;
        schema.UploaderId = request.UploaderId;
        await schema.Save();

        var message = new Message();
        message.Text = request.Message;
        message.UploaderId = request.UploaderId;
        await message.Save();
        await schema.Messages.AddAsync(message);

        return new GetKeywordResponse { Result = message };
      }
    }
  }
}
