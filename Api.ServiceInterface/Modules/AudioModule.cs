using System;
using System.Threading.Tasks;
using Api.ServiceModel.Entities;
using Api.ServiceInterface.Interfaces;
using Api.ServiceInterface.Storage;
using System.Collections.Generic;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Audio type.
  /// </summary>
  public class AudioModule : IModel<Audio>
  {
    public Audio GetTypeConstraint()
    {
      return new Audio();
    }

    public Task<Audio> Get(string name, ulong guildId)
    {
      throw new NotImplementedException();
    }

    public Task<List<Audio>> GetPage(int page, bool filter)
    {
      throw new NotImplementedException();
    }

    public Task<Audio> GetRandom(ulong guildId, string filter)
    {
      return Database<Audio>.GetRandom(guildId, filter);
    }

    public Task<bool> Exists(string name, ulong guildId)
    {
      return Database<Audio>.Exists(name, guildId);
    }

    public Task<bool> Update(Audio audio)
    {
      return Database<Audio>.Update(audio);
    }

    public Task<bool> Insert(Audio audio)
    {
      return Database<Audio>.Insert(audio);
    }
  }
}
