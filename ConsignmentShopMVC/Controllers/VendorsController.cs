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
using ConsignmentShopLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index(int? storeId)
        {
            if (storeId == null)
            {
                return NotFound();
            }

            if (await _storeData.LoadStore(storeId.Value) == null)
            {
                return NotFound();
            }

            var items = await _vendorData.LoadAllVendors((int)storeId);

            ViewBag.Id = storeId;
            ViewData["Store"] = (await _storeData.LoadStore(storeId.Value)).Name;

            return View(items);
        }

        // GET: VendorsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vendor = await _vendorData.LoadVendor(id);
            if (vendor == null)
            {
                return NotFound();
            }

            ViewData["Store"] = (await _storeData.LoadStore(vendor.StoreId)).Name;

            return View(vendor);
        }

        // GET: VendorsController/Create
        public async Task<IActionResult> Create(int? storeId)
        {
            var stores = await _storeData.LoadAllStores();

            if (stores == null || stores.Count < 1)
            {
                return RedirectToAction("ShowError", "Home", new { error = "No stores exist!" });
            }

            StoreModel selected = null;
            if (storeId != null)
            {
                //selected = await _storeData.LoadStore(storeId.Value);
                selected = stores.Where(s => s.Id == storeId).FirstOrDefault();
            }

            ViewBag.StoreId = storeId;
            ViewBag.StoreList = new SelectList(stores, "Id", "Name", selected?.Id);

            return View();
        }

        // POST: VendorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,FirstName,LastName,CommissionRate")] VendorModel vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _vendorData.CreateVendor(vendor);
                    return RedirectToAction(nameof(Index), new { storeId = vendor.StoreId });
                }
            }
            catch
            {
                return View(vendor);
            }

            return View(vendor);
        }

        // GET: VendorsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vendor = await _vendorData.LoadVendor(id);
            if(vendor == null)
            {
                return NotFound();
            }

            StoreModel store = await _storeData.LoadStore(vendor.StoreId);
            if (store == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Store"] = store.Name;
            }

            return View(vendor);
        }

        // POST: VendorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("FirstName, LastName, CommissionRate")] VendorModel vendor, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vendorDb = await _vendorData.LoadVendor(id);
                    if (vendorDb == null)
                    {
                        return NotFound();
                    }

                    vendorDb.FirstName = vendor.FirstName;
                    vendorDb.LastName = vendor.LastName;
                    vendorDb.CommissionRate = vendor.CommissionRate;

                    await _vendorData.UpdateVendor(vendorDb);

                    return RedirectToAction(nameof(Index), new { storeId = vendorDb.StoreId });
                }
            }
            catch
            {
                return View(vendor);
            }

            return View(vendor);
        }

        // GET: VendorsController/Delete/5
        public async Task<IActionResult> Delete(int id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var vendor = await _vendorData.LoadVendor(id);
                if(vendor == null)
                {
                    return NotFound();
                }

                await _vendorData.RemoveVendor(vendor);

                return RedirectToAction(nameof(Index), new { storeId = vendor.StoreId });
            }
            catch
            {
                return View();
            }
        }
    }
}
