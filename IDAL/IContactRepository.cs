using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace IDAL
{
    public interface IContactRepository : IDisposable
    {
        Task<int> Add(ContactVM vmContact);

        Task<bool> Update(ContactVM vmContact);

        Task<IEnumerable<ContactVM>> GetAll();

        Task<IEnumerable<ContactVM>> GetAll(int pageno, int rows);

        Task<ContactVM> Get(int Id);
    }
}

