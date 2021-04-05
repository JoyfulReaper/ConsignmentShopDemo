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
    public interface IItemData
    {
        /// <summary>
        /// Save an item to the database
        /// </summary>
        /// <param name="item">The item to save in the database</param>
        /// <returns>The id of the item</returns>
        Task<int> CreateItem(ItemModel item);

        /// <summary>
        /// Load all items from the database
        /// </summary>
        /// <returns>All items in the dateabase</returns>
        Task<List<ItemModel>> LoadAllItems();

        /// <summary>
        /// Load items owned be a specific Vendor from the database
        /// </summary>
        /// <param name="vendor">The vendor for which to load items</param>
        /// <returns>All items owned by vendor</returns>
        Task<List<ItemModel>> LoadItemsByVendor(VendorModel vendor);

        /// <summary>
        /// Load all sold items from the database
        /// </summary>
        /// <returns>List of all sold items</returns>
        Task<List<ItemModel>> LoadSoldItems();

        /// <summary>
        /// Loads all sold items by vendor
        /// </summary>
        /// <param name="vendor">The vendor for which to load sold items</param>
        /// <returns>All sold items owned by the given vendor</returns>
        Task<List<ItemModel>> LoadSoldItemsByVendor(VendorModel vendor);

        /// <summary>
        /// Load all unsold items from the database
        /// </summary>
        /// <returns>List of all unsold items</returns>
        Task<List<ItemModel>> LoadUnsoldItems();

        /// <summary>
        /// Remove an item from the database
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>The number of rows affected</returns>
        Task RemoveItem(ItemModel item);

        /// <summary>
        /// Update an item stored in the database
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <returns>The number of rows affected</returns>
        Task<int> UpdateItem(ItemModel item);
    }
}