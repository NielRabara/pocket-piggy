using System;
using System.Data;
using System.Windows.Forms;
using PocketPiggy.Models;
using MySql.Data.MySqlClient;

namespace PocketPiggy
{
    public partial class Questionnaire : Form
    {
        private string username;

        public Questionnaire(string user = "")
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            username = user;

            birthMonth.Items.AddRange(new string[] {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });

            for (int year = 1920; year <= 2024; year++)
            {
                birthYear.Items.Add(year.ToString());
            }

            genderCB.Items.AddRange(new string[] {
                "Female", "Male", "Non-binary", "Prefer not to say", "Other"
            });

            occupationCB.Items.AddRange(new string[] {
                "Student", "Employed", "Self-employed", "Unemployed", "Retired", "Other"
            });

            sourceOfIncomeCB.Items.AddRange(new string[] {
                "Salary", "Business", "Investments", "Pension", "Allowance", "Other"
            });

            maritalStatusCB.Items.AddRange(new string[] {
                "Single", "Married", "Divorced", "Widowed", "In a relationship"
            });

            averageIncomeCB.Items.AddRange(new string[] {
                "less than ₱1,000", "₱1,000 - ₱5,000", "₱5,001 - ₱10,000", "₱10,001 - ₱20,000", "₱20,001 - ₱40,000",
                "₱40,001 - ₱70,000", "₱70,001 - ₱100,000", "₱100,001 and above"
            });

            monthlySpendCB.Items.AddRange(new string[] {
                "less than ₱5,000", "₱5,000 - ₱15,000", "₱15,001 - ₱30,000", "₱30,001 - ₱50,000", "₱50,001 and above"
            });

            expenseCB.Items.AddRange(new string[] {
                "Every day", "Once a week", "Twice a month", "Once a month", "I don't track them"
            });

            financialGoalCB.Items.AddRange(new string[] {
                "Save for emergencies", "Pay off debts", "Build Savings", "Invest (stocks, mutual funds, etc.)",
                "Start a business", "Buy a house"
            });

            saveCB.Items.AddRange(new string[] {
                "less than ₱1,000", "₱1,000 - ₱5,000", "₱5,001 - ₱10,000", "₱10,001 - ₱20,000", "₱20,001 - ₱40,000",
                "₱40,001 - ₱70,000", "₱70,001 - ₱100,000", "₱100,001 and above"
            });

            confidenceCB.Items.AddRange(new string[] {
                "Very Confident", "Somewhat Confident", "Neutral", "Not Confident"
            });

            remindersCB.Items.AddRange(new string[] {
                "Daily", "Weekly", "Monthly", "Quaterly" , "Yearly"
            });

            birthMonth.SelectedIndexChanged += BirthMonth_SelectedIndexChanged;
            birthYear.SelectedIndexChanged += BirthYear_SelectedIndexChanged;
            genderCB.SelectedIndexChanged += GenderCB_SelectedIndexChanged;
            genderCustomTB.Enter += GenderCustomTB_Enter;

            UpdateDaysInMonth(31);

            genderCustomTB.Visible = false;
        }

