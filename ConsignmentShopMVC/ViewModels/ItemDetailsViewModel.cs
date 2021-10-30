using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.ViewModels
{
    public class ItemDetailsViewModel
    {
        public ItemViewModel Item { get; set; }
        public StoreViewModel Store { get; set; }
        public VendorViewModel Owner { get; set; }
    }
}
