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
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsignmentShopTests
{
    public class SQLiteStoreDataTests : DBTest, IDisposable
    {
        private SQLiteStoreData _storeData;

        public SQLiteStoreDataTests() : base("StoreTests.db")
        {
            _storeData = new SQLiteStoreData(_config.Connection);
        }

        [Fact]
        public async Task Test_CreateStore()
        {
            StoreModel store = new StoreModel()
            {
                Name = "Create Store Test",
                StoreBank = 0,
                StoreProfit = 0
            };

            var id = await _storeData.CreateStore(store);
            Assert.NotEqual(0, id);

            var dbStore = await _storeData.LoadStore(store.Name);
            Assert.NotNull(dbStore);
            Assert.Equal("Create Store Test", dbStore.Name);
            Assert.Equal(0, dbStore.StoreBank);
            Assert.Equal(0, dbStore.StoreProfit);
        }

        [Fact]
        public async Task Test_CreateStoreThatExists()
        {
            StoreModel store = new StoreModel()
            {
                Name = "Test Store",
                StoreBank = 0,
                StoreProfit = 0
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await _storeData.CreateStore(store));
        }

        [Fact]
        public async Task Test_LoadStore()
        {
            var dbStore = await _storeData.LoadStore("Test Store");
            Assert.NotNull(dbStore);
            Assert.Equal("Test Store", dbStore.Name);
            Assert.Equal(100, dbStore.StoreBank);
            Assert.Equal(50, dbStore.StoreProfit);
        }

        [Fact]
        public async Task Test_UpdateStore()
        {
            StoreModel store = new StoreModel()
            {
                Name = "Update Store Test",
                StoreBank = 0,
                StoreProfit = 0
            };

            await _storeData.CreateStore(store);

            var dbStore = await _storeData.LoadStore(store.Name);
            Assert.NotNull(dbStore);
            Assert.Equal("Update Store Test", dbStore.Name);
            Assert.Equal(0, dbStore.StoreBank);
            Assert.Equal(0, dbStore.StoreProfit);

            store.StoreBank = 500;
            store.StoreProfit = 350;

            await _storeData.UpdateStore(store);
            dbStore = await _storeData.LoadStore(store.Name);
            Assert.NotNull(dbStore);
            Assert.Equal("Update Store Test", dbStore.Name);
            Assert.Equal(500, dbStore.StoreBank);
            Assert.Equal(350, dbStore.StoreProfit);
        }

        protected override async void Seed()
        {
            StoreModel store = new StoreModel()
            {
                Name = "Test Store",
                StoreBank = 100,
                StoreProfit = 50
            };

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Stores ([Name], [StoreBank], [StoreProfit]) ");
            sqlBuilder.Append("values (@Name, @StoreBank, @StoreProfit);");

            await _config.Connection.ExecuteRawSQL<dynamic>(sqlBuilder.ToString(), store);
        }
    }
}
