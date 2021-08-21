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
using System;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorData _vendorData;
        private readonly IItemData _itemData;
        private readonly IStoreData _storeData;
        private readonly IConfig _config;

        public VendorService(IVendorData vendorData,
            IItemData itemData,
            IStoreData storeData,
            IConfig config)
        {
            _vendorData = vendorData;
            _itemData = itemData;
            _storeData = storeData;
            _config = config;
        }

        public async Task PayVendor(VendorModel vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor), "Vendor cannot be null.");
            }

            StoreModel store = await _storeData.LoadStore(vendor.StoreId);

            var itemsOwnedByVendor = await _itemData.LoadSoldItemsByVendor(vendor);

            foreach (ItemModel item in itemsOwnedByVendor)
            {
                if (!item.PaymentDistributed)
                {
                    //var AmountOwedFromDbList = await GlobalConfig.Connection.QueryRawSQL<decimal>($"select PaymentDue from Vendors where Id = {item.Owner.Id};");
                    //decimal paymentDueFromDb = AmountOwedFromDbList.First();

                    //item.Owner.PaymentDue = paymentDueFromDb;

                    decimal amountOwed = (decimal)item.Owner.CommissionRate * item.Price;

                    if (store.StoreBank >= amountOwed)
                    {
                        store.StoreBank -= amountOwed;

                        vendor.PaymentDue -= amountOwed;

                        item.PaymentDistributed = true;
                    }
                    else
                    {
                        throw new InvalidOperationException("The store bank does not contain enough money to pay the vendor!");
                    }
                }

                await _itemData.UpdateItem(item);
                await _vendorData.UpdateVendor(vendor);
            }

            await _storeData.UpdateStore(store);
        }

        public async Task RemoveVendor(VendorModel vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor), "Vendor cannot be null.");
            }

            var items = await _itemData.LoadItemsByVendor(vendor);

            // Can't delete vendor if they still have items in the store
            if (items.Count != 0)
            {
                throw new InvalidOperationException($"{vendor.FullName} cannot be deleted because they still have existing items.");
            }

            // Can't delete a vendor if we owe them money!
            if (vendor.PaymentDue > 0)
            {
                throw new InvalidOperationException($"{vendor.FullName} cannot be deleted until being payed {vendor.PaymentDue:C2}");
            }

            await _vendorData.RemoveVendor(vendor);
        }
    }
}
