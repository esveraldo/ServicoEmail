using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Infraestructure.Data.Contexts;

namespace ServicoDeEmail.Infraestructure.Data.Repositories
{
    public class AuxSendMailRepository
    {
        private readonly IConfiguration _configuration;

        public AuxSendMailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<ServiceUsers>> GetUserConsumerByCode(string code)
        {
            string connection = _configuration.GetSection("ConnectionStrings").GetSection("BDServicoDeEmail").Value;

            using (var context = new HelperDbContext(connection))
            {
                var result = context.ServiceUsers.Include(c => c.ConsumerUsers).Where(u => u.AccessKey.Equals(code));
            return result.ToList();
            }
        }
    }
}
