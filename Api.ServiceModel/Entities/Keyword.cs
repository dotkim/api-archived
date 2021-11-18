using System;
using System.Collections.Generic;
using Api.ServiceModel.Interfaces;
using Api.ServiceModel.Types;
using MongoDB.Entities;

namespace Api.ServiceModel.Entities
{
  [Name("keywords")]
  public class Keyword : Entity, ISchema
  {
    public string Name { get; set; }
    public ulong UploaderId { get; set; }
    public ulong GuildId { get; set; }
    public List<KeywordMessage> Messages { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
  }
}
