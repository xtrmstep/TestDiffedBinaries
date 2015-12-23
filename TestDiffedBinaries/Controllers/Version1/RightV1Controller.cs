using System;
using System.Web.Http;
using TestDiffedBinaries.Api.Repositories;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [Route("api/v1/diff/right")]
    public class RightV1Controller : StorageApiController
    {
        protected override DataRepositoryType StorageType
        {
            get
            {
                return DataRepositoryType.Right;
            }
        }        

        public IHttpActionResult Get([FromBody]string slotId)
        {
            return GetData(slotId);
        }

        public IHttpActionResult Post([FromBody]string json)
        {
            return AddData(json);
        }

        public IHttpActionResult Put([FromBody]string json)
        {
            return UpdateData(json);
        }

        public IHttpActionResult Delete([FromBody]string slotId)
        {
            return DeleteData(slotId);
        }
    }
}