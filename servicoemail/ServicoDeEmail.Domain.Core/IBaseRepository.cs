namespace ServicoDeEmail.Domain.Core
{
    public interface IBaseRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        List<TEntity> GetAll();
        TEntity GetById(TKey id);
    }
}
