using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using System.IO;

namespace PocketPiggy
{
    public partial class Receivables : Form
    {
        private int SelectedBusinessId;

        public Receivables(int businessId)
        {
            InitializeComponent();
            SelectedBusinessId = businessId;

            LoadData();

            btnAdd.Click += btnAdd_Click;
            btnMark.Click += btnMark_Click;
            btnSend.Click += btnSend_Click;
            btnExport.Click += btnExport_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;

            dgvReceivables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReceivables.MultiSelect = false;
            dgvReceivables.CellClick += dgvReceivables_CellClick;
        }

        private void LoadData()
        {
            var receivables = BusinessTransactioN
                .GetByBusinessId(SelectedBusinessId)
                .Where(t => t.Type == "Receivable")
                .OrderBy(t => t.Date)
                .ToList();

            dgvReceivables.Rows.Clear();

            foreach (var r in receivables)
            {
                string status = r.Status == "Paid" ? "Paid" : (r.Date < DateTime.Today ? "Overdue" : "Pending");
                int daysOverdue = r.Status == "Paid" ? 0 : (r.Date < DateTime.Today ? (DateTime.Today - r.Date).Days : 0);

                int rowIndex = dgvReceivables.Rows.Add(
                    r.Vendor,
                    $"{r.Amount:C2}",
                    status,
                    r.Description,
                    r.Date.ToShortDateString(),
                    daysOverdue
                );

                dgvReceivables.Rows[rowIndex].Tag = r;
            }

            UpdateSummary(receivables);
        }

