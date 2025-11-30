namespace PocketPiggy
{
    partial class Savings
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvGoals;
        private Button btnAddGoal;
        private Button btnMenu;
        private Label lblGoals;
        private FlowLayoutPanel flowGoals;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Savings));
            dgvGoals = new DataGridView();
            lblGoals = new Label();
            btnAddGoal = new Button();
            btnMenu = new Button();
            flowGoals = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvGoals).BeginInit();
            SuspendLayout();
            // 
            // dgvGoals
            // 
            dgvGoals.BackgroundColor = Color.LightPink;
            dgvGoals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGoals.Location = new Point(58, 75);
            dgvGoals.Margin = new Padding(4, 4, 4, 4);
            dgvGoals.Name = "dgvGoals";
            dgvGoals.RowHeadersWidth = 62;
            dgvGoals.Size = new Size(575, 400);
            dgvGoals.TabIndex = 0;
            // 
            // lblGoals
            // 
            lblGoals.AutoSize = true;
            lblGoals.Font = new Font("Century", 16F, FontStyle.Bold);
            lblGoals.Location = new Point(58, 31);
            lblGoals.Margin = new Padding(4, 0, 4, 0);
            lblGoals.Name = "lblGoals";
            lblGoals.Size = new Size(224, 38);
            lblGoals.TabIndex = 1;
            lblGoals.Text = "Saving Goals";
            // 
            // btnAddGoal
            // 
            btnAddGoal.BackColor = Color.LightPink;
            btnAddGoal.FlatStyle = FlatStyle.Popup;
            btnAddGoal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddGoal.Location = new Point(58, 500);
            btnAddGoal.Margin = new Padding(4, 4, 4, 4);
            btnAddGoal.Name = "btnAddGoal";
            btnAddGoal.Size = new Size(188, 56);
            btnAddGoal.TabIndex = 2;
            btnAddGoal.Text = "Add New Goal";
            btnAddGoal.UseVisualStyleBackColor = false;
            btnAddGoal.Click += btnAddGoal_Click;
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.LightPink;
            btnMenu.FlatStyle = FlatStyle.Popup;
            btnMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMenu.Location = new Point(250, 500);
            btnMenu.Margin = new Padding(4, 4, 4, 4);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(150, 56);
            btnMenu.TabIndex = 3;
            btnMenu.Text = "Menu";
            btnMenu.UseVisualStyleBackColor = false;
            btnMenu.Click += btnMenu_Click;
            // 
            // flowGoals
            // 
            flowGoals.AutoScroll = true;
            flowGoals.BackColor = Color.LightPink;
            flowGoals.FlowDirection = FlowDirection.TopDown;
            flowGoals.Location = new Point(650, 75);
            flowGoals.Margin = new Padding(4, 4, 4, 4);
            flowGoals.Name = "flowGoals";
            flowGoals.Size = new Size(600, 400);
            flowGoals.TabIndex = 4;
            flowGoals.WrapContents = false;
            // 
            // Savings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1312, 600);
            Controls.Add(dgvGoals);
            Controls.Add(lblGoals);
            Controls.Add(btnAddGoal);
            Controls.Add(btnMenu);
            Controls.Add(flowGoals);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "Savings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Savings";
            ((System.ComponentModel.ISupportInitialize)dgvGoals).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
