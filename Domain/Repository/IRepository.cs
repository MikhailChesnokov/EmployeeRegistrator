namespace Domain.Repository
{
    using System.Collections.Generic;
    using Entities;



    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        void Add(TEntity entity);

        IEnumerable<TEntity> All();

        TEntity FindById(int id);
    }
}