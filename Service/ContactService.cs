using AutoMapper;
using DAL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public class ContactService : IContactService
    {
        private ContactDB _dbContext;
        private IRepository<Contact> _repository;

        public ContactService(ContactDB dbContext,IRepository<Contact> repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }
        public async Task<int> Add(ContactVM vmContact)
        {
            if (vmContact == null)
                return 0;

            Contact dbContact = Mapper.Map<Contact>(vmContact);
            await _repository.Add(dbContact);
            return dbContact.Id;

        }

        public async Task<bool> Update(ContactVM vmContact)
        {
            var contact = await _repository.Get(vmContact.Id);
            if (contact == null)
                return false;
            contact.Name = vmContact.Name;
            contact.Email = vmContact.Email;
            contact.PhoneNo = vmContact.PhoneNo;
            return await _repository.Update(contact);

        }

        public async Task<IEnumerable<ContactVM>> GetAll()
        {
            List<ContactVM> list = new List<ContactVM>();
            var collection = await _repository.GetAll();
            foreach (var item in collection)
            {
                ContactVM vmContact = Mapper.Map<ContactVM>(item);
                list.Add(vmContact);
            };

            return list;
        }

        public async Task<IEnumerable<ContactVM>> GetAll(int pageno, int rows)
        {
            List<ContactVM> list = new List<ContactVM>();
            var collection = await _repository.GetAll();
            foreach (var item in collection.Skip((pageno - 1) * rows).Take(rows))
            {
                ContactVM vmContact = Mapper.Map<ContactVM>(item);
                list.Add(vmContact);
            };

            return list;
        }

        public async Task<ContactVM> Get(int Id)
        {
            var contact = await _repository.Get(Id);
            if (contact == null)
                return null;

            ContactVM vmContact = Mapper.Map<ContactVM>(contact);
            return vmContact;

        }
    }
}
