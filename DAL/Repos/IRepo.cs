using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    /// <summary>
    /// Base repo interface
    /// </summary>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetFromIdAsync<TProp>(int id, params Expression<Func<T, TProp>>[] includes) where TProp : class;

        Task<T> AddAsync(T newItem);

        void AddRange(List<T> newItem);

        Task<T> EditAsync(T item);

        Task RemoveAsync(T item);

        Task RemoveWithIdAsync(int id);

        Task<List<T>> GetAllAsync<TProp>(Expression<Func<T, bool>> func = null, params Expression<Func<T, TProp>>[] includes);

        IQueryable<T> GetAllQuerable<TProp>(Expression<Func<T, bool>> func = null,
            params Expression<Func<T, TProp>>[] includes);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly MySqlDbContext db;

       
        public BaseRepository(MySqlDbContext dbContext)
        {
            db = dbContext;
        }

        /// <summary>
        /// Get item from Id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Item if found or null</returns>
        public async Task<T> GetFromIdAsync<TProp>(int id, params Expression<Func<T, TProp>>[] includes) where TProp : class
        {
            try
            {
                var res = db.Set<T>().AsQueryable();
                res = includes.Aggregate(res, (current, include) => current.Include(include));
                return await res.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert item to collection
        /// </summary>
        /// <param name="newItem">New Item</param>
        /// <returns>New Item with new id</returns>
        public async Task<T> AddAsync(T newItem)
        {
            try
            {
                await db.AddAsync(newItem);
                Save();
                return newItem;
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="item">Updated item</param>
        /// <returns>Updated item</returns>
        public async Task<T> EditAsync(T item)
        {
            try
            {
                var ObjectNotNulAndHasId = item?.GetType().GetProperties().FirstOrDefault(x => x.Name == "Id");
                if (ObjectNotNulAndHasId != null)
                {
                    db.Update(item);
                    await SaveAsync();
                    return item;
                }
                throw new ArgumentException($"item is null or does not have id identifier");


            }
            catch (Exception ex)
            {
                //TODO тут обработчик, фиксация в логах или еще где
                throw;
            }
        }


        /// <summary>
        /// Remove Item
        /// </summary>
        /// <param name="item">Item that will be removed</param>
        /// <returns></returns>
        public async Task RemoveAsync(T item)
        {
            try
            {
                db.Remove(item);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove Item with Id
        /// </summary>
        /// <param name="id">Identifier of removing item</param>
        /// <returns></returns>
        public async Task RemoveWithIdAsync(int id)
        {
            try
            {
                var item = await GetFromIdAsync<object>(id);
                if (item != null)
                {
                    db.Remove(item);
                    await SaveAsync();
                }
                else
                {
                    throw new ArgumentException($"Объект типа {typeof(T).Name} с идентификатором {id} не найден для удаления");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// List Items
        /// </summary>
        /// <param name="func">Сonditions for query if null then all items</param>
        /// <param name="includes">Properties for lazy loading</param>
        /// <returns>Collection Items</returns>
        public async Task<List<T>> GetAllAsync<TProp>(Expression<Func<T, bool>> func = null, params Expression<Func<T, TProp>>[] includes)
        {

            var res = db.Set<T>().AsQueryable();
            if (includes != null)
            {
                res = includes.Aggregate(res, (current, include) => current.Include(include));
            }
            var result = func == null ? res.ToListAsync() : res.Where(func).ToListAsync();
            return await result;
        }

        /// <summary>
        /// List Items
        /// </summary>
        /// <param name="func">Сonditions for query if null then all items</param>
        /// <param name="includes">Properties for lazy loading</param>
        /// <returns>Queryable Collection items</returns>
        public IQueryable<T> GetAllQuerable<TProp>(Expression<Func<T, bool>> func = null, params Expression<Func<T, TProp>>[] includes)
        {

            var res = db.Set<T>().AsQueryable();
            if (includes != null)
            {
                res = includes.Aggregate(res, (current, include) => current.Include(include));
            }
            var result = func == null ? res : res.Where(func);
            return result;
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        private async Task SaveAsync()
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        private void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }

        /// <summary>
        /// Insert collection items
        /// </summary>
        /// <param name="newItem">Collection Items</param>
        public void AddRange(List<T> newItem)
        {
            newItem.ForEach(i =>
            {
                db.Add(i);
            });
            Save();
        }
    }
}
