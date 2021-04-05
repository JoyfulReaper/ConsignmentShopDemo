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
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.Data
{
    public interface IStoreData
    {
        /// <summary>
        /// Save a store to the database
        /// </summary>
        /// <param name="store">The store to save</param>
        /// <returns>The id of the store</returns>
        Task<int> CreateStore(StoreModel store);

        /// <summary>
        /// Load a store from the database
        /// </summary>
        /// <param name="name">The name of the store to load</param>
        /// <returns></returns>
        Task<StoreModel> LoadStore(string name);

        /// <summary>
        /// Update a store in the database
        /// </summary>
        /// <param name="store">The store to update</param>
        /// <returns>The number of rows affected</returns>
        Task<int> UpdateStore(StoreModel store);
    }
}