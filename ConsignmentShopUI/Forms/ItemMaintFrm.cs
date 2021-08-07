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
using System.ComponentModel;
using System.Windows.Forms;
using ConsignmentShopLibrary.Models;
using System.Linq;
using ConsignmentShopLibrary.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsignmentShopUI
{
    public partial class ItemMaintFrm : Form
    {
        private readonly BindingList<ItemModel> _items = new BindingList<ItemModel>();
        private readonly BindingList<VendorModel> _vendors = new BindingList<VendorModel>();

        private readonly IVendorData _vendorData;
        private readonly IItemData _itemData;
        private bool _editing = false;
        private ItemModel _editingItem = null;

        public StoreModel Store { get; set; }

        public ItemMaintFrm(IVendorData vendorData,
            IItemData itemData)
        {
            InitializeComponent();
            _vendorData = vendorData;
            _itemData = itemData;
        }

        private async void ItemMaintFrm_Load(object sender, System.EventArgs e)
        {
            await UpdateItems();
            await UpdateVendors();
        }

        private async Task UpdateVendors()
        {
            _vendors.Clear();

            var allVendors = await _vendorData.LoadAllVendors(Store.Id);
            allVendors = allVendors.OrderBy(x => x.LastName).ToList();

            foreach (var vendor in allVendors)
            {
                _vendors.Add(vendor);
            }

            listBoxVendors.DataSource = _vendors;
            listBoxVendors.DisplayMember = "FullName";
            listBoxVendors.ValueMember = "FullName";
        }

        private async Task UpdateItems()
        {
            _items.Clear();
            List<ItemModel> currentItems = new List<ItemModel>();

            if (radioButtonAll.Checked)
            {
                currentItems = await _itemData.LoadAllItems(Store.Id);
                currentItems = currentItems.OrderBy(x => x.Name).ToList();

                foreach (var item in currentItems)
                {
                    _items.Add(item);
                }
            }
            else if (radioButtonSold.Checked)
            {
                currentItems = await _itemData.LoadSoldItems(Store.Id);
                currentItems = currentItems.OrderBy(x => x.Name).ToList();

                foreach (var item in currentItems)
                {
                    _items.Add(item);
                }
            }
            else if (radioButtonUnsold.Checked)
            {
                currentItems = await _itemData.LoadUnsoldItems(Store.Id);
                currentItems = currentItems.OrderBy(x => x.Name).ToList();

                foreach (var item in currentItems)
                {
                    _items.Add(item);
                }
            }

            allItemsListBox.DataSource = _items;
            allItemsListBox.DisplayMember = "Display";
            allItemsListBox.ValueMember = "Display";

            _items.ResetBindings();
        }

        private void btnItemDelete_Click(object sender, System.EventArgs e)
        {
            ItemModel selectedItem = (ItemModel)allItemsListBox.SelectedItem;

            if(selectedItem == null)
            {
                return;
            }

            if(selectedItem.Sold && !selectedItem.PaymentDistributed)
            {
                MessageBox.Show($"{selectedItem.Owner.FullName} needs to be paid before this item can be deleted.", "Pay the vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                UpdateItems();

                return;
            }

            _itemData.RemoveItem(selectedItem);

            UpdateItems();
        }

        private async void btmAddItem_Click(object sender, System.EventArgs e)
        {
            ItemModel output = null;

            if (!validateData())
            {
                return;
            }

            if(_editing)
            {
                _editingItem.Name = textBoxName.Text;
                _editingItem.Price = decimal.Parse(textBoxPrice.Text);
                _editingItem.Description = textBoxDesc.Text;
                _editingItem.Owner = (VendorModel)listBoxVendors.SelectedItem;
                _editingItem.OwnerId = _editingItem.Owner.Id;
                _editingItem.Sold = checkBoxSold.Checked;
                _editingItem.PaymentDistributed = checkBoxVendorPaid.Checked;

                _itemData.UpdateItem(_editingItem);

                btnAddItem.Text = "Add Item";
                btnEdit.Enabled = true;
                _editing = false;

                output = _editingItem;
            }
            else
            {
                output = new ItemModel()
                {
                    Name = textBoxName.Text,
                    Price = decimal.Parse(textBoxPrice.Text),
                    Description = textBoxDesc.Text,
                    Owner = (VendorModel)listBoxVendors.SelectedItem,
                    Sold = checkBoxSold.Checked,
                    StoreId = Store.Id
                };

                await _itemData.CreateItem(output);
            }

            await UpdateItems();
            UpdateSelectedItemInfo();
            ClearItemInput();
        }

        private void ClearItemInput()
        {
            textBoxName.Text = string.Empty;
            textBoxDesc.Text = string.Empty;
            textBoxPrice.Text = string.Empty;

            checkBoxSold.Checked = false;
            listBoxVendors.ClearSelected();
        }

        private bool validateData()
        {
            string ErrorMessage = string.Empty;
            bool valid = true;
            decimal price = 0;

            if ((VendorModel)listBoxVendors.SelectedItem == null)
            {
                ErrorMessage += "Please select a valid vendor.\n";
                valid = false;
            }

            if (textBoxName.Text == "")
            {
                valid = false;
                ErrorMessage += "Please enter a valid name.\n";
            }

            //if (textBoxDesc.Text == "")
            //{
            //    valid = false;
            //    ErrorMessage += "Please enter a valid description.\n";
            //}

            if (textBoxPrice.Text == "" || !decimal.TryParse(textBoxPrice.Text, out price))
            {
                ErrorMessage += "Please enter a valid price.\n";
                valid = false;
            }

            if(price < 0)
            {
                ErrorMessage += "Price must be positive.\n";
                valid = false;
            }

            if(!valid)
            {
                MessageBox.Show(ErrorMessage, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valid;
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            ItemModel selectedItem = (ItemModel)allItemsListBox.SelectedItem;
            _editingItem = selectedItem;

            if(selectedItem == null)
            {
                return;
            }

            if (selectedItem.Sold && !selectedItem.PaymentDistributed)
            {
                MessageBox.Show($"{selectedItem.Owner.FullName} needs to be paid before this item can be edited.", "Pay the vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _editing = true;

            PopulateItemTextBoxes();

            UpdateItems();

            btnAddItem.Text = "Update Item";
            btnEdit.Enabled = false;
        }

        private void PopulateItemTextBoxes()
        {
            ItemModel selectedItem = (ItemModel)allItemsListBox.SelectedItem;

            if(selectedItem == null)
            {
                return;
            }

            textBoxName.Text = selectedItem.Name;
            textBoxDesc.Text = selectedItem.Description;
            textBoxPrice.Text = $"{selectedItem.Price:F2}";

            checkBoxSold.Checked = selectedItem.Sold;
            checkBoxVendorPaid.Checked = selectedItem.PaymentDistributed;

            //There has to be a better way of doing this:
            var vendor = _vendors.Where(x => x.Id == selectedItem.Owner.Id).First();
            listBoxVendors.SelectedItem = vendor; 

            // The below does not work, I think becasue the object reference is not equal
            //var vendor = selectedItem.Owner;
            //listBoxVendors.SelectedItem = vendor;

            _vendors.ResetBindings();
        }

        private async void radioButtonOption_CheckedChanged(object sender, System.EventArgs e)
        {
            // This event fires twice, once on the new button being checked and once on the old button being unchecked
            // We only want to react to the new button being checked

            RadioButton rb = sender as RadioButton;

            if(rb != null)
            {
                if(rb.Checked)
                {
                    await UpdateItems();
                }
            }
        }

        private async void btnDeleteSold_Click(object sender, System.EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete all sold items?\n\nOnly items for which the vendor has been paid will be deleted!\n\nThis action cannot be undone!", 
                "Delete All Sold", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                var soldItems = await _itemData.LoadSoldItems(Store.Id);

                foreach(var item in soldItems)
                {
                    if(item.PaymentDistributed)
                    {
                        await _itemData.RemoveItem(item);
                    }
                    else
                    {
                        //MessageBox.Show($"Unable to delete {item.Name}. The vendor must be paid!");
                    }
                }

                UpdateItems();
            }
        }

        private void allItemsListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateSelectedItemInfo();
        }

        private void UpdateSelectedItemInfo()
        {
            ItemModel item = (ItemModel)allItemsListBox.SelectedItem;

            if (item == null)
            {
                return;
            }

            lblNameValue.Text = item.Name;
            textBoxSelectedDesc.Text = item.Description;
            lblPriceValue.Text = $"{item.Price:C2}";
            lblVendorValue.Text = item.Owner.FullName;

            if(item.Sold)
            {
                lblSoldValue.Text = "True";
            }
            else
            {
                lblSoldValue.Text = "False";
            }

            if (item.PaymentDistributed)
            {
                lblVendorPaidValue.Text = "True";
            }
            else
            {
                lblVendorPaidValue.Text = "False";
            }
        }
    }
}
