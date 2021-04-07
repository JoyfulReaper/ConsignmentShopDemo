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

using ConsignmentShopLibrary.DataAccess;
using ConsignmentShopLibrary.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Data.MSSQL
{
    public class ItemData : IItemData
    {
        private readonly IDataAccess _dataAccess;

        public ItemData(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateItem(ItemModel item)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("Name", item.Name);
            p.Add("Description", item.Description);
            p.Add("Price", item.Price);
            p.Add("Sold", item.Sold);
            p.Add("OwnerId", item.Owner.Id);
            p.Add("PaymentDistributed", item.PaymentDistributed);
            p.Add("Id", 0, DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spItems_Insert", p);

            return p.Get<int>("Id");
        }

        public Task<int> UpdateItem(ItemModel item)
        {
            return _dataAccess.SaveData("dbo.spItems_Update", new
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Sold = item.Sold,
                OwnerId = item.Owner.Id,
                PaymentDistributed = item.PaymentDistributed
            });
        }

        public async Task<List<ItemModel>> LoadAllItems()
        {
            var allItems = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_GetAll", new { });

            await AssignOwner(allItems);

            return allItems;
        }

        public async Task<List<ItemModel>> LoadUnsoldItems()
        {
            var unsoldItems = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_GetUnsold", new { });

            await AssignOwner(unsoldItems);

            return unsoldItems;
        }

        public async Task<List<ItemModel>> LoadSoldItems()
        {
            var soldItems = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_GetSold", new { });

            await AssignOwner(soldItems);

            return soldItems;
        }

        public async Task<List<ItemModel>> LoadSoldItemsByVendor(VendorModel vendor)
        {
            var soldItems = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_GetSoldByVendorId", new { OwnerId = vendor.Id });

            foreach (var item in soldItems)
            {
                item.Owner = vendor;
            }

            return soldItems;
        }

        public async Task<List<ItemModel>> LoadItemsByVendor(VendorModel vendor)
        {
            var vendorItems = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_GetByVendorId", new { OwnerId = vendor.Id });

            foreach (var item in vendorItems)
            {
                item.Owner = vendor;
            }

            return vendorItems;
        }

        public Task RemoveItem(ItemModel item)
        {
            return _dataAccess.SaveData("dbo.spItems_Delete", new { Id = item.Id });
        }

        private async Task AssignOwner(List<ItemModel> allItems)
        {
            foreach (var item in allItems)
            {
                var owner = await _dataAccess.LoadData<VendorModel, dynamic>("dbo.spVendors_Get", new { Id = item.OwnerId});
                item.Owner = owner.First();
            }
        }

        public async Task<ItemModel> LoadItem(int id)
        {
            string sql = "select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] from Items where Id = @Id;";
            var queryResult = await _dataAccess.QueryRawSQL<ItemModel, dynamic>(sql, new { Id = id });

            await AssignOwner(queryResult);

            return queryResult.FirstOrDefault();
        }
    }
}