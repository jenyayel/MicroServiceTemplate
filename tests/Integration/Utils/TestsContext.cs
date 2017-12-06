using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Web;

namespace Integration.Utils
{
    public class TestsContext : IDisposable
    {
        private readonly TestServer _server;

        public TestsContext()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
        }

        public void Dispose()
        {
            _server.Dispose();
        }

        internal HttpClient GetClient(AuthorizationType authorization, RequestContentType type = RequestContentType.Json)
        {
            var client = _server.CreateClient();

            switch (authorization)
            {
                case AuthorizationType.InvalidBearer:
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Guid.NewGuid().ToString());
                    break;
            }

            switch (type)
            {
                case RequestContentType.Json:
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    break;
                case RequestContentType.Text:
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("text/html"));
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            return client;
        }
    }
    internal enum AuthorizationType
    {
        Anonymous,
        InvalidBearer
    }

    internal enum RequestContentType
    {
        Json,
        Text
    }
}