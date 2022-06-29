using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vonage.ContactCenter.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _testClient;
        protected readonly JsonSerializerOptions _jsonSerializerOptions;
        protected IntegrationTest()
        {
            var appFactory = new TestingWebAppFactory<Program>();
            _testClient = appFactory.CreateDefaultClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
