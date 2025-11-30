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
    public class PersonalAccountSettings : Form
    {
        private readonly string _username;
        private int _userId;

        private TabControl tabs = new TabControl();

        private TextBox txtName = new TextBox();
        private PictureBox picProfile = new PictureBox();
        private Button btnUploadPic = new Button();
        private Button btnSave = new Button();
        private Button btnCancel = new Button();
        private byte[] _currentPic;
        private string _currentName;

        private TextBox txtQ1 = new TextBox();
        private TextBox txtQ2 = new TextBox();
        private Button btnSubmitQuestionnaire = new Button();
        private TabPage tabSecurity;
        private Button btnRequestPasswordChange = new Button();
        private Button btnDeleteAccount = new Button();

        public PersonalAccountSettings(string username)
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            _username = username;
            LoadProfile();
        }

        private void InitializeUi() { }

        private void LoadProfile()
        {
            try
            {
                var dt = Database.GetData("SELECT user_id FROM users WHERE username=@u LIMIT 1", new MySqlParameter("@u", _username));
                if (dt.Rows.Count == 0) return;
                string userId = dt.Rows[0]["user_id"].ToString();
                _userId = Convert.ToInt32(userId);

                var data = ProfileRepository.GetPersonalProfileByUsername(_username);
                _userId = data.userId;
                _currentName = data.name;
                _currentPic = data.pic;
                txtName.Text = _currentName ?? string.Empty;
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
                ProfileRepository.UpdatePersonalProfile(_userId, txtName.Text?.Trim(), _currentPic);
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

        private void InitializeComponent()
        {
            this.Text = "Personal Account Settings";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            tabs = new TabControl { Dock = DockStyle.Fill };
            var tabProfile = new TabPage("Profile Info");
            var tabQ = new TabPage("Questionnaire");
            tabSecurity = new TabPage("Security");

            var pnl = new Panel { Dock = DockStyle.Fill };
            var lblName = new Label { Text = "Name", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(100, 18), Width = 300 };
            var lblPic = new Label { Text = "Profile Picture", Location = new Point(20, 60), AutoSize = true };
            picProfile = new PictureBox { Location = new Point(140, 60), Size = new Size(120, 120), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };
            btnUploadPic = new Button { Text = "Upload...", Location = new Point(270, 60) };
            btnUploadPic.Click += BtnUploadPic_Click;
            btnSave = new Button { Text = "Save", Location = new Point(20, 210), Width = 100 };
            btnCancel = new Button { Text = "Cancel", Location = new Point(130, 210), Width = 100 };
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            pnl.Controls.AddRange(new Control[] { lblName, txtName, lblPic, picProfile, btnUploadPic, btnSave, btnCancel });
            tabProfile.Controls.Add(pnl);

            var qPanel = new Panel { Dock = DockStyle.Fill };
            var q1 = new Label { Text = "What is your primary financial goal?", Location = new Point(20, 20), AutoSize = true };
            txtQ1 = new TextBox { Location = new Point(20, 45), Width = 620 };
            var q2 = new Label { Text = "Any notes about current expenses/income?", Location = new Point(20, 85), AutoSize = true };
            txtQ2 = new TextBox { Location = new Point(20, 110), Width = 620 };
            btnSubmitQuestionnaire = new Button { Text = "Submit", Location = new Point(20, 160), Width = 120 };
            btnSubmitQuestionnaire.Click += BtnSubmitQuestionnaire_Click;
            qPanel.Controls.AddRange(new Control[] { q1, txtQ1, q2, txtQ2, btnSubmitQuestionnaire });
            tabQ.Controls.Add(qPanel);

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
            tabs.TabPages.Add(tabQ);
            tabs.TabPages.Add(tabSecurity);
            this.Controls.Clear();
            this.Controls.Add(tabs);
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            txtName.Text = _currentName ?? string.Empty;
            LoadPicture(_currentPic);
            this.Close(); 
        }

        private void BtnSubmitQuestionnaire_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_userId <= 0) { MessageBox.Show("User not loaded."); return; }
                if (!string.IsNullOrWhiteSpace(txtQ1.Text))
                {
                    QuestionnaireRepository.InsertAnswer("personal", _userId, "Primary financial goal", txtQ1.Text.Trim());
                }
                if (!string.IsNullOrWhiteSpace(txtQ2.Text))
                {
                    QuestionnaireRepository.InsertAnswer("personal", _userId, "Notes about current expenses/income", txtQ2.Text.Trim());
                }
                MessageBox.Show("Questionnaire submitted.");
                txtQ1.Text = string.Empty; txtQ2.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoadPicture(byte[] pic)
        {
            if (pic == null) { picProfile.Image = null; return; }
            using (var ms = new MemoryStream(pic)) { picProfile.Image = Image.FromStream(ms); }
        }

        private void BtnRequestPasswordChange_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_userId <= 0) { MessageBox.Show("User not loaded."); return; }
                
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

                        string currentPasswordHash = SignUpViewModel.HashStatic(txtCurrentPassword.Text);
                        string verifyQuery = "SELECT password FROM users WHERE user_id = @user_id";
                        var verifyDt = Database.GetData(verifyQuery, new MySqlParameter("@user_id", _userId));
                        
                        if (verifyDt.Rows.Count == 0 || verifyDt.Rows[0]["password"].ToString() != currentPasswordHash)
                        {
                            MessageBox.Show("Current password is incorrect.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string newPasswordHash = SignUpViewModel.HashStatic(txtNewPassword.Text);
                        string reason = string.IsNullOrWhiteSpace(txtReason.Text) ? "User requested password change" : txtReason.Text;
                        
                        string ticketDescription = $"User {_username} requests a password change.\n\n" +
                                                 $"Reason: {reason}\n" +
                                                 $"New Password Hash: {newPasswordHash}\n" +
                                                 $"Request Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n" +
                                                 $"Note: Current password has been verified. Admin can approve to apply the new password.";

                        Ticket.CreateForPersonal(_userId, "Password Change", "Password Change Request", ticketDescription);
                        
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
            if (_userId <= 0) { MessageBox.Show("User not loaded."); return; }

            using (var dlg = new Form { Text = "Confirm Deletion", Width = 380, Height = 180, StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false })
            {
                var lbl = new Label { Text = "Enter your password to delete account:", Location = new Point(12, 15), AutoSize = true };
                var txt = new TextBox { Location = new Point(16, 45), Width = 330, PasswordChar = '*' };
                var btnOk = new Button { Text = "Delete", Location = new Point(16, 85), Width = 90 };
                var btnCancel = new Button { Text = "Cancel", Location = new Point(116, 85), Width = 90 };
                btnCancel.Click += (s, a) => dlg.Close();
                btnOk.Click += (s, a) =>
                {
                    try
                    {
                        string hashed = HashPassword(txt.Text);
                        var dt = Database.GetData("SELECT COUNT(*) FROM users WHERE username=@u AND password=@p", new MySqlParameter("@u", _username), new MySqlParameter("@p", hashed));
                        int ok = Convert.ToInt32(dt.Rows[0][0]);
                        if (ok == 0)
                        {
                            MessageBox.Show("Password incorrect.");
                            return;
                        }

                        Ticket.CreateForPersonal(_userId, "DeleteAccount", "Account Deletion", $"User {_username} requested account deletion.", "High");
                        Database.ExecuteQuery("DELETE FROM users WHERE username=@u AND password=@p", new MySqlParameter("@u", _username), new MySqlParameter("@p", hashed));

                        MessageBox.Show("Your account has been deleted.");
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
    }
}
