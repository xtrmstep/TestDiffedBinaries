using System;
using System.Web.Http;
using System.Web.Http.Results;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Repositories;

namespace TestDiffedBinaries.Api.Utilities
{
    /// <summary>
    /// Helper for Api controllers to interact with data storage
    /// </summary>
    public class StorageHelper
    {
        private IDataRepository storage;
        private readonly DataRepositoryType storageType;
        private readonly ApiController ctrl;

        public StorageHelper(ApiController ctrl, DataRepositoryType storageType)
        {
            this.storageType = storageType;
            this.ctrl = ctrl;
        }

        protected IDataRepository GetStorage(Guid slotId)
        {
            // thread-safe singleton is not necessary here, because DataRepository implements thread-safety under the hood
            return storage ?? (storage = new DataRepository(slotId, storageType));
        }

        public IHttpActionResult GetData(string slotId)
        {
            byte[] data = GetStorage(slotId.FromJson<Guid>()).Get();
            if (data == null)
            {
                return new NotFoundResult(ctrl);
            }
            return new OkNegotiatedContentResult<string>(data.ToJson(), ctrl);
        }

        public IHttpActionResult AddData(string json)
        {
            var data = json.FromJson<RequestData>();
            var slotId = data.Id ?? Guid.NewGuid();
            GetStorage(slotId).Create(data.Content);
            return new OkNegotiatedContentResult<string>(slotId.ToJson(), ctrl);
        }

        public IHttpActionResult UpdateData(string json)
        {
            var data = json.FromJson<RequestData>();
            GetStorage(data.Id.Value).Update(data.Content);
            return new OkResult(ctrl);
        }

        public IHttpActionResult DeleteData(string slotId)
        {
            GetStorage(slotId.FromJson<Guid>()).Delete();
            return new OkResult(ctrl);
        }
    }
}