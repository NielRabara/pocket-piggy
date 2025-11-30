using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PocketPiggy.Models;
using PocketPiggy.ViewModels;

namespace PocketPiggy
{
    public partial class BusinessExpenses : Form
    {
        private readonly int businessId;
        private readonly BusinessTransactionViewModel _vm = new BusinessTransactionViewModel();

        public BusinessExpenses(int businessId)
        {
            InitializeComponent();
            this.businessId = businessId;

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnMark.Click += btnMark_Click;

            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.MultiSelect = false;
            dgvExpenses.CellClick += dgvExpenses_CellClick;

            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = _vm.GetTransactions(businessId, "Expense");

            dgvExpenses.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["date"]);
                string category = row["category"] == DBNull.Value ? "N/A" : row["category"].ToString();
                string desc = row["description"].ToString();
                decimal amount = Convert.ToDecimal(row["amount"]);
                int id = Convert.ToInt32(row["transaction_id"]);

                int rowIndex = dgvExpenses.Rows.Add(
                    date.ToShortDateString(),
                    category,
                    desc,
                    $"{amount:C2}",
                    "N/A"
                );
                dgvExpenses.Rows[rowIndex].Tag = id;
            }

            UpdateSummary(dt);
            UpdateCharts(dt);
        }

        private void UpdateSummary(DataTable dt)
        {
            DateTime today = DateTime.Today;
            DateTime weekAhead = today.AddDays(7);

            var rows = dt.AsEnumerable();
            int dueThisWeek = rows.Count(r => {
                DateTime d = r.Field<DateTime>("date");
                return d >= today && d <= weekAhead;
            });
            int overdue = rows.Count(r => r.Field<DateTime>("date") < today);
            decimal outstanding = rows.Sum(r => r.Field<decimal>("amount"));

            lblDue.Text = $"Due This Week: {dueThisWeek}";
            lblOverdue.Text = $"Overdue Bills: {overdue}";
            lblOutstanding.Text = $"Total Outstanding: {outstanding:C2}";
        }

        private void UpdateCharts(DataTable dt)
        {
            UpdateBreakdownChart(dt);
            UpdateMonthlyChart(dt);
        }

        private void UpdateBreakdownChart(DataTable dt)
        {
            cExpenseBreakdown.Series.Clear();

            if (dt.Rows.Count == 0)
                return;

            var series = new Series("Expenses by Category")
            {
                ChartType = SeriesChartType.Pie
            };

            var grouped = dt.AsEnumerable()
                .GroupBy(r => r["category"] == DBNull.Value ? "N/A" : r.Field<string>("category"))
                .Select(g => new { Category = g.Key, Total = g.Sum(x => x.Field<decimal>("amount")) });

            foreach (var g in grouped)
                series.Points.AddXY(g.Category, g.Total);

            cExpenseBreakdown.Series.Add(series);
        }

        private void UpdateMonthlyChart(DataTable dt)
        {
            cMonthlyPayables.Series.Clear();

            if (dt.Rows.Count == 0)
                return;

            var series = new Series("Monthly Payables")
            {
                ChartType = SeriesChartType.Bar
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

            cMonthlyPayables.Series.Add(series);
        }

        private void dgvExpenses_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvExpenses.ClearSelection();
                dgvExpenses.Rows[e.RowIndex].Selected = true;
                dgvExpenses.CurrentCell = dgvExpenses.Rows[e.RowIndex].Cells[0];
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string category = Microsoft.VisualBasic.Interaction.InputBox("Enter expense category:", "Category", "");
            string description = Microsoft.VisualBasic.Interaction.InputBox("Enter description:", "Description", "");
            string amountInput = Microsoft.VisualBasic.Interaction.InputBox("Enter amount:", "Amount", "");
            string dueDateInput = Microsoft.VisualBasic.Interaction.InputBox("Enter due date (MM/DD/YYYY):", "Due Date", DateTime.Today.ToShortDateString());

            if (decimal.TryParse(amountInput, out decimal amount)
                && DateTime.TryParse(dueDateInput, out DateTime dueDate)
                && !string.IsNullOrWhiteSpace(category))
            {
                var (ok, msg) = _vm.AddTransaction(businessId, dueDate, description, amount, "Expense", category);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an expense to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvExpenses.SelectedRows[0];
            if (selectedRow.Tag is not int transactionId)
                return;

            string currentDesc = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
            string amountCell = selectedRow.Cells[3].Value?.ToString() ?? string.Empty;
            if (!decimal.TryParse(amountCell, NumberStyles.Currency, CultureInfo.CurrentCulture, out var currentAmount))
                decimal.TryParse(amountCell, NumberStyles.Any, CultureInfo.InvariantCulture, out currentAmount);

            string newDescription = Microsoft.VisualBasic.Interaction.InputBox("Edit description:", "Edit Expense", currentDesc);
            string newAmountInput = Microsoft.VisualBasic.Interaction.InputBox("Edit amount:", "Edit Expense", currentAmount.ToString());
            string newDateInput = Microsoft.VisualBasic.Interaction.InputBox("Edit date (MM/DD/YYYY):", "Edit Expense", selectedRow.Cells[0].Value?.ToString());

            if (decimal.TryParse(newAmountInput, out decimal newAmount)
                && DateTime.TryParse(newDateInput, out DateTime newDate))
            {
                var (ok, msg) = _vm.UpdateTransaction(transactionId, newDate, newDescription, newAmount);
                LoadData();
                MessageBox.Show(ok ? "Expense updated successfully!" : msg, ok ? "Updated" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Invalid input. No changes made.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an expense to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvExpenses.SelectedRows[0];
            if (selectedRow.Tag is not int transactionId)
                return;

            if (MessageBox.Show($"Delete selected expense?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (ok, msg) = _vm.DeleteTransaction(transactionId);
                LoadData();

                MessageBox.Show(ok ? "Expense deleted successfully!" : msg, ok ? "Deleted" : "Error",
                    MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mark as paid is not supported in the current schema.", "Not Supported",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
