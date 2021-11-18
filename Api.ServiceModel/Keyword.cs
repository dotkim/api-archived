using Api.ServiceModel.Types;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/keyword/{Name}/{GuildId}", "GET")]
  public class GetKeyword : IReturn<GetKeywordResponse>
  {
    public string Name { get; set; }
    public ulong GuildId { get; set; }
  }

  public class GetKeywordResponse
  {
    public KeywordMessage Result { get; set; }
  }

  [Route("/keyword", "POST")]
  public class PostKeyword : IReturn<GetKeywordResponse>
  {
    public string Name { get; set; }
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
    public string Message { get; set; }
  }
}
