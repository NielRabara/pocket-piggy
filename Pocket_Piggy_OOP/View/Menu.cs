using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using PocketPiggy.View;
using System.IO;
using System.Text;
using System.Net.Http;
using PocketPiggy.Services;
using PocketPiggy.Repositories;

namespace PocketPiggy
{
    public partial class Menu : Form
    {
        private string username;

        public Menu(string user)
        {
            InitializeComponent();
            username = user;
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void LoadMonthlyTotals()
        {
            try
            {
                string incomeSql = @"SELECT IFNULL(SUM(amount),0) FROM transactions
                                      WHERE username=@u AND transaction_type='Income'
                                      AND YEAR(date)=YEAR(CURDATE()) AND MONTH(date)=MONTH(CURDATE())";
                string expensesSql = @"SELECT IFNULL(SUM(ABS(amount)),0) FROM transactions
                                        WHERE username=@u AND transaction_type='Expenses'
                                        AND YEAR(date)=YEAR(CURDATE()) AND MONTH(date)=MONTH(CURDATE())";

                var incDt = Database.GetData(incomeSql, new MySqlParameter("@u", username));
                var expDt = Database.GetData(expensesSql, new MySqlParameter("@u", username));
                decimal monthIncome = incDt.Rows.Count>0 && incDt.Rows[0][0]!=DBNull.Value ? Convert.ToDecimal(incDt.Rows[0][0]) : 0m;
                decimal monthExpenses = expDt.Rows.Count>0 && expDt.Rows[0][0]!=DBNull.Value ? Convert.ToDecimal(expDt.Rows[0][0]) : 0m;
                lblTotalIncome.Text = $"Total Income: ₱{monthIncome:N2}";
                lblTotalExpenses.Text = $"Total Expenses: ₱{monthExpenses:N2}";
            }
            catch
            {
                lblTotalIncome.Text = "Total Income: ₱0.00";
                lblTotalExpenses.Text = "Total Expenses: ₱0.00";
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            using (var admin = new Admin())
            {
                admin.ShowDialog(this);
            }
        }

        private void btnViewTransactions_Click(object? sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT id, transaction_type, amount, date, description, category, type
                                 FROM transactions
                                 WHERE username = @username
                                 ORDER BY date DESC
                                 LIMIT 100";
                DataTable dt = Database.GetData(query, new MySqlParameter("@username", username));

                Form dlg = new Form
                {
                    Text = "Transaction History",
                    Width = 800,
                    Height = 500,
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = dt
                };

                var panelTop = new Panel { Dock = DockStyle.Top, Height = 45 };
                var btnExport = new Button { Text = "Export CSV", Width = 120, Height = 28, Location = new Point(10, 8) };
                btnExport.Click += (s, args) =>
                {
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No transactions to export.");
                        return;
                    }
                    using (var sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = $"{username}_transactions.csv" })
                    {
                        if (sfd.ShowDialog(dlg) == DialogResult.OK)
                        {
                            try
                            {
                                using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                                {
                                    for (int i = 0; i < dt.Columns.Count; i++)
                                    {
                                        sw.Write(dt.Columns[i].ColumnName);
                                        if (i < dt.Columns.Count - 1) sw.Write(",");
                                    }
                                    sw.WriteLine();
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        for (int i = 0; i < dt.Columns.Count; i++)
                                        {
                                            var val = row[i]?.ToString()?.Replace("\"", "\"\"") ?? string.Empty;
                                            if (val.Contains(",")) val = "\"" + val + "\"";
                                            sw.Write(val);
                                            if (i < dt.Columns.Count - 1) sw.Write(",");
                                        }
                                        sw.WriteLine();
                                    }
                                }
                                MessageBox.Show("Exported to CSV successfully.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Export failed: {ex.Message}");
                            }
                        }
                    }
                };

                var btnAnalyze = new Button { Text = "Analyze (FinLlamma Analyzer)", Width = 200, Height = 28, Location = new Point(140, 8) };
                btnAnalyze.Click += async (s, args) =>
                {
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No transactions to analyze.");
                        return;
                    }

                    btnAnalyze.Text = "FinLlamma Analyzer is running...";
                    btnAnalyze.Enabled = false;

                    try
                    {
                        string reportText = await PythonFinLlamaService.GenerateSpendingInsightAsync((DataTable)grid.DataSource);

                        using (var reportForm = new PocketPiggy.View.FinLlamaReportForm(username, (DataTable)grid.DataSource, reportText))
                        {
                            reportForm.ShowDialog(dlg);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"FinLlamma Analyzer failed: {ex.Message}");
                    }
                    finally
                    {
                        btnAnalyze.Text = "Analyze (FinLlamma Analyzer)";
                        btnAnalyze.Enabled = true;
                    }
                };

