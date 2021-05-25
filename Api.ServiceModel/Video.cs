using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/video/random/{GuildId}", "GET")]
  [Route("/video/random/{GuildId}/{Filter}", "GET")]
  public class GetVideoRandom
  {
    public ulong GuildId { get; set; }
    public string Filter { get; set; } = "tagme";
  }

  [Route("/video/{GuildId}", "POST")]
  public class PostVideo
  {
    public ulong GuildId { get; set; }
  }
}
