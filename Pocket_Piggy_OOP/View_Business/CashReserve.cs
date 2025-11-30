using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using PocketPiggy.ViewModels;

namespace PocketPiggy
{
    public partial class CashReserve : Form
    {
        private decimal reserveGoal = 0m;
        private readonly BusinessTransactionViewModel _viewModel;

        public CashReserve()
        {
            InitializeComponent();
            _viewModel = new BusinessTransactionViewModel();
            LoadData();

            if (btnAdd != null) btnAdd.Click += btnAdd_Click;
            if (btnExport != null) btnExport.Click += btnAddReserve_Click;
            if (button1 != null) button1.Click += btnExportCSV_Click;
            if (btnEdit != null) btnEdit.Click += btnEdit_Click;
            if (btnDelete != null) btnDelete.Click += btnDelete_Click;

            if (dgvReserveTransactions != null)
            {
                dgvReserveTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReserveTransactions.MultiSelect = false;
                dgvReserveTransactions.CellClick += dgvReserveTransactions_CellClick;
            }
        }

        public CashReserve(int businessId) : this()
        {
            BusinessSession.CurrentBusinessId = businessId;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (!BusinessSession.IsLoggedIn)
                {
                    MessageBox.Show("Business session not found. Please log in again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int businessId = BusinessSession.CurrentBusinessId;
                string query = @"SELECT transaction_id, date, description, amount 
                                FROM business_transactions 
                                WHERE business_id = @business_id AND type = 'Cash Reserve' 
                                ORDER BY date ASC";
                DataTable dt = Database.ExecuteSelectQuery(query, new MySqlParameter("@business_id", businessId));

                if (dgvReserveTransactions != null)
                {
                    dgvReserveTransactions.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        int idx = dgvReserveTransactions.Rows.Add(
                            Convert.ToDateTime(row["date"]).ToShortDateString(),
                            row["description"].ToString(),
                            $"{Convert.ToDecimal(row["amount"]):C2}"
                        );
                        dgvReserveTransactions.Rows[idx].Tag = row["transaction_id"];
                    }
                }

                UpdateSummary();
                if (cReserve != null)
                {
                    UpdateChart(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary()
        {
            try
            {
                if (!BusinessSession.IsLoggedIn) return;

                int businessId = BusinessSession.CurrentBusinessId;
                decimal total = _viewModel.GetTotalByType(businessId, "Cash Reserve");

                if (lblTotalReserve != null)
                    lblTotalReserve.Text = $"Total Cash Reserve: {total:C2}";
                if (lblTargetReserve != null)
                    lblTargetReserve.Text = $"Target Reserve: {reserveGoal:C2}";
                decimal percent = reserveGoal > 0 ? (total / reserveGoal * 100) : 0;
                if (lblPercent != null)
                    lblPercent.Text = $"% of Reserve Reached: {percent:N2}%";

                if (pbarReserve != null)
                    pbarReserve.Value = (int)Math.Min(percent, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating summary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateChart(DataTable dt)
        {
            if (cReserve == null) return;

            cReserve.Series.Clear();
            var series = new Series("Reserve Trend")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3
            };

            foreach (DataRow row in dt.Rows)
            {
                series.Points.AddXY(
                    Convert.ToDateTime(row["date"]).ToShortDateString(),
                    Convert.ToDecimal(row["amount"])
                );
            }

            cReserve.Series.Add(series);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new reserve goal amount:", "Set Reserve Goal", reserveGoal.ToString());

            if (decimal.TryParse(input, out decimal newGoal) && newGoal > 0)
            {
                reserveGoal = newGoal;
                UpdateSummary();
                MessageBox.Show("Reserve goal updated successfully!", "Goal Set",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Invalid amount entered.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddReserve_Click(object sender, EventArgs e)
        {
            if (!BusinessSession.IsLoggedIn)
            {
                MessageBox.Show("Business session not found. Please log in again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Add a new reserve transaction?", "Add Reserve",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string description = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter description:", "Reserve Description", "Cash Deposit");

                if (string.IsNullOrWhiteSpace(description))
                {
                    MessageBox.Show("Description cannot be empty.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string amountInput = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter amount:", "Reserve Amount", "");

                if (decimal.TryParse(amountInput, out decimal amount) && amount > 0)
                {
                    int businessId = BusinessSession.CurrentBusinessId;
                    var (success, message) = _viewModel.AddTransaction(
                        businessId, 
                        DateTime.Now, 
                        description, 
                        amount, 
                        "Cash Reserve");

                    if (success)
                    {
                        MessageBox.Show(message, "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid amount entered. Please enter a positive number.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvReserveTransactions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvReserveTransactions.ClearSelection();
                dgvReserveTransactions.Rows[e.RowIndex].Selected = true;
                dgvReserveTransactions.CurrentCell = dgvReserveTransactions.Rows[e.RowIndex].Cells[0];
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReserveTransactions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a reserve transaction to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvReserveTransactions.SelectedRows[0];
            int id = Convert.ToInt32(selectedRow.Tag);

            string oldDesc = selectedRow.Cells[1].Value?.ToString();
            string oldAmt = selectedRow.Cells[2].Value?.ToString();

            string newDesc = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit description:", "Edit Reserve", oldDesc);

            string newAmountInput = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit amount:", "Edit Reserve", oldAmt);

            string newDateInput = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit date (MM/DD/YYYY):", "Edit Reserve", selectedRow.Cells[0].Value?.ToString());

            if (!BusinessSession.IsLoggedIn)
            {
                MessageBox.Show("Business session not found. Please log in again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (decimal.TryParse(newAmountInput, NumberStyles.Currency | NumberStyles.Number, CultureInfo.CurrentCulture, out decimal newAmount) &&
                DateTime.TryParse(newDateInput, out DateTime newDate))
            {
                int businessId = BusinessSession.CurrentBusinessId;
                string updateQuery = @"UPDATE business_transactions 
                                       SET description = @desc, amount = @amount, date = @date 
                                       WHERE transaction_id = @id AND business_id = @business_id";
                Database.ExecuteQuery(updateQuery,
                    new MySqlParameter("@desc", newDesc),
                    new MySqlParameter("@amount", newAmount),
                    new MySqlParameter("@date", newDate),
                    new MySqlParameter("@id", id),
                    new MySqlParameter("@business_id", businessId));

                MessageBox.Show("Cash Reserve transaction updated successfully!", "Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Invalid input. No changes made.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReserveTransactions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a reserve transaction to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvReserveTransactions.SelectedRows[0];
            int id = Convert.ToInt32(selectedRow.Tag);

            if (!BusinessSession.IsLoggedIn)
            {
                MessageBox.Show("Business session not found. Please log in again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this transaction?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int businessId = BusinessSession.CurrentBusinessId;
                string deleteQuery = @"DELETE FROM business_transactions 
                                      WHERE transaction_id = @id AND business_id = @business_id";
                Database.ExecuteQuery(deleteQuery, 
                    new MySqlParameter("@id", id),
                    new MySqlParameter("@business_id", businessId));

                MessageBox.Show("Cash Reserve transaction deleted successfully!", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "CashReserve.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new System.IO.StreamWriter(sfd.FileName))
                    {
                        writer.WriteLine("Date,Description,Amount");

                        foreach (DataGridViewRow row in dgvReserveTransactions.Rows)
                        {
                            string date = row.Cells[0].Value?.ToString();
                            string desc = row.Cells[1].Value?.ToString();
                            string amt = row.Cells[2].Value?.ToString();
                            writer.WriteLine($"{date},{desc},{amt}");
                        }
                    }
                    MessageBox.Show("CSV exported successfully!", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting CSV: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
