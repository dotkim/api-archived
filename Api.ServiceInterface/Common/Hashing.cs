using System.Security.Cryptography;
using System.Text;

namespace Api.ServiceInterface.Common
{
  public static class Hashing
  {
    // From: https://github.com/ServiceStackApps/Imgur/blob/9bbac16be61ccb747525ed7eccd26f709a43a749/src/Imgur/Global.asax.cs#L118
    public static string GetMd5Hash(byte[] bytes)
    {
      var hash = MD5.Create().ComputeHash(bytes);
      var sb = new StringBuilder();
      for (var i = 0; i < hash.Length; i++)
      {
        sb.Append(hash[i].ToString("x2"));
      }
      return sb.ToString();
    }
  }
}
