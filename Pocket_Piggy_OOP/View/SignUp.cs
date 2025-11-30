using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using PocketPiggy.View;

namespace PocketPiggy
{
    public partial class SignUp: Form
    {
        private string selectedAccountType;

        public SignUp(string accountType)
        {
            InitializeComponent();
            selectedAccountType = accountType;
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            string userID = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields before proceeding.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userID.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long.", "Invalid Username",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userID.Length > 20)
            {
                MessageBox.Show("Username must be no more than 20 characters long.", "Invalid Username",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Weak Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Password Mismatch",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                DataTable dt = Database.GetData(checkQuery, new MySqlParameter("@username", userID));
                int userExists = Convert.ToInt32(dt.Rows[0][0]);

                if (userExists > 0)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", 
                        "Username Taken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking username availability: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hashedPassword = HashPassword(password);

            try
            {
                string query = @"INSERT INTO users (username, password)
                         VALUES (@username, @password)";

                Database.ExecuteQuery(query,
                    new MySqlParameter("@username", userID),
                    new MySqlParameter("@password", hashedPassword));

                MessageBox.Show("Account created successfully! Welcome to Pocket Piggy!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogIn loginForm = new LogIn();
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = checkBoxShowPassword.Checked ? '\0' : '*';
            txtConfirmPassword.PasswordChar = checkBoxShowPassword.Checked ? '\0' : '*';
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private void linkBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogIn loginForm = new LogIn();
            loginForm.Show();
            this.Hide();
        }
    }
}
