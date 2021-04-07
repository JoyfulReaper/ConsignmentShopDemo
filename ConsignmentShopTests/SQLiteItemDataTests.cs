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
using Xunit;

namespace ConsignmentShopTests
{
    public class SQLiteItemDataTests : DBTest
    {
        private SQLiteItemData _itemData;

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
                OwnerId = 0,
                Owner = null,
            };

            var id = await _itemData.CreateItem(item);
            Assert.NotEqual(0, id);
            
            //var dbItem = _itemData.LoadItem(id);
        }

        protected override async void Seed()
        {
            var item = new ItemModel()
            {
                Name = "Test Item",
                Description = "Test Description",
                Price = 1.00m,
                Sold = false,
                PaymentDistributed = false,
                OwnerId = 0,
                Owner = null,
            };

            StringBuilder sql = new StringBuilder();
            sql.Append("insert into Items (Name, Description, Price, Sold, OwnerId, PaymentDistributed) ");
            sql.Append("values (@Name, @Description, @Price, @Sold, @OwnerId, @PaymentDistributed); ");


            var sqlResult = await _config.Connection.ExecuteRawSQL<dynamic>(sql.ToString(), item);
        }
    }
}
