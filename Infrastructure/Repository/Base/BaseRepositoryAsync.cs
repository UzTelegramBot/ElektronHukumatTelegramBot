using Infrastructure.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbset;

        public BaseRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            return entity;
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            IReadOnlyList<T> entities =  await _dbset.AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            await _dbset.AsNoTracking().FirstOrDefaultAsync(expression);

        public virtual Task<T> FindByCondition(Expression<Func<T,bool>> expression,List<string> tables)
        {
            var entity = _dbset.AsNoTracking();
            foreach(var table in tables)
            {
                entity = entity.Include(table);
            }
            return entity.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<IReadOnlyList<T>> GetPageListAsync(int page, int pageSize)
        {
            IReadOnlyList<T> entities = await _dbset.AsNoTracking()
                .Skip((page-1)*pageSize)
                .Take(pageSize).ToListAsync();
            return entities;
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(Guid Id)
        {
            var entity = _dbset.Find(Id);
            if(entity != null)
               _dbset.Remove(entity);
        }

    }
}
