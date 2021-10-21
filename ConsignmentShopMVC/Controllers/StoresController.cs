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
using ConsignmentShopMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {
        private readonly IStoreData _storeData;

        public StoresController(IStoreData storeData)
        {
            _storeData = storeData;
        }

        // GET: StoreController
        public async Task<IActionResult> Index()
        {
            var stores = await _storeData.LoadAllStores();
            return View(stores);
        }

        public async Task<IActionResult> Pos(int? storeId)
        {
            if (storeId == null)
            {
                return NotFound();
            }

            var store = await _storeData.LoadStore(storeId.Value);
            if(store == null)
            {
                return NotFound();
            }

            PosVm vm = new PosVm
            {
                Store = store
            };

            return View(vm);

        }

        // GET: StoreController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var store = await _storeData.LoadStore((int)id);
            if(store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: StoreController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] StoreModel store)
        {
            if(ModelState.IsValid)
            {
                store.StoreProfit = 0;
                store.StoreBank = 0;
                await _storeData.CreateStore(store);

                return RedirectToAction(nameof(Index));
            }

            return View(store);
        }

        // GET: StoreController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _storeData.LoadStore((int)id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] StoreModel store)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbStore = await _storeData.LoadStore(id);
                if (dbStore == null)
                {
                    return NotFound();
                }

                dbStore.Name = store.Name;

                await _storeData.UpdateStore(dbStore);
                return RedirectToAction(nameof(Index));
            }

            return View(store);
        }

        // GET: StoreController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var store = await _storeData.LoadStore((int)id);

            if(store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _storeData.LoadStore(id);
            if(store == null)
            {
                return NotFound();
            }

            await _storeData.RemoveStore(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
