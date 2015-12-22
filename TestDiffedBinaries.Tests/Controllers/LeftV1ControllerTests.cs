using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace TestDiffedBinaries.Api.Tests
{
    public class LeftV1ControllerTests : IClassFixture<HttpServerFixture>, IDisposable
    {
        private readonly HttpClient client;
        private readonly HttpServerFixture httpServerFixture;

        public LeftV1ControllerTests(HttpServerFixture fixture)
        {
            httpServerFixture = fixture;
            client = httpServerFixture.CreateServer();
        }

        public void Dispose()
        {
            client.Dispose();
        }

        [Fact(DisplayName = "GET /diff/left - return NotFound")]
        public void Should_return_NoContent_whenNoData()
        {
            using (HttpRequestMessage request = httpServerFixture.CreateRequest("api/v1/diff/left", "application/json", new HttpMethod("GET")))
            {
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    Assert.NotNull(response);
                    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                }
            }
        }

        //[Fact(DisplayName = "GET /diff/left - return Data")]
        //public void Should_return_NoContent_whenNoData()
        //{
        //    using (HttpRequestMessage request = httpServerFixture.CreateRequest("api/v1/diff/left", "application/json", new HttpMethod("POST")))
        //    {
        //        request.Content = new StringContent("data");
        //        using (HttpResponseMessage response = client.SendAsync(request).Result)
        //        {
        //            Assert.NotNull(response);
        //            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //        }
        //    }

        //    using (HttpRequestMessage request = httpServerFixture.CreateRequest("api/v1/diff/left", "application/json", new HttpMethod("GET")))
        //    {
        //        using (HttpResponseMessage response = client.SendAsync(request).Result)
        //        {
        //            Assert.NotNull(response);
        //            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //        }
        //    }
        //}

        [Fact(DisplayName = "POST /diff/left - upload data")]
        public void Should_upload_new_data()
        {
            using (HttpRequestMessage request = httpServerFixture.CreateRequest("api/v1/diff/left", "application/json", new HttpMethod("POST")))
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