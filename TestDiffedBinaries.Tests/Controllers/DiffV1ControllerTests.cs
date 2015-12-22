using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace TestDiffedBinaries.Api.Tests
{
    public class DiffV1ControllerTests : IClassFixture<HttpServerFixture>, IDisposable
    {
        private readonly HttpClient client;
        private readonly HttpServerFixture httpServerFixture;

        public DiffV1ControllerTests(HttpServerFixture fixture)
        {
            httpServerFixture = fixture;
            client = httpServerFixture.CreateServer();
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}