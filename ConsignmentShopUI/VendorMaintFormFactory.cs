using ConsignmentShopLibrary;
using ConsignmentShopLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public class VendorMaintFormFactory : IFormsFactory
    {
        private readonly IVendorData _vendorData;
        private readonly IVendorService _vendorService;

        public VendorMaintFormFactory(IVendorData vendorData,
            IVendorService vendorService)
        {
            _vendorData = vendorData;
            _vendorService = vendorService;
        }

        public Form CreateForm()
        {
            return new VendorMaintFrm(_vendorData, _vendorService);
        }
    }
}
