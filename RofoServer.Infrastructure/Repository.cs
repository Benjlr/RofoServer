using Microsoft.EntityFrameworkCore;
using RofoServer.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RofoServer.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _cxt;

        private DbSet<TEntity> _dbSet => _cxt.Set<TEntity>();
        public Repository(DbContext cxt) {
            _cxt = cxt;
        }

        public async Task<int> AddAsync(TEntity entity) {
            await _dbSet.AddAsync(entity);
            return await _cxt.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities) {
            await _dbSet.AddRangeAsync(entities);
            return await _cxt.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(int Id)=>
            await _dbSet.FindAsync(Id).AsTask();

        public async Task<List<TEntity>> GetAllAsync()=>
            await _dbSet.ToListAsync();
        

        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<int> RemoveAsync(TEntity entity) {
            _dbSet.Remove(entity);
            return await _cxt.SaveChangesAsync();
        }

        public async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities) {
            _dbSet.RemoveRange(entities);
            return await _cxt.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity) {
            _dbSet.Update(entity);
            return await _cxt.SaveChangesAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.SingleOrDefaultAsync(predicate);

    }
}
