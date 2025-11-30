using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;

namespace PocketPiggy
{
    public partial class Savings : Form
    {
        private string username;

        public Savings(string user)
        {
            InitializeComponent();
            username = user;
            LoadGoals();
        }

        private void LoadGoals()
        {
            try
            {
                string query = @"SELECT id, goal_name, goal_amount, progress, date_created, due_date 
                                 FROM goals WHERE username=@username ORDER BY date_created DESC";
                DataTable dt = Database.GetData(query, new MySqlParameter("@username", username));
                dgvGoals.DataSource = dt;

                dgvGoals.Columns["id"].Visible = false;
                dgvGoals.Columns["goal_name"].HeaderText = "Goal Name";
                dgvGoals.Columns["goal_amount"].HeaderText = "Target Amount";
                dgvGoals.Columns["progress"].HeaderText = "Current Amount";
                dgvGoals.Columns["date_created"].HeaderText = "Date Created";
                dgvGoals.Columns["due_date"].HeaderText = "Due Date";

                var flow = this.Controls.Find("flowGoals", true).FirstOrDefault() as FlowLayoutPanel;
                if (flow != null)
                {
                    flow.Controls.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        int goalId = Convert.ToInt32(row["id"]);
                        string name = row["goal_name"].ToString();
                        decimal target = Convert.ToDecimal(row["goal_amount"]);
                        decimal current = Convert.ToDecimal(row["progress"]);
                        DateTime? due = row["due_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["due_date"]);
                        decimal percent = target == 0 ? 0 : Math.Min(100m, (current / target) * 100m);

                        var panel = BuildGoalPanel(name, target, current, percent, due, () => AllocateToGoal(goalId, target));
                        flow.Controls.Add(panel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading goals: {ex.Message}");
            }
        }

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            Form addGoalForm = new Form()
            {
                Width = 400,
                Height = 300,
                Text = "Add New Goal",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblName = new Label() { Text = "Goal Name:", Location = new Point(20, 20), Size = new Size(100, 25) };
            TextBox txtName = new TextBox() { Location = new Point(130, 20), Size = new Size(200, 25) };

            Label lblAmount = new Label() { Text = "Target Amount:", Location = new Point(20, 60), Size = new Size(100, 25) };
            TextBox txtAmount = new TextBox() { Location = new Point(130, 60), Size = new Size(200, 25) };

            Label lblDue = new Label() { Text = "Due Date:", Location = new Point(20, 100), Size = new Size(100, 25) };
            DateTimePicker dpDue = new DateTimePicker() { Location = new Point(130, 100), Size = new Size(200, 25), Format = DateTimePickerFormat.Short };

            Button btnSave = new Button()
            {
                Text = "Save Goal",
                Location = new Point(130, 150),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;

            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(240, 150),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            btnSave.Click += (s, args) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    !decimal.TryParse(txtAmount.Text, out decimal target) || target <= 0)
                {
                    MessageBox.Show("Please enter valid goal information.", "Invalid Input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string query = @"INSERT INTO goals (username, goal_name, goal_amount, progress, date_created, due_date)
                                     VALUES (@username, @goal_name, @goal_amount, 0, NOW(), @due_date)";
                    Database.ExecuteQuery(query,
                        new MySqlParameter("@username", username),
                        new MySqlParameter("@goal_name", txtName.Text),
                        new MySqlParameter("@goal_amount", target),
                        new MySqlParameter("@due_date", dpDue.Value));

                    MessageBox.Show($"Goal '{txtName.Text}' added successfully!");
                    addGoalForm.Close();
                    LoadGoals();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding goal: {ex.Message}");
                }
            };

            btnCancel.Click += (s, args) => addGoalForm.Close();

            addGoalForm.Controls.AddRange(new Control[] { lblName, txtName, lblAmount, txtAmount, lblDue, dpDue, btnSave, btnCancel });
            addGoalForm.ShowDialog();
        }

        private Panel BuildGoalPanel(string name, decimal target, decimal current, decimal percent, DateTime? due, Action onAllocate)
        {
            var panel = new Panel
            {
                Width = 430,
                Height = 100,
                BackColor = Color.FromArgb(245, 245, 245),
                Margin = new Padding(5)
            };

            var lbl = new Label
            {
                AutoSize = false,
                Text = $"{name}\nTarget: ₱{target:N2} | Due: {(due.HasValue ? due.Value.ToString("MM/dd/yyyy") : "None")}",
                Location = new Point(8, 8),
                Size = new Size(390, 35)
            };

            var bar = new ProgressBar { Location = new Point(8, 50), Size = new Size(260, 18), Value = (int)Math.Round(percent) };

            var btn = new Button
            {
                Text = "Allocate Money",
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(280, 45),
                Size = new Size(120, 28)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onAllocate();

            var lblAmt = new Label
            {
                AutoSize = true,
                Location = new Point(8, 73),
                ForeColor = Color.FromArgb(80, 80, 80),
                Text = $"Allocated: ₱{current:N2} / ₱{target:N2} ({percent:0}% )"
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(bar);
            panel.Controls.Add(btn);
            panel.Controls.Add(lblAmt);

            return panel;
        }

        private void AllocateToGoal(int goalId, decimal target)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter amount to allocate to goal (target ₱{target:N2}):", "Allocate Money", "0");
            if (!decimal.TryParse(input, out decimal amt) || amt <= 0) return;

            try
            {
                string balSql = @"SELECT 
                    (SELECT IFNULL(SUM(amount),0) FROM transactions WHERE username=@u AND transaction_type='Add Balance') +
                    (SELECT IFNULL(SUM(amount),0) FROM transactions WHERE username=@u AND transaction_type='Income') -
                    (SELECT IFNULL(SUM(ABS(amount)),0) FROM transactions WHERE username=@u AND transaction_type='Allocation') AS total_balance;";
                var balDt = Database.GetData(balSql, new MySqlParameter("@u", username));
                decimal available = (balDt.Rows.Count > 0 && balDt.Rows[0][0] != DBNull.Value)
                    ? Convert.ToDecimal(balDt.Rows[0][0])
                    : 0m;

                if (amt > available)
                {
                    MessageBox.Show($"Insufficient funds. Available: ₱{available:N2}", "Allocation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string upd = @"UPDATE goals SET progress = progress + @amt WHERE id=@id AND username=@u";
                Database.ExecuteQuery(upd,
                    new MySqlParameter("@amt", amt),
                    new MySqlParameter("@id", goalId),
                    new MySqlParameter("@u", username));

                string insTxn = @"INSERT INTO transactions (username, transaction_type, amount, date, description, category)
                                  VALUES (@u, 'Allocation', @negAmt, NOW(), CONCAT('Allocation to Goal #', @gid), 'Savings')";
                Database.ExecuteQuery(insTxn,
                    new MySqlParameter("@u", username),
                    new MySqlParameter("@gid", goalId),
                    new MySqlParameter("@negAmt", -Math.Abs(amt)));

                LoadGoals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Allocation failed: {ex.Message}");
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(username);
            menu.Show();
            this.Close();
        }
    }
}
