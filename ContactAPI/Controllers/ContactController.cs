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
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var list = await _contactService.GetAll();
            if (list != null)
                return Ok(list.ToList());

            return NotFound();
        }

        // GET: api/Contact/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var contact = await _contactService.Get(id);
            if (contact != null)
                return Ok(contact);

            return NotFound();
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<IHttpActionResult> Post(ContactVM vmContact)
        {
            var Id = await _contactService.Add(vmContact);
            if (Id > 0)
                return Ok(Id);

            return BadRequest("Some error occured while creating record");
        }

        // PUT: api/Contact/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(ContactVM vmContact)
        {
            bool IsSuccess = await _contactService.Update(vmContact);
            if (IsSuccess)
                return Ok("Record updated successfully!!!");

            return NotFound();
        }

        // DELETE: api/Contact/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var IsSuccess = await _contactService.Delete(id);
            if (IsSuccess)
                return Ok("Record deleted successfully");

            return NotFound();
        }
    }
}
