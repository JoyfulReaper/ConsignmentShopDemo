using ConsignmentShopLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsignmentShopMVC.ViewModels
{
    public class PosVm
    {
        public StoreViewModel Store { get; set; }
        public SelectList Items { get; set; }
    }
}
