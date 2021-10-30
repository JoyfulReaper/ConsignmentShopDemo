using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.ViewModels
{
    public class ItemCreateViewModel
    {
        public VendorViewModel Vendor { get; set; }
        public SelectList Stores { get; set; }
        public SelectList Vendors { get; set; }
        public ItemViewModel Item { get; set; }
        public int StoreId { get; set; }
    }
}
