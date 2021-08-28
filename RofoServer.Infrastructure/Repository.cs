using RofoServer.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RofoServer.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly Microsoft.EntityFrameworkCore.DbContext Cxt;
        private Microsoft.EntityFrameworkCore.DbSet<TEntity> _dbSet => Cxt.Set<TEntity>();
        public Repository(Microsoft.EntityFrameworkCore.DbContext cxt){
            Cxt = cxt;
        }

        public void Add(TEntity entity) =>
            _dbSet.Add(entity);

        public void AddRange(IEnumerable<TEntity> entities) =>
            _dbSet.AddRange(entities);

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) =>
            _dbSet.Where(predicate);

        public Task<TEntity> Get(int Id)=>
            _dbSet.FindAsync(Id).AsTask();

        public Task<List<TEntity>> GetAll()=>
            _dbSet.ToListAsync();

        public void Remove(TEntity entity)=>
            _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) =>
            _dbSet.RemoveRange(entities);

        public Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)=>
            _dbSet.SingleOrDefaultAsync(predicate);
        
    }
}
