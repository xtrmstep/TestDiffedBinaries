using System;
using System.Net;
using System.Net.Http;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Repositories;
using TestDiffedBinaries.Api.Tests.Utilities;
using Xunit;

namespace TestDiffedBinaries.Api.Tests
{
    public class LeftV1ControllerTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture httpServerFixture;

        public LeftV1ControllerTests(HttpServerFixture fixture)
        {
            httpServerFixture = fixture;
        }

        public void Dispose()
        {
            
        }

        [Fact(DisplayName = "GET /diff/left - return NotFound")]
        public void Should_return_NotFound_whenNoData()
        {
            var id = Guid.NewGuid();
            var r = new DataRepository(id, DataRepositoryType.Left);
            r.Create(new byte[] { 1, 2, 3 });

            using (HttpResponseMessage response = httpServerFixture.GetJson("api/v1/diff/left", id.ToJson()))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact(DisplayName = "POST /diff/left - post Data")]
        public void Should_Post_data_to_storage()
        {
            var expectedData = new RequestData
            {
                Id = Guid.NewGuid(),
                Content = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            };
            var json = expectedData.ToJson();
            using (HttpResponseMessage response = httpServerFixture.PostJson("api/v1/diff/left", json))
            {

            }

            //DataRepository.G
        }

        //[Fact(DisplayName = "GET /diff/left - return Data")]
        //public void Should_return_Data_whenDataExists()
        //{
        //    var expectedData = new RequestData
        //    {
        //        Id = Guid.NewGuid(),
        //        Content = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        //    };

        //    using (HttpResponseMessage response = httpServerFixture.PostJson("api/v1/diff/left", expectedData.ToJson()))
        //    {
        //    }

        //    using (HttpResponseMessage response = httpServerFixture.GetJson("api/v1/diff/left"))
        //    {
        //        Assert.NotNull(response);
        //        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //    }
        //}

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

        //[Fact(DisplayName = "POST /diff/left - upload data")]
        //public void Should_upload_new_data()
        //{
        //    using (HttpRequestMessage request = httpServerFixture.CreateRequest("api/v1/diff/left", "application/json", new HttpMethod("POST")))
        //    {
        //        using (HttpResponseMessage response = client.SendAsync(request).Result)
        //        {
        //            Assert.NotNull(response);
        //            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        //        }
        //    }
        //}
    }
}