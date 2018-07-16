using Integration.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Integration
{
    public class CommonTests : IClassFixture<TestsContext>
    {
        private readonly TestsContext _context;

        public CommonTests(TestsContext context)
        {
            _context = context;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/swagger/index.html")]
        [InlineData("/swagger/v1.0/schema.json")]
        public async Task GetSwaggerTest(string url)
        {
            // arrange
            var client = _context.CreateClient().SetContentType(HttpClientExtensions.RequestContentType.Text);

            // act
            var response = await client.GetAsync(url);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/status")]
        public async Task GetStatusTest(string url)
        {
            // arrange
            var client = _context.CreateClient().SetContentType(HttpClientExtensions.RequestContentType.Json);

            // act
            var response = await client.GetAsync(url);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("\"OK\"", await response.Content.ReadAsStringAsync());
        }
    }
}
