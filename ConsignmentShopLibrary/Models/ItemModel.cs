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

namespace ConsignmentShopLibrary.Models
{
    public class ItemModel
    {
        /// <summary>
        /// Item's database row Id
        /// </summary>
        public int Id { get; set; }

        public int StoreId { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Description of the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The price of the item
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Flag for if the item has been sold
        /// </summary>
        public bool Sold { get; set; }

        /// <summary>
        /// Flag for if the vendor had been paid for this item
        /// </summary>
        public bool PaymentDistributed { get; set; }

        /// <summary>
        /// The Id of the owner of this item
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// The owner of this item
        /// </summary>
        public VendorModel Owner { get; set; }

        /// <summary>
        /// The display name of this item
        /// </summary>
        public string Display
        {
            get => $"{ Name } - {Price:C2}";
        }
    }
}
