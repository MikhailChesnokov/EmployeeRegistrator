namespace Domain.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities;



    public interface IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> All();

        IQueryable<TEntity> AllActive();

        IQueryable<TEntity> AllInclude<TProperty>(params Expression<Func<TEntity, TProperty>>[] expression);

        TEntity FindById(int id);
    }
}