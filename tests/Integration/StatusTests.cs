using Integration.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Integration
{
    public class StatusTests : IClassFixture<TestsContext>
    {
        private readonly TestsContext _context;

        public StatusTests(TestsContext context)
        {
            _context = context;
        }

        [Fact]
        public async Task GetAnonymousStatusTest()
        {
            // act
            var response = await _context
                .GetClient(AuthorizationType.Anonymous, RequestContentType.Text)
                .GetAsync("/api/status");

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK", await response.Content.ReadAsStringAsync());
        }
    }
}
