namespace Domain.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities;



    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> All();
        
        IQueryable<TEntity> AllInclude<TProperty>(params Expression<Func<TEntity, TProperty>>[] expression);

        IQueryable<TEntity> AllActive();

        IQueryable<TEntity> AllActiveInclude<TProperty>(params Expression<Func<TEntity, TProperty>>[] expressions);

        TEntity FindById(int id);
        
        TEntity FindByIdInclude<TProperty>(int id, params Expression<Func<TEntity, TProperty>>[] expressions);
    }
}