using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PocketPiggy.Models; 
using PocketPiggy.ViewModels;

namespace PocketPiggy
{
    public partial class BusinessIncome : Form
    {
        private int businessId;
        private readonly BusinessTransactionViewModel _vm = new BusinessTransactionViewModel();

        public BusinessIncome(int businessId)
        {
            InitializeComponent();
            this.businessId = businessId;

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnExport.Click += btnExport_Click;

            dgvIncome.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIncome.MultiSelect = false;
            dgvIncome.CellClick += dgvIncome_CellClick;

            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = _vm.GetTransactions(businessId, "Income");

            dgvIncome.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["date"]);
                string category = row["category"] == DBNull.Value ? "N/A" : row["category"].ToString();
                string desc = row["description"].ToString();
                decimal amount = Convert.ToDecimal(row["amount"]);
                int id = Convert.ToInt32(row["transaction_id"]);

                int rowIndex = dgvIncome.Rows.Add(
                    date.ToShortDateString(),
                    category,
                    desc,
                    $"{amount:C2}"
                );
                dgvIncome.Rows[rowIndex].Tag = id;
            }

            UpdateSummary(dt);
            UpdateChart(dt);
        }

        private void UpdateSummary(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                lblAverage.Text = "Average Monthly Growth: ₱0.00";
                lblIncome.Text = "Top Income (Month): ₱0.00";
                lblSource.Text = "Top Income Source: N/A";
                return;
            }

            var rows = dt.AsEnumerable();
            var monthlyTotals = rows
                .GroupBy(r => new { Y = r.Field<DateTime>("date").Year, M = r.Field<DateTime>("date").Month })
                .Select(g => g.Sum(x => x.Field<decimal>("amount")))
                .ToList();

            decimal average = monthlyTotals.Any() ? monthlyTotals.Average() : 0m;
            decimal topMonth = monthlyTotals.Any() ? monthlyTotals.Max() : 0m;
            string topSource = rows
                .GroupBy(r => r["category"] == DBNull.Value ? "N/A" : r.Field<string>("category"))
                .OrderByDescending(g => g.Sum(x => x.Field<decimal>("amount")))
                .First().Key;

            lblAverage.Text = $"Average Monthly Growth: {average:C2}";
            lblIncome.Text = $"Top Income (Month): {topMonth:C2}";
            lblSource.Text = $"Top Income Source: {topSource}";
        }

        private void UpdateChart(DataTable dt)
        {
            cReserve.Series.Clear();
            var series = new Series("Income Trend")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3
            };

            var grouped = dt.AsEnumerable()
                .GroupBy(r => new { Y = r.Field<DateTime>("date").Year, M = r.Field<DateTime>("date").Month })
                .Select(g => new
                {
                    Month = $"{g.Key.M}/{g.Key.Y}",
                    Total = g.Sum(x => x.Field<decimal>("amount"))
                })
                .OrderBy(g => g.Month);

            foreach (var g in grouped)
                series.Points.AddXY(g.Month, g.Total);

            cReserve.Series.Add(series);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Add new income entry?", "Add Income",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string category = Microsoft.VisualBasic.Interaction.InputBox("Enter income category:", "Category", "");
                string description = Microsoft.VisualBasic.Interaction.InputBox("Enter description:", "Description", "");
                string amountInput = Microsoft.VisualBasic.Interaction.InputBox("Enter amount:", "Amount", "");
                string dateInput = Microsoft.VisualBasic.Interaction.InputBox("Enter date (MM/DD/YYYY):", "Date", DateTime.Today.ToShortDateString());

                if (decimal.TryParse(amountInput, out decimal amount)
                    && DateTime.TryParse(dateInput, out DateTime date)
                    && !string.IsNullOrWhiteSpace(category))
                {
                    var (ok, msg) = _vm.AddTransaction(businessId, date, description, amount, "Income", category);
                    LoadData();
                    MessageBox.Show(ok ? msg : msg, ok ? "Added" : "Error",
                        MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Invalid input. Please check your entries.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvIncome_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvIncome.ClearSelection();
                dgvIncome.Rows[e.RowIndex].Selected = true;
                dgvIncome.CurrentCell = dgvIncome.Rows[e.RowIndex].Cells[0];
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvIncome.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an income record to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvIncome.SelectedRows[0];
            if (selectedRow.Tag is not int transactionId)
            {
                MessageBox.Show("Selected record could not be mapped to data.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string currentDesc = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
            string amountCell = selectedRow.Cells[3].Value?.ToString() ?? string.Empty;
            if (!decimal.TryParse(amountCell, NumberStyles.Currency, CultureInfo.CurrentCulture, out var currentAmount))
                decimal.TryParse(amountCell, NumberStyles.Any, CultureInfo.InvariantCulture, out currentAmount);

            string newDesc = Microsoft.VisualBasic.Interaction.InputBox("Edit description:", "Edit", currentDesc);
            string newAmount = Microsoft.VisualBasic.Interaction.InputBox("Edit amount:", "Edit", currentAmount.ToString());
            string newDateStr = Microsoft.VisualBasic.Interaction.InputBox("Edit date (MM/DD/YYYY):", "Edit", selectedRow.Cells[0].Value?.ToString());

            if (decimal.TryParse(newAmount, out decimal updatedAmount) && DateTime.TryParse(newDateStr, out DateTime newDate))
            {
                var (ok, msg) = _vm.UpdateTransaction(transactionId, newDate, newDesc, updatedAmount);
                LoadData();
                MessageBox.Show(ok ? "Income record updated!" : msg, ok ? "Success" : "Error",
                    MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Invalid amount entered.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvIncome.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an income record to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvIncome.SelectedRows[0];
            if (selectedRow.Tag is not int transactionId)
            {
                MessageBox.Show("Selected record could not be mapped to data.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Delete selected income record?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (ok, msg) = _vm.DeleteTransaction(transactionId);
                LoadData();
                MessageBox.Show(ok ? "Income deleted successfully." : msg, ok ? "Deleted" : "Error",
                    MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "Income.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                DataTable dt = _vm.GetTransactions(businessId, "Income");
                using var writer = new StreamWriter(sfd.FileName);
                writer.WriteLine("Date,Source,Description,Amount");

                foreach (DataRow row in dt.Rows)
                {
                    DateTime date = Convert.ToDateTime(row["date"]);
                    string source = row["category"] == DBNull.Value ? "" : row["category"].ToString();
                    string desc = row["description"].ToString();
                    decimal amount = Convert.ToDecimal(row["amount"]);
                    writer.WriteLine($"{date:MM/dd/yyyy},{EscapeCsv(source)},{EscapeCsv(desc)},{amount}");
                }

                MessageBox.Show("CSV exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting CSV: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
