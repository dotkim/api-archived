using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/audio/random/{GuildId}", "GET")]
  [Route("/audio/random/{GuildId}/{Filter}", "GET")]
  public class GetAudioRandom
  {
    public ulong GuildId { get; set; }
    public string Filter { get; set; } = "tagme";
  }

  [Route("/audio/{GuildId}", "POST")]
  public class PostAudio
  {
    public ulong GuildId { get; set; }
  }
}
