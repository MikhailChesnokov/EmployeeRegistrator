namespace Domain.Infrastructure.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain.Repository;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;



    public class EntityFrameworkCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;



        public EntityFrameworkCoreRepository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }



        public void Add(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> All()
        {
            return _entities;
        }

        public IQueryable<TEntity> AllInclude<TProperty>(params Expression<Func<TEntity, TProperty>>[] expressions)
        {
            IIncludableQueryable<TEntity, TProperty> res = _entities.Include(expressions.First());

            foreach (Expression<Func<TEntity, TProperty>> expression in expressions.Skip(1))
            {
                res.Include(expression);
            }

            return res;
        }

        public IQueryable<TEntity> AllActive()
        {
            if (typeof(IRemovableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                return _entities
                       .Cast<IRemovableEntity>()
                       .Where(x => !x.IsDeleted())
                       .Cast<TEntity>();
            }

            return _entities;
        }

        public TEntity FindById(int id)
        {
            return _entities.SingleOrDefault(x => x.Id == id);
        }
    }
}