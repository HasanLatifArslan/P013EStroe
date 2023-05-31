using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStroe.Service.Abstract;

namespace P013EStroe.MVCUI.Areas.Admin.Controllers
{
	[Area("Admin"), Authorize(Policy = "AdminPolicy")]
	public class ContactsController : Controller
    {
        private readonly IService<Contact> _service;

        public ContactsController(IService<Contact> service)
        {
            _service = service;
        }

        // GET: ContactsController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync(); 
            return View(model);
        }

        // GET: ContactsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Contact collection)
        {
            try
            {
                _service.Update(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View();
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contact collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Contact collection)
        {
            try
            {
                _service.Delete(collection);
               await _service.SaveAsync();
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
