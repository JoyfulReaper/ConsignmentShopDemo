using ConsignmentShopLibrary.Data;
using ConsignmentShopLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.Controllers
{
    public class StoresController : Controller
    {
        private readonly IStoreData _storeData;

        public StoresController(IStoreData storeData)
        {
            _storeData = storeData;
        }

        // GET: StoreController
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var stores = await _storeData.LoadAllStores();
            return View(stores);
        }

        // GET: StoreController/Details/5
        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] StoreModel store)
        {
            if(ModelState.IsValid)
            {
                store.StoreProfit = 0;
                store.StoreBank = 0;
                _storeData.CreateStore(store);

                return RedirectToAction(nameof(Index));
            }

            return View(store);
        }

        // GET: StoreController/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
