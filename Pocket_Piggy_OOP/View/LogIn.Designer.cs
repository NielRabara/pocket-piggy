namespace PocketPiggy
{
    partial class LogIn
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private LibVLCSharp.WinForms.VideoView videoView1;
        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.Button btnLoginPersonal;
        private System.Windows.Forms.Button btnLoginBusiness;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        // Added controls used in LogIn.cs
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnMute;
        private System.Windows.Forms.TrackBar volumeSlider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelLeft = new Panel();
            linkLabel1 = new LinkLabel();
            label2 = new Label();
            checkBox1 = new CheckBox();
            label1 = new Label();
            btnSignIn = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            btnLoginBusiness = new Button();
            btnLoginPersonal = new Button();
            lblGreeting = new Label();
            panelRight = new Panel();
            videoView1 = new LibVLCSharp.WinForms.VideoView();
            controlsPanel = new Panel();
            btnPlayPause = new Button();
            btnStop = new Button();
            btnMute = new Button();
            volumeSlider = new TrackBar();
            process1 = new System.Diagnostics.Process();
            timer1 = new System.Windows.Forms.Timer(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
            controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(248, 249, 250);
            panelLeft.Controls.Add(linkLabel1);
            panelLeft.Controls.Add(label2);
            panelLeft.Controls.Add(checkBox1);
            panelLeft.Controls.Add(label1);
            panelLeft.Controls.Add(btnSignIn);
            panelLeft.Controls.Add(txtPassword);
            panelLeft.Controls.Add(lblPassword);
            panelLeft.Controls.Add(txtUsername);
            panelLeft.Controls.Add(lblUsername);
            panelLeft.Controls.Add(btnLoginBusiness);
            panelLeft.Controls.Add(btnLoginPersonal);
            panelLeft.Controls.Add(lblGreeting);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(446, 683);
            panelLeft.TabIndex = 0;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(273, 578);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(54, 20);
            linkLabel1.TabIndex = 15;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Sign Up";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(114, 578);
            label2.Name = "label2";
            label2.Size = new Size(163, 20);
            label2.TabIndex = 14;
            label2.Text = "Don't have an account?";
            label2.Click += label2_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(57, 439);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(132, 24);
            checkBox1.TabIndex = 13;
            checkBox1.Text = "Show Password";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(51, 51, 51);
            label1.Location = new Point(40, 93);
            label1.Name = "label1";
            label1.Size = new Size(388, 38);
            label1.TabIndex = 8;
            label1.Text = "Your Personal Budget Tracker";
            label1.Click += label1_Click;
            // 
            // btnSignIn
            // 
            btnSignIn.BackColor = SystemColors.AppWorkspace;
            btnSignIn.FlatAppearance.BorderSize = 0;
            btnSignIn.FlatStyle = FlatStyle.Flat;
            btnSignIn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSignIn.ForeColor = Color.White;
            btnSignIn.Location = new Point(56, 498);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(343, 60);
            btnSignIn.TabIndex = 7;
            btnSignIn.Text = "Sign In";
            btnSignIn.UseVisualStyleBackColor = false;
            btnSignIn.Click += btnSignIn_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(57, 398);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(342, 34);
            txtPassword.TabIndex = 6;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10.8F);
            lblPassword.ForeColor = Color.FromArgb(51, 51, 51);
            lblPassword.Location = new Point(56, 366);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(91, 25);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(57, 309);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(342, 34);
            txtUsername.TabIndex = 4;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10.8F);
            lblUsername.ForeColor = Color.FromArgb(51, 51, 51);
            lblUsername.Location = new Point(58, 280);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(95, 25);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Username:";
            lblUsername.Click += lblUsername_Click;
            // 
            // btnLoginBusiness
            // 
            btnLoginBusiness.BackColor = Color.FromArgb(200, 200, 200);
            btnLoginBusiness.FlatAppearance.BorderSize = 0;
            btnLoginBusiness.FlatStyle = FlatStyle.Flat;
            btnLoginBusiness.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLoginBusiness.ForeColor = Color.FromArgb(100, 100, 100);
            btnLoginBusiness.Location = new Point(234, 169);
            btnLoginBusiness.Name = "btnLoginBusiness";
            btnLoginBusiness.Size = new Size(137, 60);
            btnLoginBusiness.TabIndex = 2;
            btnLoginBusiness.Text = "Business";
            btnLoginBusiness.UseVisualStyleBackColor = false;
            btnLoginBusiness.Click += btnLoginBusiness_Click;
            // 
            // btnLoginPersonal
            // 
            btnLoginPersonal.BackColor = Color.FromArgb(103, 58, 183);
            btnLoginPersonal.FlatAppearance.BorderSize = 0;
            btnLoginPersonal.FlatStyle = FlatStyle.Flat;
            btnLoginPersonal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLoginPersonal.ForeColor = Color.White;
            btnLoginPersonal.Location = new Point(67, 169);
            btnLoginPersonal.Name = "btnLoginPersonal";
            btnLoginPersonal.Size = new Size(137, 60);
            btnLoginPersonal.TabIndex = 1;
            btnLoginPersonal.Text = "Personal";
            btnLoginPersonal.UseVisualStyleBackColor = false;
            btnLoginPersonal.Click += btnLoginPersonal_Click;
            // 
            // lblGreeting
            // 
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblGreeting.ForeColor = Color.FromArgb(51, 51, 51);
            lblGreeting.Location = new Point(40, 39);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(268, 54);
            lblGreeting.TabIndex = 0;
            lblGreeting.Text = "Pocket Piggy";
            lblGreeting.Click += lblGreeting_Click;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.Black;
            panelRight.Controls.Add(videoView1);
            panelRight.Controls.Add(controlsPanel);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(446, 0);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(740, 683);
            panelRight.TabIndex = 1;
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Dock = DockStyle.Fill;
            videoView1.Location = new Point(0, 0);
            videoView1.MediaPlayer = null;
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(740, 638);
            videoView1.TabIndex = 0;
            videoView1.Text = "videoView1";
            videoView1.Click += videoView1_Click;
            // 
            // controlsPanel
            // 
            controlsPanel.BackColor = Color.Transparent;
            controlsPanel.Controls.Add(btnPlayPause);
            controlsPanel.Controls.Add(volumeSlider);
            controlsPanel.Dock = DockStyle.Bottom;
            controlsPanel.Location = new Point(0, 638);
            controlsPanel.Name = "controlsPanel";
            controlsPanel.Padding = new Padding(20, 0, 20, 10);
            controlsPanel.Size = new Size(740, 45);
            controlsPanel.TabIndex = 1;
            // 
            // btnPlayPause
            // 
            btnPlayPause.BackColor = Color.Transparent;
            btnPlayPause.FlatAppearance.BorderSize = 0;
            btnPlayPause.FlatStyle = FlatStyle.Flat;
            // Use an emoji-capable font so unicode symbols render correctly (fallback to Segoe UI Symbol if unavailable)
            try { btnPlayPause.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Regular, GraphicsUnit.Point, 0); } catch { btnPlayPause.Font = new Font("Segoe UI Symbol", 14F, FontStyle.Regular, GraphicsUnit.Point, 0); }
            btnPlayPause.ForeColor = Color.White;
            btnPlayPause.Location = new Point(20, 5);
            btnPlayPause.Name = "btnPlayPause";
            btnPlayPause.Size = new Size(40, 40);
            btnPlayPause.TabIndex = 0;
            // Initialize with pause symbol U+23F8 (?) to reduce glyph substitution; disable mnemonics
            btnPlayPause.UseMnemonic = false;
            btnPlayPause.Text = "\u23F8"; // ?
            btnPlayPause.TextAlign = ContentAlignment.MiddleCenter;
            btnPlayPause.UseVisualStyleBackColor = false;
            btnPlayPause.Click += BtnPlayPause_Click;
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            // TrackBar does not reliably support transparent BackColor; use black to match video background
            volumeSlider.BackColor = Color.Black;
            volumeSlider.Location = new Point(80, 15);
            volumeSlider.Maximum = 100;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(150, 20);
            volumeSlider.TabIndex = 1;
            volumeSlider.Value = 100;
            volumeSlider.TickStyle = TickStyle.None;
            volumeSlider.Scroll += VolumeSlider_Scroll;
            // wire stop and mute buttons
            btnStop.Click += BtnStop_Click;
            btnMute.Click += BtnMute_Click;
            // 
            // process1
            // 
            process1.StartInfo.Domain = "";
            process1.StartInfo.LoadUserProfile = false;
            process1.StartInfo.Password = null;
            process1.StartInfo.StandardErrorEncoding = null;
            process1.StartInfo.StandardInputEncoding = null;
            process1.StartInfo.StandardOutputEncoding = null;
            process1.StartInfo.UseCredentialsForNetworkingOnly = false;
            process1.StartInfo.UserName = "";
            process1.SynchronizingObject = this;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1186, 683);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Name = "LogIn";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pocket Piggy - LogIn";
            Load += LogIn_Load;
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
            controlsPanel.ResumeLayout(false);
            controlsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel controlsPanel;
    }
}
