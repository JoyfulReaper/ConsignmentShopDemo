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
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Data.MSSQL
{
    public class VendorData : IVendorData
    {
        private readonly IDataAccess _dataAccess;

        public VendorData(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateVendor(VendorModel vendor)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("FirstName", vendor.FirstName);
            p.Add("LastName", vendor.LastName);
            p.Add("CommissionRate", vendor.CommissionRate);
            p.Add("PaymentDue", vendor.PaymentDue);
            p.Add("Id", 0, DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spVendors_Insert", p);

            return p.Get<int>("Id");
        }

        public Task<int> UpdateVendor(VendorModel vendor)
        {
            return _dataAccess.SaveData("dbo.spVendors_Update", new
            {
                Id = vendor.Id,
                FirstName = vendor.FirstName,
                LastName = vendor.LastName,
                CommissionRate = vendor.CommissionRate,
                PaymentDue = vendor.PaymentDue
            });
        }

        public Task<List<VendorModel>> LoadAllVendors()
        {
            return _dataAccess.LoadData<VendorModel, dynamic>("dbo.spVendors_GetAll", new { });
        }

        public Task<int> RemoveVendor(VendorModel vendor)
        {
            return _dataAccess.SaveData("dbo.spVendors_Delete", new { Id = vendor.Id });
        }
    }
}