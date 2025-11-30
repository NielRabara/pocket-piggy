using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PocketPiggy.Models;

namespace PocketPiggy
{
    public partial class KpiSummary : Form
    {
        private int SelectedBusinessId;

        public KpiSummary(int businessId)
        {
            InitializeComponent();
            SelectedBusinessId = businessId;
            SetupKpiLabels();

            cbDataRange.Items.AddRange(new string[] { "All Time", "This Month", "Last Month", "This Year" });
            cbDataRange.SelectedIndex = 0;
            cbDataRange.SelectedIndexChanged += (s, e) => LoadKpiData();
            LoadKpiData();
        }

        private void SetupKpiLabels()
        {
            Label lblIncome = new Label
            {
                Name = "lblIncome",
                Font = new Font("Century", 12F, FontStyle.Regular),
                Location = new Point(20, 50),
                AutoSize = true,
                Text = "Total Income: ₱0.00"
            };
            pProfitMargin.Controls.Add(lblIncome);

            Label lblExpenses = new Label
            {
                Name = "lblExpenses",
                Font = new Font("Century", 12F, FontStyle.Regular),
                Location = new Point(20, 90),
                AutoSize = true,
                Text = "Total Expenses: ₱0.00"
            };
            pIncomeGrowth.Controls.Add(lblExpenses);

            Label lblReceivables = new Label
            {
                Name = "lblReceivables",
                Font = new Font("Century", 12F, FontStyle.Regular),
                Location = new Point(20, 50),
                AutoSize = true,
                Text = "Outstanding Receivables: ₱0.00"
            };
            pExpenseRatio.Controls.Add(lblReceivables);

            Label lblNetProfit = new Label
            {
                Name = "lblNetProfit",
                Font = new Font("Century", 12F, FontStyle.Regular),
                Location = new Point(20, 90),
                AutoSize = true,
                Text = "Net Profit: ₱0.00"
            };
            pExpenseRatio.Controls.Add(lblNetProfit);
        }

        private void LoadKpiData()
        {
            var transactions = BusinessTransactioN.GetByBusinessId(SelectedBusinessId);
            if (transactions == null) return;

            DateTime startDate = DateTime.MinValue;
            switch (cbDataRange.SelectedItem.ToString())
            {
                case "This Month":
                    startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    break;
                case "Last Month":
                    var lastMonth = DateTime.Today.AddMonths(-1);
                    startDate = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                    break;
                case "This Year":
                    startDate = new DateTime(DateTime.Today.Year, 1, 1);
                    break;
            }

            var filtered = transactions.Where(t => t.Date >= startDate).ToList();

            decimal totalIncome = filtered.Where(t => t.Type == "Income").Sum(t => t.Amount);
            decimal totalExpense = filtered.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            decimal totalReceivable = filtered.Where(t => t.Type == "Receivable" && t.Status != "Paid").Sum(t => t.Amount);
            decimal netProfit = totalIncome - totalExpense;

            var lblIncome = pProfitMargin.Controls.Find("lblIncome", true).FirstOrDefault() as Label;
            var lblExpenses = pIncomeGrowth.Controls.Find("lblExpenses", true).FirstOrDefault() as Label;
            var lblReceivables = pExpenseRatio.Controls.Find("lblReceivables", true).FirstOrDefault() as Label;
            var lblNetProfit = pExpenseRatio.Controls.Find("lblNetProfit", true).FirstOrDefault() as Label;

            if (lblIncome != null) lblIncome.Text = $"Total Income: {totalIncome:C2}";
            if (lblExpenses != null) lblExpenses.Text = $"Total Expenses: {totalExpense:C2}";
            if (lblReceivables != null) lblReceivables.Text = $"Outstanding Receivables: {totalReceivable:C2}";
            if (lblNetProfit != null) lblNetProfit.Text = $"Net Profit: {netProfit:C2}";
        }
    }
}
