namespace Domain.Infrastructure.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain.Repository;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;


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

        public IQueryable<TEntity> AllInclude<TProperty>(params Expression<Func<TEntity, TProperty>>[] expressions)
        {
            IIncludableQueryable<TEntity, TProperty> res = _entities.Include(expressions.First());

            expressions.Skip(1).ToList().ForEach(x => res.Include(x));

            return res;
        }

        public IQueryable<TEntity> AllActive()
        {
            return
                typeof(IRemovableEntity).IsAssignableFrom(typeof(TEntity))
                    ? _entities.Where(x => !((IRemovableEntity) x).IsDeleted())
                    : _entities;
        }

        public IQueryable<TEntity> AllActiveInclude<TProperty>(
            params Expression<Func<TEntity, TProperty>>[] expressions)
        {
            var includedEntities = AllInclude(expressions);

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

        
        public TEntity FindByIdInclude<TProperty>(int id, params Expression<Func<TEntity, TProperty>>[] expressions)
        {
            return AllActiveInclude(expressions).FirstOrDefault(x => x.Id == id);
        }
    }
}