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
        
        IQueryable<TEntity> AllInclude<TProperty>(Expression<Func<TEntity, TProperty>> expression);
        
        IQueryable<TEntity> AllInclude<TProperty1, TProperty2>(Expression<Func<TEntity, TProperty1>> expression1, Expression<Func<TEntity, TProperty2>> expression2);

        IQueryable<TEntity> AllActive();

        IQueryable<TEntity> AllActiveInclude<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        TEntity FindById(int id);
        
        TEntity FindByIdInclude<TProperty>(int id, Expression<Func<TEntity, TProperty>> expression);
    }
}