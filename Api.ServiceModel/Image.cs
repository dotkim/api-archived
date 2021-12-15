using Api.ServiceModel.Entities;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/image/random/{GuildId}", "GET")]
  [Route("/image/random/{GuildId}/{Filter}", "GET")]
  public class GetImageRandom : IReturn<GetImageRandomResponse>
  {
    public ulong GuildId { get; set; }
    public string Filter { get; set; } = "tagme";
  }

  [Route("/image/{GuildId}/{UploaderId}", "POST")]
  public class PostImage
  {
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
  }

  public class GetImageRandomResponse
  {
    public Image FileInfo { get; set; }
  }
}
