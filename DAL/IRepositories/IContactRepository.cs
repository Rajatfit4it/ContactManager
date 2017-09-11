using DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IContactRepository
    {
        Task<int> GetTotalRecordsCount();
    }
}
