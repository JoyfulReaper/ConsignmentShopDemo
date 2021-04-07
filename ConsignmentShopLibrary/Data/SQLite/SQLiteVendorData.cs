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
    public class SQLiteVendorData : IVendorData
    {
        private readonly IDataAccess _dataAccess;

        public SQLiteVendorData(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateVendor(VendorModel vendor)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into Vendors (FirstName, LastName, CommissionRate, PaymentDue) ");
            sql.Append("values (@FirstName, @LastName, @CommissionRate, @PaymentDue); ");
            sql.Append("select last_insert_rowid();");

            var queryResult = await _dataAccess.QueryRawSQL<Int64, dynamic>(sql.ToString(), vendor);

            vendor.Id = (int)queryResult.FirstOrDefault();
            return vendor.Id;
        }

        public async Task<List<VendorModel>> LoadAllVendors()
        {
            string sql = "select [Id], [FirstName], [LastName], [CommissionRate], [PaymentDue] from Vendors;";
            return await _dataAccess.QueryRawSQL<VendorModel, dynamic>(sql, new { });
        }

        public async Task<VendorModel> LoadVendor(int id)
        {
            string sql = "select [Id], [FirstName], [LastName], [CommissionRate], [PaymentDue] from Vendors where Id = @Id;";
            var queryResult = await _dataAccess.QueryRawSQL<VendorModel, dynamic>(sql, new { Id = id });

            return queryResult.FirstOrDefault();
        }

        public Task<int> RemoveVendor(VendorModel vendor)
        {
            string sql = "delete from Vendors where Id = @Id";

            return _dataAccess.ExecuteRawSQL<dynamic>(sql, vendor);
        }

        public Task<int> UpdateVendor(VendorModel vendor)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE Vendors ");
            sql.Append("SET FirstName = @FirstName, LastName = @LastName, CommissionRate = @CommissionRate, PaymentDue = @PaymentDue ");
            sql.Append("WHERE Id = @Id");

            return _dataAccess.ExecuteRawSQL<dynamic>(sql.ToString(), vendor);
        }
    }
}
