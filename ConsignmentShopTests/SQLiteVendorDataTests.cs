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

        protected override async void Seed()
        {
            VendorModel vendor = new VendorModel()
            {
                FirstName = "Test",
                LastName = "Vendor",
                CommissionRate = .5,
                PaymentDue = 0,
            };

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Vendors (FirstName, LastName, CommissionRate, PaymentDue) ");
            sqlBuilder.Append("values (@FirstName, @LastName, @CommissionRate, @PaymentDue); ");

            await _config.Connection.ExecuteRawSQL<dynamic>(sqlBuilder.ToString(), vendor);
        }
    }
}
