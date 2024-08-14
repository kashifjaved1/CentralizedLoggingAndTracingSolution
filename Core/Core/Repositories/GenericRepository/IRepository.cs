using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllOrderedAsync<TKey>(Expression<Func<T, bool>> filter = null, Expression<Func<T, TKey>> keySelector = null, bool orderByDescending = false);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

}
