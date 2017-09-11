using DAL.DBModel;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private IDbContext _context;
        private DbSet<Contact> entities;

        public ContactRepository(IDbContext context)
        {
            _context = context;
            entities = _context.Set<Contact>();
        }

        public async Task<int> GetTotalRecordsCount()
        {
            return await entities.CountAsync();
        }
    }
}
