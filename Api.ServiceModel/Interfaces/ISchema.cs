using System.Collections.Generic;
using MongoDB.Entities;

namespace Api.ServiceModel.Interfaces
{
  public interface ISchema : IEntity, ICreatedOn, IModifiedOn
  {
    string Name { get; set; }
    ulong GuildId { get; set; }
    List<string> Tags { get; set; }
  }
}
