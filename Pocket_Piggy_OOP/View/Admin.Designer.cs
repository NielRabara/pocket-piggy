using System;
using System.Drawing;
using System.Windows.Forms;

namespace PocketPiggy
{
    partial class Admin
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvUsers;
        private DataGridView dgvRequests;
        private Label lblUsers;
        private Label lblRequests;
        private Button btnApprove;
        private Button btnReject;
        private Button btnBackToMenu;
        private Button btnRefresh;
        private Panel panelUsers;
        private Panel panelRequests;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            dgvUsers = new DataGridView();
            dgvRequests = new DataGridView();
            lblUsers = new Label();
            lblRequests = new Label();
            btnApprove = new Button();
            btnReject = new Button();
            btnBackToMenu = new Button();
            btnRefresh = new Button();
            panelUsers = new Panel();
            panelRequests = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRequests).BeginInit();
            panelUsers.SuspendLayout();
            panelRequests.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.BackgroundColor = Color.LightPink;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(12, 50);
            dgvUsers.Margin = new Padding(4);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(725, 312);
            dgvUsers.TabIndex = 0;
            // 
            // dgvRequests
            // 
            dgvRequests.AllowUserToAddRows = false;
            dgvRequests.AllowUserToDeleteRows = false;
            dgvRequests.BackgroundColor = Color.LightPink;
            dgvRequests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRequests.Location = new Point(12, 50);
            dgvRequests.Margin = new Padding(4);
            dgvRequests.Name = "dgvRequests";
            dgvRequests.ReadOnly = true;
            dgvRequests.RowHeadersWidth = 51;
            dgvRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRequests.Size = new Size(725, 250);
            dgvRequests.TabIndex = 0;
            // 
            // lblUsers
            // 
            lblUsers.AutoSize = true;
            lblUsers.Font = new Font("Century", 16F, FontStyle.Bold);
            lblUsers.Location = new Point(12, 7);
            lblUsers.Margin = new Padding(4, 0, 4, 0);
            lblUsers.Name = "lblUsers";
            lblUsers.Size = new Size(236, 38);
            lblUsers.TabIndex = 1;
            lblUsers.Text = "All Users List";
            // 
            // lblRequests
            // 
            lblRequests.AutoSize = true;
            lblRequests.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRequests.Location = new Point(12, 9);
            lblRequests.Margin = new Padding(4, 0, 4, 0);
            lblRequests.Name = "lblRequests";
            lblRequests.Size = new Size(245, 38);
            lblRequests.TabIndex = 1;
            lblRequests.Text = "User Requests";
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.FromArgb(40, 167, 69);
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApprove.ForeColor = Color.White;
            btnApprove.Location = new Point(788, 400);
            btnApprove.Margin = new Padding(4);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(150, 44);
            btnApprove.TabIndex = 1;
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnReject
            // 
            btnReject.BackColor = Color.FromArgb(220, 53, 69);
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReject.ForeColor = Color.White;
            btnReject.Location = new Point(962, 400);
            btnReject.Margin = new Padding(4);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(150, 44);
            btnReject.TabIndex = 0;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;
            // 
            // btnBackToMenu
            // 
            btnBackToMenu.BackColor = Color.LightPink;
            btnBackToMenu.FlatStyle = FlatStyle.Popup;
            btnBackToMenu.Font = new Font("Segoe UI", 9F);
            btnBackToMenu.ForeColor = Color.Black;
            btnBackToMenu.Location = new Point(15, 412);
            btnBackToMenu.Margin = new Padding(4);
            btnBackToMenu.Name = "btnBackToMenu";
            btnBackToMenu.Size = new Size(150, 44);
            btnBackToMenu.TabIndex = 3;
            btnBackToMenu.Text = "Back to Menu";
            btnBackToMenu.UseVisualStyleBackColor = false;
            btnBackToMenu.Click += btnBackToMenu_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.LightPink;
            btnRefresh.FlatStyle = FlatStyle.Popup;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.Black;
            btnRefresh.Location = new Point(188, 412);
            btnRefresh.Margin = new Padding(4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(150, 44);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh Data";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // panelUsers
            // 
            panelUsers.Controls.Add(dgvUsers);
            panelUsers.Controls.Add(lblUsers);
            panelUsers.Location = new Point(15, 15);
            panelUsers.Margin = new Padding(4);
            panelUsers.Name = "panelUsers";
            panelUsers.Size = new Size(750, 375);
            panelUsers.TabIndex = 0;
            // 
            // panelRequests
            // 
            panelRequests.Controls.Add(dgvRequests);
            panelRequests.Controls.Add(lblRequests);
            panelRequests.Location = new Point(788, 15);
            panelRequests.Margin = new Padding(4);
            panelRequests.Name = "panelRequests";
            panelRequests.Size = new Size(750, 375);
            panelRequests.TabIndex = 1;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1562, 500);
            Controls.Add(btnReject);
            Controls.Add(btnApprove);
            Controls.Add(btnRefresh);
            Controls.Add(btnBackToMenu);
            Controls.Add(panelRequests);
            Controls.Add(panelUsers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Admin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Panel";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRequests).EndInit();
            panelUsers.ResumeLayout(false);
            panelUsers.PerformLayout();
            panelRequests.ResumeLayout(false);
            panelRequests.PerformLayout();
            ResumeLayout(false);
        }
    }
}
