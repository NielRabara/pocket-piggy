using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using PocketPiggy.Repositories;
using PocketPiggy.ViewModels;

namespace PocketPiggy
{
    public class BusinessAccountSettings : Form
    {
        private readonly int _businessId;

        private TabControl tabs = new TabControl();

        private TextBox txtName = new TextBox();
        private PictureBox picProfile = new PictureBox();
        private Button btnUploadPic = new Button();
        private Button btnSave = new Button();
        private Button btnCancel = new Button();
        private byte[] _currentPic;
        private string _currentName;
        private TabPage tabSecurity;
        private Button btnRequestPasswordChange = new Button();
        private Button btnDeleteAccount = new Button();

        public BusinessAccountSettings(int businessId)
        {
            InitializeComponent();
            this.FormClosing += BusinessAccountSettings_FormClosing;
            _businessId = businessId;
            LoadProfile();
        }

        private void InitializeComponent()
        {
            this.Text = "Business Account Settings";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            tabs = new TabControl { Dock = DockStyle.Fill };
            var tabProfile = new TabPage("Profile Info");
            tabSecurity = new TabPage("Security");

            var pnl = new Panel { Dock = DockStyle.Fill };
            var lblName = new Label { Text = "Name", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(100, 18), Width = 300 };
            var lblPic = new Label { Text = "Profile Picture", Location = new Point(20, 60), AutoSize = true };
            picProfile = new PictureBox { Location = new Point(140, 60), Size = new Size(120, 120), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };
            btnUploadPic = new Button { Text = "Upload...", Location = new Point(270, 60) }; btnUploadPic.Click += BtnUploadPic_Click;
            btnSave = new Button { Text = "Save", Location = new Point(20, 210), Width = 100 }; btnSave.Click += BtnSave_Click;
            btnCancel = new Button { Text = "Cancel", Location = new Point(130, 210), Width = 100 }; btnCancel.Click += BtnCancel_Click;
            pnl.Controls.AddRange(new Control[] { lblName, txtName, lblPic, picProfile, btnUploadPic, btnSave, btnCancel });
            tabProfile.Controls.Add(pnl);

            var secPanel = new Panel { Dock = DockStyle.Fill };
            var lblSec = new Label { Text = "Account Security", Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            btnRequestPasswordChange = new Button { Text = "Request Password Change", Location = new Point(20, 60), Width = 220 };
            btnDeleteAccount = new Button { Text = "Delete Account", Location = new Point(20, 100), Width = 220 };
            btnRequestPasswordChange.Click += BtnRequestPasswordChange_Click;
            btnDeleteAccount.Click += BtnDeleteAccount_Click;
            secPanel.Controls.AddRange(new Control[] { lblSec, btnRequestPasswordChange, btnDeleteAccount });
            tabSecurity.Controls.Add(secPanel);

            tabs.TabPages.Clear();
            tabs.TabPages.Add(tabProfile);
            tabs.TabPages.Add(tabSecurity);
            this.Controls.Clear();
            this.Controls.Add(tabs);
        }

        private void LoadProfile()
        {
            try
            {
                var (name, pic) = ProfileRepository.GetBusinessProfile(_businessId);
                _currentName = name;
                _currentPic = pic;
                txtName.Text = name ?? string.Empty;
                LoadPicture(_currentPic);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}");
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                ProfileRepository.UpdateBusinessProfile(_businessId, txtName.Text?.Trim(), _currentPic);
                _currentName = txtName.Text?.Trim();
                MessageBox.Show("Profile updated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}");
            }
        }

        private void BtnUploadPic_Click(object? sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _currentPic = File.ReadAllBytes(dlg.FileName);
                    LoadPicture(_currentPic);
                }
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            txtName.Text = _currentName ?? string.Empty;
            LoadPicture(_currentPic);
        }

