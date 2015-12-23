using System;
using System.Web.Http;
using TestDiffedBinaries.Api.Repositories;
using TestDiffedBinaries.Api.Tests.Utilities;
using TestDiffedBinaries.Api.Utilities;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [Route("api/v1/diff")]
    public class DiffV1Controller : BaseApiController
    {
        public IHttpActionResult Get([FromBody]string slotId)
        {
            var data = DataRepository.PickSlot(slotId.FromJson<Guid>());
            var diff = BinaryComparer.Compare(data);
            return Ok(diff.ToJson());
        }
    }
}