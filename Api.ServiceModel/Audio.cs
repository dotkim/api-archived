using Api.ServiceModel.Entities;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/audio/random/{GuildId}", "GET")]
  [Route("/audio/random/{GuildId}/{Filter}", "GET")]
  public class GetAudioRandom : IReturn<GetAudioRandomResponse>
  {
    public ulong GuildId { get; set; }
    public string Filter { get; set; } = "tagme";
  }

  [Route("/audio/{GuildId}/{UploaderId}", "POST")]
  public class PostAudio
  {
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
  }

  public class GetAudioRandomResponse
  {
    public Audio FileInfo { get; set; }
  }
}
