using System;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Utilities;
using Xunit;

namespace TestDiffedBinaries.Api.Tests.Utilities
{
    public class JsonSerializerTests
    {
        [Fact(DisplayName = "Json should return string for object")]
        public void Should_return_string_for_object()
        {
            var data = new RequestData { Id = Guid.NewGuid(), Content = new byte[] { 1, 2, 3, 4, 5 } };
            var actual = data.ToJson();

            Assert.False(string.IsNullOrWhiteSpace(actual));
        }

        [Fact(DisplayName = "Json should return correct format")]
        public void Should_return_correct_format()
        {
            var data = new RequestData { Id = Guid.Parse("250458d1-5ea3-4b68-83c8-86e88c68945a"), Content = new byte[] { 1, 2, 3, 4, 5 } };
            var actual = data.ToJson();
            const string expected = @"{""Id"":""250458d1-5ea3-4b68-83c8-86e88c68945a"",""Content"":""AQIDBAU=""}";
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "Json should return base64 encoded byte[]")]
        public void Should_return_base64_encoded_byteArray()
        {
            var byteArray = new byte[] { 1, 2, 3, 4, 5 };
            var data = new RequestData { Id = Guid.NewGuid(), Content = byteArray };
            var jsonString = data.ToJson();
            // because of the size it's possible to carve out concrete value
            var actual = jsonString.Substring(56, 8);
            string expected = Convert.ToBase64String(byteArray);
            Assert.Equal(expected, actual);
        }
    }
}
