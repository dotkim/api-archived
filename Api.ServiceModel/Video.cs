using Api.ServiceModel.Entities;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/video/random/{GuildId}/{Filter}", "GET")]
  public class GetVideoRandom : IReturn<GetVideoRandomResponse>
  {
    public ulong GuildId { get; set; }
    public string Filter { get; set; }
  }

  [Route("/video/{GuildId}", "POST")]
  public class PostVideo
  {
    public ulong GuildId { get; set; }
  }

  public class GetVideoRandomResponse
  {
    public Video Result { get; set; }
  }
}
