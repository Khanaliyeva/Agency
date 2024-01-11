using Agency.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public Task<IQueryable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> expression = null,
            Expression<Func<TEntity, object>> OrderByExpression = null,
            bool isDescending = false,
            params string[] includes);

        DbSet<TEntity> Table { get; }
        Task SaveChangesAsync();        
        Task Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity> FindById(int id);
       
    }
}
