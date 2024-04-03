namespace ServicoDeEmail.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();

        IEmailRepository emailRepository { get; }
        IServiceUsersRepository serviceRepository { get; }
        IConsumerUsersRepository consumerRepository { get; }
    }
}
