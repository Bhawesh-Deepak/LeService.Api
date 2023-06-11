using LeService.Api.DataLayer;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LeService.Api.ServiceLayer.Implementation
{
    public class GenericImplementation<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private LeContext baseContext = null;
        private DbSet<TEntity> TEntities = null;

        public GenericImplementation()
        {
            baseContext = new LeContext();
            TEntities = baseContext.Set<TEntity>();
        }
        public async Task<bool> AddEntity(TEntity model)
        {
            try
            {
                await TEntities.AddAsync(model);
                await baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await Task.Run(() => throw new Exception(ex.Message, ex));
                return false;
            }
        }

        public async Task<bool> AddEnttities(params TEntity[] items)
        {
            try
            {
                await baseContext.AddRangeAsync(items);
                await baseContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                await Task.Run(() => throw new Exception(ex.Message, ex));
                return false;
            }
        }

        public async Task<bool> Delete(params TEntity[] items)
        {
            try
            {
                using (baseContext)
                {
                    baseContext.UpdateRange(items);
                    await baseContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                await Task.Run(() => throw new Exception(ex.Message, ex));
                return false;
            }
        }

        public async Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list = new List<TEntity>();
            try
            {
                IQueryable<TEntity> dbQuery = baseContext.Set<TEntity>();
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);
                }
                list = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();
                return await Task.Run(() => list);
            }
            catch (Exception ex)
            {
                await Task.Run(() => throw new Exception(ex.Message, ex));
                return await Task.Run(() => list);
            }
        }

        public async Task<TEntity> GetSingle(Func<TEntity, bool> where)
        {
            try {
                TEntity item = null;
                IQueryable<TEntity> dbQuery = baseContext.Set<TEntity>();

                item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(where);
                return await Task.Run(() => item);
            }

            catch (Exception ex)
            {
                await Task.Run(() => throw new Exception(ex.Message, ex));
                return null;
            }

        }

        public async Task<bool> Update(params TEntity[] items)
        {
            try
            {
                using (baseContext)
                {
                    baseContext.UpdateRange(items);
                    await baseContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                await Task.Run(() => throw new Exception(ex.Message, ex));
                return false;
            }
        }
    }
}
