using ServicoDeEmail.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Domain.Interfaces.Services
{
    public interface IConsumerUsersDomainService
    {
        Task NewConsumer(ConsumerUsers consumer);
    }
}
