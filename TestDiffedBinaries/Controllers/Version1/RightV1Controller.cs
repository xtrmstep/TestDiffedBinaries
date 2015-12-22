using System.Web.Http;
using TestDiffedBinaries.Api.Repositories;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [Route("api/v1/diff/right")]
    public class RightV1Controller : SideV1Controller
    {
        protected override DataRepositoryType StorageType
        {
            get
            {
                return DataRepositoryType.Right;
            }
        }
    }
}