using System.Web.Http;
using TestDiffedBinaries.Api.Repositories;
using TestDiffedBinaries.Api.Utilities;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [Route("api/v1/diff/left")]
    public class LeftV1Controller : ApiController
    {
        private readonly StorageHelper dbHelper;

        public LeftV1Controller()
        {
            dbHelper = new StorageHelper(this, DataRepositoryType.Left);
        }

        public IHttpActionResult Get([FromBody]string slotId)
        {
            return dbHelper.GetData(slotId);            
        }

        public IHttpActionResult Post([FromBody]string json)
        {
            return dbHelper.AddData(json);            
        }

        public IHttpActionResult Put([FromBody]string json)
        {
            return dbHelper.UpdateData(json);
        }

        public IHttpActionResult Delete([FromBody]string slotId)
        {
            return dbHelper.DeleteData(slotId);
        }
    }
}