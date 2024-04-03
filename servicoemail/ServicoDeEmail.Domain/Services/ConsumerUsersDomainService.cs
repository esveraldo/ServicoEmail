using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using ServicoDeEmail.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Domain.Services
{
    public class ConsumerUsersDomainService : IConsumerUsersDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsumerUsersDomainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task NewConsumer(ConsumerUsers consumer)
        {
            _unitOfWork.consumerRepository.Create(consumer);
            _unitOfWork.SaveChanges();
        }
    }
}
