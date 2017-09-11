using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(IDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }
        public async Task<T> Get(int id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
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

            ((DbContext)context).Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
