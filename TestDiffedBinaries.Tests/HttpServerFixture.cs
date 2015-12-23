using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace TestDiffedBinaries.Api.Tests
{
    public class HttpServerFixture : IDisposable
    {
        private const string baseUrl = "http://dummyurl/";
        private readonly HttpServer httpServer;

        public HttpServerFixture()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiApplication.Configure(config);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.EnsureInitialized();
            httpServer = new HttpServer(config);
        }

        public void Dispose()
        {
            if (httpServer != null)
            {
                httpServer.Dispose();
            }
        }

        public HttpRequestMessage CreateRequest(string url, string mthv, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(baseUrl + url)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;
            return request;
        }

        public HttpRequestMessage CreateRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = CreateRequest(url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }

        public HttpClient CreateServer()
        {
            return new HttpClient(httpServer);
        }

        public HttpResponseMessage GetJson(string url)
        {
            using (HttpRequestMessage request = CreateRequest(url, "text/plain", HttpMethod.Get))
            {
                using (HttpResponseMessage response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage GetJson(string url, string json)
        {
            using (HttpRequestMessage request = CreateRequest(url, "text/plain", HttpMethod.Get))
            {
                request.Content = new ObjectContent(typeof(string), json, new JsonMediaTypeFormatter());
                using (HttpResponseMessage response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage PostJson(string url, string json)
        {
            using (HttpRequestMessage request = CreateRequest(url, "text/plain", HttpMethod.Post))
            {
                request.Content = new ObjectContent(typeof(string), json, new JsonMediaTypeFormatter());
                using (HttpResponseMessage response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage PutJson(string url, string json)
        {
            using (HttpRequestMessage request = CreateRequest(url, "text/plain", HttpMethod.Put))
            {
                request.Content = new ObjectContent(typeof(string), json, new JsonMediaTypeFormatter());
                using (HttpResponseMessage response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage DeleteJson(string url, string json)
        {
            using (HttpRequestMessage request = CreateRequest(url, "text/plain", HttpMethod.Delete))
            {
                request.Content = new ObjectContent(typeof(string), json, new JsonMediaTypeFormatter());
                using (HttpResponseMessage response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }
    }
}