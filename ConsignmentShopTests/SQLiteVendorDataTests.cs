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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsignmentShopTests
{
    public class SQLiteVendorDataTests : DBTest
    {
        private SQLiteVendorData _vendorData;
        private StoreModel _store;

        public SQLiteVendorDataTests() : base ("VendorTests.db")
        {
            _vendorData = new SQLiteVendorData(_config.Connection);
        }

        [Fact]
        public async Task Test_CreateVendor()
        {
            VendorModel vendor = new VendorModel()
            {
                FirstName = "Test",
                LastName = "Creating",
                CommissionRate = .10,
                PaymentDue = 0,
            };

            var id = await _vendorData.CreateVendor(vendor);
            Assert.NotEqual(0, id);

            var dbVendor = await _vendorData.LoadVendor(id);
            Assert.NotNull(dbVendor);
            Assert.Equal("Test", dbVendor.FirstName);
            Assert.Equal("Creating", dbVendor.LastName);
            Assert.Equal(.10, dbVendor.CommissionRate);
            Assert.Equal(0, dbVendor.PaymentDue);
        }

        [Fact]
        public async Task Test_LoadVendor()
        {
            var dbVendor = await _vendorData.LoadVendor(1);
            Assert.NotNull(dbVendor);
            Assert.Equal("Test", dbVendor.FirstName);
            Assert.Equal("Vendor", dbVendor.LastName);
            Assert.Equal(.5, dbVendor.CommissionRate);
            Assert.Equal(0, dbVendor.PaymentDue);
        }

        [Fact]
        public async Task Test_LoadAllVendors()
        {
            //TODO improve test
            var allVendors = await _vendorData.LoadAllVendors(_store.Id);
            Assert.NotNull(allVendors);
            Assert.True(allVendors.Count > 0);
        }

        [Fact]
        public async Task Test_UpdateVendor()
        {
            VendorModel vendor = new VendorModel()
            {
                FirstName = "Update",
                LastName = "Test",
                CommissionRate = .5,
                PaymentDue = 0,
            };

            await _vendorData.CreateVendor(vendor);

            vendor.LastName = "Vendor";
            vendor.CommissionRate = .25;
            vendor.PaymentDue = 10;

            await _vendorData.UpdateVendor(vendor);
            var dbVendor = await _vendorData.LoadVendor(vendor.Id);
            Assert.NotNull(dbVendor);
            Assert.Equal("Update", dbVendor.FirstName);
            Assert.Equal("Vendor", dbVendor.LastName);
            Assert.Equal(.25, dbVendor.CommissionRate);
            Assert.Equal(10, dbVendor.PaymentDue);

        }

        [Fact]
        public async Task Test_RemoveVendor()
        {
            VendorModel vendor = new VendorModel()
            {
                FirstName = "Delete",
                LastName = "Me",
                CommissionRate = .5,
                PaymentDue = 100,
            };

            await _vendorData.CreateVendor(vendor);

            await _vendorData.RemoveVendor(vendor);

            var vendorDb = await _vendorData.LoadVendor(vendor.Id);
            Assert.Null(vendorDb);
        }

        protected override async void Seed()
        {
            _store = new StoreModel()
            {
                Name = "Test Store",
                StoreBank = 0,
                StoreProfit = 0
            };

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Stores (Name, StoreBank, StoreProfit) ");
            sqlBuilder.Append("values (@Name, @StoreBank, @StoreProfit); ");
            _store.Id = await _config.Connection.ExecuteRawSQL<dynamic>(sqlBuilder.ToString(), _store);

            VendorModel vendor = new VendorModel()
            {
                FirstName = "Test",
                LastName = "Vendor",
                CommissionRate = .5,
                PaymentDue = 0,
                StoreId = _store.Id
            };

            sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Vendors (FirstName, LastName, CommissionRate, PaymentDue, StoreId) ");
            sqlBuilder.Append("values (@FirstName, @LastName, @CommissionRate, @PaymentDue, @StoreId); ");

            await _config.Connection.ExecuteRawSQL<dynamic>(sqlBuilder.ToString(), vendor);
        }
    }
}
