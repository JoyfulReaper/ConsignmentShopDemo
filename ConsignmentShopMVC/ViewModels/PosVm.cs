using ConsignmentShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.ViewModels
{
    public class PosVm
    {
        public StoreModel Store { get; set; }
        public IEnumerable<ItemModel> Items { get; set; }
    }
}
