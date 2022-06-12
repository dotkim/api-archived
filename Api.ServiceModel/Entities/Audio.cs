using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceModel.Interfaces;
using Api.ServiceModel.Storage;
using MongoDB.Entities;

namespace Api.ServiceModel.Entities
{
  [Collection("audio")]
  public class Audio : Entity, ISchema, IDatabase<Audio>
  {
    public string Name { get; set; }
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
    public string Extension { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public static Task<Audio> Get(string name, ulong guildId)
    {
      return Database<Audio>.Get(name, guildId);
    }

    public static Task<List<Audio>> GetAllNames(ulong guildId)
    {
      return Database<Audio>.GetAllNames(guildId);
    }

    public static Task<Audio> GetRandom(ulong guildId, string filter)
    {
      return Database<Audio>.GetRandom(guildId, filter);
    }

    public Task<bool> Exists()
    {
      return Database<Audio>.Exists(this);
    }

    public Task<bool> Insert()
    {
      return Database<Audio>.Insert(this);
    }

    public Task<bool> Update()
    {
      return Database<Audio>.Update(this);
    }

    public Task<bool> ChangeGuild(ulong current, ulong change)
    {
      return Database<Audio>.ChangeGuild(this.Name, current, change);
    }
  }
}
