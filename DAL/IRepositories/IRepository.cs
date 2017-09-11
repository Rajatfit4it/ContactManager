using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace DAL.IRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> Get(int id);
        Task<int> Add(T entity);
        Task<bool> Update(T entity);

        //Task<void> Delete(int id);
    }
}
