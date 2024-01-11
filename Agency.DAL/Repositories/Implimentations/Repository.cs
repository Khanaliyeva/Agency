using Agency.Core.Entities.Common;
using Agency.DAL.Context;
using Agency.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repositories.Implimentations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<TEntity> Table => _context.Set<TEntity>();


        public async Task<IQueryable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> expression = null,
            Expression<Func<TEntity, object>> OrderByExpression = null,
            bool isDescending = false,
            params string[] includes)
        {
            IQueryable<TEntity> query = Table;
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if (OrderByExpression != null)
            {
                query = isDescending ? query.OrderByDescending(OrderByExpression) :
                    query.OrderBy(OrderByExpression);
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            return query;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Create(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
        }


        public async Task<TEntity> FindById(int id)
        {
            return await Table.FirstOrDefaultAsync(e => e.Id == id);

        }

    }
}
