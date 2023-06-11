using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LeService.Api.ServiceLayer.Repository
{
    public interface IRepository<TEntity> where TEntity:class
    {
        Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<TEntity> GetSingle(Func<TEntity, bool> where);
        Task<bool> AddEnttities(params TEntity[] items);
        Task<bool> Update(params TEntity[] items);
        Task<bool> Delete(params TEntity[] items);
        Task<bool> AddEntity(TEntity model);
    }
}
