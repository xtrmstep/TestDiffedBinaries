using System.Web.Http;
using TestDiffedBinaries.Api.Repositories;

namespace TestDiffedBinaries.Api.Controllers.Version1
{
    [Route("api/v1/diff/left")]
    public class LeftV1Controller : SideV1Controller
    {
        protected override DataRepositoryType StorageType
        {
            get
            {
                return DataRepositoryType.Left;
            }
        }
    }
}