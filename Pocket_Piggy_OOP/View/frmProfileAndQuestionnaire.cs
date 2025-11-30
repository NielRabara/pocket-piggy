using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using PocketPiggy.Repositories;

namespace PocketPiggy.View
{
    public class frmProfileAndQuestionnaire : Form
    {
        private readonly string _personalUsername;
        private readonly bool _isBusiness;
        private int _businessId;
        private int _personalUserId;

        private TabControl tabs;

        private TextBox txtName;
        private PictureBox picProfile;
        private Button btnUploadPic;
        private Button btnSave;
        private Button btnCancel;
        private byte[] _currentPic;
        private string _currentName;

        private TextBox txtQ1;
        private TextBox txtQ2;
        private Button btnSubmitQuestionnaire;

        public frmProfileAndQuestionnaire(string personalUsername = null)
        {
            _personalUsername = personalUsername;
            _isBusiness = BusinessSession.IsLoggedIn && BusinessSession.CurrentBusinessId > 0;
            if (_isBusiness)
            {
                _businessId = BusinessSession.CurrentBusinessId;
            }

            InitializeUi();
            EnsureSchemas();
            LoadProfile();
        }

        private void EnsureSchemas()
        {
            try
            {
                ProfileRepository.EnsureSchema();
                QuestionnaireRepository.EnsureSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Schema ensure failed: {ex.Message}");
            }
        }

        private void InitializeUi()
        {
            this.Text = "Profile & Questionnaire";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            tabs = new TabControl { Dock = DockStyle.Fill };
            var tabProfile = new TabPage("Profile Info");
            var tabQ = new TabPage("Questionnaire");

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

            tabs.TabPages.Add(tabProfile);
            tabs.TabPages.Add(tabQ);
            this.Controls.Add(tabs);
        }

        private void LoadProfile()
        {
            try
            {
                if (_isBusiness)
                {
                    var (name, pic) = ProfileRepository.GetBusinessProfile(_businessId);
                    _currentName = name;
                    _currentPic = pic;
                    txtName.Text = name ?? string.Empty;
                    LoadPicture(pic);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(_personalUsername))
                    {
                        MessageBox.Show("No personal username provided.");
                        return;
                    }
                    var (uid, name, pic) = ProfileRepository.GetPersonalProfileByUsername(_personalUsername);
                    _personalUserId = uid;
                    _currentName = name;
                    _currentPic = pic;
                    txtName.Text = name ?? string.Empty;
                    LoadPicture(pic);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load profile: {ex.Message}");
            }
        }

        private void LoadPicture(byte[] pic)
        {
            if (pic == null)
            {
                picProfile.Image = null;
                return;
            }
            using (var ms = new MemoryStream(pic))
            {
                picProfile.Image = Image.FromStream(ms);
            }
        }

        private void BtnUploadPic_Click(object sender, EventArgs e)
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text?.Trim();
                if (_isBusiness)
                {
                    ProfileRepository.UpdateBusinessProfile(_businessId, name, _currentPic);
                    MessageBox.Show("Business profile updated.");
                }
                else
                {
                    if (_personalUserId <= 0)
                    {
                        MessageBox.Show("No personal user ID loaded.");
                        return;
                    }
                    ProfileRepository.UpdatePersonalProfile(_personalUserId, name, _currentPic);
                    MessageBox.Show("Profile updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed: {ex.Message}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = _currentName ?? string.Empty;
            LoadPicture(_currentPic);
        }

        private void BtnSubmitQuestionnaire_Click(object sender, EventArgs e)
        {
            try
            {
                string userType = _isBusiness ? "business" : "personal";
                int userId = _isBusiness ? _businessId : _personalUserId;
                if (userId <= 0)
                {
                    MessageBox.Show("User not loaded.");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(txtQ1.Text))
                {
                    QuestionnaireRepository.InsertAnswer(userType, userId, "Primary financial goal", txtQ1.Text.Trim());
                }
                if (!string.IsNullOrWhiteSpace(txtQ2.Text))
                {
                    QuestionnaireRepository.InsertAnswer(userType, userId, "Notes about current expenses/income", txtQ2.Text.Trim());
                }
                MessageBox.Show("Questionnaire submitted.");
                txtQ1.Text = string.Empty; txtQ2.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Submit failed: {ex.Message}");
            }
        }
    }
}
