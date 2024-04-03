using Microsoft.EntityFrameworkCore.Storage;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using ServicoDeEmail.Infraestructure.Data.Contexts;

namespace ServicoDeEmail.Infraestructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IDbContextTransaction _dbContextTransaction;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _dataContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction?.Rollback();
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public IEmailRepository emailRepository => new EmailRepository(_dataContext);
        public IServiceUsersRepository serviceRepository => new ServiceUsersRepository(_dataContext);
        public IConsumerUsersRepository consumerRepository => new ConsumerUsersRepository(_dataContext);

        public void Dispose()
        {
            _dataContext?.Dispose();
        }
    }
}
