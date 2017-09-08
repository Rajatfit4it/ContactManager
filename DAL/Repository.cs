using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ContactDB context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(ContactDB context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.Run(() => entities.AsEnumerable());
        }
        public async Task<T> Get(int id)
        {
            return await Task.Run(() => entities.SingleOrDefault(s => s.Id == id));
        }
        public async Task<int> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<bool> Update(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
        //public void Delete(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }
        //    entities.Remove(entity);
        //    context.SaveChanges();
        //}
    }
}
