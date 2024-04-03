using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServicoDeEmail.Unit.Tests.Repositories
{
    public class EmailRepositoryTest
    {
        private readonly IEmailRepository _emailRepository;

        public EmailRepositoryTest(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [Fact]
        public void TesteDeCriacao()
        {
            var email = CriarEmail();

            var emailById = _emailRepository.GetById(email.Id);

            emailById.Should().NotBeNull();
            emailById.From.Should().Be(email.From);
            emailById.Subject.Should().Be(email.Subject);
            emailById.Message.Should().Be(email.Message);
        }

        private Email CriarEmail()
        {
            var email = new Email()
            {
                Id = Guid.NewGuid(),
                From = "teste@remetente.com",
                Subject = "Teste de envio",
                Message = "Mensagem de teste de mensagem de email.",
                AccessKey = "bfuYRVlQqv55jc12HoRq",
            };

            _emailRepository.Create(email);
            return email;
        }
    }
}
