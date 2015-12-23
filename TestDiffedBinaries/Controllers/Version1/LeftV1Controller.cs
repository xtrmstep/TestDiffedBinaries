using System;
using System.Web.Http;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Repositories;
using TestDiffedBinaries.Api.Tests.Utilities;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [Route("api/v1/diff/left")]
    public class LeftV1Controller : BaseApiController
    {
        private DataRepository storage;

        public DataRepository Storage(Guid slotId)
        {
            // thread-safe singleton is not necessary here, because DataRepository implements the one under the hood
            return storage ?? (storage = new DataRepository(slotId, DataRepositoryType.Left));
        }

        public IHttpActionResult Get([FromBody]string slotId)
        {
            byte[] data = Storage(Guid.Parse(slotId)).Get();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        public IHttpActionResult Post([FromBody]string json)
        {
            var data = json.FromJson<RequestData>();
            var slotId = data.Id ?? Guid.NewGuid();
            Storage(slotId).Create(data.Content);
            return Ok(slotId.ToString());
        }

        public IHttpActionResult Put([FromBody]string json)
        {
            var data = json.FromJson<RequestData>();
            Storage(data.Id.Value).Update(data.Content);
            return Ok();
        }

        public IHttpActionResult Delete(Guid slotId)
        {
            Storage(slotId).Delete();
            return Ok();
        }
    }
}