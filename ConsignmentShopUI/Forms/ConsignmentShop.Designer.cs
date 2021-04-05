
namespace ConsignmentShopUI
{
    partial class ConsignmentShop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerText = new System.Windows.Forms.Label();
            this.itemsListbox = new System.Windows.Forms.ListBox();
            this.itemsListboxLabel = new System.Windows.Forms.Label();
            this.addToCart = new System.Windows.Forms.Button();
            this.shoppingCartListBox = new System.Windows.Forms.ListBox();
            this.shoppingCartListboxLabel = new System.Windows.Forms.Label();
            this.makePurchase = new System.Windows.Forms.Button();
            this.vendorListBoxLabel = new System.Windows.Forms.Label();
            this.vendorListBox = new System.Windows.Forms.ListBox();
            this.storePayoutLabel = new System.Windows.Forms.Label();
            this.storeProfitValue = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnVenderMaint = new System.Windows.Forms.Button();
            this.btnItemMaint = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblVendor = new System.Windows.Forms.Label();
            this.lblDescValue = new System.Windows.Forms.Label();
            this.lblNameValue = new System.Windows.Forms.Label();
            this.lblPriceValue = new System.Windows.Forms.Label();
            this.lblVendorValue = new System.Windows.Forms.Label();
            this.linkLabelGitHub = new System.Windows.Forms.LinkLabel();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblStoreBankValue = new System.Windows.Forms.Label();
            this.lblStoreBank = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // headerText
            // 
            this.headerText.AutoSize = true;
            this.headerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headerText.ForeColor = System.Drawing.Color.DarkBlue;
            this.headerText.Location = new System.Drawing.Point(262, 9);
            this.headerText.Name = "headerText";
            this.headerText.Size = new System.Drawing.Size(405, 37);
            this.headerText.TabIndex = 0;
            this.headerText.Text = "Consignment Shop Demo";
            // 
            // itemsListbox
            // 
            this.itemsListbox.FormattingEnabled = true;
            this.itemsListbox.ItemHeight = 20;
            this.itemsListbox.Location = new System.Drawing.Point(33, 117);
            this.itemsListbox.Name = "itemsListbox";
            this.itemsListbox.Size = new System.Drawing.Size(329, 224);
            this.itemsListbox.TabIndex = 1;
            this.itemsListbox.SelectedIndexChanged += new System.EventHandler(this.itemsListbox_SelectedIndexChanged);
            // 
            // itemsListboxLabel
            // 
            this.itemsListboxLabel.AutoSize = true;
            this.itemsListboxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.itemsListboxLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.itemsListboxLabel.Location = new System.Drawing.Point(31, 94);
            this.itemsListboxLabel.Name = "itemsListboxLabel";
            this.itemsListboxLabel.Size = new System.Drawing.Size(103, 20);
            this.itemsListboxLabel.TabIndex = 2;
            this.itemsListboxLabel.Text = "Store Items";
            // 
            // addToCart
            // 
            this.addToCart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.addToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addToCart.ForeColor = System.Drawing.Color.RoyalBlue;
            this.addToCart.Location = new System.Drawing.Point(382, 162);
            this.addToCart.Name = "addToCart";
            this.addToCart.Size = new System.Drawing.Size(168, 48);
            this.addToCart.TabIndex = 0;
            this.addToCart.Text = "Add To Cart ->";
            this.addToCart.UseVisualStyleBackColor = true;
            this.addToCart.Click += new System.EventHandler(this.addToCart_Click);
            // 
            // shoppingCartListBox
            // 
            this.shoppingCartListBox.FormattingEnabled = true;
            this.shoppingCartListBox.ItemHeight = 20;
            this.shoppingCartListBox.Location = new System.Drawing.Point(566, 117);
            this.shoppingCartListBox.Name = "shoppingCartListBox";
            this.shoppingCartListBox.Size = new System.Drawing.Size(329, 224);
            this.shoppingCartListBox.TabIndex = 1;
            // 
            // shoppingCartListboxLabel
            // 
            this.shoppingCartListboxLabel.AutoSize = true;
            this.shoppingCartListboxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.shoppingCartListboxLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.shoppingCartListboxLabel.Location = new System.Drawing.Point(564, 94);
            this.shoppingCartListboxLabel.Name = "shoppingCartListboxLabel";
            this.shoppingCartListboxLabel.Size = new System.Drawing.Size(124, 20);
            this.shoppingCartListboxLabel.TabIndex = 2;
            this.shoppingCartListboxLabel.Text = "Shopping Cart";
            // 
            // makePurchase
            // 
            this.makePurchase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.makePurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.makePurchase.ForeColor = System.Drawing.Color.RoyalBlue;
            this.makePurchase.Location = new System.Drawing.Point(765, 347);
            this.makePurchase.Name = "makePurchase";
            this.makePurchase.Size = new System.Drawing.Size(130, 47);
            this.makePurchase.TabIndex = 2;
            this.makePurchase.Text = "Purchase";
            this.makePurchase.UseVisualStyleBackColor = true;
            this.makePurchase.Click += new System.EventHandler(this.makePurchase_Click);
            // 
            // vendorListBoxLabel
            // 
            this.vendorListBoxLabel.AutoSize = true;
            this.vendorListBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.vendorListBoxLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.vendorListBoxLabel.Location = new System.Drawing.Point(564, 390);
            this.vendorListBoxLabel.Name = "vendorListBoxLabel";
            this.vendorListBoxLabel.Size = new System.Drawing.Size(76, 20);
            this.vendorListBoxLabel.TabIndex = 6;
            this.vendorListBoxLabel.Text = "Vendors";
            // 
            // vendorListBox
            // 
            this.vendorListBox.FormattingEnabled = true;
            this.vendorListBox.ItemHeight = 20;
            this.vendorListBox.Location = new System.Drawing.Point(566, 413);
            this.vendorListBox.Name = "vendorListBox";
            this.vendorListBox.Size = new System.Drawing.Size(329, 224);
            this.vendorListBox.TabIndex = 5;
            // 
            // storePayoutLabel
            // 
            this.storePayoutLabel.AutoSize = true;
            this.storePayoutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.storePayoutLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.storePayoutLabel.Location = new System.Drawing.Point(31, 549);
            this.storePayoutLabel.Name = "storePayoutLabel";
            this.storePayoutLabel.Size = new System.Drawing.Size(155, 20);
            this.storePayoutLabel.TabIndex = 7;
            this.storePayoutLabel.Text = "Total Gross Profit:";
            // 
            // storeProfitValue
            // 
            this.storeProfitValue.AutoSize = true;
            this.storeProfitValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.storeProfitValue.ForeColor = System.Drawing.Color.DodgerBlue;
            this.storeProfitValue.Location = new System.Drawing.Point(33, 569);
            this.storeProfitValue.Name = "storeProfitValue";
            this.storeProfitValue.Size = new System.Drawing.Size(54, 20);
            this.storeProfitValue.TabIndex = 8;
            this.storeProfitValue.Text = "$0.00";
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnRemove.Location = new System.Drawing.Point(382, 226);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(168, 48);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "<- Remove Item";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnVenderMaint
            // 
            this.btnVenderMaint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnVenderMaint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVenderMaint.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnVenderMaint.Location = new System.Drawing.Point(34, 601);
            this.btnVenderMaint.Name = "btnVenderMaint";
            this.btnVenderMaint.Size = new System.Drawing.Size(168, 48);
            this.btnVenderMaint.TabIndex = 3;
            this.btnVenderMaint.Text = "Vendor Maintenance";
            this.btnVenderMaint.UseVisualStyleBackColor = true;
            this.btnVenderMaint.Click += new System.EventHandler(this.btnVenderMaint_Click);
            // 
            // btnItemMaint
            // 
            this.btnItemMaint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnItemMaint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemMaint.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnItemMaint.Location = new System.Drawing.Point(261, 601);
            this.btnItemMaint.Name = "btnItemMaint";
            this.btnItemMaint.Size = new System.Drawing.Size(168, 48);
            this.btnItemMaint.TabIndex = 4;
            this.btnItemMaint.Text = "Item Maintenance";
            this.btnItemMaint.UseVisualStyleBackColor = true;
            this.btnItemMaint.Click += new System.EventHandler(this.btnItemMaint_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblName.Location = new System.Drawing.Point(31, 391);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 20);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDesc.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDesc.Location = new System.Drawing.Point(31, 423);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(105, 20);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "Description:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPrice.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblPrice.Location = new System.Drawing.Point(31, 455);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(59, 20);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price: ";
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblVendor.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblVendor.Location = new System.Drawing.Point(31, 487);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(72, 20);
            this.lblVendor.TabIndex = 2;
            this.lblVendor.Text = "Vendor:";
            // 
            // lblDescValue
            // 
            this.lblDescValue.AutoSize = true;
            this.lblDescValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDescValue.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblDescValue.Location = new System.Drawing.Point(142, 422);
            this.lblDescValue.Name = "lblDescValue";
            this.lblDescValue.Size = new System.Drawing.Size(59, 20);
            this.lblDescValue.TabIndex = 2;
            this.lblDescValue.Text = "{desc}";
            // 
            // lblNameValue
            // 
            this.lblNameValue.AutoSize = true;
            this.lblNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNameValue.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblNameValue.Location = new System.Drawing.Point(143, 390);
            this.lblNameValue.Name = "lblNameValue";
            this.lblNameValue.Size = new System.Drawing.Size(65, 20);
            this.lblNameValue.TabIndex = 2;
            this.lblNameValue.Text = "{name}";
            // 
            // lblPriceValue
            // 
            this.lblPriceValue.AutoSize = true;
            this.lblPriceValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPriceValue.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblPriceValue.Location = new System.Drawing.Point(142, 454);
            this.lblPriceValue.Name = "lblPriceValue";
            this.lblPriceValue.Size = new System.Drawing.Size(60, 20);
            this.lblPriceValue.TabIndex = 2;
            this.lblPriceValue.Text = "{price}";
            // 
            // lblVendorValue
            // 
            this.lblVendorValue.AutoSize = true;
            this.lblVendorValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblVendorValue.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblVendorValue.Location = new System.Drawing.Point(142, 486);
            this.lblVendorValue.Name = "lblVendorValue";
            this.lblVendorValue.Size = new System.Drawing.Size(75, 20);
            this.lblVendorValue.TabIndex = 2;
            this.lblVendorValue.Text = "{vendor}";
            // 
            // linkLabelGitHub
            // 
            this.linkLabelGitHub.AutoSize = true;
            this.linkLabelGitHub.Location = new System.Drawing.Point(692, 653);
            this.linkLabelGitHub.Name = "linkLabelGitHub";
            this.linkLabelGitHub.Size = new System.Drawing.Size(225, 20);
            this.linkLabelGitHub.TabIndex = 9;
            this.linkLabelGitHub.TabStop = true;
            this.linkLabelGitHub.Text = "http://github.com/JoyfulReaper";
            this.linkLabelGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGitHub_LinkClicked);
            // 
            // lblStoreName
            // 
            this.lblStoreName.AutoSize = true;
            this.lblStoreName.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStoreName.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStoreName.Location = new System.Drawing.Point(31, 57);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(197, 33);
            this.lblStoreName.TabIndex = 0;
            this.lblStoreName.Text = "{Store name}";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTotal.Location = new System.Drawing.Point(788, 94);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(107, 20);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "Total: {total}";
            // 
            // lblStoreBankValue
            // 
            this.lblStoreBankValue.AutoSize = true;
            this.lblStoreBankValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStoreBankValue.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblStoreBankValue.Location = new System.Drawing.Point(262, 569);
            this.lblStoreBankValue.Name = "lblStoreBankValue";
            this.lblStoreBankValue.Size = new System.Drawing.Size(54, 20);
            this.lblStoreBankValue.TabIndex = 12;
            this.lblStoreBankValue.Text = "$0.00";
            // 
            // lblStoreBank
            // 
            this.lblStoreBank.AutoSize = true;
            this.lblStoreBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStoreBank.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblStoreBank.Location = new System.Drawing.Point(262, 549);
            this.lblStoreBank.Name = "lblStoreBank";
            this.lblStoreBank.Size = new System.Drawing.Size(104, 20);
            this.lblStoreBank.TabIndex = 11;
            this.lblStoreBank.Text = "Store Bank:";
            // 
            // ConsignmentShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(929, 682);
            this.Controls.Add(this.lblStoreBankValue);
            this.Controls.Add(this.lblStoreBank);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.linkLabelGitHub);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.storeProfitValue);
            this.Controls.Add(this.storePayoutLabel);
            this.Controls.Add(this.vendorListBoxLabel);
            this.Controls.Add(this.vendorListBox);
            this.Controls.Add(this.makePurchase);
            this.Controls.Add(this.btnItemMaint);
            this.Controls.Add(this.btnVenderMaint);
            this.Controls.Add(this.addToCart);
            this.Controls.Add(this.lblVendor);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblVendorValue);
            this.Controls.Add(this.lblPriceValue);
            this.Controls.Add(this.lblNameValue);
            this.Controls.Add(this.lblDescValue);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.shoppingCartListboxLabel);
            this.Controls.Add(this.itemsListboxLabel);
            this.Controls.Add(this.shoppingCartListBox);
            this.Controls.Add(this.itemsListbox);
            this.Controls.Add(this.lblStoreName);
            this.Controls.Add(this.headerText);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ConsignmentShop";
            this.Text = "Consignment Shop Demo";
            this.Load += new System.EventHandler(this.ConsignmentShop_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerText;
        private System.Windows.Forms.ListBox itemsListbox;
        private System.Windows.Forms.Label itemsListboxLabel;
        private System.Windows.Forms.Button addToCart;
        private System.Windows.Forms.ListBox shoppingCartListBox;
        private System.Windows.Forms.Label shoppingCartListboxLabel;
        private System.Windows.Forms.Button makePurchase;
        private System.Windows.Forms.Label vendorListBoxLabel;
        private System.Windows.Forms.ListBox vendorListBox;
        private System.Windows.Forms.Label storePayoutLabel;
        private System.Windows.Forms.Label storeProfitValue;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnVenderMaint;
        private System.Windows.Forms.Button btnItemMaint;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblVendor;
        private System.Windows.Forms.Label lblDescValue;
        private System.Windows.Forms.Label lblNameValue;
        private System.Windows.Forms.Label lblPriceValue;
        private System.Windows.Forms.Label lblVendorValue;
        private System.Windows.Forms.LinkLabel linkLabelGitHub;
        private System.Windows.Forms.Label lblStoreName;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblStoreBankValue;
        private System.Windows.Forms.Label lblStoreBank;
    }
}

