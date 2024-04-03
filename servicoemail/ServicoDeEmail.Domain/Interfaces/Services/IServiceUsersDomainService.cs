using ServicoDeEmail.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Domain.Interfaces.Services
{
    public interface IServiceUsersDomainService
    {
        Task NewUser(ServiceUsers user);
        Task<List<ServiceUsers>> GetAllUsers();
        Task InactivatingUser(Guid id);
        Task ActivatingUser(Guid id);
        Task<ServiceUsers> GetUserByCode(string code);
    }
}
