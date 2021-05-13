using System;
using System.Threading.Tasks;
using Api.ServiceInterface.Interfaces;
using Api.ServiceModel.Entities;
using MongoDB.Entities;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Keyword type.
  /// </summary>
  public class KeywordModule : Keyword, IDatabase<Keyword>
  {
    internal static Task<Keyword> Get(string name, ulong guildId)
    {
      return IDatabase<Keyword>.Get(name, guildId);
    }

    internal static Task<Keyword> GetPage(int page, bool filter)
    {
      throw new NotImplementedException();
    }

    internal static Task<Keyword> GetRandom(ulong guildId)
    {
      throw new NotImplementedException();
    }

    internal static Task<bool> Exists(string name, ulong guildId)
    {
      return IDatabase<Keyword>.Exists(name, guildId);
    }

    public async static Task<bool> Update(Keyword keyword)
    {
      var res = await DB.Update<Keyword>()
        .Match(a => a.Name == keyword.Name)
        .Match(a => a.GuildId == keyword.GuildId)
        .Modify(x => x.Set(a => a.Messages, keyword.Messages))
        .ExecuteAsync();

      return res.IsAcknowledged;
    }

    internal static Task<bool> Insert(Keyword keyword)
    {
      return IDatabase<Keyword>.Insert(keyword);
    }
  }
}
