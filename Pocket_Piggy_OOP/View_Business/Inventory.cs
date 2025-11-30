using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PocketPiggy.Models; 

namespace PocketPiggy
{
    public partial class Inventory : Form
    {
        private int _currentBusinessId = 1;

        public Inventory()
        {
            InitializeComponent();

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnRestock.Click += btnRestock_Click;
            btnExport.Click += btnExport_Click;
            btnDelete.Click += btnDelete_Click;

            dgvReceivables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReceivables.MultiSelect = false;
            dgvReceivables.CellClick += dgvReceivables_CellClick;

            LoadInventory();
        }

        public Inventory(int businessId) : this()
        {
            _currentBusinessId = businessId;
            LoadInventory();
        }

        private void LoadInventory()
        {
            dgvReceivables.Rows.Clear();

            var items = InventoryModel.GetByBusinessId(_currentBusinessId)
                                      .OrderBy(i => i.ItemName)
                                      .ToList();

            foreach (var item in items)
            {
                int rowIndex = dgvReceivables.Rows.Add(
                    item.ItemId,
                    item.ItemName,
                    item.Quantity,
                    $"{item.CostPrice:C2}",
                    $"{item.SellingPrice:C2}",
                    $"{item.TotalValue:C2}",
                    item.DateAdded.ToShortDateString()
                );

                dgvReceivables.Rows[rowIndex].Tag = item;
            }

            UpdateSummary(items);
        }

        private void UpdateSummary(List<InventoryModel> items)
        {
            int totalItems = items.Sum(i => i.Quantity);
            decimal totalValue = items.Sum(i => i.TotalValue);

            lblItems.Text = $"Total Items: {totalItems}";
            lblValue.Text = $"Total Value: {totalValue:C2}";
            lblLow.Text = $"Low Stock Items: {items.Count(i => i.Quantity <= 5)}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter item name:", "Add Item", "");
            if (string.IsNullOrWhiteSpace(name)) return;

            string qtyInput = Microsoft.VisualBasic.Interaction.InputBox("Enter quantity:", "Quantity", "");
            string costInput = Microsoft.VisualBasic.Interaction.InputBox("Enter cost price:", "Cost", "");
            string sellInput = Microsoft.VisualBasic.Interaction.InputBox("Enter selling price:", "Selling Price", "");

            if (int.TryParse(qtyInput, out int qty) &&
                decimal.TryParse(costInput, out decimal cost) &&
                decimal.TryParse(sellInput, out decimal sell))
            {
                var item = new InventoryModel
                {
                    BusinessId = _currentBusinessId,
                    ItemName = name,
                    Quantity = qty,
                    CostPrice = cost,
                    SellingPrice = sell
                };
                item.Save();

                MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventory();
            }
            else
            {
                MessageBox.Show("Invalid input. Please check your entries.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReceivables.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = dgvReceivables.SelectedRows[0].Tag as InventoryModel;
            if (selectedItem == null) return;

            string name = Microsoft.VisualBasic.Interaction.InputBox("Edit item name:", "Edit Item", selectedItem.ItemName);
            string qtyInput = Microsoft.VisualBasic.Interaction.InputBox("Edit quantity:", "Quantity", selectedItem.Quantity.ToString());
            string costInput = Microsoft.VisualBasic.Interaction.InputBox("Edit cost price:", "Cost", selectedItem.CostPrice.ToString());
            string sellInput = Microsoft.VisualBasic.Interaction.InputBox("Edit selling price:", "Selling Price", selectedItem.SellingPrice.ToString());

            if (int.TryParse(qtyInput, out int qty) &&
                decimal.TryParse(costInput, out decimal cost) &&
                decimal.TryParse(sellInput, out decimal sell))
            {
                selectedItem.ItemName = name;
                selectedItem.Quantity = qty;
                selectedItem.CostPrice = cost;
                selectedItem.SellingPrice = sell;
                selectedItem.Update();

                MessageBox.Show("Item updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventory();
            }
            else
            {
                MessageBox.Show("Invalid input. Changes not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestock_Click(object sender, EventArgs e)
        {
            if (dgvReceivables.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to restock.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = dgvReceivables.SelectedRows[0].Tag as InventoryModel;
            if (selectedItem == null) return;

            string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter quantity to add for {selectedItem.ItemName}:", "Restock", "");
            if (int.TryParse(input, out int addQty) && addQty > 0)
            {
                selectedItem.Quantity += addQty;
                selectedItem.Update();
                MessageBox.Show($"{addQty} units added to {selectedItem.ItemName}.", "Restocked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventory();
            }
            else
            {
                MessageBox.Show("Invalid quantity entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReceivables.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = dgvReceivables.SelectedRows[0].Tag as InventoryModel;
            if (selectedItem == null) return;

            var confirm = MessageBox.Show($"Delete item '{selectedItem.ItemName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                InventoryModel.Delete(selectedItem.ItemId);
                MessageBox.Show("Item deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventory();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Export Inventory Data"
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                var items = InventoryModel.GetByBusinessId(_currentBusinessId).OrderBy(i => i.ItemName).ToList();

                using (StreamWriter writer = new StreamWriter(save.FileName))
                {
                    writer.WriteLine("Item ID,Name,Quantity,Cost Price,Selling Price,Total Value,Date Added");
                    foreach (var item in items)
                    {
                        writer.WriteLine($"{item.ItemId},{EscapeCsv(item.ItemName)},{item.Quantity},{item.CostPrice},{item.SellingPrice},{item.TotalValue},{item.DateAdded}");
                    }
                }

                MessageBox.Show("Inventory exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvReceivables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvReceivables.ClearSelection();
                dgvReceivables.Rows[e.RowIndex].Selected = true;
                dgvReceivables.CurrentCell = dgvReceivables.Rows[e.RowIndex].Cells[0];
            }
        }

        private static string EscapeCsv(string? s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Contains(',') || s.Contains('"') || s.Contains('\n') || s.Contains('\r'))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }
    }
}
