using Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IBaseRepositoryAsync<T> where T : class
    {
        Task<T> FindByCondition(Expression<Func<T, bool>> expression, List<string> tables);
        Task<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetPageListAsync(int page, int pageSize);
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        void Delete(Guid Id);

    }
}
