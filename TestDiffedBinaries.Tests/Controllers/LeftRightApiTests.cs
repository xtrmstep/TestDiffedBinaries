using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Repositories;
using TestDiffedBinaries.Api.Utilities;
using Xunit;

namespace TestDiffedBinaries.Api.Tests
{
    public class LeftRightApiTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture httpServerFixture;

        public LeftRightApiTests(HttpServerFixture fixture)
        {
            httpServerFixture = fixture;
        }

        [Theory(DisplayName = "GET return NotFound when no data")]
        [InlineData("api/v1/diff/left")]
        [InlineData("api/v1/diff/right")]
        public void Should_return_NotFound_whenNoData(string url)
        {
            using (HttpResponseMessage response = httpServerFixture.GetJson(url))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Theory(DisplayName = "GET return data")]
        [InlineData("api/v1/diff/left", DataRepositoryType.Left, new byte[] { 1, 2, 3 })]
        [InlineData("api/v1/diff/right", DataRepositoryType.Right, new byte[] { 1, 2, 3 })]
        public void Should_return_data(string url, DataRepositoryType storageType, byte[] expected)
        {
            var id = Guid.NewGuid();
            var rep = new DataRepository(id, storageType);
            rep.Create(expected);

            using (HttpResponseMessage response = httpServerFixture.GetJson(url, id.ToJson()))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var content = response.Content as ObjectContent<byte[]>;
                Assert.NotNull(content);
                var actual = content.Value as byte[];
                Assert.True(Enumerable.SequenceEqual(expected, actual));
            }
        }

        [Theory(DisplayName = "POST create data")]
        [InlineData("api/v1/diff/left", DataRepositoryType.Left, new byte[] { 1, 2, 3 })]
        [InlineData("api/v1/diff/right", DataRepositoryType.Right, new byte[] { 1, 2, 3 })]
        public void Should_Post_data_to_storage(string url, DataRepositoryType storageType, byte[] expected)
        {
            var expectedData = new RequestData
            {
                Id = Guid.NewGuid(),
                Content = expected
            };
            var json = expectedData.ToJson();
            using (HttpResponseMessage response = httpServerFixture.PostJson(url, json))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            var rep = new DataRepository(expectedData.Id.Value, storageType);
            var actual = rep.Get();

            Assert.True(Enumerable.SequenceEqual(expected, actual));
        }

        [Theory(DisplayName = "PUT update data")]
        [InlineData("api/v1/diff/left", DataRepositoryType.Left, new byte[] { 1, 2, 3 })]
        [InlineData("api/v1/diff/right", DataRepositoryType.Right, new byte[] { 1, 2, 3 })]
        public void Should_update_data(string url, DataRepositoryType storageType, byte[] expected)
        {
            var id = Guid.NewGuid();
            var expectedData = new RequestData
            {
                Id = id,
                Content = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            };

            var rep = new DataRepository(id, storageType);
            rep.Create(expectedData.Content);

            var json = new RequestData { Id = id, Content = expected }.ToJson();
            using (HttpResponseMessage response = httpServerFixture.PutJson(url, json))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            var actual = rep.Get();

            Assert.True(Enumerable.SequenceEqual(expected, actual));
        }

        [Theory(DisplayName = "DELETE remove data")]
        [InlineData("api/v1/diff/left", DataRepositoryType.Left, new byte[] { 1, 2, 3 })]
        [InlineData("api/v1/diff/right", DataRepositoryType.Right, new byte[] { 1, 2, 3 })]
        public void Should_remove_data(string url, DataRepositoryType storageType, byte[] expected)
        {
            var id = Guid.NewGuid();
            var rep = new DataRepository(id, storageType);
            rep.Create(expected);

            using (HttpResponseMessage response = httpServerFixture.DeleteJson(url, id.ToJson()))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            var actual = rep.Get();

            Assert.Null(actual);
        }
    }
}