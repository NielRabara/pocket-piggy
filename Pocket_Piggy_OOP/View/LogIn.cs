using System;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using PocketPiggy.View;
using PocketPiggy.View_Business;
using PocketPiggy.ViewModels;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace PocketPiggy
{
    public partial class LogIn : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private bool isPaused = false;
        private bool isMuted = false;
        private string selectedUserType = "Personal"; 
        private System.Drawing.Image playIcon;
        private System.Drawing.Image pauseIcon;
        private System.Drawing.Image stopIcon;
        private System.Drawing.Image muteIcon;
        private System.Drawing.Image unmuteIcon;
        private System.Drawing.Image smallPlayIcon;
        private System.Drawing.Image smallPauseIcon;

        public LogIn()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            string logoPath = @"C:\Users\Niel Rabara\source\repos\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOP\PocketPiggyLogo.png";
            if (File.Exists(logoPath))
            {
                try
                {
                    playIcon = System.Drawing.Image.FromFile(logoPath);
                    pauseIcon = playIcon;
                    stopIcon = playIcon;
                    muteIcon = playIcon;
                    unmuteIcon = playIcon;

                    try
                    {
                        var bmp = new System.Drawing.Bitmap(logoPath);
                        var h = bmp.GetHicon();
                        this.Icon = System.Drawing.Icon.FromHandle(h);
                    }
                    catch { /* non-fatal */ }
                }
                catch
                {
                    playIcon = CreatePlayIcon(24, 24, System.Drawing.Color.White);
                    pauseIcon = CreatePauseIcon(24, 24, System.Drawing.Color.White);
                    stopIcon = CreateStopIcon(24, 24, System.Drawing.Color.White);
                    muteIcon = CreateMuteIcon(24, 24, System.Drawing.Color.White);
                    unmuteIcon = CreateUnmuteIcon(24, 24, System.Drawing.Color.White);
                }
            }
            else
            {
                playIcon = CreatePlayIcon(24, 24, System.Drawing.Color.White);
                pauseIcon = CreatePauseIcon(24, 24, System.Drawing.Color.White);
                stopIcon = CreateStopIcon(24, 24, System.Drawing.Color.White);
                muteIcon = CreateMuteIcon(24, 24, System.Drawing.Color.White);
                unmuteIcon = CreateUnmuteIcon(24, 24, System.Drawing.Color.White);
            }

            try
            {
                if (btnPlayPause != null)
                { 
                    smallPlayIcon = CreatePlayIcon(18, 18, System.Drawing.Color.White);
                    smallPauseIcon = CreatePauseIcon(18, 18, System.Drawing.Color.White);
                    btnPlayPause.Text = string.Empty;
                    btnPlayPause.Image = smallPauseIcon;
                    btnPlayPause.ImageAlign = ContentAlignment.MiddleCenter;
                }
                if (btnStop != null)
                {
                    btnStop.Text = string.Empty;
                    btnStop.Image = stopIcon ?? CreateStopIcon(18,18,System.Drawing.Color.White);
                    btnStop.ImageAlign = ContentAlignment.MiddleCenter;
                }
                if (btnMute != null)
                {
                    btnMute.Text = string.Empty;
                    btnMute.Image = unmuteIcon ?? CreateUnmuteIcon(18,18,System.Drawing.Color.White);
                    btnMute.ImageAlign = ContentAlignment.MiddleCenter;
                }
            }
            catch { }

            try
            {
                // Initialize LibVLC with the path to the native libraries
                Core.Initialize();

                // Create LibVLC instance with hardware acceleration enabled
                _libVLC = new LibVLC(
                    "--no-xlib", // Don't use Xlib (Linux specific, but safe to include)
                    "--aout=directsound" // Use DirectSound for Windows audio
                );

                _mediaPlayer = new MediaPlayer(_libVLC);
                videoView1.MediaPlayer = _mediaPlayer;

                _mediaPlayer.EnableHardwareDecoding = true;
                _mediaPlayer.Media = null; 

                PlayLoginVideo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize video player: {ex.Message}",
                    "Video Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                videoView1.Visible = false;
            }

        }

        private void PlayLoginVideo()
        {
            try
            {
                if (_libVLC == null || _mediaPlayer == null)
                {
                    throw new InvalidOperationException("LibVLC or MediaPlayer is not initialized.");
                }

                string videoPath = @"C:\Users\Niel Rabara\source\repos\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOP\PocketPiggyLogIn.mp4";

                if (!File.Exists(videoPath))
                {
                    videoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PocketPiggyLogIn.mp4");
                    if (!File.Exists(videoPath))
                    {
                        videoPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, "PocketPiggyLogIn.mp4");
                    }
                }

                if (!File.Exists(videoPath))
                {
                    throw new FileNotFoundException($"Video file not found. Tried:\n{@"C:\Users\Niel Rabara\source\repos\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOP\PocketPiggyLogIn.mp4"}\n{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PocketPiggyLogIn.mp4")}");
                }

                if (_mediaPlayer.Media != null)
                {
                    _mediaPlayer.Stop();
                    _mediaPlayer.Media.Dispose();
                }

                var media = new Media(_libVLC, new Uri(videoPath));
                media.AddOption(":no-video-title-show"); 
                media.AddOption(":input-repeat=65535");  

                _mediaPlayer.Media = media;
                
                if (volumeSlider != null)
                {
                    _mediaPlayer.Volume = volumeSlider.Value;
                }

                if (!_mediaPlayer.Play())
                {
                    throw new Exception("Failed to start video playback.");
                }

                if (btnPlayPause != null)
                {
                    btnPlayPause.Text = string.Empty;
                    btnPlayPause.Image = smallPauseIcon;
                    isPaused = false;
                }

                if (videoView1 != null)
                {
                    _mediaPlayer.AspectRatio = $"{videoView1.Width}:{videoView1.Height}";
                }

                Console.WriteLine($"Playing video from: {videoPath}");
            }
            catch (Exception ex)
            {
                var errorLabel = new Label
                {
                    Text = "Video playback not available: " + ex.Message,
                    ForeColor = Color.White,
                    BackColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    AutoSize = false
                };

                if (videoView1.Parent != null)
                {
                    videoView1.Parent.Controls.Add(errorLabel);
                    errorLabel.BringToFront();
                }

                videoView1.Visible = false;
            }
        }

        private System.Drawing.Image CreatePlayIcon(int width, int height, System.Drawing.Color color)
        {
            var bmp = new System.Drawing.Bitmap(width, height);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                using (var b = new System.Drawing.SolidBrush(color))
                {
                    var pts = new System.Drawing.Point[] {
                        new System.Drawing.Point(width/4, height/6),
                        new System.Drawing.Point(width/4, height*5/6),
                        new System.Drawing.Point(width*3/4, height/2)
                    };
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillPolygon(b, pts);
                }
            }
            return bmp;
        }

        private System.Drawing.Image CreatePauseIcon(int width, int height, System.Drawing.Color color)
        {
            var bmp = new System.Drawing.Bitmap(width, height);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                using (var b = new System.Drawing.SolidBrush(color))
                {
                    int w = width/5;
                    int h = height*2/3;
                    int y = (height - h)/2;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillRectangle(b, width/4 - w/2, y, w, h);
                    g.FillRectangle(b, width*3/4 - w/2, y, w, h);
                }
            }
            return bmp;
        }

        private System.Drawing.Image CreateStopIcon(int width, int height, System.Drawing.Color color)
        {
            var bmp = new System.Drawing.Bitmap(width, height);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                using (var b = new System.Drawing.SolidBrush(color))
                {
                    int pad = width/6;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillRectangle(b, pad, pad, width - pad*2, height - pad*2);
                }
            }
            return bmp;
        }

        private System.Drawing.Image CreateMuteIcon(int width, int height, System.Drawing.Color color)
        {
            var bmp = new System.Drawing.Bitmap(width, height);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                using (var p = new System.Drawing.Pen(color, 2))
                using (var b = new System.Drawing.SolidBrush(color))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    
                    var pts = new System.Drawing.Point[] {
                        new System.Drawing.Point(width/6, height/3),
                        new System.Drawing.Point(width/3, height/3),
                        new System.Drawing.Point(width*2/3, height/6),
                        new System.Drawing.Point(width*2/3, height*5/6),
                        new System.Drawing.Point(width/3, height*2/3),
                        new System.Drawing.Point(width/6, height*2/3)
                    };
                    g.FillPolygon(b, pts);
                }
            }
            return bmp;
        }

        private System.Drawing.Image CreateUnmuteIcon(int width, int height, System.Drawing.Color color)
        {
            var bmp = CreateMuteIcon(width, height, color);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            using (var p = new System.Drawing.Pen(color, 2))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                g.DrawArc(p, width/2, height/4, width/3, height/2, -30, 60);
            }
            return bmp;
        }

        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mediaPlayer == null || btnPlayPause == null) 
                {
                    MessageBox.Show("Media player is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_mediaPlayer.IsPlaying)
                {
                    _mediaPlayer.Pause();
                    isPaused = true;
                    if (btnPlayPause != null && smallPlayIcon != null)
                        btnPlayPause.Image = smallPlayIcon;
                    Console.WriteLine("Video paused");
                    return;
                }

                {
                    if (_mediaPlayer.Media == null)
                    {
                        PlayLoginVideo();
                    }
                    
                    if (_mediaPlayer.Play())
                    {
                        isPaused = false;
                        if (btnPlayPause != null && smallPauseIcon != null)
                            btnPlayPause.Image = smallPauseIcon;
                        
                        if (videoView1 != null)
                        {
                            _mediaPlayer.AspectRatio = $"{videoView1.Width}:{videoView1.Height}";
                        }
                        Console.WriteLine("Video playing");
                    }
                    else
                    {
                        MessageBox.Show("Failed to play video.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error controlling video playback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Play/Pause error: {ex}");
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _mediaPlayer.Stop();
            if (btnPlayPause != null && smallPlayIcon != null)
                btnPlayPause.Image = smallPlayIcon;
             isPaused = false;
        }

        private void VolumeSlider_Scroll(object sender, EventArgs e)
        {
            if (_mediaPlayer != null && volumeSlider != null)
            {
                _mediaPlayer.Volume = volumeSlider.Value;
                
                if (volumeSlider.Value == 0 && !isMuted)
                {
                    _mediaPlayer.Mute = true;
                    isMuted = true;
                    if (btnMute != null && muteIcon != null)
                    {
                        btnMute.Image = muteIcon;
                        btnMute.Text = string.Empty;
                    }
                }
                else if (volumeSlider.Value > 0 && isMuted)
                {
                    _mediaPlayer.Mute = false;
                    isMuted = false;
                    if (btnMute != null && unmuteIcon != null)
                    {
                        btnMute.Image = unmuteIcon;
                        btnMute.Text = string.Empty;
                    }
                }
            }
        }

        private void BtnMute_Click(object sender, EventArgs e)
        {
            if (_mediaPlayer == null || btnMute == null) return;
            
            isMuted = !isMuted;
            _mediaPlayer.Mute = isMuted;
            if (isMuted && muteIcon != null)
            {
                btnMute.Image = muteIcon;
                btnMute.Text = string.Empty;
            }
            else if (!isMuted && unmuteIcon != null)
            {
                btnMute.Image = unmuteIcon;
                btnMute.Text = string.Empty;
            }
            
             if (!isMuted && volumeSlider != null && volumeSlider.Value == 0)
             {
                 volumeSlider.Value = 50;
                 _mediaPlayer.Volume = 50;
             }
         }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string userID = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your user ID and password.",
                    "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var loginViewModel = new LoginViewModel();
                if (selectedUserType == "Business")
                {
                    var (success, authenticatedUsername, userType, businessId, businessName) = loginViewModel.AuthenticateBusinessUser(userID, password);
                    if (success)
                    {
                        MessageBox.Show("Login successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        if (businessId > 0)
                        {
                            BusinessSession.CurrentBusinessId = businessId;
                            BusinessSession.CurrentBusinessName = businessName;
                        }
                        StopAndDisposeVideo();
                        businessMain mainForm = new businessMain(authenticatedUsername);
                        mainForm.Show();
                        this.Hide();
                        return;
                    }
                    MessageBox.Show("Invalid business username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                {
                    var (success, authenticatedUsername, userType) = loginViewModel.AuthenticatePersonalUser(userID, password);
                    if (success)
                    {
                        MessageBox.Show("Login successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        StopAndDisposeVideo();
                        Menu menuForm = new Menu(authenticatedUsername);
                        menuForm.Show();
                        this.Hide();
                        return;
                    }
                    MessageBox.Show("Invalid personal username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\n{ex.StackTrace}", 
                    "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopAndDisposeVideo()
        {
            try
            {
                if (_mediaPlayer != null)
                {
                    try { _mediaPlayer.Stop(); } catch { }
                    try { _mediaPlayer.Media?.Dispose(); } catch { }
                    try { _mediaPlayer.Dispose(); } catch { }
                    _mediaPlayer = null;
                }
                if (_libVLC != null)
                {
                    try { _libVLC.Dispose(); } catch { }
                    _libVLC = null;
                }
                if (videoView1 != null)
                {
                    videoView1.Visible = false;
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form selectForm = new Form()
            {
                Width = 350,
                Height = 200,
                Text = "Select Account Type",
                StartPosition = FormStartPosition.CenterParent
            };

            Label lbl = new Label()
            {
                Text = "Choose account type to create:",
                AutoSize = true,
                Top = 20,
                Left = 50
            };
            Button btnPersonal = new Button()
            {
                Text = "Personal",
                Width = 100,
                Top = 70,
                Left = 40
            };
            Button btnBusiness = new Button()
            {
                Text = "Business",
                Width = 100,
                Top = 70,
                Left = 180
            };

            btnPersonal.Click += (s, args) =>
            {
                selectForm.DialogResult = DialogResult.OK;
                selectForm.Tag = "Personal";
                selectForm.Close();
            };
            btnBusiness.Click += (s, args) =>
            {
                selectForm.DialogResult = DialogResult.OK;
                selectForm.Tag = "Business";
                selectForm.Close();
            };

            selectForm.Controls.Add(lbl);
            selectForm.Controls.Add(btnPersonal);
            selectForm.Controls.Add(btnBusiness);

            var result = selectForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                string choice = selectForm.Tag.ToString();

                if (choice == "Personal")
                {
                    SignUp signUpForm = new SignUp("Personal");
                    signUpForm.Show();
                    this.Hide();
                }
                else if (choice == "Business")
                {
                    SignUpBusiness business = new SignUpBusiness("Business");
                    business.Show();
                    this.Hide();
                }
            }
        }

        private void btnLoginPersonal_Click(object sender, EventArgs e)
        {
            btnLoginPersonal.BackColor = System.Drawing.Color.FromArgb(103, 58, 183);
            btnLoginBusiness.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            selectedUserType = "Personal";
        }

        private void btnLoginBusiness_Click(object sender, EventArgs e)
        {
            btnLoginBusiness.BackColor = System.Drawing.Color.FromArgb(103, 58, 183);
            btnLoginPersonal.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            selectedUserType = "Business";
        }

        private void lblGreeting_Click(object sender, EventArgs e) { }
        private void lblUsername_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void LogIn_Load(object sender, EventArgs e) { }

        private void videoView1_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
