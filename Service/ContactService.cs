using AutoMapper;
using DAL;
using DAL.DBModel;
using DAL.IRepositories;
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
        private IRepository<Contact> _repository;
        private IContactRepository _contactRepository;

        public ContactService(IRepository<Contact> repository, IContactRepository contactrepository)
        {
            _repository = repository;
            _contactRepository = contactrepository;
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
            /*question: Why automapper doesn't work here?*/
            contact.Name = vmContact.Name;
            contact.Email = vmContact.Email;
            contact.PhoneNo = vmContact.PhoneNo;
            return await _repository.Update(contact);

        }

        public async Task<IEnumerable<ContactVM>> GetAll()
        {
            List<ContactVM> list = new List<ContactVM>();
            var collection = _repository.GetAll();
            await Task.Run(() =>
            {
                foreach (var item in collection.OrderBy(e => e.Id))
                {
                    ContactVM vmContact = Mapper.Map<ContactVM>(item);
                    list.Add(vmContact);
                };
            });
            return list;
        }

        public async Task<IEnumerable<ContactVM>> GetAll(int pageno, int rows)
        {
            List<ContactVM> list = new List<ContactVM>();
            var collection = _repository.GetAll();
            await Task.Run(() =>
            {
                foreach (var item in collection.OrderBy(e => e.Id).Skip((pageno - 1) * rows).Take(rows))
                {
                    ContactVM vmContact = Mapper.Map<ContactVM>(item);
                    list.Add(vmContact);
                };
            });
            return list;
        }

        public async Task<int> GetTotalRecordsCount()
        {
            return await _contactRepository.GetTotalRecordsCount();
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
