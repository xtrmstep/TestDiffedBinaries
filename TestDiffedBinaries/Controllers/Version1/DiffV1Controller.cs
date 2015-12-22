using System.Web.Http;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [Route("api/v1/diff")]
    public class DiffV1Controller : BaseApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("Diff");
        }
    }
}