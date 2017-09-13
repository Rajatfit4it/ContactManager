using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModel;

namespace ContactClient.Controllers
{
    public class ContactSPAController : ApiController
    {
        private string _ContactApiBaseUrl;
        public ContactSPAController()
        {
            _ContactApiBaseUrl = "http://localhost:59670/api/contact/";
        }
        // GET: api/ContactSPA
        public async Task<IHttpActionResult> Get()
        {
            List<ContactVM> list = new List<ContactVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    string url = _ContactApiBaseUrl;
                    var result = await client.GetAsync(url);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ContactVM>>(resultContent);
                    return Ok(list);
                }
                catch
                {
                }
                return NotFound();
            }
        }

        // GET: api/ContactSPA/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            ContactVM vm = new ContactVM();
            try
            {
                var client = new HttpClient();
                string url = _ContactApiBaseUrl + id;
                var result = await client.GetAsync(url);
                string resultContent = await result.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<ContactVM>(resultContent);
                if (vm != null)
                    return Ok(vm);
            }
            catch
            {
            }
            return NotFound();
        }

        // POST: api/ContactSPA
        [HttpPost]
        public async Task<IHttpActionResult> Post(ContactVM vmContact)
        {
            try
            {
                var client = new HttpClient();
                string url = _ContactApiBaseUrl;
                var content = JsonConvert.SerializeObject(vmContact);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(url, byteContent);

                string resultContent = await result.Content.ReadAsStringAsync();

                int Id = JsonConvert.DeserializeObject<int>(resultContent);
                return Ok(Id);
            }
            catch
            {

            }
            return BadRequest("Some error occured while creating record");
        }

        // PUT: api/ContactSPA/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(ContactVM vmContact)
        {
            try
            {
                var client = new HttpClient();
                string url = _ContactApiBaseUrl;
                var content = JsonConvert.SerializeObject(vmContact);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PutAsync(url, byteContent);

                string resultContent = await result.Content.ReadAsStringAsync();

                bool IsSuccess = JsonConvert.DeserializeObject<bool>(resultContent);
                if (IsSuccess)
                {
                    return Ok(IsSuccess);
                }
            }
            catch
            {
            }

            return NotFound();
        }

        // DELETE: api/ContactSPA/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var client = new HttpClient();
                string url = _ContactApiBaseUrl + id;
                var result = await client.DeleteAsync(url);
                string resultContent = await result.Content.ReadAsStringAsync();
                bool IsSuccess = JsonConvert.DeserializeObject<bool>(resultContent);
                if (IsSuccess)
                {
                    return Ok(IsSuccess);
                }
            }
            catch
            {
            }

            return NotFound();
        }
    }
}