        private void BirthMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDaysBasedOnMonthAndYear();
        }

        private void BirthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDaysBasedOnMonthAndYear();
        }

        private void UpdateDaysBasedOnMonthAndYear()
        {
            int selectedDay = birthDate.SelectedIndex + 1;
            birthDate.Items.Clear();
            int selectedMonth = birthMonth.SelectedIndex + 1;
            int selectedYear = GetSelectedYear();
            int daysInMonth = GetDaysInMonth(selectedMonth, selectedYear);
            UpdateDaysInMonth(daysInMonth);
            if (selectedDay > 0 && selectedDay <= daysInMonth)
            {
                birthDate.SelectedIndex = selectedDay - 1;
            }
        }

        private int GetSelectedYear()
        {
            if (birthYear.SelectedIndex >= 0)
            {
                return int.Parse(birthYear.SelectedItem.ToString());
            }
            return DateTime.Now.Year;
        }

        private void UpdateDaysInMonth(int days)
        {
            for (int day = 1; day <= days; day++)
            {
                birthDate.Items.Add(day.ToString());
            }
        }

        private int GetDaysInMonth(int month, int year)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    return IsLeapYear(year) ? 29 : 28;
                default:
                    return 31;
            }
        }

        private bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        private void GenderCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (genderCB.SelectedIndex >= 0 && genderCB.SelectedItem.ToString() == "Other")
            {
                genderCustomTB.Visible = true;
            }
            else
            {
                genderCustomTB.Visible = false;
                genderCustomTB.Text = "";
            }
        }

        private void GenderCustomTB_Enter(object sender, EventArgs e)
        {
            if (genderCustomTB.Text == "Please specify...")
            {
                genderCustomTB.Text = "";
            }
        }
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameTB.Text) || birthMonth.SelectedIndex == -1 || birthDate.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill out all required fields before submitting.", "Missing Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string fullName = nameTB.Text.Trim();
                string age = ageTB.Text.Trim();
                string month = birthMonth.SelectedItem?.ToString() ?? "";
                string date = birthDate.SelectedItem?.ToString() ?? "";
                string year = birthYear.SelectedItem?.ToString() ?? "";
                string gender = genderCB.SelectedItem?.ToString() == "Other" ? genderCustomTB.Text.Trim() : genderCB.SelectedItem?.ToString();
                string occupation = occupationCB.SelectedItem?.ToString() ?? "";
                string sourceIncome = sourceOfIncomeCB.SelectedItem?.ToString() ?? "";
                string marital = maritalStatusCB.SelectedItem?.ToString() ?? "";
                string avgIncome = averageIncomeCB.SelectedItem?.ToString() ?? "";
                string spend = monthlySpendCB.SelectedItem?.ToString() ?? "";
                string expense = expenseCB.SelectedItem?.ToString() ?? "";
                string goal = financialGoalCB.SelectedItem?.ToString() ?? "";
                string save = saveCB.SelectedItem?.ToString() ?? "";
                string confidence = confidenceCB.SelectedItem?.ToString() ?? "";
                string reminder = remindersCB.SelectedItem?.ToString() ?? "";

                string createTableQuery = @"CREATE TABLE IF NOT EXISTS questionnaire (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    username VARCHAR(50),
                    full_name VARCHAR(100),
                    age VARCHAR(10),
                    birth_month VARCHAR(20),
                    birth_day VARCHAR(10),
                    birth_year VARCHAR(10),
                    gender VARCHAR(50),
                    occupation VARCHAR(50),
                    source_of_income VARCHAR(50),
                    marital_status VARCHAR(50),
                    average_income VARCHAR(50),
                    monthly_spend VARCHAR(50),
                    expense_tracking VARCHAR(50),
                    financial_goal VARCHAR(100),
                    savings_goal VARCHAR(50),
                    confidence_level VARCHAR(50),
                    reminders_preference VARCHAR(50),
                    date_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (username) REFERENCES users(user_id) ON DELETE CASCADE
                )";
                
                Database.ExecuteQuery(createTableQuery);

                string checkQuery = "SELECT COUNT(*) FROM questionnaire WHERE username = @username";
                DataTable checkDt = Database.GetData(checkQuery, new MySqlParameter("@username", username));
                int existingCount = Convert.ToInt32(checkDt.Rows[0][0]);

                string query;
                if (existingCount > 0)
                {
                    query = @"UPDATE questionnaire SET 
                        full_name = @name, age = @age, birth_month = @month, birth_day = @date, birth_year = @year,
                        gender = @gender, occupation = @occupation, source_of_income = @source, marital_status = @marital,
                        average_income = @income, monthly_spend = @spend, expense_tracking = @expense,
                        financial_goal = @goal, savings_goal = @save, confidence_level = @confidence,
                        reminders_preference = @reminder
                        WHERE username = @username";
                }
                else
                {
                    query = @"INSERT INTO questionnaire 
                        (username, full_name, age, birth_month, birth_day, birth_year, gender, occupation, 
                         source_of_income, marital_status, average_income, monthly_spend, 
                         expense_tracking, financial_goal, savings_goal, confidence_level, reminders_preference)
                         VALUES (@username, @name, @age, @month, @date, @year, @gender, @occupation, 
                         @source, @marital, @income, @spend, @expense, @goal, @save, @confidence, @reminder)";
                }

                Database.ExecuteQuery(query,
                    new MySqlParameter("@username", username),
                    new MySqlParameter("@name", fullName),
                    new MySqlParameter("@age", age),
                    new MySqlParameter("@month", month),
                    new MySqlParameter("@date", date),
                    new MySqlParameter("@year", year),
                    new MySqlParameter("@gender", gender),
                    new MySqlParameter("@occupation", occupation),
                    new MySqlParameter("@source", sourceIncome),
                    new MySqlParameter("@marital", marital),
                    new MySqlParameter("@income", avgIncome),
                    new MySqlParameter("@spend", spend),
                    new MySqlParameter("@expense", expense),
                    new MySqlParameter("@goal", goal),
                    new MySqlParameter("@save", save),
                    new MySqlParameter("@confidence", confidence),
                    new MySqlParameter("@reminder", reminder)
                );

                MessageBox.Show("Your questionnaire responses have been saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                if (!string.IsNullOrEmpty(username))
                {
                    Menu menu = new Menu(username);
                    menu.Show();
                }
                else
                {
                    LogIn log = new LogIn();
                    log.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Questionnaire_Load(object sender, EventArgs e)
        {

        }
    }
}
