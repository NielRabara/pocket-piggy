using System;
using System.Windows.Forms;
using PocketPiggy.ViewModels;

namespace PocketPiggy.View_Business
{
    public partial class SignUpBusiness : Form
    {
        private readonly SignUpBusinessViewModel _viewModel;

        public SignUpBusiness(string accountType)
        {
            InitializeComponent();
            _viewModel = new SignUpBusinessViewModel();
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string businessName = txtBusinessName.Text.Trim();
            string businessAddress = "N/A";
            string contactNumber = txtContactNumber.Text.Trim();
            string email = string.Empty;
            string industry = txtBusinessType.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(businessName) ||
                string.IsNullOrWhiteSpace(businessAddress) ||
                string.IsNullOrWhiteSpace(contactNumber))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please re-enter.", "Password Mismatch",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (success, message) = _viewModel.RegisterBusiness(
                username,
                password,
                businessName,
                businessAddress,
                contactNumber,
                string.IsNullOrWhiteSpace(email) ? null : email,
                string.IsNullOrWhiteSpace(industry) ? null : industry);

            if (success)
            {
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LogIn loginForm = new LogIn();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = checkBoxShowPassword.Checked;
            txtPassword.PasswordChar = show ? '\0' : '*';
            txtConfirmPassword.PasswordChar = show ? '\0' : '*';
        }

        private void linkBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogIn loginForm = new LogIn();
            loginForm.Show();
            this.Hide();
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            btnRegister_Click(sender, e);
        }
    }
}
