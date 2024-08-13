using Core.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector = null)
        {
            if (keySelector is null)
            {
                return await _dbSet.Where(filter).ToListAsync();
            }

            return await _dbSet.Where(filter).OrderBy(keySelector).ToListAsync();
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
