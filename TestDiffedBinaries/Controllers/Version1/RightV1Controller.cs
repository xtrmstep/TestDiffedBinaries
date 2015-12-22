using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [RoutePrefix("api/v1/diff")]
    public class RightV1Controller : ApiController
    {
        [Route("right")]
        public IHttpActionResult Get()
        {
            return Ok("right");
        }
    }
}
