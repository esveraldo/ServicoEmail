using FluentValidation;
using ServicoDeEmail.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Domain.Validations
{
    public class ServiceUsersValidation : AbstractValidator<ServiceUsers>
    {
        public ServiceUsersValidation()
        {
            RuleFor(e => e.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id é obrigatório");

            RuleFor(e => e.Smtp)
                .NotNull()
                .NotEmpty()
                .WithMessage("SMTP é obrigatório");

            RuleFor(e => e.Port)
                .NotNull()
                .NotEmpty()
                .WithMessage("A porta é obrigatório");

            RuleFor(e => e.AccessKey)
                .NotNull()
                .NotEmpty()
                .WithMessage("Chave de acesso é obrigatório");
        }
    }
}
