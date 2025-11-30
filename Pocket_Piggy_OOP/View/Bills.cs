using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;

namespace PocketPiggy.View
{
    public partial class Bills: Form
    {
        private string username;

        public Bills(string user)
        {
            InitializeComponent();
            username = user;
            LoadBillsData();
        }

        private void LoadBillsData()
        {
            try
            {
                lblUpBills.Text = "Upcoming Bills";
                lblPendingBills.Text = "Pending Bills";
                if (Controls.Find("flowUpcoming", true).FirstOrDefault() is FlowLayoutPanel fu)
                    fu.Controls.Clear();
                if (Controls.Find("flowPending", true).FirstOrDefault() is FlowLayoutPanel fp)
                    fp.Controls.Clear();

                var flowUpcoming = this.Controls.Find("flowUpcoming", true).FirstOrDefault() as FlowLayoutPanel;
                var flowPending = this.Controls.Find("flowPending", true).FirstOrDefault() as FlowLayoutPanel;

                string planQuery = @"SELECT id, amount, date, description, category
                                      FROM transactions
                                      WHERE username=@username AND transaction_type='BillPlan'
                                      ORDER BY date ASC";
                DataTable planDt = Database.GetData(planQuery, new MySqlParameter("@username", username));

                foreach (DataRow row in planDt.Rows)
                {
                    int planId = Convert.ToInt32(row["id"]);
                    decimal targetAmt = Convert.ToDecimal(row["amount"]);
                    DateTime due = Convert.ToDateTime(row["date"]);
                    string desc = row["description"]?.ToString() ?? "Bill";

                    string allocQ = @"SELECT IFNULL(SUM(amount),0) FROM allocations WHERE username=@username AND plan_type='BillPlan' AND plan_id=@pid";
                    DataTable allocDt = Database.GetData(allocQ,
                        new MySqlParameter("@username", username),
                        new MySqlParameter("@pid", planId));
                    decimal allocated = allocDt.Rows.Count > 0 && allocDt.Rows[0][0] != DBNull.Value ? Convert.ToDecimal(allocDt.Rows[0][0]) : 0m;
                    decimal progress = targetAmt == 0 ? 0 : Math.Min(100m, (allocated / targetAmt) * 100m);

                    var panel = BuildBillItemPanel(desc, due, targetAmt, allocated, progress, () => AllocateToPlan(planId, "BillPlan", targetAmt));

                    if (due >= DateTime.Now)
                        flowUpcoming?.Controls.Add(panel);
                    else
                        flowPending?.Controls.Add(panel);
                }

                UpdateBillsChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bills data: {ex.Message}");
            }
        }

