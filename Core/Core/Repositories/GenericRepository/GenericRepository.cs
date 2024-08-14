using Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositories.GenericRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ActivityDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ActivityDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllOrderedAsync<TKey>(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, TKey>> keySelector = null,
            bool orderByDescending = false)
        {
            if (keySelector is null && filter is not null)
            {
                return await _dbSet.Where(filter).ToListAsync();
            }

            if (filter is null && keySelector is not null)
            {
                if (orderByDescending)
                {
                    return await _dbSet.OrderByDescending(keySelector).ToListAsync();
                }

                return await _dbSet.OrderBy(keySelector).ToListAsync();
            }

            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
