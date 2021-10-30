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

using AutoMapper;
using ConsignmentShopLibrary.Data;
using ConsignmentShopLibrary.Models;
using ConsignmentShopMVC.ViewModels;
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
        private readonly IMapper _mapper;

        public ItemsController(IItemData itemData,
            IStoreData storeData,
            IVendorData vendorData,
            IMapper mapper)
        {
            _itemData = itemData;
            _storeData = storeData;
            _vendorData = vendorData;
            _mapper = mapper;
        }

        // GET: ItemsController
        [HttpGet]
        public async Task<IActionResult> Index(int? storeId)
        {
            if(storeId == null)
            {
                return NotFound();
            }

            var store = _mapper.Map<StoreViewModel>(await _storeData.LoadStore((int)storeId));

            if (store == null)
            {
                return NotFound();
            }

            var items = _mapper.Map<List<ItemModel>, IEnumerable<ItemViewModel>>(await _itemData.LoadAllItems((int)storeId));

            ViewBag.StoreId = storeId;
            ViewData["Store"] = store.Name;

            return View(items);
        }

        // GET: ItemsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = _mapper.Map<ItemModel, ItemViewModel>(await _itemData.LoadItem(id));
            if(item == null)
            {
                return NotFound();
            }

            var store = _mapper.Map<StoreModel, StoreViewModel>( await _storeData.LoadStore(item.StoreId));
            if(store == null)
            {
                return NotFound();
            }

            var owner = _mapper.Map<VendorModel, VendorViewModel>( await _vendorData.LoadVendor(item.OwnerId));
            if (owner == null)
            {
                return NotFound();
            }

            var vm = new ItemDetailsViewModel
            {
                Item = item,
                Store = store,
                Owner = owner
            };

            return View(vm);
        }

        // GET: ItemsController/Create
        public async Task<IActionResult> Create(int? storeId)
        {
            var stores = _mapper.Map<List<StoreModel>, IEnumerable<StoreViewModel>>(await _storeData.LoadAllStores());
            StoreViewModel selected = null;
            List<VendorViewModel> vendors = null;

            if(storeId != null)
            {
                selected = stores.Where(s => s.Id == storeId).FirstOrDefault();
                vendors = _mapper.Map<List<VendorModel>, List<VendorViewModel>>(await _vendorData.LoadAllVendors(storeId.Value));
                if(!vendors.Any())
                {
                    return RedirectToAction("ShowError", "Home", new { error = $"No vendors exist" });
                }
            }

            var vm = new ItemCreateViewModel
            {
                Stores = new SelectList(stores, "Id", "Name", selected?.Id),
                Vendors = new SelectList(vendors, "Id", "Display"),
                StoreId = selected.Id
            };

            return View(vm);
        }

        // POST: ItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCreateViewModel vmItem)
        {
            var vm = new ItemCreateViewModel
            {
                Item = vmItem.Item
            };

            try
            {
                if (ModelState.IsValid)
                {
                    vmItem.Item.Sold = false;
                    vmItem.Item.PaymentDistributed = false;

                    await _itemData.CreateItem(_mapper.Map<ItemModel>(vmItem.Item));

                    return RedirectToAction(nameof(Index), new { storeId = vmItem.Item.StoreId });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"An exception occured: {e.Message}");
                return View(vm);
            }

            return View(vm);
        }

        // GET: ItemsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = _mapper.Map<ItemViewModel>(await _itemData.LoadItem(id));

            if(item == null)
            {
                return NotFound();
            }

            StoreViewModel store = _mapper.Map<StoreViewModel>(await _storeData.LoadStore(item.StoreId));
            if (store == null)
            {
                return NotFound();
            }

            var vendors = _mapper.Map<IEnumerable<VendorViewModel>>(await _vendorData.LoadAllVendors(store.Id));
            var selected = _mapper.Map<VendorViewModel>(await _vendorData.LoadVendor(item.OwnerId));

            var vm = new ItemEditViewModel
            {
                StoreName = store.Name,
                Item = item,
                Vendors = new SelectList(vendors, "Id", "Display", selected?.Id)
        };

            return View(vm);
        }

        // POST: ItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit(int id, ItemViewModel item)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var itemDb = _mapper.Map<ItemViewModel>( await _itemData.LoadItem(id));
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

                    await _itemData.UpdateItem(_mapper.Map<ItemModel>(itemDb));

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var item = await _itemData.LoadItem(id);
                if(item == null)
                {
                    return NotFound();
                }

                if (item.Sold && !item.PaymentDistributed)
                {
                    ModelState.AddModelError("", $"{item.Owner.FullName} needs to be paid before the item can be deleted!");

                    ViewData["Store"] = (await _storeData.LoadStore(item.StoreId)).Name;
                    ViewData["Owner"] = item.Owner.Display;

                    return View(item);
                }

                await _itemData.RemoveItem(item);

                return RedirectToAction("Index", new { storeId = item.StoreId });
            }
            catch
            {
                return View();
            }
        }
    }
}
