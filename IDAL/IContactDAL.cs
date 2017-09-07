using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace IDAL
{
    public interface IContactDAL
    {
        Task<int> Add(ContactVM vmContact);

        Task<bool> Update(ContactVM vmContact);

        Task<List<ContactVM>> GetAllContacts();

        Task<List<ContactVM>> GetAllContacts(int pageno, int rows);

        Task<ContactVM> GetContactById(int Id);
    }
}

