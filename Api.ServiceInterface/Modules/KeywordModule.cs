using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceInterface.Interfaces;
using Api.ServiceInterface.Storage;
using Api.ServiceModel.Entities;
using MongoDB.Entities;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Keyword type.
  /// </summary>
  public class KeywordModule : Keyword, IModel<Keyword>
  {
    public Keyword GetTypeConstraint()
    {
      return new Keyword();
    }

    public Task<Keyword> Get(string name, ulong guildId)
    {
      return Database<Keyword>.Get(name, guildId);
    }

    public Task<List<Keyword>> GetPage(int page, bool filter)
    {
      throw new NotImplementedException();
    }

    public Task<Keyword> GetRandom(ulong guildId, string filter)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Exists(string name, ulong guildId)
    {
      return Database<Keyword>.Exists(name, guildId);
    }

    public async Task<bool> Update(Keyword keyword)
    {
      var res = await DB.Update<Keyword>()
        .Match(a => a.Name == keyword.Name)
        .Match(a => a.GuildId == keyword.GuildId)
        .Modify(x => x.Set(a => a.Messages, keyword.Messages))
        .ExecuteAsync();

      return res.IsAcknowledged;
    }

    public Task<bool> Insert(Keyword keyword)
    {
      return Database<Keyword>.Insert(keyword);
    }

    public Task<bool> ChangeGuild(string name, ulong current, ulong change)
    {
      return Database<Keyword>.ChangeGuild(name, current, change);
    }
  }
}
