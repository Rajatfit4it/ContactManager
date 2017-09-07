using AutoMapper;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace DAL
{
    public class ContactDAL : IContactDAL
    {
        public async Task<int> Add(ContactVM vmContact)
        {
            if (vmContact == null)
                return 0;

            Contact dbContact = Mapper.Map<Contact>(vmContact);

            using (var dbEntity = new ContactDB())
            {
                dbEntity.Contacts.Add(dbContact);
                await dbEntity.SaveChangesAsync();
            }
            return dbContact.Id;

        }

        public async Task<bool> Update(ContactVM vmContact)
        {
            using (var dbEntity = new ContactDB())
            {
                var contact = dbEntity.Contacts.Where(e => e.Id == vmContact.Id).FirstOrDefault();
                if (contact == null)
                    return false;

                contact.Name = vmContact.Name;
                contact.PhoneNo = vmContact.PhoneNo;
                await dbEntity.SaveChangesAsync();
                return true;
            }

        }

        public async Task<List<ContactVM>> GetAllContacts()
        {
            List<ContactVM> list = new List<ContactVM>();
            using (var dbEntity = new ContactDB())
            {
                await Task.Run(() =>
                {
                    foreach (var item in dbEntity.Contacts)
                    {
                        ContactVM vmContact = Mapper.Map<ContactVM>(item);
                        list.Add(vmContact);
                    };
                });
            }

            return list;
        }

        public async Task<List<ContactVM>> GetAllContacts(int pageno, int rows)
        {
            List<ContactVM> list = new List<ContactVM>();
            using (var dbEntity = new ContactDB())
            {
                await Task.Run(() =>
                {
                    foreach (var item in dbEntity.Contacts.Skip((pageno - 1) * rows).Take(rows))
                    {
                        ContactVM vmContact = Mapper.Map<ContactVM>(item);
                        list.Add(vmContact);
                    };
                });
            }

            return list;
        }

        public async Task<ContactVM> GetContactById(int Id)
        {
            using (var dbEntity = new ContactDB())
            {
                var contact = await Task.Run(() => dbEntity.Contacts.Where(e => e.Id == Id).FirstOrDefault());
                if (contact == null)
                    return null;

                ContactVM vmContact = Mapper.Map<ContactVM>(contact);
                return vmContact;

            }
        }

    }
}
