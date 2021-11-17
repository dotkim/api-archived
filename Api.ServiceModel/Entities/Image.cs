using System;
using System.Collections.Generic;
using Api.ServiceModel.Interfaces;
using MongoDB.Entities;

namespace Api.ServiceModel.Entities
{
  [Name("images")]
  public class Image : Entity, ISchema
  {
    public string Name { get; set; }
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
    public string Extension { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
  }
}
