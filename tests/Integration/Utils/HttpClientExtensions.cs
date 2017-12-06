using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Utils
{
    public static class HttpClientExtensions
    {
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
