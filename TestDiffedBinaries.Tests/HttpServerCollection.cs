using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestDiffedBinaries.Api.Tests
{
    [CollectionDefinition("HttpServer")]
    public class HttpServerCollection : ICollectionFixture<HttpServerFixture>
    {
    }
}
