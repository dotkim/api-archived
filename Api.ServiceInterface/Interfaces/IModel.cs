using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ServiceModel.Interfaces;

namespace Api.ServiceInterface.Interfaces
{
  public interface IModel<T> where T : ISchema
  {
    T GetTypeConstraint();
    Task<T> Get(string name, ulong guildId);
    Task<List<T>> GetAllNames(ulong guildId);
    Task<T> GetRandom(ulong guildId, string filter);
    Task<bool> Exists(string name, ulong gid);
    Task<bool> Insert(T type);
    Task<bool> Update(T type);
    Task<bool> ChangeGuild(string name, ulong current, ulong change);
  }
}