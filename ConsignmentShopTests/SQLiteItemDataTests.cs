/*
MIT License

Copyright(c) 2021 Kyle Givler
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

using ConsignmentShopLibrary.Data.SQLite;
using ConsignmentShopLibrary.Models;
using System.Linq;
using System.Text;
using Xunit;

namespace ConsignmentShopTests
{
    public class SQLiteItemDataTests : DBTest
    {
        private SQLiteItemData _itemData;
        private VendorModel _vendor;

        public SQLiteItemDataTests() : base ("ItemTests.db")
        {
            _itemData = new SQLiteItemData(_config.Connection);
        }

        [Fact]
        public async void Test_CreateItem()
        {
            var item = new ItemModel()
            {
                Name = "Create Item",
                Description = "Create Description",
                Price = 5.00m,
                Sold = false,
                PaymentDistributed = false,
                OwnerId = _vendor.Id,
                Owner = _vendor,
            };

            var id = await _itemData.CreateItem(item);
            Assert.NotEqual(0, id);
            
            var dbItem = await _itemData.LoadItem(id);
            Assert.NotNull(dbItem);

            Assert.Equal("Create Item", dbItem.Name);
            Assert.Equal("Create Description", dbItem.Description);
            Assert.False(dbItem.PaymentDistributed);
            Assert.Equal(_vendor.Id, dbItem.OwnerId);
        }

        [Fact]
        public async void Test_LoadItem()
        {
            var dbItem = await _itemData.LoadItem(1);
            Assert.NotNull(dbItem);

            Assert.Equal("Test Item", dbItem.Name);
            Assert.Equal("Test Description", dbItem.Description);
            Assert.False(dbItem.PaymentDistributed);
            Assert.Equal(_vendor.Id, dbItem.OwnerId);
        }

        [Fact]
        public async void Test_LoadAllItems()
        {
            //TODO Improve test
            var allItems = await _itemData.LoadAllItems();
            Assert.NotNull(allItems);
            Assert.True(allItems.Count > 0);
        }

        [Fact]
        public async void Test_LoadItemsByVendor()
        {
            //TODO Improve test
            var allItems = await _itemData.LoadItemsByVendor(_vendor);
            Assert.NotNull(allItems);
            Assert.True(allItems.Count > 0);
            Assert.True(allItems.TrueForAll(x => x.OwnerId == _vendor.Id));
        }

        [Fact]
        public async void Test_LoadSoldItems()
        {
            var item = new ItemModel()
            {
                Name = "Sold Item",
                Description = "Sold Description",
                Price = 1.00m,
                Sold = true,
                PaymentDistributed = false,
                OwnerId = _vendor.Id,
                Owner = _vendor,
            };

            int id = await _itemData.CreateItem(item);

            var allItems = await _itemData.LoadSoldItems();
            Assert.NotNull(allItems);
            Assert.True(allItems.Count > 0);
            Assert.True(allItems.TrueForAll(x => x.Sold == true));

            item = allItems.Where(x => x.Id == id).FirstOrDefault();
            Assert.NotNull(item);
            Assert.Equal("Sold Item", item.Name);
            Assert.Equal("Sold Description", item.Description);
            Assert.False(item.PaymentDistributed);
            Assert.Equal(_vendor.Id, item.OwnerId);
        }

        [Fact]
        public async void Test_LoadSoldItemsByVendor()
        {
            var item = new ItemModel()
            {
                Name = "Vendor Sold Item",
                Description = "Vendor Sold Description",
                Price = 1.00m,
                Sold = true,
                PaymentDistributed = false,
                OwnerId = _vendor.Id,
                Owner = _vendor,
            };

            int id = await _itemData.CreateItem(item);

            var allItems = await _itemData.LoadSoldItems();
            Assert.NotNull(allItems);
            Assert.True(allItems.Count > 0);
            Assert.True(allItems.TrueForAll(x => x.Sold == true));
            Assert.True(allItems.TrueForAll(x => x.OwnerId == _vendor.Id));

            item = allItems.Where(x => x.Id == id).FirstOrDefault();
            Assert.NotNull(item);
            Assert.Equal("Vendor Sold Item", item.Name);
            Assert.Equal("Vendor Sold Description", item.Description);
            Assert.False(item.PaymentDistributed);
            Assert.Equal(_vendor.Id, item.OwnerId);
        }

        [Fact]
        public async void Test_LoadUnsoldItems()
        {
            var allItems = await _itemData.LoadUnsoldItems();
            Assert.NotNull(allItems);
            Assert.True(allItems.Count > 0);
            Assert.True(allItems.TrueForAll(x => x.Sold == false));
        }

        [Fact]
        public async void Test_RemoveItem()
        {
            var item = new ItemModel()
            {
                Name = "Delete Item",
                Description = "Delete Description",
                Price = 1.00m,
                Sold = true,
                PaymentDistributed = true,
                OwnerId = _vendor.Id,
                Owner = _vendor,
            };

            int id = await _itemData.CreateItem(item);

            await _itemData.RemoveItem(item);

            item = await _itemData.LoadItem(id);
            Assert.Null(item);
        }

        [Fact]
        public async void Test_UpdateItem()
        {
            var item = new ItemModel()
            {
                Name = "Update Item",
                Description = "Update Description",
                Price = 1.00m,
                Sold = false,
                PaymentDistributed = false,
                OwnerId = _vendor.Id,
                Owner = _vendor,
            };

            int id = await _itemData.CreateItem(item);

            item.Name = "Updated Item";
            item.Price = 5.00m;
            item.Sold = true;

            await _itemData.UpdateItem(item);

            var dbItem = _itemData.LoadItem(id);
            Assert.NotNull(item);
            Assert.Equal("Updated Item", item.Name);
            Assert.Equal("Update Description", item.Description);
            Assert.Equal(5.00m, item.Price);
            Assert.False(item.PaymentDistributed);
            Assert.Equal(_vendor.Id, item.OwnerId);
        }

        protected override async void Seed()
        {
            _vendor = new VendorModel()
            {
                FirstName = "test",
                LastName = "vendor",
                CommissionRate = 0,
                PaymentDue = 0
            };

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Vendors (FirstName, LastName, CommissionRate, PaymentDue) ");
            sqlBuilder.Append("values (@FirstName, @LastName, @CommissionRate, @PaymentDue); ");

            _vendor.Id = await _config.Connection.ExecuteRawSQL<dynamic>(sqlBuilder.ToString(), _vendor);

            var item = new ItemModel()
            {
                Name = "Test Item",
                Description = "Test Description",
                Price = 1.00m,
                Sold = false,
                PaymentDistributed = false,
                OwnerId = _vendor.Id,
                Owner = _vendor,
            };

            StringBuilder sql = new StringBuilder();
            sql.Append("insert into Items (Name, Description, Price, Sold, OwnerId, PaymentDistributed) ");
            sql.Append("values (@Name, @Description, @Price, @Sold, @OwnerId, @PaymentDistributed); ");


            var sqlResult = await _config.Connection.ExecuteRawSQL<dynamic>(sql.ToString(), item);
        }
    }
}
