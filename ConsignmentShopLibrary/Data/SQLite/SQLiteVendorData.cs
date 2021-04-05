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
            sql.Append("values (@FirstName, @LastName, @CommissionRate, @PaymentDue);");

            await _dataAccess.ExecuteRawSQL<dynamic>(sql.ToString(), vendor);
            var queryResult = await _dataAccess.QueryRawSQL<Int64, dynamic>("select last_insert_rowid();", new { });

            vendor.Id = (int)queryResult.FirstOrDefault();
            return vendor.Id;
        }

        public async Task<List<VendorModel>> LoadAllVendors()
        {
            string sql = "select [Id], [FirstName], [LastName], [CommissionRate], [PaymentDue] from Vendors;";
            return await _dataAccess.QueryRawSQL<VendorModel, dynamic>(sql, new { });
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
