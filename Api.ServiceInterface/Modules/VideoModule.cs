using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceInterface.Interfaces;
using Api.ServiceInterface.Storage;
using Api.ServiceModel.Entities;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Video type.
  /// </summary>
  public class VideoModule : Video, IModel<Video>
  {
    public Video GetTypeConstraint()
    {
      return new Video();
    }

    public Task<Video> Get(string name, ulong guildId)
    {
      throw new NotImplementedException();
    }

    public Task<List<Video>> GetPage(int page, bool filter)
    {
      throw new NotImplementedException();
    }

    public Task<Video> GetRandom(ulong guildId, string filter)
    {
      return Database<Video>.GetRandom(guildId, filter);
    }

    public Task<bool> Exists(string name, ulong guildId)
    {
      return Database<Video>.Exists(name, guildId);
    } 

    public Task<bool> Update(Video video)
    {
      return Database<Video>.Update(video);
    }

    public Task<bool> Insert(Video video)
    {
      return Database<Video>.Insert(video);
    }
  }
}
