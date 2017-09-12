using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModel;

namespace ContactAPI.Controllers
{
    public class ContactController : ApiController
    {
        private IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        // GET: api/Contact
        public async Task<IHttpActionResult> Get()
        {
            var list = await _contactService.GetAll();
            if (list != null)
                return Ok(list.ToList());

            return NotFound();
        }

        // GET: api/Contact/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Contact
        public void Post(ContactVM contactVM)
        {

        }

        // PUT: api/Contact/5
        public void Put(int id, ContactVM contactVM)
        {
        }

        // DELETE: api/Contact/5
        public void Delete(int id)
        {
        }
    }
}
