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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Data.SQLite
{
    public class SQLiteStoreData : IStoreData
    {
        private readonly IDataAccess _dataAccess;

        public SQLiteStoreData(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateStore(StoreModel store)
        {
            string sql = "select count(id) from Stores where name = @Name";
            var resList = await _dataAccess.QueryRawSQL<int, dynamic>(sql, new { Name = store.Name });
            int res = resList.First();

            if (res != 0)
            {
                throw new ArgumentException("A store with the same name already exists.", nameof(store));
            }

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Stores ([Name], [StoreBank], [StoreProfit]) ");
            sqlBuilder.Append("values (@Name, @StoreBank, @StoreProfit);");


            await _dataAccess.ExecuteRawSQL<dynamic>(sqlBuilder.ToString(), store);
            var queryResult = await _dataAccess.QueryRawSQL<Int64, dynamic>("select last_insert_rowid();", new { });

            store.Id = (int)queryResult.FirstOrDefault();
            return store.Id;
        }

        public async Task<StoreModel> LoadStore(string name)
        {
            string sql = "select [Id], [Name], [StoreBank], [StoreProfit] from Stores where [Name] = @Name;";
            var queryResult = await _dataAccess.QueryRawSQL<StoreModel, dynamic>(sql, new { Name = name });
            
            if(!queryResult.Any())
            {
                StoreModel store = new StoreModel { Name = name, StoreBank = 0, StoreProfit = 0 };
                await CreateStore(store);
                return store;
            }

            if(queryResult.Count > 1)
            {
                throw new Exception($"Multiple stores named {name} exit.");
            }

            return queryResult.First();
        }

        public async Task<int> UpdateStore(StoreModel store)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update Stores ");
            sql.Append("set [Name] = @Name, StoreBank = @StoreBank, StoreProfit = @StoreProfit ");
            sql.Append("where Id = @Id;");

            return await _dataAccess.ExecuteRawSQL<dynamic>(sql.ToString(), store);
        }
    }
}
