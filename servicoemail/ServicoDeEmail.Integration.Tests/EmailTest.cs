using FluentAssertions;
using ServicoDeEmail.Application.Dtos.Emails;
using ServicoDeEmail.Integration.Tests.Helpers;
using System.Net;
using Xunit;

namespace ServicoDeEmail.Integration.Tests
{
    public class EmailTest
    {
        private readonly TestHelper _testHelper;

        public EmailTest()
        {
            _testHelper = new TestHelper();
        }

        [Fact]
        public async Task Test_Post_Email_Returns_Created()
        {
            var email = new CreateEmailDto
        {
            From = "teste@destinatarion.com",
            Subject = "Esta é uma mensagem de teste.",
            Message = "Mensagem de teste",
            AccessKey = "Test",
            Attachments = null
            };

        var content = _testHelper.CreateContent(email);
        var result = await _testHelper.CreateClient().PostAsync("/api/emails", content);

        result.StatusCode
            .Should()
                .Be(HttpStatusCode.Created);
    }
}
}
