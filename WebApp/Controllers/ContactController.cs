using PagedList;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        // GET: Contact
        public async Task<ActionResult> Index(int? pageNumber)
        {
            if (TempData["SuccessMessage"] != null)
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();

            int PageNo = pageNumber ?? 1;

            var list = await _contactService.GetAll(PageNo, Common.RecordsPerPage);
            int TotalRecords = await _contactService.GetTotalRecordsCount();

            var pager = new PagingModel() { ActionName = "Index", ControllerName = "Contact", CurrentPage = PageNo, TotalRecords = TotalRecords, RecordsPerPage = Common.RecordsPerPage };
            ContactListViewModel model = new ContactListViewModel() {list = list, pager = pager };

            return View(model);
        }

        // GET: Contact/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ContactVM vm = new ContactVM();
            try
            {
                vm = await _contactService.Get(id);
                if (vm != null)
                    return View(vm);
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        public async Task<ActionResult> Create(ContactVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int Id = await _contactService.Add(vm);
                    if (Id > 0)
                    {
                        TempData["SuccessMessage"] = "Record Created Successfully!!!";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
            }
            ModelState.AddModelError("err", "Some error occured");
            return View(vm);
        }

        // GET: Contact/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ContactVM vm = new ContactVM();
            try
            {
                vm = await _contactService.Get(id);
                if (vm != null)
                    return View(vm);
            }
            catch
            {
                
            }
            return View(vm);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ContactVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsSuccess = await _contactService.Update(vm);
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
            ModelState.AddModelError("err", "Some error occured");
            return View(vm);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