        private void UpdateBillsChart()
        {
            try { } catch { }
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            Form addBillForm = new Form()
            {
                Width = 400,
                Height = 350,
                Text = "Add New Bill",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            Label lblBillName = new Label()
            {
                Text = "Bill Name:",
                Location = new Point(20, 20),
                Size = new Size(80, 20)
            };
            TextBox txtBillName = new TextBox()
            {
                Location = new Point(110, 18),
                Size = new Size(200, 25),
                PlaceholderText = "e.g., Electricity Bill"
            };
            
            Label lblAmount = new Label()
            {
                Text = "Amount:",
                Location = new Point(20, 60),
                Size = new Size(80, 20)
            };
            TextBox txtAmount = new TextBox()
            {
                Location = new Point(110, 58),
                Size = new Size(200, 25),
                PlaceholderText = "Enter amount"
            };
            
            Label lblDueDate = new Label()
            {
                Text = "Due Date:",
                Location = new Point(20, 100),
                Size = new Size(80, 20)
            };
            DateTimePicker dtpDueDate = new DateTimePicker()
            {
                Location = new Point(110, 98),
                Size = new Size(200, 25),
                Format = DateTimePickerFormat.Short
            };
            
            Label lblCategory = new Label()
            {
                Text = "Bill Type:",
                Location = new Point(20, 140),
                Size = new Size(80, 20)
            };
            ComboBox cmbCategory = new ComboBox()
            {
                Location = new Point(110, 138),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.Items.AddRange(new string[] {
                "Utilities", "Internet", "Phone", "Insurance", "Rent/Mortgage",
                "Credit Card", "Loan Payment", "Other"
            });
            cmbCategory.SelectedIndex = 0;
            
            CheckBox chkRecurring = new CheckBox()
            {
                Text = "Recurring Bill",
                Location = new Point(20, 180),
                Size = new Size(120, 20)
            };
            
            Button btnSave = new Button()
            {
                Text = "Add Bill",
                Location = new Point(20, 220),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(130, 220),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            btnSave.Click += (s, args) =>
            {
                if (decimal.TryParse(txtAmount.Text, out decimal amount) && amount > 0)
                {
                    try
                    {
                        string billName = string.IsNullOrEmpty(txtBillName.Text) ? "Bill Payment" : txtBillName.Text;
                        string description = $"{billName} (Due: {dtpDueDate.Value:MM/dd/yyyy})";
                        
                        string query = "INSERT INTO transactions (username, transaction_type, amount, date, description, category) VALUES (@username, 'BillPlan', @amount, @date, @description, @category)";
                        Database.ExecuteQuery(query,
                            new MySqlParameter("@username", username),
                            new MySqlParameter("@amount", amount),
                            new MySqlParameter("@date", dtpDueDate.Value),
                            new MySqlParameter("@description", description),
                            new MySqlParameter("@category", cmbCategory.SelectedItem?.ToString() ?? "Other"));

                        LoadBillsData();
                        MessageBox.Show($"Bill of ₱{amount:N2} recorded. Use Allocate to fund this bill.", "Saved",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        addBillForm.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding bill: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid amount greater than 0.", "Invalid Amount",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            btnCancel.Click += (s, args) => addBillForm.Close();
            
            addBillForm.Controls.AddRange(new Control[] {
                lblBillName, txtBillName,
                lblAmount, txtAmount,
                lblDueDate, dtpDueDate,
                lblCategory, cmbCategory,
                chkRecurring,
                btnSave, btnCancel
            });

            addBillForm.ShowDialog();
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(username);
            menu.Show();
            this.Close();
        }
        
        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT amount, date, description, category 
                                FROM transactions 
                                WHERE username = @username AND transaction_type = 'Bills' 
                                ORDER BY date DESC";
                DataTable dt = Database.GetData(query, new MySqlParameter("@username", username));
                
                if (dt.Rows.Count > 0)
                {
                    string history = "Bill History:\n\n";
                    decimal totalBills = 0;
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal amount = Convert.ToDecimal(row["amount"]);
                        string date = Convert.ToDateTime(row["date"]).ToString("MM/dd/yyyy");
                        string description = row["description"]?.ToString() ?? "Bill Payment";
                        string category = row["category"]?.ToString() ?? "Other";
                        
                        totalBills += Math.Abs(amount);
                        history += $"{date} - {description} ({category}): ₱{Math.Abs(amount):N2}\n";
                    }
                    
                    history += $"\nTotal Bills: ₱{totalBills:N2}";
                    
                    MessageBox.Show(history, "Bill History", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No bills found.", "Bill History", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bill history: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    private Panel BuildBillItemPanel(string description, DateTime due, decimal target, decimal allocated, decimal progressPercent, Action onAllocate)
        {
            var container = new Panel
            {
                Width = 350,
                Height = 90,
                Margin = new Padding(5),
                BackColor = Color.FromArgb(245, 245, 245)
            };

            var lbl = new Label
            {
                AutoSize = false,
                Text = $"{description}\nDue: {due:MM/dd/yyyy}  Amount: ₱{target:N2}",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Location = new Point(8, 8),
                Size = new Size(330, 34)
            };

            var progress = new ProgressBar
            {
                Location = new Point(8, 45),
                Size = new Size(230, 18),
                Value = (int)Math.Round(progressPercent)
            };

            var btn = new Button
            {
                Text = "Allocate",
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(245, 40),
                Size = new Size(90, 28)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onAllocate();

            var lblAmt = new Label
            {
                AutoSize = true,
                Location = new Point(8, 68),
                ForeColor = Color.FromArgb(80,80,80),
                Text = $"Allocated: ₱{allocated:N2} / ₱{target:N2} ({progressPercent:0}% )"
            };

            container.Controls.Add(lbl);
            container.Controls.Add(progress);
            container.Controls.Add(btn);
            container.Controls.Add(lblAmt);
            return container;
        }

        private void AllocateToPlan(int planId, string planType, decimal target)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter amount to allocate (target ₱{target:N2}):", "Allocate", "0");
            if (!decimal.TryParse(input, out decimal amt) || amt <= 0)
            {
                return;
            }

            try
            {
                Database.ExecuteQuery(@"CREATE TABLE IF NOT EXISTS allocations (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    username VARCHAR(100) NOT NULL,
                    plan_type VARCHAR(50) NOT NULL,
                    plan_id INT NOT NULL,
                    amount DECIMAL(12,2) NOT NULL,
                    date DATETIME NOT NULL DEFAULT NOW()
                )");
                
                string balSql = @"SELECT 
                    (SELECT IFNULL(SUM(amount),0) FROM transactions WHERE username=@u AND transaction_type='Add Balance') +
                    (SELECT IFNULL(SUM(amount),0) FROM transactions WHERE username=@u AND transaction_type='Income') -
                    (SELECT IFNULL(SUM(ABS(amount)),0) FROM transactions WHERE username=@u AND transaction_type IN ('Allocation')) AS total_balance;";
                var balDt = Database.GetData(balSql, new MySqlParameter("@u", username));
                decimal available = (balDt.Rows.Count > 0 && balDt.Rows[0][0] != DBNull.Value)
                    ? Convert.ToDecimal(balDt.Rows[0][0])
                    : 0m;

                if (amt > available)
                {
                    MessageBox.Show($"Insufficient amount. Available: ₱{available:N2}", "Allocation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                string insAlloc = @"INSERT INTO allocations (username, plan_type, plan_id, amount, date) VALUES (@u, @t, @pid, @amt, NOW())";
                Database.ExecuteQuery(insAlloc,
                    new MySqlParameter("@u", username),
                    new MySqlParameter("@t", planType),
                    new MySqlParameter("@pid", planId),
                    new MySqlParameter("@amt", amt));

                string insTxn = @"INSERT INTO transactions (username, transaction_type, amount, date, description, category)
                                  VALUES (@u, 'Allocation', @negAmt, NOW(), CONCAT('Allocation to ', @t, ' #', @pid), 'Allocation')";
                Database.ExecuteQuery(insTxn,
                    new MySqlParameter("@u", username),
                    new MySqlParameter("@t", planType),
                    new MySqlParameter("@pid", planId),
                    new MySqlParameter("@negAmt", -Math.Abs(amt)));

                LoadBillsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Allocation failed: {ex.Message}");
            }
        }
    }
}
