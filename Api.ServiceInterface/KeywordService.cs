using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ServiceInterface.Modules;
using Api.ServiceInterface.Storage;
using Api.ServiceModel;
using Api.ServiceModel.Entities;
using Api.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Logging;

namespace Api.ServiceInterface
{
  [Authenticate]
  public class KeywordService : Service
  {
    private static ILog _Log = LogManager.GetLogger(typeof(KeywordService));

    public async Task<GetKeywordResponse> GetAsync(GetKeyword request)
    {
      string name = request.Name.ToUpperInvariant();
      var query = await KeywordModule.Get(name, request.GuildId);

      int min = query.Messages.Min(x => x.Count);
      KeywordMessage response = query.Messages.Where(x => x.Count == min).First();

      query.Messages.Find(x => x.Message.Equals(response.Message)).Count++;
      var update = await KeywordModule.Update(query);

      return new GetKeywordResponse { Result = response };
    }

    public async Task<GetKeywordResponse> PostAsync(PostKeyword request)
    {
      string name = request.Name.ToUpperInvariant();
      var exist = await KeywordModule.Exists(name, request.GuildId);

      if (exist)
      {
        var keyword = await KeywordModule.Get(name, request.GuildId);
        int count = keyword.Messages.Min(x => x.Count);

        var schema = new KeywordMessage { Message = request.Message, Count = count };
        keyword.Messages.Add(schema);

        var query = await KeywordModule.Update(keyword);
      }
      else
      {
        KeywordModule schema = new KeywordModule
        {
          Name = name,
          GuildId = request.GuildId,
          Messages = new List<KeywordMessage> { new KeywordMessage { Message = request.Message } }
        };

        var query = await KeywordModule.Insert(schema);
      }

      return new GetKeywordResponse { Result = new KeywordMessage { Message = request.Message } };
    }
  }
}
