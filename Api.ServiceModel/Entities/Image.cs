using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceModel.Interfaces;
using Api.ServiceModel.Storage;
using MongoDB.Entities;

namespace Api.ServiceModel.Entities
{
  [Collection("images")]
  public class Image : Entity, ISchema, IDatabase<Image>
  {
    public string Name { get; set; }
    public ulong GuildId { get; set; }
    public ulong UploaderId { get; set; }
    public string Extension { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public static Task<Image> Get(string name, ulong guildId)
    {
      return Database<Image>.Get(name, guildId);
    }

    public static Task<List<Image>> GetAllNames(ulong guildId)
    {
      return Database<Image>.GetAllNames(guildId);
    }

    public static Task<Image> GetRandom(ulong guildId, string filter)
    {
      return Database<Image>.GetRandom(guildId, filter);
    }

    public Task<bool> Exists()
    {
      return Database<Image>.Exists(this);
    }

    public Task<bool> Insert()
    {
      return Database<Image>.Insert(this);
    }

    public Task<bool> Update()
    {
      return Database<Image>.Update(this);
    }

    public Task<bool> ChangeGuild(ulong current, ulong change)
    {
      return Database<Image>.ChangeGuild(this.Name, current, change);
    }
  }
}
