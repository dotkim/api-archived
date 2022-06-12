using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceModel.Interfaces;
using Api.ServiceModel.Storage;
using MongoDB.Entities;

namespace Api.ServiceModel.Entities
{
  [Collection("keywords")]
  public class Keyword : Entity, ISchema, IDatabase<Keyword>
  {
    public string Name { get; set; }
    public ulong UploaderId { get; set; }
    public ulong GuildId { get; set; }
    public Many<Message> Messages { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public Keyword()
    {
      this.InitOneToMany(() => Messages);
    }

    public Task Save()
    {
      return this.SaveAsync();
    }

    public static Task<Keyword> Get(string name, ulong guildId)
    {
      return Database<Keyword>.Get(name, guildId);
    }

    public static Task<List<Keyword>> GetAllNames(ulong guildId)
    {
      return Database<Keyword>.GetAllNames(guildId);
    }

    public static Task<Keyword> GetRandom(ulong guildId, string filter)
    {
      return Database<Keyword>.GetRandom(guildId, filter);
    }

    public Task<bool> Exists()
    {
      return Database<Keyword>.Exists(this);
    }

    public Task<bool> ChangeGuild(ulong current, ulong change)
    {
      return Database<Keyword>.ChangeGuild(this.Name, current, change);
    }

    Task<bool> IDatabase<Keyword>.Insert()
    {
      throw new NotImplementedException();
    }

    Task<bool> IDatabase<Keyword>.Update()
    {
      throw new NotImplementedException();
    }
  }
}
