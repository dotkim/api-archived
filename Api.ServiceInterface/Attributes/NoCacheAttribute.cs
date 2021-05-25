using ServiceStack;
using ServiceStack.Web;

namespace Api.ServiceInterface.Attributes
{
  public class NoCacheAttribute : RequestFilterAttribute
  {
    public override void Execute(IRequest req, IResponse res, object responseDto)
    {
      res.AddHeader(HttpHeaders.CacheControl, "no-store,must-revalidate,no-cache,max-age=0");
    }
  }
}