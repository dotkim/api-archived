using System.Collections.Generic;
using Api.ServiceModel.Entities;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/image/random/{GuildId}", "GET")]
  [Route("/image/random/{GuildId}/{Filter}", "GET")]
  public class GetImageRandom : IReturn<GetImageRandomResponse>
  {
    [ApiMember(IsRequired = true)]
    public ulong GuildId { get; set; }
    public string Filter { get; set; } = "tagme";
  }

  [Route("/image/{GuildId}", "POST")]
  public class PostImage
  {
    [ApiMember(IsRequired = true)]
    public ulong GuildId { get; set; }
  }

  public class GetImagePageResponse
  {
    public List<Image> Result { get; set; }
  }

  public class GetImageRandomResponse
  {
    public Image Result { get; set; }
  }
}
