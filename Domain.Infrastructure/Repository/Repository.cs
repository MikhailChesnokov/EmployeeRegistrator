namespace Domain.Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repository;
    using Entities;
    using Microsoft.EntityFrameworkCore;



    internal class EntityFrameworkCoreRepository<TEntity> : IRepository<TEntity>
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

        public IEnumerable<TEntity> All()
        {
            return _entities;
        }

        public TEntity FindById(int id)
        {
            return _entities.SingleOrDefault(x => x.Id == id);
        }
    }
}