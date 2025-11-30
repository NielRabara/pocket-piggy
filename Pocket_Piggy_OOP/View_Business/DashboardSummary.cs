using System;
using System.Linq;
using System.Windows.Forms;
using PocketPiggy.Models;
using System.Collections.Generic;

namespace PocketPiggy
{
    public partial class dashboardSummary : Form
    {
        private int currentBusinessId; 

        public dashboardSummary(int businessId)
        {
            InitializeComponent();
            currentBusinessId = businessId;

            cbDataRange.Items.AddRange(new string[]
            {
                "All Time",
                "This Month",
                "Last Month",
                "This Year"
            });
            cbDataRange.SelectedIndex = 0;

            cbDataRange.SelectedIndexChanged += (s, e) => LoadDashboardData();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                List<BusinessTransactioN> transactions = BusinessTransactioN.GetByBusinessId(currentBusinessId);

                DateTime now = DateTime.Now;
                IEnumerable<BusinessTransactioN> filtered = transactions;

                switch (cbDataRange.SelectedItem?.ToString())
                {
                    case "This Month":
                        filtered = filtered.Where(t => t.Date.Month == now.Month && t.Date.Year == now.Year);
                        break;
                    case "Last Month":
                        var lastMonth = now.AddMonths(-1);
                        filtered = filtered.Where(t => t.Date.Month == lastMonth.Month && t.Date.Year == lastMonth.Year);
                        break;
                    case "This Year":
                        filtered = filtered.Where(t => t.Date.Year == now.Year);
                        break;
                    case "All Time":
                    default:
                        break;
                }

                decimal totalIncome = filtered.Where(t => t.Type == "Income").Sum(t => t.Amount);
                decimal totalExpenses = filtered.Where(t => t.Type == "Expense").Sum(t => t.Amount);
                decimal totalCashReserve = filtered.Where(t => t.Type == "Cash Reserve").Sum(t => t.Amount);

                decimal netProfit = totalIncome - totalExpenses;
                decimal balance = (totalIncome - totalExpenses) + totalCashReserve;
                decimal cashFlow = totalIncome - totalExpenses;

                lblTotalIncome.Text = $"Total Income: {totalIncome:C2}";
                lblTotalExpenses.Text = $"Total Expenses: {totalExpenses:C2}";
                lblNetProfit.Text = $"Net Profit: {netProfit:C2}";
                lblCurrent.Text = $"Current Balance: {balance:C2}";
                lblCashFlow.Text = $"Cash Flow: {cashFlow:C2}";
                lblCashReserve.Text = $"Cash Reserve: {totalCashReserve:C2}";

                var upcomingBills = transactions
                    .Where(t => t.Type == "Expense" && t.Date > DateTime.Now)
                    .OrderBy(t => t.Date)
                    .Take(3)
                    .ToList();

                if (upcomingBills.Count == 0)
                    lblUpcomingBills.Text = "Upcoming Bills: None";
                else
                {
                    lblUpcomingBills.Text = "Upcoming Bills:\n" +
                        string.Join("\n", upcomingBills.Select(t =>
                            $"{t.Date.ToShortDateString()} - {t.Description} ({t.Amount:C2})"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKpiSummary_Click(object sender, EventArgs e)
        {
            var kpiForm = new KpiSummary(currentBusinessId);
            kpiForm.ShowDialog();
        }
    }
}
