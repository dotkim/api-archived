using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceInterface.Interfaces;
using Api.ServiceInterface.Storage;
using Api.ServiceModel.Entities;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Image type.
  /// </summary>
  public class ImageModule : Image, IModel<Image>
  {
    public Image GetTypeConstraint()
    {
      return new Image();
    }

    public Task<Image> Get(string name, ulong guildId)
    {
      throw new NotImplementedException();
    }

    public Task<List<Image>> GetPage(int page, bool filter)
    {
      return Database<Image>.GetPage(page, filter);
    }

    public Task<Image> GetRandom(ulong guildId, string filter)
    {
      return Database<Image>.GetRandom(guildId, filter);
    }

    public Task<bool> Exists(string name, ulong guildId)
    {
      return Database<Image>.Exists(name, guildId);
    }

    public Task<bool> Update(Image image)
    {
      return Database<Image>.Update(image);
    }

    public Task<bool> Insert(Image image)
    {
      return Database<Image>.Insert(image);
    }
  }
}
