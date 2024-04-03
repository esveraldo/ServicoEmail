using Microsoft.Extensions.Logging;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using ServicoDeEmail.Domain.Interfaces.Services;
using System.Runtime.InteropServices;

namespace ServicoDeEmail.Domain.Services
{
    public class ServiceUsersDomainService : IServiceUsersDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceUsersDomainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ServiceUsers>> GetAllUsers()
        {
            return _unitOfWork.serviceRepository.GetAll().ToList();
        }

        public async Task ActivatingUser(Guid id)
        {
            _unitOfWork.serviceRepository.ActivatingUser(id);
        }

        public async Task InactivatingUser(Guid id)
        {
            _unitOfWork.serviceRepository.InactivatingUser(id);
        }

        public Task<ServiceUsers> GetUserByCode(string code)
        {
            var result = _unitOfWork.serviceRepository.GetUserByCode(code);
            return result;
        }

        public async Task NewUser(ServiceUsers user)
        
        {
            _unitOfWork.serviceRepository.Create(user);
            _unitOfWork.SaveChanges();
        }
    }
}
