using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace TestDiffedBinaries.Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected IHttpActionResult NoContent()
        {
            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }
    }
}