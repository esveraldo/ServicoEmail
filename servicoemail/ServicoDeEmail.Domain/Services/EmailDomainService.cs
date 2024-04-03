using Microsoft.Extensions.Logging;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using ServicoDeEmail.Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace ServicoDeEmail.Domain.Services
{
    public class EmailDomainService : IEmailDomainService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public EmailDomainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<List<Email>> GetAllEmails()
        {
            return _unitOfWork.emailRepository.GetAll().ToList();
        }

       
        public async Task NewEmail(Email email)
        {
            _unitOfWork.emailRepository.Create(email);
            _unitOfWork.SaveChanges();
        }
    }
}
