namespace PocketPiggy
{
    partial class Inventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            pInventory = new Panel();
            label1 = new Label();
            lblValue = new Label();
            lblItems = new Label();
            lblLow = new Label();
            lblTitle = new Label();
            btnExport = new Button();
            btnEdit = new Button();
            btnRestock = new Button();
            btnAdd = new Button();
            panel1 = new Panel();
            btnDelete = new Button();
            dgvReceivables = new DataGridView();
            cItemID = new DataGridViewTextBoxColumn();
            cName = new DataGridViewTextBoxColumn();
            cQuantity = new DataGridViewTextBoxColumn();
            cReorder = new DataGridViewTextBoxColumn();
            cCost = new DataGridViewTextBoxColumn();
            cValue = new DataGridViewTextBoxColumn();
            cSupplier = new DataGridViewTextBoxColumn();
            pInventory.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReceivables).BeginInit();
            SuspendLayout();
            // 
            // pInventory
            // 
            pInventory.Controls.Add(label1);
            pInventory.Controls.Add(lblValue);
            pInventory.Controls.Add(lblItems);
            pInventory.Controls.Add(lblLow);
            pInventory.Location = new Point(12, 12);
            pInventory.Name = "pInventory";
            pInventory.Size = new Size(572, 224);
            pInventory.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(261, 57);
            label1.Name = "label1";
            label1.Size = new Size(0, 32);
            label1.TabIndex = 4;
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValue.Location = new Point(4, 107);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(146, 32);
            lblValue.TabIndex = 3;
            lblValue.Text = "Total Value:";
            // 
            // lblItems
            // 
            lblItems.AutoSize = true;
            lblItems.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblItems.Location = new Point(4, 57);
            lblItems.Name = "lblItems";
            lblItems.Size = new Size(147, 32);
            lblItems.TabIndex = 2;
            lblItems.Text = "Total Items:";
            // 
            // lblLow
            // 
            lblLow.AutoSize = true;
            lblLow.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLow.Location = new Point(4, 165);
            lblLow.Name = "lblLow";
            lblLow.Size = new Size(205, 32);
            lblLow.TabIndex = 1;
            lblLow.Text = "Low Stock Items:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(142, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(329, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Inventory Overview";
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.LightPink;
            btnExport.FlatStyle = FlatStyle.Popup;
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExport.Location = new Point(234, 75);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(167, 69);
            btnExport.TabIndex = 11;
            btnExport.Text = "Export CSV";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightPink;
            btnEdit.FlatStyle = FlatStyle.Popup;
            btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEdit.Location = new Point(888, 16);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(167, 67);
            btnEdit.TabIndex = 10;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnRestock
            // 
            btnRestock.BackColor = Color.LightPink;
            btnRestock.FlatStyle = FlatStyle.Popup;
            btnRestock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRestock.Location = new Point(654, 91);
            btnRestock.Name = "btnRestock";
            btnRestock.Size = new Size(198, 69);
            btnRestock.TabIndex = 9;
            btnRestock.Text = "Restock";
            btnRestock.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightPink;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.Location = new Point(654, 16);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(198, 66);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add Item";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnExport);
            panel1.Location = new Point(654, 16);
            panel1.Name = "panel1";
            panel1.Size = new Size(401, 220);
            panel1.TabIndex = 12;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightPink;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.Location = new Point(115, 151);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(198, 69);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvReceivables
            // 
            dgvReceivables.AllowUserToAddRows = false;
            dgvReceivables.AllowUserToDeleteRows = false;
            dgvReceivables.BackgroundColor = Color.LightPink;
            dgvReceivables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReceivables.Columns.AddRange(new DataGridViewColumn[] { cItemID, cName, cQuantity, cReorder, cCost, cValue, cSupplier });
            dgvReceivables.Location = new Point(12, 268);
            dgvReceivables.Name = "dgvReceivables";
            dgvReceivables.ReadOnly = true;
            dgvReceivables.RowHeadersVisible = false;
            dgvReceivables.RowHeadersWidth = 62;
            dgvReceivables.Size = new Size(1064, 394);
            dgvReceivables.TabIndex = 13;
            // 
            // cItemID
            // 
            cItemID.HeaderText = "Item ID";
            cItemID.MinimumWidth = 8;
            cItemID.Name = "cItemID";
            cItemID.ReadOnly = true;
            cItemID.Width = 151;
            // 
            // cName
            // 
            cName.HeaderText = "Name";
            cName.MinimumWidth = 8;
            cName.Name = "cName";
            cName.ReadOnly = true;
            cName.Width = 150;
            // 
            // cQuantity
            // 
            cQuantity.HeaderText = "Quantity";
            cQuantity.MinimumWidth = 8;
            cQuantity.Name = "cQuantity";
            cQuantity.ReadOnly = true;
            cQuantity.Width = 150;
            // 
            // cReorder
            // 
            cReorder.HeaderText = "Reorder Level";
            cReorder.MinimumWidth = 8;
            cReorder.Name = "cReorder";
            cReorder.ReadOnly = true;
            cReorder.Width = 160;
            // 
            // cCost
            // 
            cCost.HeaderText = "Cost";
            cCost.MinimumWidth = 8;
            cCost.Name = "cCost";
            cCost.ReadOnly = true;
            cCost.Width = 150;
            // 
            // cValue
            // 
            cValue.HeaderText = "Total Value";
            cValue.MinimumWidth = 8;
            cValue.Name = "cValue";
            cValue.ReadOnly = true;
            cValue.Width = 150;
            // 
            // cSupplier
            // 
            cSupplier.HeaderText = "Supplier";
            cSupplier.MinimumWidth = 8;
            cSupplier.Name = "cSupplier";
            cSupplier.ReadOnly = true;
            cSupplier.Width = 150;
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1092, 674);
            Controls.Add(dgvReceivables);
            Controls.Add(btnEdit);
            Controls.Add(btnRestock);
            Controls.Add(lblTitle);
            Controls.Add(btnAdd);
            Controls.Add(panel1);
            Controls.Add(pInventory);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Inventory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inventory";
            pInventory.ResumeLayout(false);
            pInventory.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReceivables).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pInventory;
        private Label label1;
        private Label lblValue;
        private Label lblItems;
        private Label lblLow;
        private Label lblTitle;
        private Button btnExport;
        private Button btnEdit;
        private Button btnRestock;
        private Button btnAdd;
        private Panel panel1;
        private DataGridView dgvReceivables;
        private DataGridViewTextBoxColumn cItemID;
        private DataGridViewTextBoxColumn cName;
        private DataGridViewTextBoxColumn cQuantity;
        private DataGridViewTextBoxColumn cReorder;
        private DataGridViewTextBoxColumn cCost;
        private DataGridViewTextBoxColumn cValue;
        private DataGridViewTextBoxColumn cSupplier;
        private Button btnDelete;
    }
}