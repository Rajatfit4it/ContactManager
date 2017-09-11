using DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContactDBInitializer : CreateDatabaseIfNotExists<ContactDB>
    {
        protected override void Seed(ContactDB context)
        {
            IList<Contact> defaultContacts = new List<Contact>();

            defaultContacts.Add(new Contact() { Name = "Default 1", Email = "default1@default.com", PhoneNo = "53456464" });
            defaultContacts.Add(new Contact() { Name = "Default 2", Email = "default2@default.com", PhoneNo = "53456464" });
            defaultContacts.Add(new Contact() { Name = "Default 3", Email = "default3@default.com", PhoneNo = "53456464" });

            foreach (Contact contact in defaultContacts)
                context.Contacts.Add(contact);

            base.Seed(context);
        }
    }
}
