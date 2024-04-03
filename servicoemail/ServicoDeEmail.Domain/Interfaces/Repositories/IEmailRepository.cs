using ServicoDeEmail.Domain.Core;
using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Domain.Interfaces.Repositories
{
    public interface IEmailRepository : IBaseRepository<Email, Guid>
    {
        void UpdateEmail(Guid id, string status);
    }
}
