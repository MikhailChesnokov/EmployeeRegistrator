namespace Domain.Infrastructure.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain.Repository;
    using Entities;
    using Microsoft.EntityFrameworkCore;


    public sealed class EntityFrameworkCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;


        public EntityFrameworkCoreRepository(
            DbContext context)
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

        public IQueryable<TEntity> AllInclude<TProperty>(
            Expression<Func<TEntity, TProperty>> expression)
        {
            return _entities.Include(expression);
        }
        
        public IQueryable<TEntity> AllInclude<TProperty1, TProperty2>(
            Expression<Func<TEntity, TProperty1>> expression1,
            Expression<Func<TEntity, TProperty2>> expression2)
        {
            return _entities.Include(expression1).Include(expression2);
        }

        public IQueryable<TEntity> AllActive()
        {
            return
                typeof(IRemovableEntity).IsAssignableFrom(typeof(TEntity))
                    ? _entities.Where(x => !((IRemovableEntity) x).IsDeleted())
                    : _entities;
        }

        public IQueryable<TEntity> AllActiveInclude<TProperty>(
            Expression<Func<TEntity, TProperty>> expression)
        {
            var includedEntities = AllInclude(expression);

            return
                typeof(IRemovableEntity).IsAssignableFrom(typeof(TEntity))
                    ? includedEntities.Where(x => !((IRemovableEntity) x).IsDeleted())
                    : includedEntities;
        }

        public TEntity FindById(int id)
        {
            return
                typeof(IRemovableEntity).IsAssignableFrom(typeof(TEntity))
                    ? _entities.Where(x => !((IRemovableEntity) x).IsDeleted()).FirstOrDefault(x => x.Id == id)
                    : _entities.FirstOrDefault(x => x.Id == id);
        }

        
        public TEntity FindByIdInclude<TProperty>(int id, Expression<Func<TEntity, TProperty>> expression)
        {
            return AllActiveInclude(expression).FirstOrDefault(x => x.Id == id);
        }
    }
}