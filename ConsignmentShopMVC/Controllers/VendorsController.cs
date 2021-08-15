/*
MIT License

Copyright(c) 2020 Kyle Givler
https://github.com/JoyfulReaper

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using ConsignmentShopLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.Controllers
{
    [Authorize]
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
        public async Task<ActionResult> Details(int id)
        {
            var vendor = await _vendorData.LoadVendor(id);
            if(vendor == null)
            {
                return NotFound();
            }

            ViewData["Store"] = (await _storeData.LoadStore(vendor.StoreId)).Name;

            return View(vendor);
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
        public async Task<ActionResult> Delete(int id)
        {
            var vendor = await _vendorData.LoadVendor(id);

            if(vendor == null)
            {
                return NotFound();
            }

            ViewData["Store"] = (await _storeData.LoadStore(vendor.StoreId)).Name;

            return View(vendor);
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
