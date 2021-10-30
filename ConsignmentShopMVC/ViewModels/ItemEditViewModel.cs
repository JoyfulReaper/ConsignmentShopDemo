using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.ViewModels
{
    public class ItemEditViewModel
    {
        public ItemViewModel Item { get; set; }
        public string StoreName { get; set; }
        public SelectList Vendors { get; set; }
    }
}
