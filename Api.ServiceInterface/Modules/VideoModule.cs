using System;
using System.Threading.Tasks;
using Api.ServiceInterface.Interfaces;
using Api.ServiceModel.Entities;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Video type.
  /// </summary>
  public class VideoModule : Video, IDatabase<Video>
  {
    internal static Task<Video> Get(string name, ulong guildId)
    {
      throw new NotImplementedException();
    }

    internal static Task<Video> GetPage(int page, bool filter)
    {
      throw new NotImplementedException();
    }

    internal static Task<Video> GetRandom(ulong guildId, string filter)
    {
      return IDatabase<Video>.GetRandom(guildId, filter);
    }

    internal static Task<bool> Exists(string name, ulong guildId)
    {
      return IDatabase<Video>.Exists(name, guildId);
    } 

    internal static Task<bool> Update(Video video)
    {
      return IDatabase<Video>.Update(video);
    }

    internal static Task<bool> Insert(Video video)
    {
      return IDatabase<Video>.Insert(video);
    }
  }
}
