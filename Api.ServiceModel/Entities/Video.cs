using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceModel.Interfaces;
using Api.ServiceModel.Storage;
using MongoDB.Entities;

namespace Api.ServiceModel.Entities
{
  [Collection("videos")]
  public class Video : Entity, ISchema, IDatabase<Video>
  {
    public string Name { get; set; }
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
    public string Extension { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public static Task<Video> Get(string name, ulong guildId)
    {
      return Database<Video>.Get(name, guildId);
    }

    public static Task<List<Video>> GetAllNames(ulong guildId)
    {
      return Database<Video>.GetAllNames(guildId);
    }

    public static Task<Video> GetRandom(ulong guildId, string filter)
    {
      return Database<Video>.GetRandom(guildId, filter);
    }

    public Task<bool> Exists()
    {
      return Database<Video>.Exists(this);
    }

    public Task<bool> Insert()
    {
      return Database<Video>.Insert(this);
    }

    public Task<bool> Update()
    {
      return Database<Video>.Update(this);
    }

    public Task<bool> ChangeGuild(ulong current, ulong change)
    {
      return Database<Video>.ChangeGuild(this.Name, current, change);
    }
  }
}