                panelTop.Controls.Add(btnExport);
                panelTop.Controls.Add(btnAnalyze);
                dlg.Controls.Add(grid);
                dlg.Controls.Add(panelTop);
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load transactions: {ex.Message}");
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            lblGreeting.Text = $"Hello, {username}!";
            RefreshAllData();
            btnAdmin.Visible = (string.Equals(username, "20250001", StringComparison.OrdinalIgnoreCase)
                || string.Equals(username, "ShunAdmin", StringComparison.OrdinalIgnoreCase)
                || string.Equals(username, "RawrAdmin", StringComparison.OrdinalIgnoreCase)
                || string.Equals(username, "adminYato", StringComparison.OrdinalIgnoreCase));
        }

        public void RefreshAllData()
        {
            try
            {
                LoadCurrentBalance();
                LoadRecentTransactions();
                LoadMonthlyTotals();
                LoadProfilePicture();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Refresh Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadProfilePicture()
        {
            try
            {
                var (userId, name, profilePic) = ProfileRepository.GetPersonalProfileByUsername(username);
                if (profilePic != null && profilePic.Length > 0)
                {
                    using (var ms = new MemoryStream(profilePic))
                    {
                        picProfile.Image = Image.FromStream(ms);
                        picProfile.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    picProfile.Image = null;
                }
            }
            catch (Exception ex)
            {
                picProfile.Image = null;
            }
        }

        private void picProfile_Click(object sender, EventArgs e)
        {
            var dlg = new PersonalAccountSettings(username);
            dlg.ShowDialog(this);
            LoadProfilePicture();
        }

        private void btnChangeInfo_Click(object sender, EventArgs e)
        {
            var dlg = new PersonalAccountSettings(username);
            dlg.ShowDialog(this);
            LoadProfilePicture();
        }

        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var sb = new System.Text.StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSavings_Click(object sender, EventArgs e)
        {
            Savings savingsForm = new Savings(username);
            savingsForm.Show();
            this.Hide();
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            Bills billsForm = new Bills(username);
            billsForm.Show();
            this.Hide();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            Expenses expensesForm = new Expenses(username);
            expensesForm.Show();
            this.Hide();
        }


        private void btnIncome_Click(object sender, EventArgs e)
        {
            Income incomeForm = new Income(username);
            incomeForm.Show();
            this.Hide();
        }

        private void LoadCurrentBalance()
        {
            try
            {
                string query = @"SELECT 
                    (SELECT IFNULL(SUM(amount),0) FROM transactions WHERE username=@username AND transaction_type='Add Balance') +
                    (SELECT IFNULL(SUM(amount),0) FROM transactions WHERE username=@username AND transaction_type='Income') -
                    (SELECT IFNULL(SUM(ABS(amount)),0) FROM transactions WHERE username=@username AND transaction_type IN ('Allocation')) AS total_balance;";
                DataTable dt = Database.GetData(query, new MySqlParameter("@username", username));
                decimal total = Convert.ToDecimal(dt.Rows[0]["total_balance"]);
                lblCurrent.Text = $"Current: ₱{total:N2}";
            }
            catch
            {
                lblCurrent.Text = "Current: ₱0.00";
            }
        }

        private void LoadRecentTransactions()
        {
            try
            {
                string savingsQuery = @"SELECT amount, date FROM transactions WHERE username=@username AND transaction_type='Savings' ORDER BY date DESC LIMIT 3";
                DataTable savingsDt = Database.GetData(savingsQuery, new MySqlParameter("@username", username));
                UpdatePanelLabel(lblSavings, "Savings", savingsDt);

                string expensesQuery = @"SELECT amount, date FROM transactions WHERE username=@username AND transaction_type='Expenses' ORDER BY date DESC LIMIT 3";
                DataTable expensesDt = Database.GetData(expensesQuery, new MySqlParameter("@username", username));
                UpdatePanelLabel(lblExpenses, "Expenses", expensesDt);

                string billsQuery = @"SELECT amount, date FROM transactions WHERE username=@username AND transaction_type='Bills' ORDER BY date DESC LIMIT 3";
                DataTable billsDt = Database.GetData(billsQuery, new MySqlParameter("@username", username));
                UpdatePanelLabel(lblBills, "Bills", billsDt);

                string incomeQuery = @"SELECT amount, date FROM transactions WHERE username=@username AND transaction_type='Income' ORDER BY date DESC LIMIT 3";
                DataTable incomeDt = Database.GetData(incomeQuery, new MySqlParameter("@username", username));
                UpdatePanelLabel(lblIncome, "Income", incomeDt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdatePanelLabel(Label label, string title, DataTable data)
        {
            if (data.Rows.Count > 0)
            {
                string text = $"{title}\n\n";
                foreach (DataRow row in data.Rows)
                {
                    decimal amount = Convert.ToDecimal(row["amount"]);
                    string date = Convert.ToDateTime(row["date"]).ToString("MM/dd");
                    text += $"{date}: ₱{amount:N0}\n";
                }
                label.Text = text;
            }
            else
            {
                label.Text = $"{title}\n\nNo recent\ntransactions";
            }
        }
    }
}
