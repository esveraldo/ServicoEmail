using Dapper;
using Microsoft.Data.SqlClient;
using ServicoDeEmail.Domain.Entities;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using ServicoDeEmail.Infraestructure.Data.Contexts;

namespace ServicoDeEmail.Infraestructure.Data.Repositories
{
    public class EmailRepository : BaseRepository<Email, Guid>, IEmailRepository
    {
        private readonly DataContext _dataContext;

        public EmailRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public void UpdateEmail(Guid id, string status)
        {
            var result = _dataContext.Emails.Where(e => e.Id.Equals(id)).FirstOrDefault();
            result.Status = status;
            if (result != null)
            {
                _dataContext.Attach(result).Property(e => e.Status).IsModified = true;
                _dataContext.SaveChanges();
            }
        }
    }
}
