using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public interface IContactService
    {
        Task<int> Add(ContactVM vmContact);

        Task<bool> Update(ContactVM vmContact);

        Task<IEnumerable<ContactVM>> GetAll();

        Task<IEnumerable<ContactVM>> GetAll(int pageno, int rows);

        Task<ContactVM> Get(int Id);

        Task<int> GetTotalRecordsCount();

        Task<bool> Delete(int Id);
    }
}
