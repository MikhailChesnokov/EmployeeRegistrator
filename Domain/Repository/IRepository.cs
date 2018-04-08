namespace Domain.Repository
{
    using System.Collections.Generic;
    using Entities;



    public interface IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IEnumerable<TEntity> All();

        TEntity FindById(int id);
    }
}