using ConsignmentShopLibrary.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ItemsController(IItemData itemData,
            IStoreData storeData)
        {
            _itemData = itemData;
            _storeData = storeData;
        }

        // GET: ItemsController
        [HttpGet]
        [Route("Items/Index/{storeId:int}")]
        public async Task<IActionResult> Index(int? storeId)
        {
            if(storeId == null)
            {
                return NotFound();
            }

            if(await _storeData.LoadStore((int)storeId) == null)
            {
                return NotFound();
            }

            var items = await _itemData.LoadAllItems((int)storeId);

            return View(items);
        }

        // GET: ItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemsController/Create
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

        // GET: ItemsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemsController/Edit/5
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

        // GET: ItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
