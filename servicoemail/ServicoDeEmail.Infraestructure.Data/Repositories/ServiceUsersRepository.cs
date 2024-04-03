using Microsoft.EntityFrameworkCore;
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
    public class ServiceUsersRepository : BaseRepository<ServiceUsers, Guid>, IServiceUsersRepository
    {
        private readonly DataContext _dataContext;

        public ServiceUsersRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceUsers> GetUserByCode(string code)
        {
            var result = _dataContext.ServiceUsers.FirstOrDefault(u => u.AccessKey.Equals(code));
           
            return (ServiceUsers)result;
        }

        public void ActivatingUser(Guid id)
        {
            var result = _dataContext.ServiceUsers.FirstOrDefault(u => u.Id == id);
            result.Active = true;
            _dataContext.SaveChanges();
        }

        public void InactivatingUser(Guid id)
        {
            var result = _dataContext.ServiceUsers.FirstOrDefault(u => u.Id == id);
            result.Active = false;
            _dataContext.SaveChanges();
        }
    }
}
