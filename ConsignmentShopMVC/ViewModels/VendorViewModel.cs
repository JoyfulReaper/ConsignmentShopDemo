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

using System.ComponentModel.DataAnnotations;

namespace ConsignmentShopMVC.ViewModels
{
    public class VendorViewModel
    {
        /// <summary>
        /// Id of database row
        /// </summary>
        public int Id { get; set; }

        [Display(Name = "Store")]
        public int StoreId { get; set; }

        /// <summary>
        /// Vendor's First Name
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Vendor's Last Name
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Commission Rate to pay vendor on items sold
        /// </summary>
        [Display(Name = "Commision Rate")]
        public double CommissionRate { get; set; }

        /// <summary>
        /// The amount of payment the vendor is due
        /// </summary>
        [Display(Name = "Payment Due")]
        public decimal PaymentDue { get; set; }

        /// <summary>
        /// The Vendor's Display Name
        /// </summary>
        public string Display
        {
            get
            {
                //return string.Format("{0} {1} {2} - {2:C2}", FirstName, LastName, PaymentDue);
                return $"{FirstName} {LastName} ({CommissionRate * 100}%) - {PaymentDue:C2}";
            }
        }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
