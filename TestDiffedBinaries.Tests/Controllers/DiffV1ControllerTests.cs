using System;
using System.Net;
using System.Net.Http;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Tests.Utilities;
using Xunit;

namespace TestDiffedBinaries.Api.Tests
{
    public class DiffV1ControllerTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture httpServerFixture;

        public DiffV1ControllerTests(HttpServerFixture fixture)
        {
            httpServerFixture = fixture;
        }

        [Fact(DisplayName = "GET return diff between two byte[]")]
        public void Should_return_diffs_between_two_byteArrays()
        {
            var id = Guid.NewGuid();

            #region Arrange data
            var leftData = new RequestData { Id = id, Content = new byte[] { 1, 2, 3, 5, 4 } };
            var rightData = new RequestData { Id = id, Content = new byte[] { 1, 2, 3, 4, 5 } };
            using (HttpResponseMessage response = httpServerFixture.PostJson("api/v1/diff/left", leftData.ToJson()))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            using (HttpResponseMessage response = httpServerFixture.PostJson("api/v1/diff/right", rightData.ToJson()))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            #endregion

            #region Action
            string actual = null;
            using (HttpResponseMessage response = httpServerFixture.GetJson("api/v1/diff", id.ToJson()))
            {
                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var content = response.Content as ObjectContent<string>;
                actual = content.Value as string;                
            }
            #endregion

            Assert.Equal(@"{""AreEqual"":false,""StatusMessage"":""not equal"",""Mismatches"":[{""Item1"":3,""Item2"":2}]}", actual);
        }
    }
}