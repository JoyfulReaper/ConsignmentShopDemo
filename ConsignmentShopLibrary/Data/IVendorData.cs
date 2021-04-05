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

using ConsignmentShopLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Data
{
    public interface IVendorData
    {
        /// <summary>
        /// Save vendor to database
        /// </summary>
        /// <param name="vendor">The vendor to save</param>
        /// <returns>The id of the vendor</returns>
        Task<int> CreateVendor(VendorModel vendor);

        /// <summary>
        /// Load all vendors from the database
        /// </summary>
        /// <returns>A List of all vendors from the database</returns>
        Task<List<VendorModel>> LoadAllVendors();

        /// <summary>
        /// Remove a vendor from the database
        /// </summary>
        /// <param name="vendor">The vendor to remove</param>
        /// <returns>The number of rows affacted</returns>
        Task<int> RemoveVendor(VendorModel vendor);

        /// <summary>
        /// Update vendor information in the database
        /// </summary>
        /// <param name="vendor">The vendor to update</param>
        /// <returns>The number of rows affacted</returns>
        Task<int> UpdateVendor(VendorModel vendor);
    }
}