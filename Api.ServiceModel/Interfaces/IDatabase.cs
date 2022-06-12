using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.ServiceModel.Interfaces
{
  public interface IDatabase<T> where T : ISchema
  {
    Task<bool> Exists();
    Task<bool> Insert();
    Task<bool> Update();
    Task<bool> ChangeGuild(ulong current, ulong change);
  }
}