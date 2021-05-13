using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceInterface.Interfaces;
using Api.ServiceModel.Entities;

namespace Api.ServiceInterface.Modules
{
  /// <summary>
  /// Module for performing database specific queries on the Image type.
  /// </summary>
  public class ImageModule : Image, IDatabase<Image>
  {
    internal static Task<Image> Get(string name, ulong guildId)
    {
      throw new NotImplementedException();
    }

    internal static Task<List<Image>> GetPage(int page, bool filter)
    {
      return IDatabase<Image>.GetPage(page, filter);
    }

    internal static Task<Image> GetRandom(ulong guildId, string filter)
    {
      return IDatabase<Image>.GetRandom(guildId, filter);
    }

    internal static Task<bool> Exists(string name, ulong guildId)
    {
      return IDatabase<Image>.Exists(name, guildId);
    }

    internal static Task<bool> Update(Image image)
    {
      return IDatabase<Image>.Update(image);
    }

    internal static Task<bool> Insert(Image image)
    {
      return IDatabase<Image>.Insert(image);
    }
  }
}
