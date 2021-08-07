using ConsignmentShopLibrary.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.Controllers
{
    public class VendorsController : Controller
    {
        private readonly IVendorData _vendorData;
        private readonly IStoreData _storeData;

        public VendorsController(IVendorData vendorData,
            IStoreData storeData)
        {
            _vendorData = vendorData;
            _storeData = storeData;
        }

        // GET: VendorsController
        [HttpGet]
        [Route("Vendors/Index/{storeId:int}")]
        public async Task<IActionResult> Index(int? storeId)
        {
            if (storeId == null)
            {
                return NotFound();
            }

            if (await _storeData.LoadStore((int)storeId) == null)
            {
                return NotFound();
            }

            var items = await _vendorData.LoadAllVendors((int)storeId);

            return View(items);
        }

        // GET: VendorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VendorsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VendorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VendorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VendorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
