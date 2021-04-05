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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
    public class ItemService : IItemService
    {
        private readonly IConfig _config;
        private readonly IVendorData _vendorData;
        private readonly IStoreData _storeData;
        private readonly IItemData _itemData;

        public ItemService(IConfig config,
            IVendorData vendorData,
            IStoreData storeData,
            IItemData itemData)
        {
            _config = config;
            _vendorData = vendorData;
            _storeData = storeData;
            _itemData = itemData;
        }

        public async Task PurchaseItems(List<ItemModel> shoppingCart)
        {
            StoreModel store = await _storeData.LoadStore(_config.Configuration.GetSection("Store:Name").Value);

            foreach (ItemModel item in shoppingCart)
            {
                var PayementDueFromDbList = await _config.Connection.QueryRawSQL<decimal, dynamic>($"select PaymentDue from Vendors where Id = {item.Owner.Id};", new { });
                decimal paymentDueFromDb = PayementDueFromDbList.First();

                item.Owner.PaymentDue += paymentDueFromDb;

                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.CommissionRate * item.Price;

                store.StoreProfit += (1 - (decimal)item.Owner.CommissionRate) * item.Price;
                store.StoreBank += item.Price;

                await _itemData.UpdateItem(item);
                await _vendorData.UpdateVendor(item.Owner);
                await _storeData.UpdateStore(store);
            }
        }
    }
}