        private void BtnRequestPasswordChange_Click(object? sender, EventArgs e)
        {
            try
            {
                using (Form passwordChangeForm = new Form())
                {
                    passwordChangeForm.Text = "Request Password Change";
                    passwordChangeForm.Size = new System.Drawing.Size(400, 280);
                    passwordChangeForm.StartPosition = FormStartPosition.CenterParent;
                    passwordChangeForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    passwordChangeForm.MaximizeBox = false;
                    passwordChangeForm.MinimizeBox = false;

                    Label lblCurrentPassword = new Label() 
                    { 
                        Text = "Current Password:", 
                        Location = new System.Drawing.Point(20, 20), 
                        Size = new System.Drawing.Size(120, 20) 
                    };
                    TextBox txtCurrentPassword = new TextBox() 
                    { 
                        Location = new System.Drawing.Point(150, 18), 
                        Size = new System.Drawing.Size(200, 25), 
                        PasswordChar = '*' 
                    };

                    Label lblNewPassword = new Label() 
                    { 
                        Text = "New Password:", 
                        Location = new System.Drawing.Point(20, 60), 
                        Size = new System.Drawing.Size(120, 20) 
                    };
                    TextBox txtNewPassword = new TextBox() 
                    { 
                        Location = new System.Drawing.Point(150, 58), 
                        Size = new System.Drawing.Size(200, 25), 
                        PasswordChar = '*' 
                    };

                    Label lblConfirmPassword = new Label() 
                    { 
                        Text = "Confirm Password:", 
                        Location = new System.Drawing.Point(20, 100), 
                        Size = new System.Drawing.Size(120, 20) 
                    };
                    TextBox txtConfirmPassword = new TextBox() 
                    { 
                        Location = new System.Drawing.Point(150, 98), 
                        Size = new System.Drawing.Size(200, 25), 
                        PasswordChar = '*' 
                    };

                    Label lblReason = new Label() 
                    { 
                        Text = "Reason (Optional):", 
                        Location = new System.Drawing.Point(20, 140), 
                        Size = new System.Drawing.Size(120, 20) 
                    };
                    TextBox txtReason = new TextBox() 
                    { 
                        Location = new System.Drawing.Point(150, 138), 
                        Size = new System.Drawing.Size(200, 25),
                        PlaceholderText = "Security, forgot, etc."
                    };

                    Button btnSubmit = new Button() 
                    { 
                        Text = "Submit Request", 
                        Location = new System.Drawing.Point(20, 180), 
                        Size = new System.Drawing.Size(120, 35),
                        BackColor = System.Drawing.Color.FromArgb(40, 167, 69),
                        ForeColor = System.Drawing.Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    Button btnCancel = new Button() 
                    { 
                        Text = "Cancel", 
                        Location = new System.Drawing.Point(150, 180), 
                        Size = new System.Drawing.Size(120, 35),
                        BackColor = System.Drawing.Color.FromArgb(108, 117, 125),
                        ForeColor = System.Drawing.Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    btnSubmit.Click += (s, args) =>
                    {
                        if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
                        {
                            MessageBox.Show("Please enter your current password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                        {
                            MessageBox.Show("Please enter a new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (txtNewPassword.Text != txtConfirmPassword.Text)
                        {
                            MessageBox.Show("New password and confirmation do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (txtNewPassword.Text.Length < 6)
                        {
                            MessageBox.Show("New password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string currentPasswordHash = SignUpBusinessViewModel.HashStatic(txtCurrentPassword.Text);
                        string verifyQuery = "SELECT password, business_name FROM business_users WHERE business_id = @business_id";
                        var verifyDt = Database.GetData(verifyQuery, new MySqlParameter("@business_id", _businessId));
                        
                        if (verifyDt.Rows.Count == 0 || verifyDt.Rows[0]["password"].ToString() != currentPasswordHash)
                        {
                            MessageBox.Show("Current password is incorrect.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string newPasswordHash = SignUpBusinessViewModel.HashStatic(txtNewPassword.Text);
                        string businessName = verifyDt.Rows[0]["business_name"].ToString();
                        string reason = string.IsNullOrWhiteSpace(txtReason.Text) ? "Business requested password change" : txtReason.Text;
                        
                        string ticketDescription = $"Business '{businessName}' (ID: {_businessId}) requests a password change.\n\n" +
                                                 $"Reason: {reason}\n" +
                                                 $"New Password Hash: {newPasswordHash}\n" +
                                                 $"Request Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n" +
                                                 $"Note: Current password has been verified. Admin can approve to apply the new password.";

                        Ticket.CreateForBusiness(_businessId, "Password Change", "Password Change Request", ticketDescription);
                        
                        MessageBox.Show("Password change request submitted successfully!\n\nYour request includes the new password and will be reviewed by an admin.", 
                                      "Request Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        passwordChangeForm.DialogResult = DialogResult.OK;
                    };

                    btnCancel.Click += (s, args) => passwordChangeForm.DialogResult = DialogResult.Cancel;

                    passwordChangeForm.Controls.AddRange(new Control[] {
                        lblCurrentPassword, txtCurrentPassword,
                        lblNewPassword, txtNewPassword,
                        lblConfirmPassword, txtConfirmPassword,
                        lblReason, txtReason,
                        btnSubmit, btnCancel
                    });

                    passwordChangeForm.AcceptButton = btnSubmit;
                    passwordChangeForm.CancelButton = btnCancel;
                    passwordChangeForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating password change request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnDeleteAccount_Click(object? sender, EventArgs e)
        {
            using (var dlg = new Form { Text = "Confirm Deletion", Width = 380, Height = 180, StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false })
            {
                var lbl = new Label { Text = "Enter your password to delete business account:", Location = new Point(12, 15), AutoSize = true };
                var txt = new TextBox { Location = new Point(16, 45), Width = 330, PasswordChar = '*' };
                var btnOk = new Button { Text = "Delete", Location = new Point(16, 85), Width = 90 };
                var btnCancel = new Button { Text = "Cancel", Location = new Point(116, 85), Width = 90 };
                btnCancel.Click += (s, a) => dlg.Close();
                btnOk.Click += (s, a) =>
                {
                    try
                    {
                        string hashed = HashPassword(txt.Text);
                        var dt = Database.GetData("SELECT COUNT(*) FROM business_users WHERE business_id=@id AND password=@p",
                            new MySqlParameter("@id", _businessId), new MySqlParameter("@p", hashed));
                        int ok = Convert.ToInt32(dt.Rows[0][0]);
                        if (ok == 0)
                        {
                            MessageBox.Show("Password incorrect.");
                            return;
                        }

                        Ticket.CreateForBusiness(_businessId, "DeleteAccount", "Account Deletion", $"Business {_businessId} requested account deletion.", "High");
                        Database.ExecuteQuery("DELETE FROM business_users WHERE business_id=@id AND password=@p",
                            new MySqlParameter("@id", _businessId), new MySqlParameter("@p", hashed));
                        MessageBox.Show("Your business account has been deleted.");
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Delete failed: {ex.Message}");
                    }
                };
                dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
                dlg.ShowDialog(this);
            }
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

        private void LoadPicture(byte[] pic)
        {
            if (pic == null) { picProfile.Image = null; return; }
            using (var ms = new MemoryStream(pic)) { picProfile.Image = Image.FromStream(ms); }
        }

        private void BusinessAccountSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.Owner == null)
                {
                    var main = new businessMain(BusinessSession.CurrentBusinessName ?? string.Empty);
                    main.Show();
                }
            }
            catch
            {   
            }
        }
    }
}
