using ContactAPI.Infrastructure;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ViewModel;

namespace ContactAPI.Controllers
{
    //[AllowCORS]
    public class ContactController : ApiController
    {
        private IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        // GET: api/Contact
        /// <summary>
        /// This method will get all contacts.
        /// </summary>
        ///  <response code="403">Authentication Failed</response>
        ///  <response code="429">Too Many Requests</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="200">Company Data</response>
        /// <response code="400">Bad request</response>
        ///  <response code="403">Authentication Failed</response>
        ///  <response code="429">Too Many Requests</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ResponseType(typeof(List<ContactVM>))]
        public async Task<IHttpActionResult> Get()
        {
            var list = await _contactService.GetAll();
            if (list != null)
                return Ok(list.ToList());

            return NotFound();
        }

        // GET: api/Contact/5
        /// <summary>
        /// This method will get all contact with specified id.
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(ContactVM))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var contact = await _contactService.Get(id);
            if (contact != null)
                return Ok(contact);

            return NotFound();
        }

        // POST: api/Contact
        /// <summary>
        /// This method will add contact.
        /// </summary>
        [HttpPost]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post(ContactVM vmContact)
        {
            var Id = await _contactService.Add(vmContact);
            if (Id > 0)
                return Ok(Id);

            return BadRequest("Some error occured while creating record");
        }

        // PUT: api/Contact/5
        /// <summary>
        /// This method will update contact.
        /// </summary>
        [HttpPut]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put(ContactVM vmContact)
        {
            bool IsSuccess = await _contactService.Update(vmContact);
            if (IsSuccess)
                return Ok(IsSuccess);

            return NotFound();
        }

        // DELETE: api/Contact/5
        /// <summary>
        /// This method will delete contact.
        /// </summary>
        [HttpDelete]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var IsSuccess = await _contactService.Delete(id);
            if (IsSuccess)
                return Ok(IsSuccess);

            return NotFound();
        }
    }
}
