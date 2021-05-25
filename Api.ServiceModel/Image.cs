using System.Collections.Generic;
using Api.ServiceModel.Entities;
using ServiceStack;

namespace Api.ServiceModel
{
  [Route("/image", "GET")]
  [Route("/image/{Page}", "GET")]
  [Route("/image/{Page}/{Filter}", "GET")]
  public class GetImagePage : IReturn<GetImagePageResponse>
  {
    [ApiMember(IsRequired = true)]
    public int Page { get; set; }
    public bool Filter { get; set; }
  }

  [Route("/image/random/{GuildId}", "GET")]
  [Route("/image/random/{GuildId}/{Filter}", "GET")]
  public class GetImageRandom
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
}