        private void UpdateSummary(List<BusinessTransactioN> receivables)
        {
            DateTime today = DateTime.Today;

            int overdue = receivables.Count(r => r.Status != "Paid" && r.Date < today);
            decimal outstanding = receivables.Where(r => r.Status != "Paid").Sum(r => r.Amount);

            double avgDays = receivables
                .Where(r => r.Status != "Paid")
                .Select(r => (today - r.Date).TotalDays)
                .DefaultIfEmpty(0)
                .Average();

            lblOverdue.Text = $"Overdue: {overdue}";
            lblOutstanding.Text = $"Total Outstanding: {outstanding:C2}";
            lblAverage.Text = $"Average Collection Period: {avgDays:F1} days";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customer = Microsoft.VisualBasic.Interaction.InputBox("Enter customer name:", "Customer", "");
            string invoice = Microsoft.VisualBasic.Interaction.InputBox("Enter invoice/description:", "Invoice", "");
            string amountInput = Microsoft.VisualBasic.Interaction.InputBox("Enter amount:", "Amount", "");
            string dueDateInput = Microsoft.VisualBasic.Interaction.InputBox("Enter due date (MM/DD/YYYY):", "Due Date", DateTime.Today.ToShortDateString());

            if (decimal.TryParse(amountInput, out decimal amount) && DateTime.TryParse(dueDateInput, out DateTime dueDate) && !string.IsNullOrWhiteSpace(customer))
            {
                var transaction = new BusinessTransactioN
                {
                    BusinessId = SelectedBusinessId,
                    Vendor = customer,
                    Description = invoice,
                    Amount = amount,
                    Type = "Receivable",
                    Date = dueDate,
                    Status = "Active"
                };
                transaction.Save();

                MessageBox.Show("Receivable added successfully!", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Invalid input. Please check your entries.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnMark_Click(object sender, EventArgs e)
        {
            if (dgvReceivables.SelectedRows.Count == 0) return;

            var transaction = dgvReceivables.SelectedRows[0].Tag as BusinessTransactioN;
            if (transaction == null) return;

            if (MessageBox.Show($"Mark invoice '{transaction.Description}' from {transaction.Vendor} as paid?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                transaction.Status = "Paid";
                string query = "UPDATE business_transactions SET status = @status WHERE transaction_id = @id";
                Database.ExecuteQuery(query,
                    new MySqlParameter("@status", transaction.Status),
                    new MySqlParameter("@id", transaction.TransactionId));

                LoadData();
                MessageBox.Show("Receivable marked as paid!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReceivables.SelectedRows.Count == 0) return;

            var transaction = dgvReceivables.SelectedRows[0].Tag as BusinessTransactioN;
            if (transaction == null) return;

            if (MessageBox.Show($"Delete receivable '{transaction.Description}' from {transaction.Vendor}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "DELETE FROM business_transactions WHERE transaction_id = @id";
                Database.ExecuteQuery(query, new MySqlParameter("@id", transaction.TransactionId));

                LoadData();
                MessageBox.Show("Receivable deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReceivables.SelectedRows.Count == 0) return;

            var transaction = dgvReceivables.SelectedRows[0].Tag as BusinessTransactioN;
            if (transaction == null) return;

            string newVendor = Microsoft.VisualBasic.Interaction.InputBox("Edit customer name:", "Edit Receivable", transaction.Vendor);
            string newInvoice = Microsoft.VisualBasic.Interaction.InputBox("Edit invoice/description:", "Edit Receivable", transaction.Description);
            string newAmountInput = Microsoft.VisualBasic.Interaction.InputBox("Edit amount:", "Edit Receivable", transaction.Amount.ToString());
            string newDateInput = Microsoft.VisualBasic.Interaction.InputBox("Edit due date (MM/DD/YYYY):", "Edit Receivable", transaction.Date.ToShortDateString());

            if (!decimal.TryParse(newAmountInput, out decimal newAmount) || !DateTime.TryParse(newDateInput, out DateTime newDate) || string.IsNullOrWhiteSpace(newVendor))
            {
                MessageBox.Show("Invalid input. Changes not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            transaction.Vendor = newVendor;
            transaction.Description = newInvoice;
            transaction.Amount = newAmount;
            transaction.Date = newDate;

            string query = @"UPDATE business_transactions 
                             SET vendor=@vendor, description=@desc, amount=@amount, date=@date 
                             WHERE transaction_id=@id";
            Database.ExecuteQuery(query,
                new MySqlParameter("@vendor", transaction.Vendor),
                new MySqlParameter("@desc", transaction.Description),
                new MySqlParameter("@amount", transaction.Amount),
                new MySqlParameter("@date", transaction.Date),
                new MySqlParameter("@id", transaction.TransactionId));

            LoadData();
            MessageBox.Show("Receivable updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dgvReceivables.SelectedRows.Count == 0) return;

            var transaction = dgvReceivables.SelectedRows[0].Tag as BusinessTransactioN;
            if (transaction == null) return;

            MessageBox.Show($"Reminder sent to {transaction.Vendor} for '{transaction.Description}' due on {transaction.Date:MM/dd/yyyy}.",
                "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Title = "Export Receivables Data"
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(save.FileName))
                {
                    writer.WriteLine("Customer,Amount,Status,Invoice,Due Date,Days Overdue");

                    var receivables = BusinessTransactioN.GetByBusinessId(SelectedBusinessId)
                        .Where(t => t.Type == "Receivable")
                        .OrderBy(t => t.Date)
                        .ToList();

                    foreach (var r in receivables)
                    {
                        int daysOverdue = r.Status == "Paid" ? 0 : (r.Date < DateTime.Today ? (DateTime.Today - r.Date).Days : 0);
                        string status = r.Status == "Paid" ? "Paid" : (r.Date < DateTime.Today ? "Overdue" : "Pending");
                        writer.WriteLine($"{EscapeCsv(r.Vendor)},{r.Amount},{status},{EscapeCsv(r.Description)},{r.Date:MM/dd/yyyy},{daysOverdue}");
                    }
                }

                MessageBox.Show("Receivables exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static string EscapeCsv(string? s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Contains(',') || s.Contains('"') || s.Contains('\n') || s.Contains('\r'))
            {
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }
    }
}
