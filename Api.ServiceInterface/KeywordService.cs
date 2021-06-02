using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ServiceInterface.Modules;
using Api.ServiceModel;
using Api.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Logging;

namespace Api.ServiceInterface
{
  [Authenticate]
  public class KeywordService : Service
  {
    private static ILog _Log = LogManager.GetLogger(typeof(KeywordService));
    private KeywordModule _module = new KeywordModule();

    public async Task<GetKeywordResponse> GetAsync(GetKeyword request)
    {
      string name = request.Name.ToUpperInvariant();
      var query = await _module.Get(name, request.GuildId);

      int min = query.Messages.Min(x => x.Count);
      KeywordMessage response = query.Messages.Where(x => x.Count == min).First();

      query.Messages.Find(x => x.Message.Equals(response.Message)).Count++;
      var update = await _module.Update(query);

      return new GetKeywordResponse { Result = response };
    }

    public async Task<GetKeywordResponse> PostAsync(PostKeyword request)
    {
      string name = request.Name.ToUpperInvariant();
      var exist = await _module.Exists(name, request.GuildId);

      if (exist)
      {
        var keyword = await _module.Get(name, request.GuildId);
        int count = keyword.Messages.Min(x => x.Count);

        var schema = new KeywordMessage { Message = request.Message, Count = count };
        keyword.Messages.Add(schema);

        var query = await _module.Update(keyword);
      }
      else
      {
        var schema = _module.GetTypeConstraint();
        schema.Name = name;
        schema.GuildId = request.GuildId;
        schema.Messages = new List<KeywordMessage> { new KeywordMessage { Message = request.Message } };

        var query = await _module.Insert(schema);
      }

      return new GetKeywordResponse { Result = new KeywordMessage { Message = request.Message } };
    }
  }
}
