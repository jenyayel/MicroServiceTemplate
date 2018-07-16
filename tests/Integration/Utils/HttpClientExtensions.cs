using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Utils
{
    public static class HttpClientExtensions
    {
        public enum AuthorizationType
        {
            Anonymous,
            InvalidBearer
        }

        public enum RequestContentType
        {
            Json,
            Text
        }

        public static HttpClient Authorize(this HttpClient self, AuthorizationType type)
        {
            switch (type)
            {
                case AuthorizationType.InvalidBearer:
                    self.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Guid.NewGuid().ToString());
                    break;
                case AuthorizationType.Anonymous:
                    self.DefaultRequestHeaders.Authorization = null;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            return self;
        }

        public static HttpClient SetContentType(this HttpClient self, RequestContentType type)
        {
            switch (type)
            {
                case RequestContentType.Json:
                    self.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    break;
                case RequestContentType.Text:
                    self.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("text/html"));
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            return self;
        }

        public static async Task<HttpResponseMessage> PostAsync(this HttpClient self, string requestUri, object payload)
        {
            var payloadContent = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json");
            return await self.PostAsync(requestUri, payloadContent);
        }

        public static async Task<T> GetAsync<T>(this HttpClient self, string requestUri)
            where T : class
        {
            var result = await self.GetAsync(requestUri);

            if (!result.IsSuccessStatusCode)
                throw new HttpRequestException($"Received non successful response : {result}");

            var payload = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(payload);
        }

        public static async Task<HttpResponseMessage> PutAsync(this HttpClient self, string requestUri, object payload)
        {
            var payloadContent = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json");
            return await self.PutAsync(requestUri, payloadContent);
        }
    }
}
