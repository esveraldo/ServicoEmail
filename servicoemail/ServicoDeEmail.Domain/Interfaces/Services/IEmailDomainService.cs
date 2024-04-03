using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Domain.Interfaces.Services
{
    public interface IEmailDomainService
    {
        Task NewEmail(Email email);
        Task<List<Email>> GetAllEmails();
    }
}
