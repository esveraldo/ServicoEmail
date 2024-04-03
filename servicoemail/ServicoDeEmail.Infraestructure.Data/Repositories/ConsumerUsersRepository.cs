using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using ServicoDeEmail.Infraestructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoDeEmail.Infraestructure.Data.Repositories
{
    public class ConsumerUsersRepository : BaseRepository<ConsumerUsers, Guid>, IConsumerUsersRepository
    {
        private readonly DataContext _dataContext;

        public ConsumerUsersRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
