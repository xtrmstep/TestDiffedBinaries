using System;
using System.Web.Http;
using TestDiffedBinaries.Api.Repositories;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    public abstract class SideV1Controller : BaseApiController
    {
        private DataRepository storage;

        protected abstract DataRepositoryType StorageType
        {
            get;
        }

        public DataRepository Storage(Guid slotId)
        {
            // thread-safe singletone is not necessary here, because DataRepository implements the one under the hood
            return storage ?? (storage = new DataRepository(slotId, StorageType));
        }

        public IHttpActionResult Get(Guid slotId)
        {
            byte[] data = Storage(slotId).Get();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        public IHttpActionResult Post(string data, Guid? slotId)
        {
            byte[] bytes = Convert.FromBase64String(data);
            slotId = slotId ?? Guid.NewGuid();
            Storage(slotId.Value).Create(bytes);
            return Ok(slotId.Value);
        }

        public IHttpActionResult Put(Guid slotId, string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            Storage(slotId).Update(bytes);
            return Ok();
        }

        public IHttpActionResult Delete(Guid slotId)
        {
            Storage(slotId).Delete();
            return Ok();
        }
    }
}