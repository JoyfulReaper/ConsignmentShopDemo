using ConsignmentShopLibrary;
using ConsignmentShopLibrary.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public class ItemMaintFormFactory : IFormsFactory
    {
        private readonly IVendorData _vendorData;
        private readonly IItemData _itemData;

        public ItemMaintFormFactory(IVendorData vendorData,
            IItemData itemData)
        {
            _vendorData = vendorData;
            _itemData = itemData;
        }

        public Form CreateForm()
        {
            return new ItemMaintFrm(_vendorData, _itemData);
        }
    }
}
