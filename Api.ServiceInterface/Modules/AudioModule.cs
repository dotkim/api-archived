using System;
using System.Threading.Tasks;
using Api.ServiceModel.Entities;
using Api.ServiceInterface.Interfaces;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Audio type.
  /// </summary>
  public class AudioModule : Audio, IDatabase<Audio>
  {
    internal static Task<Audio> Get(string name, ulong guildId)
    {
      throw new NotImplementedException();
    }

    internal static Task<Audio> GetPage(int page, bool filter)
    {
      throw new NotImplementedException();
    }

    internal static Task<Audio> GetRandom(ulong guildId, string filter)
    {
      return IDatabase<Audio>.GetRandom(guildId, filter);
    }

    internal static Task<bool> Exists(string name, ulong guildId)
    {
      return IDatabase<Audio>.Exists(name, guildId);
    }

    internal static Task<bool> Update(Audio audio)
    {
      return IDatabase<Audio>.Update(audio);
    }

    internal static Task<bool> Insert(Audio audio)
    {
      return IDatabase<Audio>.Insert(audio);
    }
  }
}
