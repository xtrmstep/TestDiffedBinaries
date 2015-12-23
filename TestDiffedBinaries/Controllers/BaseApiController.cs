using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using TestDiffedBinaries.Api.Repositories;
using TestDiffedBinaries.Api.Tests.Utilities;

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