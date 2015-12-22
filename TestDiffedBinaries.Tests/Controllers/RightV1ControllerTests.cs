using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace TestDiffedBinaries.Api.Tests
{
    public class RightV1ControllerTests : IClassFixture<HttpServerFixture>, IDisposable
    {
        private readonly HttpClient client;
        private readonly HttpServerFixture httpServerFixture;

        public RightV1ControllerTests(HttpServerFixture fixture)
        {
            httpServerFixture = fixture;
            client = httpServerFixture.CreateServer();
        }

        public void Dispose()
        {
            client.Dispose();
        }

        [Fact(DisplayName = "GET /diff/right - return NoContent")]
        public void Should_return_NoContent_whenNoData()
        {
            using (HttpRequestMessage request = httpServerFixture.CreateRequest("api/v1/diff/right", "application/json", new HttpMethod("GET")))
            {
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    Assert.NotNull(response);
                    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                }
            }
        }
    }
}