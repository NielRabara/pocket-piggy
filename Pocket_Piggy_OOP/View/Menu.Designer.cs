using System;
using System.Drawing;
using System.Windows.Forms;

namespace PocketPiggy
{
    partial class Menu
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblGreeting;
        private Label lblCurrent;
        private Label lblTotalIncome;
        private Label lblTotalExpenses;
        private Button btnChangeInfo;
        private Button btnLogout;
        private Panel panelSavings;
        private Panel panelExpenses;
        private Panel panelBills;
        private Panel panelIncome;
        private Label lblSavings;
        private Label lblExpenses;
        private Label lblBills;
        private Label lblIncome;
        private PictureBox picProfile;
        private Button btnAdmin;
        private Button btnAddBalance;
        private Button btnViewTransactions;
        private Button btnSavings;
        private Button btnExpenses;
        private Button btnBills;
        private Button btnIncome;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            lblGreeting = new Label();
            lblCurrent = new Label();
            lblTotalIncome = new Label();
            lblTotalExpenses = new Label();
            btnChangeInfo = new Button();
            btnLogout = new Button();
            panelSavings = new Panel();
            btnSavings = new Button();
            lblSavings = new Label();
            panelExpenses = new Panel();
            btnExpenses = new Button();
            lblExpenses = new Label();
            panelBills = new Panel();
            btnBills = new Button();
            lblBills = new Label();
            panelIncome = new Panel();
            btnIncome = new Button();
            lblIncome = new Label();
            picProfile = new PictureBox();
            btnAdmin = new Button();
            btnViewTransactions = new Button();
            panelSavings.SuspendLayout();
            panelExpenses.SuspendLayout();
            panelBills.SuspendLayout();
            panelIncome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picProfile).BeginInit();
            SuspendLayout();
            // 
            // lblGreeting
            // 
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Century", 16F, FontStyle.Bold);
            lblGreeting.Location = new Point(245, 66);
            lblGreeting.Margin = new Padding(4, 0, 4, 0);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(224, 38);
            lblGreeting.TabIndex = 0;
            lblGreeting.Text = "Hello, {user}!";
            // 
            // lblCurrent
            // 
            lblCurrent.AutoSize = true;
            lblCurrent.Font = new Font("Segoe UI", 10F);
            lblCurrent.Location = new Point(688, 50);
            lblCurrent.Margin = new Padding(4, 0, 4, 0);
            lblCurrent.Name = "lblCurrent";
            lblCurrent.Size = new Size(81, 28);
            lblCurrent.TabIndex = 1;
            lblCurrent.Text = "Current:";
            // 
            // lblTotalIncome
            // 
            lblTotalIncome.AutoSize = true;
            lblTotalIncome.Font = new Font("Segoe UI", 10F);
            lblTotalIncome.Location = new Point(688, 88);
            lblTotalIncome.Margin = new Padding(4, 0, 4, 0);
            lblTotalIncome.Name = "lblTotalIncome";
            lblTotalIncome.Size = new Size(127, 28);
            lblTotalIncome.TabIndex = 2;
            lblTotalIncome.Text = "Total Income:";
            // 
            // lblTotalExpenses
            // 
            lblTotalExpenses.AutoSize = true;
            lblTotalExpenses.Font = new Font("Segoe UI", 10F);
            lblTotalExpenses.Location = new Point(688, 125);
            lblTotalExpenses.Margin = new Padding(4, 0, 4, 0);
            lblTotalExpenses.Name = "lblTotalExpenses";
            lblTotalExpenses.Size = new Size(141, 28);
            lblTotalExpenses.TabIndex = 3;
            lblTotalExpenses.Text = "Total Expenses:";
            // 
            // btnChangeInfo
            // 
            btnChangeInfo.BackColor = Color.LightPink;
            btnChangeInfo.FlatStyle = FlatStyle.Popup;
            btnChangeInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChangeInfo.Location = new Point(245, 116);
            btnChangeInfo.Margin = new Padding(4, 4, 4, 4);
            btnChangeInfo.Name = "btnChangeInfo";
            btnChangeInfo.Size = new Size(150, 38);
            btnChangeInfo.TabIndex = 4;
            btnChangeInfo.Text = "Change Info";
            btnChangeInfo.UseVisualStyleBackColor = false;
            btnChangeInfo.Click += btnChangeInfo_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.LightPink;
            btnLogout.FlatStyle = FlatStyle.Popup;
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogout.Location = new Point(872, 15);
            btnLogout.Margin = new Padding(4, 4, 4, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(112, 38);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Log Out";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // panelSavings
            // 
            panelSavings.BackColor = Color.LightPink;
            panelSavings.BorderStyle = BorderStyle.FixedSingle;
            panelSavings.Controls.Add(btnSavings);
            panelSavings.Controls.Add(lblSavings);
            panelSavings.Location = new Point(62, 200);
            panelSavings.Margin = new Padding(4, 4, 4, 4);
            panelSavings.Name = "panelSavings";
            panelSavings.Size = new Size(200, 200);
            panelSavings.TabIndex = 6;
            // 
            // btnSavings
            // 
            btnSavings.BackColor = Color.LightPink;
            btnSavings.FlatStyle = FlatStyle.Popup;
            btnSavings.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnSavings.Location = new Point(0, 0);
            btnSavings.Margin = new Padding(4, 4, 4, 4);
            btnSavings.Name = "btnSavings";
            btnSavings.Size = new Size(200, 42);
            btnSavings.TabIndex = 11;
            btnSavings.Text = "Savings";
            btnSavings.UseVisualStyleBackColor = false;
            btnSavings.Click += btnSavings_Click;
            // 
            // lblSavings
            // 
            lblSavings.Dock = DockStyle.Bottom;
            lblSavings.Font = new Font("Segoe UI", 10F);
            lblSavings.Location = new Point(0, 43);
            lblSavings.Margin = new Padding(4, 0, 4, 0);
            lblSavings.Name = "lblSavings";
            lblSavings.Size = new Size(198, 155);
            lblSavings.TabIndex = 12;
            lblSavings.TextAlign = ContentAlignment.TopCenter;
            // 
            // panelExpenses
            // 
            panelExpenses.BorderStyle = BorderStyle.FixedSingle;
            panelExpenses.Controls.Add(btnExpenses);
            panelExpenses.Controls.Add(lblExpenses);
            panelExpenses.Location = new Point(288, 200);
            panelExpenses.Margin = new Padding(4, 4, 4, 4);
            panelExpenses.Name = "panelExpenses";
            panelExpenses.Size = new Size(200, 200);
            panelExpenses.TabIndex = 7;
            // 
            // btnExpenses
            // 
            btnExpenses.BackColor = Color.LightPink;
            btnExpenses.FlatStyle = FlatStyle.Popup;
            btnExpenses.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnExpenses.Location = new Point(0, 0);
            btnExpenses.Margin = new Padding(4, 4, 4, 4);
            btnExpenses.Name = "btnExpenses";
            btnExpenses.Size = new Size(200, 42);
            btnExpenses.TabIndex = 13;
            btnExpenses.Text = "Expenses";
            btnExpenses.UseVisualStyleBackColor = false;
            btnExpenses.Click += btnExpenses_Click;
            // 
            // lblExpenses
            // 
            lblExpenses.BackColor = Color.LightPink;
            lblExpenses.Dock = DockStyle.Bottom;
            lblExpenses.Font = new Font("Segoe UI", 10F);
            lblExpenses.Location = new Point(0, 43);
            lblExpenses.Margin = new Padding(4, 0, 4, 0);
            lblExpenses.Name = "lblExpenses";
            lblExpenses.Size = new Size(198, 155);
            lblExpenses.TabIndex = 0;
            lblExpenses.TextAlign = ContentAlignment.TopCenter;
            // 
            // panelBills
            // 
            panelBills.BorderStyle = BorderStyle.FixedSingle;
            panelBills.Controls.Add(btnBills);
            panelBills.Controls.Add(lblBills);
            panelBills.Location = new Point(512, 200);
            panelBills.Margin = new Padding(4, 4, 4, 4);
            panelBills.Name = "panelBills";
            panelBills.Size = new Size(200, 200);
            panelBills.TabIndex = 8;
            // 
            // btnBills
            // 
            btnBills.BackColor = Color.LightPink;
            btnBills.FlatStyle = FlatStyle.Popup;
            btnBills.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnBills.Location = new Point(0, 0);
            btnBills.Margin = new Padding(4, 4, 4, 4);
            btnBills.Name = "btnBills";
            btnBills.Size = new Size(200, 42);
            btnBills.TabIndex = 13;
            btnBills.Text = "Bills";
            btnBills.UseVisualStyleBackColor = false;
            btnBills.Click += btnBills_Click;
            // 
            // lblBills
            // 
            lblBills.BackColor = Color.LightPink;
            lblBills.Dock = DockStyle.Bottom;
            lblBills.Font = new Font("Segoe UI", 10F);
            lblBills.Location = new Point(0, 43);
            lblBills.Margin = new Padding(4, 0, 4, 0);
            lblBills.Name = "lblBills";
            lblBills.Size = new Size(198, 155);
            lblBills.TabIndex = 0;
            lblBills.TextAlign = ContentAlignment.TopCenter;
            // 
            // panelIncome
            // 
            panelIncome.BorderStyle = BorderStyle.FixedSingle;
            panelIncome.Controls.Add(btnIncome);
            panelIncome.Controls.Add(lblIncome);
            panelIncome.Location = new Point(738, 200);
            panelIncome.Margin = new Padding(4, 4, 4, 4);
            panelIncome.Name = "panelIncome";
            panelIncome.Size = new Size(200, 200);
            panelIncome.TabIndex = 9;
            // 
            // btnIncome
            // 
            btnIncome.BackColor = Color.LightPink;
            btnIncome.FlatStyle = FlatStyle.Popup;
            btnIncome.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnIncome.Location = new Point(0, 0);
            btnIncome.Margin = new Padding(4, 4, 4, 4);
            btnIncome.Name = "btnIncome";
            btnIncome.Size = new Size(200, 42);
            btnIncome.TabIndex = 14;
            btnIncome.Text = "Income";
            btnIncome.UseVisualStyleBackColor = false;
            btnIncome.Click += btnIncome_Click;
            // 
            // lblIncome
            // 
            lblIncome.BackColor = Color.LightPink;
            lblIncome.Dock = DockStyle.Bottom;
            lblIncome.Font = new Font("Segoe UI", 10F);
            lblIncome.Location = new Point(0, 43);
            lblIncome.Margin = new Padding(4, 0, 4, 0);
            lblIncome.Name = "lblIncome";
            lblIncome.Size = new Size(198, 155);
            lblIncome.TabIndex = 0;
            lblIncome.TextAlign = ContentAlignment.TopCenter;
            // 
            // picProfile
            // 
            picProfile.BorderStyle = BorderStyle.FixedSingle;
            picProfile.Location = new Point(86, 29);
            picProfile.Margin = new Padding(4, 4, 4, 4);
            picProfile.Name = "picProfile";
            picProfile.Size = new Size(124, 124);
            picProfile.SizeMode = PictureBoxSizeMode.Zoom;
            picProfile.TabIndex = 0;
            picProfile.TabStop = false;
            picProfile.Click += picProfile_Click;
            // 
            // btnAdmin
            // 
            btnAdmin.BackColor = Color.LightPink;
            btnAdmin.FlatStyle = FlatStyle.Popup;
            btnAdmin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdmin.Location = new Point(402, 116);
            btnAdmin.Margin = new Padding(4, 4, 4, 4);
            btnAdmin.Name = "btnAdmin";
            btnAdmin.Size = new Size(150, 38);
            btnAdmin.TabIndex = 10;
            btnAdmin.Text = "Admin Panel";
            btnAdmin.UseVisualStyleBackColor = false;
            btnAdmin.Click += btnAdmin_Click;
            // 
            // btnViewTransactions
            // 
            btnViewTransactions.BackColor = Color.LightPink;
            btnViewTransactions.FlatStyle = FlatStyle.Popup;
            btnViewTransactions.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnViewTransactions.Location = new Point(61, 422);
            btnViewTransactions.Margin = new Padding(4, 4, 4, 4);
            btnViewTransactions.Name = "btnViewTransactions";
            btnViewTransactions.Size = new Size(150, 44);
            btnViewTransactions.TabIndex = 12;
            btnViewTransactions.Text = "View History";
            btnViewTransactions.UseVisualStyleBackColor = false;
            btnViewTransactions.Click += btnViewTransactions_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1000, 500);
            Controls.Add(btnViewTransactions);
            Controls.Add(btnAdmin);
            Controls.Add(picProfile);
            Controls.Add(lblGreeting);
            Controls.Add(lblCurrent);
            Controls.Add(lblTotalIncome);
            Controls.Add(lblTotalExpenses);
            Controls.Add(btnChangeInfo);
            Controls.Add(btnLogout);
            Controls.Add(panelSavings);
            Controls.Add(panelExpenses);
            Controls.Add(panelBills);
            Controls.Add(panelIncome);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            Load += Menu_Load;
            panelSavings.ResumeLayout(false);
            panelExpenses.ResumeLayout(false);
            panelBills.ResumeLayout(false);
            panelIncome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picProfile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
