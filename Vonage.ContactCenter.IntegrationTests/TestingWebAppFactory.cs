using Microsoft.AspNetCore.Mvc.Testing;

namespace Vonage.ContactCenter.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
    }
}
