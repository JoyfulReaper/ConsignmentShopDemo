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

using ConsignmentShopLibrary;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using ConsignmentShopLibrary.Models;
using System.Diagnostics;
using System.Linq;
using ConsignmentShopLibrary.Data;
using System.Threading.Tasks;
using ConsignmentShopUI.Factories;
using ConsignmentShopLibrary.Services;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private readonly BindingList<ItemModel> _shoppingCart = new BindingList<ItemModel>();
        private readonly BindingList<VendorModel> _vendors = new BindingList<VendorModel>();
        private readonly BindingList<ItemModel> _items = new BindingList<ItemModel>();

        private readonly IStoreData _storeData;
        private readonly IVendorData _vendorData;
        private readonly IItemData _itemData;
        private readonly IConfig _config;
        private readonly IItemService _itemService;
        private readonly ItemMaintFormFactory _itemMaintFormFactory;
        private readonly VendorMaintFormFactory _vendorMaintFormFactory;

        private StoreModel _store;

        public ConsignmentShop(IStoreData storeData,
            IVendorData vendorData,
            IItemData itemData,
            IConfig config,
            IItemService itemService,
            ItemMaintFormFactory itemMaintFormFactory,
            VendorMaintFormFactory vendorMaintFormFactory)
        {
            InitializeComponent();

            _storeData = storeData;
            _vendorData = vendorData;
            _itemData = itemData;
            _config = config;
            _itemService = itemService;
            _itemMaintFormFactory = itemMaintFormFactory;
            _vendorMaintFormFactory = vendorMaintFormFactory;
        }

        private async void ConsignmentShop_Load(object sender, EventArgs e)
        {
            await SetupStore();

            await SetupData();

            UpdateTotal();
        }

        private async Task SetupStore()
        {
            string storeName = _config.Configuration.GetSection("Store:Name").Value;
            _store = await _storeData.LoadStore(storeName);
        }

        private async Task SetupData()
        {
            shoppingCartListBox.DataSource = _shoppingCart;
            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            lblStoreName.Text = _store.Name;

            await UpdateVendors();
            await UpdateItems();
            UpdateBankData();
        }

        private async Task UpdateBankData()
        {
            _store = await _storeData.LoadStore(_store.Name);

            storeProfitValue.Text = $"{ _store.StoreProfit:C2}";
            lblStoreBankValue.Text = $"{ _store.StoreBank:C2}";
        }

        private async Task UpdateVendors()
        {
            _vendors.Clear();

            var allVendors = await _vendorData.LoadAllVendors(_store.Id);
            allVendors = allVendors.OrderBy(x => x.LastName).ToList();

            foreach(VendorModel v in allVendors)
            {
                _vendors.Add(v);
            }

            vendorListBox.DataSource = _vendors;
            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "Display";

            _vendors.ResetBindings();
        }

        private async Task UpdateItems()
        {
            _items.Clear();

            var unsoldItems = await _itemData.LoadUnsoldItems(_store.Id);
            unsoldItems = unsoldItems.OrderBy(x => x.Name).ToList();

            foreach (ItemModel itm in unsoldItems)
            {
                _items.Add(itm);
            }

            itemsListbox.DataSource = _items;
            itemsListbox.DisplayMember = "Display";
            itemsListbox.ValueMember = "Display";

            _items.ResetBindings();
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            ItemModel selectedItem = (ItemModel)itemsListbox.SelectedItem;

            if(selectedItem == null)
            {
                return;
            }

            _items.Remove(selectedItem); // Remove from available items
            _shoppingCart.Add(selectedItem); // Add to shopping cart

            itemsListbox_SelectedIndexChanged(this, EventArgs.Empty);

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (var item in _shoppingCart)
            {
                total += item.Price;
            }

            lblTotal.Text = $"Total: {total:C2}";
        }

        private async void makePurchase_Click(object sender, EventArgs e)
        {
            await _itemService.PurchaseItems(_shoppingCart.ToList());

            _shoppingCart.Clear();

            UpdateVendors();
            UpdateItems();
            UpdateBankData();
            UpdateTotal();

            ClearItemLabels();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ItemModel selectedItem = (ItemModel)shoppingCartListBox.SelectedItem;

            if(selectedItem == null)
            {
                return;
            }

            _items.Add(selectedItem);
            _shoppingCart.Remove(selectedItem);

            UpdateTotal();
        }

        private void btnItemMaint_Click(object sender, EventArgs e)
        {
            if (_shoppingCart.Count > 0)
            {
                MessageBox.Show("Item maintenance cannnot be performed during a transaction.");
                return;
            }

            ItemMaintFrm frm = (ItemMaintFrm) _itemMaintFormFactory.CreateForm();
            frm.Store = _store;
            frm.ShowDialog(this);

            UpdateItems();
        }

        private void itemsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemModel selectedItem = (ItemModel)itemsListbox.SelectedItem;

            if(selectedItem == null)
            {
                ClearItemLabels();
                return;
            }

            lblNameValue.Text = $"{selectedItem.Name}";
            lblDescValue.Text = $"{selectedItem.Description}";
            lblPriceValue.Text = $"{selectedItem.Price:C2}";
            lblVendorValue.Text = $"{selectedItem.Owner.FullName}";
        }

        private void ClearItemLabels()
        {
            lblNameValue.Text = string.Empty;
            lblDescValue.Text = string.Empty;
            lblPriceValue.Text = string.Empty;
            lblVendorValue.Text = string.Empty;
        }

        private async void btnVenderMaint_Click(object sender, EventArgs e)
        {
            if(_shoppingCart.Count > 0)
            {
                MessageBox.Show("Vendor maintenance cannnot be performed during a transaction.");
                return;
            }

            VendorMaintFrm frm = (VendorMaintFrm) _vendorMaintFormFactory.CreateForm();
            frm.Store = _store;
            frm.ShowDialog(this);

            await UpdateVendors();
            await UpdateItems();
            await UpdateBankData();
        }

        private void linkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/JoyfulReaper") { UseShellExecute = true });
        }

        private void SetupDemoData()
        {
            _vendors.Add(new VendorModel { FirstName = "Bill", LastName = "Smith" });
            _vendors.Add(new VendorModel { FirstName = "Sue", LastName = "Jones" });

            _items.Add(new ItemModel
            {
                Name = "Moby Dick",
                Description = "A book about a whale",
                Price = 4.50M,
                Owner = _vendors[0]
            });

            _items.Add(new ItemModel
            {
                Name = "A Tale of Two Cities",
                Description = "A book about a revolution",
                Price = 3.80M,
                Owner = _vendors[1]
            });

            _items.Add(new ItemModel
            {
                Name = "Harry Potter Book 1",
                Description = "A book about a boy",
                Price = 5.20M,
                Owner = _vendors[1]
            });

            _items.Add(new ItemModel
            {
                Name = "Jane Eyre",
                Description = "A book about a girl",
                Price = 1.50M,
                Owner = _vendors[0]
            });

            _vendors.ResetBindings();
            _items.ResetBindings();
        }
    }
}
