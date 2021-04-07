using ConsignmentShopLibrary.Data.SQLite;
using ConsignmentShopLibrary.Models;
using System;
using System.IO;
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

        public void Dispose()
        {
            if(File.Exists(_dbFile))
            {
                File.Delete(_dbFile);
            }
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

            await _storeData.CreateStore(store);

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
