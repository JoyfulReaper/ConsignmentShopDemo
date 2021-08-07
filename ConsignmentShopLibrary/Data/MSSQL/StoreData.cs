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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Data.MSSQL
{
    public class StoreData : IStoreData
    {
        private readonly IDataAccess _dataAccess;

        public StoreData(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<StoreModel> LoadStore(string name)
        {
            var rows = await _dataAccess.LoadData<StoreModel, dynamic>("dbo.SpStores_Get", new { Name = name });

            if (!rows.Any())
            {
                StoreModel store = new StoreModel { Name = name, StoreBank = 0, StoreProfit = 0 };

                await CreateStore(store);

                return store;
            }

            if (rows.Count > 1)
            {
                throw new Exception($"Multiple stores named {name} exit.");
            }

            return rows.First();
        }

        public async Task<int> CreateStore(StoreModel store)
        {
            string sql = "select count(id) from Stores where name = @Name";
            var resList = await _dataAccess.QueryRawSQL<int, dynamic>(sql, new { Name = store.Name });
            int res = resList.First();

            if(res != 0)
            {
                throw new ArgumentException("A store with the same name already exists.", nameof(store));
            }

            DynamicParameters p = new DynamicParameters();

            p.Add("Name", store.Name);
            p.Add("StoreBank", store.StoreBank);
            p.Add("StoreProfit", store.StoreProfit);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            // we do something with Id so we have to await
            await _dataAccess.SaveData("dbo.spStores_Insert", p);

            return p.Get<int>("Id");
        }

        public Task<int> UpdateStore(StoreModel store)
        {
            return _dataAccess.SaveData("dbo.spStores_Update", new
            {
                Id = store.Id,
                Name = store.Name,
                StoreBank = store.StoreBank,
                StoreProfit = store.StoreProfit
            });
        }

        public async Task<List<StoreModel>> LoadAllStores()
        {
            var allStores = await _dataAccess.LoadData<StoreModel, dynamic>("dbo.spStores_GetAll", new { });
            return allStores;
        }

        public async Task<StoreModel> LoadStore(int id)
        {
            var rows = await _dataAccess.LoadData<StoreModel, dynamic>("dbo.SpStores_GetById", new { Id = id });

            return rows.FirstOrDefault();
        }

        public async Task RemoveStore(int id)
        {
            await _dataAccess.LoadData<StoreModel, dynamic>("dbo.SpStores_Delete", new { Id = id });
        }
    }
}