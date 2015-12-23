using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Repositories;
using TestDiffedBinaries.Api.Tests.Utilities;

namespace TestDiffedBinaries.Api.Controllers
{
    public abstract class StorageApiController : BaseApiController
    {
        private DataRepository storage;

        public DataRepository Storage(Guid slotId)
        {
            // thread-safe singleton is not necessary here, because DataRepository implements the one under the hood
            return storage ?? (storage = new DataRepository(slotId, StorageType));
        }

        protected abstract DataRepositoryType StorageType { get; }

        protected IHttpActionResult GetData(string slotId)
        {
            byte[] data = Storage(slotId.FromJson<Guid>()).Get();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        protected IHttpActionResult AddData(string json)
        {
            var data = json.FromJson<RequestData>();
            var slotId = data.Id ?? Guid.NewGuid();
            Storage(slotId).Create(data.Content);
            return Ok(slotId.ToString());
        }

        protected IHttpActionResult UpdateData(string json)
        {
            var data = json.FromJson<RequestData>();
            Storage(data.Id.Value).Update(data.Content);
            return Ok();
        }

        protected IHttpActionResult DeleteData(string slotId)
        {
            Storage(slotId.FromJson<Guid>()).Delete();
            return Ok();
        }
    }
}