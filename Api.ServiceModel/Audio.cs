using Api.ServiceModel.Entities;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/audio/random/{GuildId}/{Filter}", "GET")]
  public class GetAudioRandom : IReturn<GetAudioRandomResponse>
  {
    public ulong GuildId { get; set; }
    public string Filter { get; set; }
  }

  [Route("/audio/{GuildId}", "POST")]
  public class PostAudio
  {
    public ulong GuildId { get; set; }
  }

  public class GetAudioRandomResponse
  {
    public Audio Result { get; set; }
  }
}
