using FluentValidation;
using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Domain.Validations
{
    public class EmailValidation : AbstractValidator<Email>
    {
        public EmailValidation()
        {
            RuleFor(e => e.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id é obrigatório");

            RuleFor(e => e.From)
                .NotNull()
                .NotEmpty()
                .WithMessage("Destinatário é obrigatório");

            RuleFor(e => e.Subject)
                .NotNull()
                .NotEmpty()
                .WithMessage("Assunto é obrigatório");

            RuleFor(e => e.Message)
                .NotNull()
                .NotEmpty()
                .WithMessage("Mensagem é obrigatório");

        }
    }
}
