using ServicoDeEmail.Application.Dtos.ConsumersUsers;
using ServicoDeEmail.Application.Dtos.ServicesUsers;
using ServicoDeEmail.Application.Dtos.User;
using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Application.Interfaces
{
    public interface IServiceUsersAppService
    {
        Task<string> Add(ServiceUsersDto serviceUsersDto);
        Task<List<GetUserDto>> GetAll();
        Task InactivatingUserOfSystem(Guid id);
        Task ActivatingUserOfSystem(Guid id);
        Task<ServiceUsers> GetUserByCodeOfSystem(string code);
    }
}
