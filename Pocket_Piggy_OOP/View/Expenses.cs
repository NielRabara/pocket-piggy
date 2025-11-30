using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;

namespace PocketPiggy.View
{
    public partial class Expenses : Form
    {
        private readonly string username;

        public Expenses(string user)
        {
            InitializeComponent();
            username = user;
            LoadExpensesData();
        }

        private void LoadExpensesData()
        {
            try
            {
                string totalQuery = @"SELECT SUM(ABS(amount)) 
                                      FROM transactions 
                                      WHERE username=@username 
                                      AND transaction_type='Expenses'";
                DataTable totalDt = Database.GetData(totalQuery, new MySqlParameter("@username", username));
                decimal totalExpenses = totalDt.Rows.Count > 0 && totalDt.Rows[0][0] != DBNull.Value
                    ? Convert.ToDecimal(totalDt.Rows[0][0])
                    : 0;
                lblTotalExpenses.Text = $"Total Expenses: ₱{totalExpenses:N2}";

                string planQuery = @"SELECT id, amount, date, description, category, type
                                     FROM transactions
                                     WHERE username=@username 
                                     AND transaction_type='ExpensePlan'
                                     ORDER BY date DESC LIMIT 20";
                DataTable dt = Database.GetData(planQuery, new MySqlParameter("@username", username));

                if (dt.Rows.Count > 0)
                {
                    lblRecentExpenses.Text = "Planned Expenses:";

                    var parentPanel = panelExpensesList;

                    if (parentPanel == null)
                    {
                        MessageBox.Show("Error: Expenses panel is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (Control c in parentPanel.Controls.OfType<Panel>().ToList())
                        parentPanel.Controls.Remove(c);


                    int y = lblRecentExpenses.Bottom + 8;
                    foreach (DataRow row in dt.Rows)
                    {
                        int planId = Convert.ToInt32(row["id"]);
                        decimal targetAmt = Convert.ToDecimal(row["amount"]);
                        DateTime date = Convert.ToDateTime(row["date"]);
                        string description = row["description"]?.ToString() ?? "Expense";
                        string category = row["category"]?.ToString() ?? "Other";
                        string type = row["type"]?.ToString() ?? "Essential";

                        string allocQ = @"SELECT IFNULL(SUM(amount),0) 
                                          FROM allocations 
                                          WHERE username=@username 
                                          AND plan_type='ExpensePlan' 
                                          AND plan_id=@pid";
                        DataTable allocDt = Database.GetData(allocQ,
                            new MySqlParameter("@username", username),
                            new MySqlParameter("@pid", planId));
                        decimal allocated = allocDt.Rows.Count > 0 && allocDt.Rows[0][0] != DBNull.Value
                            ? Convert.ToDecimal(allocDt.Rows[0][0])
                            : 0m;
                        decimal progress = targetAmt == 0 ? 0 : Math.Min(100m, (allocated / targetAmt) * 100m);

                        var panel = BuildItemPanel(
                            $"{description} ({date:MM/dd})",
                            targetAmt, allocated, progress,
                            type, category,
                            () => AllocateToPlan(planId, "ExpensePlan", targetAmt)
                        );

                        panel.Location = new Point(16, y);
                        parentPanel.Controls.Add(panel);
                        y += panel.Height + 6;
                    }
                }
                else
                {
                    lblRecentExpenses.Text = "No planned expenses";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expenses data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            Form addExpenseForm = new Form()
            {
                Width = 420,
                Height = 400,
                Text = "Add New Expense Plan",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            Label lblAmount = new Label() { Text = "Amount:", Location = new Point(20, 20), Size = new Size(80, 20) };
            TextBox txtAmount = new TextBox() { Location = new Point(110, 18), Size = new Size(200, 25), PlaceholderText = "Enter amount" };
            
            Label lblDescription = new Label() { Text = "Description:", Location = new Point(20, 60), Size = new Size(80, 20) };
            TextBox txtDescription = new TextBox() { Location = new Point(110, 58), Size = new Size(200, 25), PlaceholderText = "Enter description" };

            Label lblCategory = new Label() { Text = "Category:", Location = new Point(20, 100), Size = new Size(80, 20) };
            ComboBox cmbCategory = new ComboBox()
            {
                Location = new Point(110, 98),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.Items.AddRange(new string[]
            {
                "Food & Dining", "Transportation", "Entertainment", "Healthcare",
                "Education", "Shopping", "Utilities", "Housing", "Insurance", "Other"
            });
            cmbCategory.SelectedIndex = 0;
            
            Label lblType = new Label() { Text = "Type:", Location = new Point(20, 140), Size = new Size(80, 20) };
            ComboBox cmbType = new ComboBox()
            {
                Location = new Point(110, 138),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbType.Items.AddRange(new string[] { "Essential", "Non-Essential" });
            cmbType.SelectedIndex = 0;
            
            Label lblDate = new Label() { Text = "Date:", Location = new Point(20, 180), Size = new Size(80, 20) };
            DateTimePicker dtpDate = new DateTimePicker()
            {
                Location = new Point(110, 178),
                Size = new Size(200, 25),
                Format = DateTimePickerFormat.Short
            };

            Button btnSave = new Button()
            {
                Text = "Add Expense",
                Location = new Point(20, 230),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(150, 230),
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
                        string query = @"INSERT INTO transactions 
                                         (username, transaction_type, amount, date, description, category, type) 
                                         VALUES (@username, 'ExpensePlan', @amount, @date, @description, @category, @type)";
                        Database.ExecuteQuery(query,
                            new MySqlParameter("@username", username),
                            new MySqlParameter("@amount", amount),
                            new MySqlParameter("@date", dtpDate.Value),
                            new MySqlParameter("@description", string.IsNullOrEmpty(txtDescription.Text) ? "Expense (Planned)" : txtDescription.Text),
                            new MySqlParameter("@category", cmbCategory.SelectedItem?.ToString() ?? "Other"),
                            new MySqlParameter("@type", cmbType.SelectedItem?.ToString() ?? "Essential"));

                        LoadExpensesData();
                        MessageBox.Show($"Planned expense of ₱{amount:N2} added successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        addExpenseForm.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding expense: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid amount greater than 0.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            btnCancel.Click += (s, args) => addExpenseForm.Close();

            addExpenseForm.Controls.AddRange(new Control[] {
                lblAmount, txtAmount,
                lblDescription, txtDescription,
                lblCategory, cmbCategory,
                lblType, cmbType,
                lblDate, dtpDate,
                btnSave, btnCancel
            });

            addExpenseForm.ShowDialog();
        }

        private Panel BuildItemPanel(string title, decimal target, decimal allocated, decimal progressPercent, string type, string category, Action onAllocate)
        {
            var container = new Panel { Width = 360, Height = 90, BackColor = Color.FromArgb(245, 245, 245) };
            var lbl = new Label { Text = $"{title}\n{category} ({type}) - ₱{target:N2}", AutoSize = false, Location = new Point(8, 8), Size = new Size(320, 30) };
            var bar = new ProgressBar { Location = new Point(8, 45), Size = new Size(230, 18), Value = (int)Math.Round(progressPercent) };
            var btn = new Button { Text = "Allocate", BackColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(245, 40), Size = new Size(90, 28) };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onAllocate();
            var lblAmt = new Label { AutoSize = true, Location = new Point(8, 66), ForeColor = Color.FromArgb(80, 80, 80), Text = $"Allocated: ₱{allocated:N2} / ₱{target:N2} ({progressPercent:0}% )" };
            container.Controls.Add(lbl);
            container.Controls.Add(bar);
            container.Controls.Add(btn);
            container.Controls.Add(lblAmt);
            return container;
        }

        private void AllocateToPlan(int planId, string planType, decimal target)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter amount to allocate (target ₱{target:N2}):", "Allocate", "0");
            if (!decimal.TryParse(input, out decimal amt) || amt <= 0) return;

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

                LoadExpensesData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Allocation failed: {ex.Message}");
            }
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
                string query = @"SELECT amount, date, description, category, type
                                 FROM transactions 
                                 WHERE username = @username AND transaction_type = 'Expenses' 
                                 ORDER BY date DESC";
                DataTable dt = Database.GetData(query, new MySqlParameter("@username", username));

                if (dt.Rows.Count > 0)
                {
                    string history = "Expense History:\n\n";
                    decimal totalExpenses = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal amount = Convert.ToDecimal(row["amount"]);
                        string date = Convert.ToDateTime(row["date"]).ToString("MM/dd/yyyy");
                        string description = row["description"]?.ToString() ?? "Expense";
                        string category = row["category"]?.ToString() ?? "Other";
                        string type = row["type"]?.ToString() ?? "Essential";

                        totalExpenses += Math.Abs(amount);
                        history += $"{date} - {description} ({category}, {type}): ₱{Math.Abs(amount):N2}\n";
                    }

                    history += $"\nTotal Expenses: ₱{totalExpenses:N2}";
                    MessageBox.Show(history, "Expense History", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No expenses found.", "Expense History", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expense history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Expenses_Load(object sender, EventArgs e)
        {

        }
    }
}
