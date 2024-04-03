using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Text;

namespace ServicoDeEmail.Integration.Tests.Helpers
{
    public class TestHelper
    {
        public HttpClient CreateClient()
        {
            return new WebApplicationFactory<Program>().CreateClient();
        }

        public StringContent CreateContent<T>(T entity)
        {
            return new StringContent(JsonConvert.SerializeObject(entity),
                Encoding.UTF8, "application/json");
        }
    }
}
