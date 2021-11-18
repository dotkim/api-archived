using Api.ServiceModel.Entities;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/video/random/{GuildId}", "GET")]
  [Route("/video/random/{GuildId}/{Filter}", "GET")]
  public class GetVideoRandom : IReturn<GetVideoRandomResponse>
  {
    public ulong GuildId { get; set; }
    public string Filter { get; set; } = "tagme";
  }

  [Route("/video/{GuildId}/{UploaderId}", "POST")]
  public class PostVideo
  {
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
  }

  public class GetVideoRandomResponse
  {
    public Video FileInfo { get; set; }
  }
}
