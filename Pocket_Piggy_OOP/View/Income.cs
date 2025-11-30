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
    public partial class Income: Form
    {
        private string username;

        public Income(string user)
        {
            InitializeComponent();
            username = user;
            this.Load += Income_Load;
            btnAdd.Click += btnAddIncome_Click;
            btnMenu.Click += btnBackToMenu_Click;
        }

        private void Income_Load(object? sender, EventArgs e)
        {
            LoadIncomeData();
        }

        private void LoadIncomeData()
        {
            try
            {
                string totalQuery = "SELECT SUM(amount) FROM transactions WHERE username=@username AND transaction_type='Income'";
                DataTable totalDt = Database.GetData(totalQuery, new MySqlParameter("@username", username));
                decimal totalIncome = totalDt.Rows.Count > 0 && totalDt.Rows[0][0] != DBNull.Value ?
                    Convert.ToDecimal(totalDt.Rows[0][0]) : 0;

                string query = "SELECT amount, date, description FROM transactions WHERE username=@username AND transaction_type='Income' ORDER BY date DESC LIMIT 5";
                DataTable dt = Database.GetData(query, new MySqlParameter("@username", username));

                if (dt.Rows.Count > 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Total Income: ₱{totalIncome:N2}");
                    sb.AppendLine();
                    sb.AppendLine("Recent Income:");
                    sb.AppendLine();
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal amount = Convert.ToDecimal(row["amount"]);
                        string date = Convert.ToDateTime(row["date"]).ToString("MM/dd");
                        string description = row["description"]?.ToString() ?? "Income";
                        sb.AppendLine($"{date} - {description}: ₱{amount:N2}");
                    }
                    lblIncome.Text = sb.ToString();
                }
                else
                {
                    lblIncome.Text = $"Total Income: ₱{totalIncome:N2}\n\nNo recent income";
                }

                UpdateIncomeChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading income data: {ex.Message}");
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(username);
            menu.Show();
            this.Close();
        }

        private void UpdateIncomeChart()
        {
            try
            {
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Legends.Clear();

                var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("IncomeChart");
                chartArea.AxisX.Title = "Date";
                chartArea.AxisY.Title = "Amount (₱)";
                chart1.ChartAreas.Add(chartArea);

                var series = new System.Windows.Forms.DataVisualization.Charting.Series("Income");
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                series.Color = System.Drawing.Color.FromArgb(40, 167, 69);

                string chartQuery = @"SELECT DATE(date) as income_date, SUM(amount) as daily_income 
                                     FROM transactions 
                                     WHERE username=@username AND transaction_type='Income' 
                                     AND date >= DATE_SUB(NOW(), INTERVAL 7 DAY)
                                     GROUP BY DATE(date) 
                                     ORDER BY income_date";
                DataTable chartDt = Database.GetData(chartQuery, new MySqlParameter("@username", username));

                foreach (DataRow row in chartDt.Rows)
                {
                    string date = Convert.ToDateTime(row["income_date"]).ToString("MM/dd");
                    decimal amount = Convert.ToDecimal(row["daily_income"]);
                    series.Points.AddXY(date, amount);
                }

                chart1.Series.Add(series);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chart update error: {ex.Message}");
            }
        }

        private void btnAddIncome_Click(object sender, EventArgs e)
        {
            Form addIncomeForm = new Form()
            {
                Width = 400,
                Height = 300,
                Text = "Add New Income",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblAmount = new Label()
            {
                Text = "Amount:",
                Location = new Point(20, 20),
                Size = new Size(80, 20)
            };
            TextBox txtAmount = new TextBox()
            {
                Location = new Point(110, 18),
                Size = new Size(200, 25),
                PlaceholderText = "Enter amount"
            };

            Label lblDescription = new Label()
            {
                Text = "Description:",
                Location = new Point(20, 60),
                Size = new Size(80, 20)
            };
            TextBox txtDescription = new TextBox()
            {
                Location = new Point(110, 58),
                Size = new Size(200, 25),
                PlaceholderText = "Enter description"
            };

            Label lblSource = new Label()
            {
                Text = "Source:",
                Location = new Point(20, 100),
                Size = new Size(80, 20)
            };
            ComboBox cmbSource = new ComboBox()
            {
                Location = new Point(110, 98),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSource.Items.AddRange(new string[] {
                "Salary", "Freelance", "Investment", "Business", "Gift", "Other"
            });
            cmbSource.SelectedIndex = 0;

            Button btnSave = new Button()
            {
                Text = "Add Income",
                Location = new Point(20, 150),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(130, 150),
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
                        string description = string.IsNullOrEmpty(txtDescription.Text) ? "Income" : txtDescription.Text;
                        string source = cmbSource.SelectedItem?.ToString() ?? "Other";
                        
                        string query = "INSERT INTO transactions (username, transaction_type, amount, date, description, category) VALUES (@username, 'Income', @amount, NOW(), @description, @category)";
                        Database.ExecuteQuery(query,
                            new MySqlParameter("@username", username),
                            new MySqlParameter("@amount", amount),
                            new MySqlParameter("@description", description),
                            new MySqlParameter("@category", source));

                        LoadIncomeData();
                        MessageBox.Show($"₱{amount:N2} income added successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        addIncomeForm.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding income: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid amount greater than 0.", "Invalid Amount",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            btnCancel.Click += (s, args) => addIncomeForm.Close();

            addIncomeForm.Controls.AddRange(new Control[] {
                lblAmount, txtAmount,
                lblDescription, txtDescription,
                lblSource, cmbSource,
                btnSave, btnCancel
            });

            addIncomeForm.ShowDialog();
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(username);
            menu.Show();
            this.Close();
        }
    }
}
