using Microsoft.EntityFrameworkCore;
using ServicoDeEmail.Domain.Core;
using ServicoDeEmail.Infraestructure.Data.Contexts;

namespace ServicoDeEmail.Infraestructure.Data.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(TEntity entity)
        {
            //_dataContext.Entry(entity).State = EntityState.Added;
            _dataContext.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Deleted;
        }

        public List<TEntity> GetAll()
        {
            return _dataContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(TKey id)
        {
            return _dataContext.Set<TEntity>().Find(id);
        }

        public void Dispose()
        {
            _dataContext?.Dispose();
        }
    }
}
