using Microsoft.EntityFrameworkCore;
using ServicoDeEmail.Domain.Entities;

namespace ServicoDeEmail.Infraestructure.Data.Contexts
{
    public class HelperDbContext : DbContext
    {
        private string _connection;
        public HelperDbContext(string connection)
        {

            _connection = connection;

        }

        public DbSet<ServiceUsers> ServiceUsers { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)

            => options.UseSqlServer(_connection);
            
    }
}
