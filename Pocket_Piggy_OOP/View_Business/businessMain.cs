using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using PocketPiggy.View_Business;
using PocketPiggy.ViewModels;

namespace PocketPiggy
{
    public partial class businessMain : Form
    {
        private string _businessUsername = string.Empty;
        private int _businessId;
        private string _businessName = string.Empty;
        private readonly BusinessDashboardViewModel _viewModel;

        public businessMain(string username)
        {
            InitializeComponent();
            _businessUsername = username;
            _viewModel = new BusinessDashboardViewModel();
            this.FormClosed += (s, e) => Application.Exit();
            LoadBusinessInfo();
            SetBusinessSession();
        }

        private void LoadBusinessInfo()
        {
            try
            {
                var (success, businessId, businessName) = _viewModel.GetBusinessInfo(_businessUsername);
                if (success)
                {
                    _businessId = businessId;
                    _businessName = businessName;
                }
                else
                {
                    MessageBox.Show("Business user not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading business info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetBusinessSession()
        {
            BusinessSession.CurrentBusinessId = _businessId;
            BusinessSession.CurrentBusinessName = _businessName;
        }

        public int CurrentBusinessId => _businessId;

        private void businessMain_Load(object sender, EventArgs e)
        {
            if (lblGreeting != null)
            {
                lblGreeting.Text = $"Welcome, {_businessName}!";
                AdjustGreetingToFit();
            }
            LoadRecentData();
        }

        private void btnChangeInfo_Click(object sender, EventArgs e)
        {
            using (var dlg = new BusinessAccountSettings(_businessId))
            {
                dlg.ShowDialog(this);
            }

            LoadBusinessInfo();
            SetBusinessSession();
            if (lblGreeting != null)
            {
                lblGreeting.Text = $"Welcome, {_businessName}!";
                AdjustGreetingToFit();
            }
        }

        private void AdjustGreetingToFit()
        {
            try
            {
                if (lblGreeting == null || string.IsNullOrWhiteSpace(lblGreeting.Text)) return;

                int leftPadding = 200; 
                int rightPadding = 40; 
                int maxWidth = Math.Max(100, this.ClientSize.Width - leftPadding - rightPadding);

                using (var g = lblGreeting.CreateGraphics())
                {
                    float fontSize = lblGreeting.Font.Size;
                    var fontFamily = lblGreeting.Font.FontFamily;
                    var fontStyle = lblGreeting.Font.Style;

                    SizeF size = g.MeasureString(lblGreeting.Text, lblGreeting.Font);
                    
                    lblGreeting.AutoSize = false;
                    lblGreeting.MaximumSize = new Size(maxWidth, 0);
                    lblGreeting.Width = maxWidth;

                    int safety = 0;
                    while (size.Width > maxWidth && fontSize > 8f && safety < 50)
                    {
                        fontSize -= 0.5f;
                        using (var tempFont = new Font(fontFamily, fontSize, fontStyle))
                        {
                            size = g.MeasureString(lblGreeting.Text, tempFont);
                            lblGreeting.Font = new Font(fontFamily, fontSize, fontStyle);
                        }
                        safety++;
                    }
                }
            }
            catch { /* non-fatal UI adjustment */ }
        }

        private void LoadRecentData()
        {
            try
            {
                LoadPanelData(reservePanel, "Cash Reserve");
                LoadPanelData(incomePanel, "Income");
                LoadPanelData(expensesPanel, "Expense");
                LoadPanelData(receivablesPanel, "Receivable");
                LoadInventoryData(inventoryPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recent data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPanelData(Panel panel, string type)
        {
            panel.Controls.Clear();

            if (!BusinessSession.IsLoggedIn)
            {
                Label empty = new Label
                {
                    Text = "No session found",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                panel.Controls.Add(empty);
                return;
            }

            int businessId = BusinessSession.CurrentBusinessId;
            string query = @"SELECT date, description, amount 
                             FROM business_transactions 
                             WHERE business_id = @business_id AND type = @type 
                             ORDER BY date DESC 
                             LIMIT 5;";

            DataTable dt = Database.ExecuteSelectQuery(query,
                new MySqlParameter("@business_id", businessId),
                new MySqlParameter("@type", type));

            if (dt.Rows.Count == 0)
            {
                Label empty = new Label
                {
                    Text = "No recent records",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                panel.Controls.Add(empty);
                return;
            }

            int y = 30;
            foreach (DataRow row in dt.Rows)
            {
                string date = Convert.ToDateTime(row["date"]).ToString("MM/dd/yyyy");
                string desc = Convert.ToString(row["description"]) ?? string.Empty;
                decimal amount = Convert.ToDecimal(row["amount"]);

                Label entry = new Label
                {
                    Text = $"{date} | {desc} | ₱{amount:N2}",
                    AutoSize = false,
                    Width = panel.Width - 10,
                    Height = 25,
                    Location = new Point(5, y),
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.Black
                };
                panel.Controls.Add(entry);
                y += 28;
            }
        }

        private void LoadInventoryData(Panel panel)
        {
            panel.Controls.Clear();

            if (!BusinessSession.IsLoggedIn)
            {
                Label empty = new Label
                {
                    Text = "No session found",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                panel.Controls.Add(empty);
                return;
            }

            int businessId = BusinessSession.CurrentBusinessId;
            string query = @"SELECT item_name, quantity, cost 
                             FROM inventory 
                             WHERE business_id = @business_id 
                             ORDER BY id DESC 
                             LIMIT 5;";

            DataTable dt = Database.ExecuteSelectQuery(query, new MySqlParameter("@business_id", businessId));

            if (dt.Rows.Count == 0)
            {
                Label empty = new Label
                {
                    Text = "No inventory items",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                panel.Controls.Add(empty);
                return;
            }

            int y = 30;
            foreach (DataRow row in dt.Rows)
            {
                string name = Convert.ToString(row["item_name"]) ?? string.Empty;
                int qty = Convert.ToInt32(row["quantity"]);
                decimal cost = Convert.ToDecimal(row["cost"]);

                Label entry = new Label
                {
                    Text = $"{name} | Qty: {qty} | ₱{cost:N2} each",
                    AutoSize = false,
                    Width = panel.Width - 10,
                    Height = 25,
                    Location = new Point(5, y),
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.Black
                };
                panel.Controls.Add(entry);
                y += 28;
            }
        }

        private string HashPassword(string password)
        {
            return SignUpBusinessViewModel.HashStatic(password);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Log Out",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BusinessSession.Clear();
                this.Hide();
                LogIn loginForm = new LogIn();
                loginForm.Show();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            int businessId = CurrentBusinessId;
            if (businessId == 0)
            {
                MessageBox.Show("No business record found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dashboardSummary dash = new dashboardSummary(businessId);
            dash.ShowDialog();
        }

        private void btnKpi_Click(object sender, EventArgs e)
        {
            int businessId = CurrentBusinessId;
            if (businessId == 0)
            {
                MessageBox.Show("No business record found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KpiSummary kpi = new KpiSummary(businessId);
            kpi.ShowDialog();
        }

        private void btnCashReserve_Click(object sender, EventArgs e)
        {
            int businessId = CurrentBusinessId;
            if (businessId == 0)
            {
                MessageBox.Show("No business record found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CashReserve reserve = new CashReserve(businessId);
            reserve.ShowDialog();
            LoadRecentData(); 
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            int businessId = CurrentBusinessId;
            if (businessId == 0)
            {
                MessageBox.Show("No business record found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BusinessIncome incomeForm = new BusinessIncome(businessId);
            incomeForm.ShowDialog();
            LoadRecentData();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            int businessId = CurrentBusinessId;
            if (businessId == 0)
            {
                MessageBox.Show("No business record found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BusinessExpenses exp = new BusinessExpenses(businessId);
            exp.ShowDialog();
            LoadRecentData();
        }

        private void btnReceivables_Click(object sender, EventArgs e)
        {
            int businessId = CurrentBusinessId;
            if (businessId == 0)
            {
                MessageBox.Show("No business record found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Receivables recv = new Receivables(businessId);
            recv.ShowDialog();
            LoadRecentData(); 
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            int businessId = CurrentBusinessId;
            if (businessId == 0)
            {
                MessageBox.Show("No business record found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Inventory inv = new Inventory(businessId);
            inv.ShowDialog();
            LoadRecentData();
        }
    }
}
