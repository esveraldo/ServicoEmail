using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ServicoDeEmail.Domain.Entities;
using FluentAssertions;

namespace ServicoDeEmail.Unit.Tests.Entities
{
    public class EmailTest
    {
        [Fact]
        public void ValidarIdTest()
        {
            var email = new Email
            {
                Id = Guid.Empty
            };

            email.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Id é obrigatório"))
                .Should()
                .NotBeNull();
        }


        [Fact]
        public void ValidarDestinatarioTest()
        {
            var email = new Email
            {
                From = string.Empty
            };

            email.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Destinatário é obrigatório"))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void ValidarAssuntoTest()
        {
            var email = new Email
            {
                Subject = string.Empty
            };

            email.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Assunto é obrigatório"))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void ValidarMensgaemTest()
        {
            var email = new Email
            {
                Message = string.Empty
            };

            email.Validate
                .Errors
                .FirstOrDefault(er => er.ErrorMessage.Contains("Mensagem é obrigatório"))
                .Should()
                .NotBeNull();
        }
    }
}
