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

using ConsignmentShopLibrary.DataAccess;
using ConsignmentShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Data.SQLite
{
    public class SQLiteItemData : IItemData
    {
        private readonly IDataAccess _dataAccess;

        public SQLiteItemData(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateItem(ItemModel item)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into Items (Name, Description, Price, Sold, OwnerId, PaymentDistributed) ");
            sql.Append("values (@Name, @Description, @Price, @Sold, @OwnerId, @PaymentDistributed); ");
            sql.Append("select last_insert_rowid();");

            item.OwnerId = item.Owner.Id;

            var sqlResult = await _dataAccess.QueryRawSQL<Int64, dynamic>(sql.ToString(), item);
            item.Id = (int)sqlResult.First();

            return item.Id;
        }

        public async Task<List<ItemModel>> LoadAllItems()
        {
            string sql = "select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] from Items;";
            var allItems = await _dataAccess.QueryRawSQL<ItemModel, dynamic>(sql, new { });

            await AssignOwner(allItems);

            return allItems;
        }

        public async Task<ItemModel> LoadItem(int id)
        {
            string sql = "select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] from Items where Id = @Id;";
            var queryResult = await _dataAccess.QueryRawSQL<ItemModel, dynamic>(sql, new { Id = id});

            await AssignOwner(queryResult);

            return queryResult.FirstOrDefault();
        }

        public async Task<List<ItemModel>> LoadItemsByVendor(VendorModel vendor)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] ");
            sql.Append("from Items where OwnerId = @OwnerId;");

            var sqlResult = await _dataAccess.QueryRawSQL<ItemModel, dynamic>(sql.ToString(), new { OwnerId = vendor.Id });
            sqlResult.ForEach(x => x.Owner = vendor);

            return sqlResult;
        }

        public async Task<List<ItemModel>> LoadSoldItems()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] ");
            sql.Append("from Items where Sold = 1;");

            var sqlResult = await _dataAccess.QueryRawSQL<ItemModel, dynamic>(sql.ToString(), new { });

            await AssignOwner(sqlResult);

            return sqlResult;
        }

        public async Task<List<ItemModel>> LoadSoldItemsByVendor(VendorModel vendor)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] ");
            sql.Append("from Items where OwnerId = @OwnerId and Sold = 1;");

            var sqlResult = await _dataAccess.QueryRawSQL<ItemModel, dynamic>(sql.ToString(), new { OwnerId = vendor.Id });
            sqlResult.ForEach(x => x.Owner = vendor);

            return sqlResult;
        }

        public async Task<List<ItemModel>> LoadUnsoldItems()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] ");
            sql.Append("from Items where Sold = 0");

            var sqlResult = await _dataAccess.QueryRawSQL<ItemModel, dynamic>(sql.ToString(), new { });

            await AssignOwner(sqlResult);

            return sqlResult;
        }

        public async Task RemoveItem(ItemModel item)
        {
            string sql = "delete from Items where Id=@Id";

            await _dataAccess.ExecuteRawSQL<dynamic>(sql, item);
        }

        public async Task<int> UpdateItem(ItemModel item)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Items ");
            sql.Append("SET Name = @Name, Description = @Description, Price = @Price, Sold = @Sold, OwnerId = @OwnerId, PaymentDistributed = @PaymentDistributed ");
            sql.Append("WHERE Id = @Id;");

            return await _dataAccess.ExecuteRawSQL<dynamic>(sql.ToString(), item);
        }

        private async Task AssignOwner(List<ItemModel> allItems)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("select [Id], [FirstName], [LastName], [CommissionRate], [PaymentDue] ");
            sql.Append("from Vendors where Id = @Id;");

            foreach (var item in allItems)
            {
                var owner = await _dataAccess.QueryRawSQL<VendorModel, dynamic>(sql.ToString(), new { Id = item.OwnerId });
                item.Owner = owner.First();
            }
        }
    }
}
