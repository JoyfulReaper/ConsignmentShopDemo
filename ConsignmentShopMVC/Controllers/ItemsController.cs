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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemData _itemData;
        private readonly IStoreData _storeData;
        private readonly IVendorData _vendorData;

        public ItemsController(IItemData itemData,
            IStoreData storeData,
            IVendorData vendorData)
        {
            _itemData = itemData;
            _storeData = storeData;
            _vendorData = vendorData;
        }

        // GET: ItemsController
        [HttpGet]
        public async Task<IActionResult> Index(int? storeId)
        {
            if(storeId == null)
            {
                return NotFound();
            }

            var store = await _storeData.LoadStore((int)storeId);
            if (store == null)
            {
                return NotFound();
            }

            var items = await _itemData.LoadAllItems((int)storeId);

            ViewBag.StoreId = storeId;
            ViewData["Store"] = store.Name;

            return View(items);
        }

        // GET: ItemsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemData.LoadItem(id);
            if(item == null)
            {
                return NotFound();
            }

            var store = await _storeData.LoadStore(item.StoreId);
            if(store == null)
            {
                return NotFound();
            }

            var owner = await _vendorData.LoadVendor(item.OwnerId);
            if (owner == null)
            {
                return NotFound();
            }

            ViewData["Store"] = store.Name;
            ViewData["Owner"] = owner.Display;

            return View(item);
        }

        // GET: ItemsController/Create
        public async Task<IActionResult> Create(int? storeId)
        {
            var stores = await _storeData.LoadAllStores();

            StoreModel selected = null;
            List<VendorModel> vendors = null;

            if(storeId != null)
            {
                //selected = await _storeData.LoadStore(storeId.Value);
                selected = stores.Where(s => s.Id == storeId).FirstOrDefault();

                vendors = await _vendorData.LoadAllVendors(storeId.Value);
                if(vendors == null || vendors.Count < 1)
                {
                    return RedirectToAction("ShowError", "Home", new { error = $"No vendors exist" });
                }
            }

            ViewBag.StoreId = storeId;
            ViewBag.StoreList = new SelectList(stores, "Id", "Name", selected?.Id);
            ViewBag.VendorList = new SelectList(vendors, "Id", "Display");

            return View();
        }

        // POST: ItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,Name,Description,Price,OwnerId")] ItemModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item.Sold = false;
                    item.PaymentDistributed = false;

                    await _itemData.CreateItem(item);

                    return RedirectToAction(nameof(Index), new { storeId = item.StoreId });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"An exception occured: {e.Message}");
                return View(item);
            }

            return View(item);
        }

        // GET: ItemsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _itemData.LoadItem(id);

            if(item == null)
            {
                return NotFound();
            }

            StoreModel store = await _storeData.LoadStore(item.StoreId);
            if (store == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Store"] = store.Name;
                ViewData["Item"] = item.Name;
            }

            var vendors = await _vendorData.LoadAllVendors(store.Id);
            var selected = await _vendorData.LoadVendor(item.OwnerId);
            ViewBag.VendorId = new SelectList(vendors, "Id", "Display", selected?.Id);

            return View(item);
        }

        // POST: ItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit(int id, [Bind("")] ItemModel item)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var itemDb = await _itemData.LoadItem(id);
                    if(itemDb == null)
                    {
                        return NotFound();
                    }

                    itemDb.Name = item.Name;
                    itemDb.Description = item.Description;
                    itemDb.Price = item.Price;
                    itemDb.Sold = item.Sold;
                    itemDb.PaymentDistributed = item.PaymentDistributed;
                    itemDb.Owner = item.Owner;

                    await _itemData.UpdateItem(itemDb);

                    return RedirectToAction(nameof(Index), new { storeId = itemDb.StoreId });
                }
            }
            catch
            {
                return View(item);
            }

            return View(item);
        }

        // GET: ItemsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemData.LoadItem(id);
            if(item == null)
            {
                return NotFound();
            }

            var store = await _storeData.LoadStore(item.StoreId);
            if (store == null)
            {
                return NotFound();
            }

            var owner = await _vendorData.LoadVendor(item.OwnerId);
            if (owner == null)
            {
                return NotFound();
            }

            ViewData["Store"] = store.Name;
            ViewData["Owner"] = owner.Display;

            return View(item);
        }

        // POST: ItemsController/Delete/5
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
