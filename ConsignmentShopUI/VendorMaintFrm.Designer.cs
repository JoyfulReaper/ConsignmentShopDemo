
namespace ConsignmentShopUI
{
    partial class VendorMaintFrm
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
            this.lblheaderText = new System.Windows.Forms.Label();
            this.lbFirstlName = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.lblCommison = new System.Windows.Forms.Label();
            this.textBoxCommison = new System.Windows.Forms.TextBox();
            this.listBoxVendors = new System.Windows.Forms.ListBox();
            this.btnAddVendor = new System.Windows.Forms.Button();
            this.lblPercent = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnItemDelete = new System.Windows.Forms.Button();
            this.groupBoxVendors = new System.Windows.Forms.GroupBox();
            this.textboxOwed = new System.Windows.Forms.TextBox();
            this.lblOwed = new System.Windows.Forms.Label();
            this.btnPayVendor = new System.Windows.Forms.Button();
            this.groupBoxVendors.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblheaderText
            // 
            this.lblheaderText.AutoSize = true;
            this.lblheaderText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblheaderText.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblheaderText.Location = new System.Drawing.Point(13, 9);
            this.lblheaderText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblheaderText.Name = "lblheaderText";
            this.lblheaderText.Size = new System.Drawing.Size(330, 37);
            this.lblheaderText.TabIndex = 2;
            this.lblheaderText.Text = "Vendor Maintenance";
            // 
            // lbFirstlName
            // 
            this.lbFirstlName.AutoSize = true;
            this.lbFirstlName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbFirstlName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbFirstlName.Location = new System.Drawing.Point(6, 34);
            this.lbFirstlName.Name = "lbFirstlName";
            this.lbFirstlName.Size = new System.Drawing.Size(101, 20);
            this.lbFirstlName.TabIndex = 9;
            this.lbFirstlName.Text = "First Name:";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(118, 31);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(290, 26);
            this.textBoxFirstName.TabIndex = 0;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLastName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblLastName.Location = new System.Drawing.Point(7, 71);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(100, 20);
            this.lblLastName.TabIndex = 11;
            this.lblLastName.Text = "Last Name:";
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(119, 68);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(289, 26);
            this.textBoxLastName.TabIndex = 1;
            // 
            // lblCommison
            // 
            this.lblCommison.AutoSize = true;
            this.lblCommison.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCommison.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCommison.Location = new System.Drawing.Point(10, 106);
            this.lblCommison.Name = "lblCommison";
            this.lblCommison.Size = new System.Drawing.Size(97, 20);
            this.lblCommison.TabIndex = 13;
            this.lblCommison.Text = "Commison:";
            // 
            // textBoxCommison
            // 
            this.textBoxCommison.Location = new System.Drawing.Point(119, 103);
            this.textBoxCommison.Name = "textBoxCommison";
            this.textBoxCommison.Size = new System.Drawing.Size(43, 26);
            this.textBoxCommison.TabIndex = 2;
            // 
            // listBoxVendors
            // 
            this.listBoxVendors.FormattingEnabled = true;
            this.listBoxVendors.ItemHeight = 20;
            this.listBoxVendors.Location = new System.Drawing.Point(449, 60);
            this.listBoxVendors.Name = "listBoxVendors";
            this.listBoxVendors.Size = new System.Drawing.Size(329, 184);
            this.listBoxVendors.TabIndex = 14;
            // 
            // btnAddVendor
            // 
            this.btnAddVendor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnAddVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVendor.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddVendor.Location = new System.Drawing.Point(119, 144);
            this.btnAddVendor.Name = "btnAddVendor";
            this.btnAddVendor.Size = new System.Drawing.Size(168, 48);
            this.btnAddVendor.TabIndex = 3;
            this.btnAddVendor.Text = "Add Vendor";
            this.btnAddVendor.UseVisualStyleBackColor = true;
            this.btnAddVendor.Click += new System.EventHandler(this.btnAddVendor_Click);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPercent.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblPercent.Location = new System.Drawing.Point(168, 106);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(24, 20);
            this.lblPercent.TabIndex = 16;
            this.lblPercent.Text = "%";
            // 
            // btnEdit
            // 
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnEdit.Location = new System.Drawing.Point(784, 60);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(146, 43);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Text = "Edit Selected";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnItemDelete
            // 
            this.btnItemDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnItemDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemDelete.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnItemDelete.Location = new System.Drawing.Point(784, 128);
            this.btnItemDelete.Name = "btnItemDelete";
            this.btnItemDelete.Size = new System.Drawing.Size(146, 43);
            this.btnItemDelete.TabIndex = 17;
            this.btnItemDelete.Text = "Remove Selected";
            this.btnItemDelete.UseVisualStyleBackColor = true;
            this.btnItemDelete.Click += new System.EventHandler(this.btnItemDelete_Click);
            // 
            // groupBoxVendors
            // 
            this.groupBoxVendors.Controls.Add(this.textboxOwed);
            this.groupBoxVendors.Controls.Add(this.lblOwed);
            this.groupBoxVendors.Controls.Add(this.lbFirstlName);
            this.groupBoxVendors.Controls.Add(this.textBoxFirstName);
            this.groupBoxVendors.Controls.Add(this.textBoxLastName);
            this.groupBoxVendors.Controls.Add(this.lblPercent);
            this.groupBoxVendors.Controls.Add(this.lblLastName);
            this.groupBoxVendors.Controls.Add(this.btnAddVendor);
            this.groupBoxVendors.Controls.Add(this.textBoxCommison);
            this.groupBoxVendors.Controls.Add(this.lblCommison);
            this.groupBoxVendors.Location = new System.Drawing.Point(12, 49);
            this.groupBoxVendors.Name = "groupBoxVendors";
            this.groupBoxVendors.Size = new System.Drawing.Size(419, 205);
            this.groupBoxVendors.TabIndex = 19;
            this.groupBoxVendors.TabStop = false;
            this.groupBoxVendors.Text = "New Vendor";
            // 
            // textboxOwed
            // 
            this.textboxOwed.Enabled = false;
            this.textboxOwed.Location = new System.Drawing.Point(328, 106);
            this.textboxOwed.Name = "textboxOwed";
            this.textboxOwed.Size = new System.Drawing.Size(80, 26);
            this.textboxOwed.TabIndex = 17;
            // 
            // lblOwed
            // 
            this.lblOwed.AutoSize = true;
            this.lblOwed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOwed.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblOwed.Location = new System.Drawing.Point(205, 109);
            this.lblOwed.Name = "lblOwed";
            this.lblOwed.Size = new System.Drawing.Size(126, 20);
            this.lblOwed.TabIndex = 18;
            this.lblOwed.Text = "Amount Owed:";
            // 
            // btnPayVendor
            // 
            this.btnPayVendor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aquamarine;
            this.btnPayVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayVendor.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnPayVendor.Location = new System.Drawing.Point(784, 196);
            this.btnPayVendor.Name = "btnPayVendor";
            this.btnPayVendor.Size = new System.Drawing.Size(146, 43);
            this.btnPayVendor.TabIndex = 18;
            this.btnPayVendor.Text = "Pay Vendor";
            this.btnPayVendor.UseVisualStyleBackColor = true;
            this.btnPayVendor.Click += new System.EventHandler(this.btnPayVendor_Click);
            // 
            // VendorMaintFrm
            // 
            this.AcceptButton = this.btnAddVendor;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(947, 268);
            this.Controls.Add(this.groupBoxVendors);
            this.Controls.Add(this.btnPayVendor);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnItemDelete);
            this.Controls.Add(this.listBoxVendors);
            this.Controls.Add(this.lblheaderText);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "VendorMaintFrm";
            this.Text = "VendorMaintFrm";
            this.Load += new System.EventHandler(this.VendorMaintFrm_Load);
            this.groupBoxVendors.ResumeLayout(false);
            this.groupBoxVendors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblheaderText;
        private System.Windows.Forms.Label lbFirstlName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Label lblCommison;
        private System.Windows.Forms.TextBox textBoxCommison;
        private System.Windows.Forms.ListBox listBoxVendors;
        private System.Windows.Forms.Button btnAddVendor;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnItemDelete;
        private System.Windows.Forms.GroupBox groupBoxVendors;
        private System.Windows.Forms.TextBox textboxOwed;
        private System.Windows.Forms.Label lblOwed;
        private System.Windows.Forms.Button btnPayVendor;
    }
}