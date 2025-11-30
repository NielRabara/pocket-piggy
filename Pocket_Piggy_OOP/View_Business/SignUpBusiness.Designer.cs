namespace PocketPiggy.View_Business
{
    partial class SignUpBusiness
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblUsername = new Label();
            lblBusinessName = new Label();
            lblBusinessType = new Label();
            lblContactNumber = new Label();
            lblPassword = new Label();
            lblConfirmPassword = new Label();
            txtUsername = new TextBox();
            txtBusinessName = new TextBox();
            txtBusinessType = new TextBox();
            txtContactNumber = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            checkBoxShowPassword = new CheckBox();
            btnProceed = new Button();
            linkBackToLogin = new LinkLabel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Century", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(143, 33);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(497, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Create Your Business Account";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(114, 133);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(180, 28);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Business Username:";
            // 
            // lblBusinessName
            // 
            lblBusinessName.AutoSize = true;
            lblBusinessName.Font = new Font("Segoe UI", 10F);
            lblBusinessName.Location = new Point(114, 200);
            lblBusinessName.Margin = new Padding(4, 0, 4, 0);
            lblBusinessName.Name = "lblBusinessName";
            lblBusinessName.Size = new Size(145, 28);
            lblBusinessName.TabIndex = 3;
            lblBusinessName.Text = "Business Name:";
            // 
            // lblBusinessType
            // 
            lblBusinessType.AutoSize = true;
            lblBusinessType.Font = new Font("Segoe UI", 10F);
            lblBusinessType.Location = new Point(114, 267);
            lblBusinessType.Margin = new Padding(4, 0, 4, 0);
            lblBusinessType.Name = "lblBusinessType";
            lblBusinessType.Size = new Size(134, 28);
            lblBusinessType.TabIndex = 5;
            lblBusinessType.Text = "Business Type:";
            // 
            // lblContactNumber
            // 
            lblContactNumber.AutoSize = true;
            lblContactNumber.Font = new Font("Segoe UI", 10F);
            lblContactNumber.Location = new Point(114, 333);
            lblContactNumber.Margin = new Padding(4, 0, 4, 0);
            lblContactNumber.Name = "lblContactNumber";
            lblContactNumber.Size = new Size(161, 28);
            lblContactNumber.TabIndex = 7;
            lblContactNumber.Text = "Contact Number:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(114, 400);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(97, 28);
            lblPassword.TabIndex = 9;
            lblPassword.Text = "Password:";
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 10F);
            lblConfirmPassword.Location = new Point(114, 467);
            lblConfirmPassword.Margin = new Padding(4, 0, 4, 0);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(172, 28);
            lblConfirmPassword.TabIndex = 11;
            lblConfirmPassword.Text = "Confirm Password:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(343, 128);
            txtUsername.Margin = new Padding(4, 5, 4, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(313, 34);
            txtUsername.TabIndex = 2;
            // 
            // txtBusinessName
            // 
            txtBusinessName.Font = new Font("Segoe UI", 10F);
            txtBusinessName.Location = new Point(343, 195);
            txtBusinessName.Margin = new Padding(4, 5, 4, 5);
            txtBusinessName.Name = "txtBusinessName";
            txtBusinessName.Size = new Size(313, 34);
            txtBusinessName.TabIndex = 4;
            // 
            // txtBusinessType
            // 
            txtBusinessType.Font = new Font("Segoe UI", 10F);
            txtBusinessType.Location = new Point(343, 262);
            txtBusinessType.Margin = new Padding(4, 5, 4, 5);
            txtBusinessType.Name = "txtBusinessType";
            txtBusinessType.Size = new Size(313, 34);
            txtBusinessType.TabIndex = 6;
            // 
            // txtContactNumber
            // 
            txtContactNumber.Font = new Font("Segoe UI", 10F);
            txtContactNumber.Location = new Point(343, 328);
            txtContactNumber.Margin = new Padding(4, 5, 4, 5);
            txtContactNumber.Name = "txtContactNumber";
            txtContactNumber.Size = new Size(313, 34);
            txtContactNumber.TabIndex = 8;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(343, 395);
            txtPassword.Margin = new Padding(4, 5, 4, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(313, 34);
            txtPassword.TabIndex = 10;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            txtConfirmPassword.Location = new Point(343, 462);
            txtConfirmPassword.Margin = new Padding(4, 5, 4, 5);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(313, 34);
            txtConfirmPassword.TabIndex = 12;
            // 
            // checkBoxShowPassword
            // 
            checkBoxShowPassword.AutoSize = true;
            checkBoxShowPassword.Font = new Font("Segoe UI", 9F);
            checkBoxShowPassword.Location = new Point(343, 513);
            checkBoxShowPassword.Margin = new Padding(4, 5, 4, 5);
            checkBoxShowPassword.Name = "checkBoxShowPassword";
            checkBoxShowPassword.Size = new Size(162, 29);
            checkBoxShowPassword.TabIndex = 13;
            checkBoxShowPassword.Text = "Show Password";
            checkBoxShowPassword.CheckedChanged += checkBoxShowPassword_CheckedChanged;
            // 
            // btnProceed
            // 
            btnProceed.BackColor = Color.LightPink;
            btnProceed.FlatStyle = FlatStyle.Popup;
            btnProceed.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnProceed.ForeColor = Color.Black;
            btnProceed.Location = new Point(343, 567);
            btnProceed.Margin = new Padding(4, 5, 4, 5);
            btnProceed.Name = "btnProceed";
            btnProceed.Size = new Size(314, 58);
            btnProceed.TabIndex = 14;
            btnProceed.Text = "Create Account";
            btnProceed.UseVisualStyleBackColor = false;
            btnProceed.Click += btnProceed_Click;
            // 
            // linkBackToLogin
            // 
            linkBackToLogin.AutoSize = true;
            linkBackToLogin.Font = new Font("Segoe UI", 9F);
            linkBackToLogin.Location = new Point(436, 650);
            linkBackToLogin.Margin = new Padding(4, 0, 4, 0);
            linkBackToLogin.Name = "linkBackToLogin";
            linkBackToLogin.Size = new Size(125, 25);
            linkBackToLogin.TabIndex = 15;
            linkBackToLogin.TabStop = true;
            linkBackToLogin.Text = "Back to Log In";
            linkBackToLogin.LinkClicked += linkBackToLogin_LinkClicked;
            // 
            // SignUpBusiness
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(800, 733);
            Controls.Add(linkBackToLogin);
            Controls.Add(btnProceed);
            Controls.Add(checkBoxShowPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtContactNumber);
            Controls.Add(lblContactNumber);
            Controls.Add(txtBusinessType);
            Controls.Add(lblBusinessType);
            Controls.Add(txtBusinessName);
            Controls.Add(lblBusinessName);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "SignUpBusiness";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign Up";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblBusinessName;
        private System.Windows.Forms.Label lblBusinessType;
        private System.Windows.Forms.Label lblContactNumber;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtBusinessName;
        private System.Windows.Forms.TextBox txtBusinessType;
        private System.Windows.Forms.TextBox txtContactNumber;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.CheckBox checkBoxShowPassword;
        private System.Windows.Forms.Button btnProceed;
        private System.Windows.Forms.LinkLabel linkBackToLogin;
    }
}
