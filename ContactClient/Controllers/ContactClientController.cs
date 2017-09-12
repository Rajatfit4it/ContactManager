using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ContactClient.Controllers
{
    public class ContactClientController : Controller
    {
        private string _ContactApiBaseUrl;
        public ContactClientController()
        {
            _ContactApiBaseUrl = "http://localhost:59670/api/contact/";
        }
        // GET: ContactClient
        public async Task<ActionResult> Index()
        {
            if (TempData["SuccessMessage"] != null)
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();

            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            List<ContactVM> list = new List<ContactVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    string url = _ContactApiBaseUrl;
                    var result = await client.GetAsync(url);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ContactVM>>(resultContent);

                }
                catch
                {
                    ViewBag.ErrorMessage = "Some Error Occured";
                }
            }
            return View(list);
        }

        // GET: ContactClient/Details/5
        public async Task<ActionResult> Details(int id)
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
                    return View(vm);
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }

        // GET: ContactClient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactClient/Create
        [HttpPost]
        public async Task<ActionResult> Create(ContactVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new HttpClient();
                    string url = _ContactApiBaseUrl;
                    var content = JsonConvert.SerializeObject(vm);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var result = await client.PostAsync(url, byteContent);

                    string resultContent = await result.Content.ReadAsStringAsync();

                    int Id = JsonConvert.DeserializeObject<int>(resultContent);
                    if (Id > 0)
                    {
                        TempData["SuccessMessage"] = "Record Created Successfully!!!";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "There was some error while creating the record.";
            }
            return View(vm);
        }

        // GET: ContactClient/Edit/5
        public async Task<ActionResult> Edit(int id)
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
                    return View(vm);
            }
            catch
            {

            }
            TempData["ErrorMessage"] = "No Record Found!!!";
            return RedirectToAction("Index");
        }

        // POST: ContactClient/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ContactVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new HttpClient();
                    string url = _ContactApiBaseUrl;
                    var content = JsonConvert.SerializeObject(vm);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var result = await client.PutAsync(url, byteContent);

                    string resultContent = await result.Content.ReadAsStringAsync();

                    bool IsSuccess = JsonConvert.DeserializeObject<bool>(resultContent);
                    if (IsSuccess)
                    {
                        TempData["SuccessMessage"] = "Record Updated Successfully!!!";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "There was some error while updating the record.";
            }
            return View(vm);
        }

        // GET: ContactClient/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            ContactVM vm = new ContactVM();
            try
            {
                var client = new HttpClient();
                string url = _ContactApiBaseUrl + id;
                var result = await client.GetAsync(url);
                string resultContent = await result.Content.ReadAsStringAsync();
                vm = JsonConvert.DeserializeObject<ContactVM>(resultContent);

                if (vm != null)
                    return View(vm);
            }
            catch
            {

            }
            TempData["ErrorMessage"] = "No Record Found!!!";
            return RedirectToAction("Index");
        }

        // POST: ContactClient/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteConfirm(int id)
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
                    TempData["SuccessMessage"] = "Record deleted successfully!!!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            TempData["ErrorMessage"] = "Some error occured.";
            return RedirectToAction("Delete", new { id = id });
        }
    }
}
