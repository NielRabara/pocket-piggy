namespace PocketPiggy
{
    partial class businessMain
    {
        private System.ComponentModel.IContainer components = null;

        private PictureBox businessLogo;
        private Label lblGreeting;
        private Button btnDashboard;
        private Button btnKpi;
        private Button btnChangeInfo;
        private Button btnLogout;
        private Button btnCashReserve;
        private Button btnIncome;
        private Button btnExpenses;
        private Button btnInventory;
        private Button btnReceivables;
        private Panel reservePanel;
        private Panel incomePanel;
        private Panel expensesPanel;
        private Panel receivablesPanel;
        private Panel inventoryPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(businessMain));
            businessLogo = new PictureBox();
            lblGreeting = new Label();
            btnDashboard = new Button();
            btnKpi = new Button();
            btnChangeInfo = new Button();
            btnLogout = new Button();
            btnCashReserve = new Button();
            btnIncome = new Button();
            btnExpenses = new Button();
            btnReceivables = new Button();
            btnInventory = new Button();
            reservePanel = new Panel();
            incomePanel = new Panel();
            expensesPanel = new Panel();
            receivablesPanel = new Panel();
            inventoryPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)businessLogo).BeginInit();
            SuspendLayout();
            // 
            // businessLogo
            // 
            businessLogo.Location = new Point(38, 48);
            businessLogo.Margin = new Padding(4, 4, 4, 4);
            businessLogo.Name = "businessLogo";
            businessLogo.Size = new Size(189, 168);
            businessLogo.TabIndex = 15;
            businessLogo.TabStop = false;
            // 
            // lblGreeting
            // 
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Castellar", 20F);
            lblGreeting.Location = new Point(250, 59);
            lblGreeting.Margin = new Padding(4, 0, 4, 0);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(246, 96);
            lblGreeting.TabIndex = 14;
            lblGreeting.Text = "{Business \r\nName}\r\n";
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.LightPink;
            btnDashboard.FlatStyle = FlatStyle.Popup;
            btnDashboard.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDashboard.Location = new Point(532, 32);
            btnDashboard.Margin = new Padding(4, 4, 4, 4);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(314, 82);
            btnDashboard.TabIndex = 13;
            btnDashboard.Text = "Dashboard Summary";
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // btnKpi
            // 
            btnKpi.BackColor = Color.LightPink;
            btnKpi.FlatStyle = FlatStyle.Popup;
            btnKpi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKpi.Location = new Point(532, 134);
            btnKpi.Margin = new Padding(4, 4, 4, 4);
            btnKpi.Name = "btnKpi";
            btnKpi.Size = new Size(314, 82);
            btnKpi.TabIndex = 12;
            btnKpi.Text = "KPI Summary";
            btnKpi.UseVisualStyleBackColor = false;
            btnKpi.Click += btnKpi_Click;
            // 
            // btnChangeInfo
            // 
            btnChangeInfo.BackColor = Color.LightPink;
            btnChangeInfo.FlatStyle = FlatStyle.Popup;
            btnChangeInfo.Location = new Point(250, 171);
            btnChangeInfo.Margin = new Padding(4, 4, 4, 4);
            btnChangeInfo.Name = "btnChangeInfo";
            btnChangeInfo.Size = new Size(132, 44);
            btnChangeInfo.TabIndex = 11;
            btnChangeInfo.Text = "Change Info";
            btnChangeInfo.UseVisualStyleBackColor = false;
            btnChangeInfo.Click += btnChangeInfo_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.LightPink;
            btnLogout.FlatStyle = FlatStyle.Popup;
            btnLogout.Location = new Point(936, 32);
            btnLogout.Margin = new Padding(4, 4, 4, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(128, 35);
            btnLogout.TabIndex = 10;
            btnLogout.Text = "Log Out";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnCashReserve
            // 
            btnCashReserve.BackColor = Color.LightPink;
            btnCashReserve.FlatStyle = FlatStyle.Popup;
            btnCashReserve.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCashReserve.Location = new Point(0, 276);
            btnCashReserve.Margin = new Padding(4, 4, 4, 4);
            btnCashReserve.Name = "btnCashReserve";
            btnCashReserve.Size = new Size(240, 39);
            btnCashReserve.TabIndex = 9;
            btnCashReserve.Text = "Cash Reserve";
            btnCashReserve.UseVisualStyleBackColor = false;
            btnCashReserve.Click += btnCashReserve_Click;
            // 
            // btnIncome
            // 
            btnIncome.BackColor = Color.LightPink;
            btnIncome.FlatStyle = FlatStyle.Popup;
            btnIncome.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnIncome.Location = new Point(240, 276);
            btnIncome.Margin = new Padding(4, 4, 4, 4);
            btnIncome.Name = "btnIncome";
            btnIncome.Size = new Size(208, 39);
            btnIncome.TabIndex = 8;
            btnIncome.Text = "Income";
            btnIncome.UseVisualStyleBackColor = false;
            btnIncome.Click += btnIncome_Click;
            // 
            // btnExpenses
            // 
            btnExpenses.BackColor = Color.LightPink;
            btnExpenses.FlatStyle = FlatStyle.Popup;
            btnExpenses.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExpenses.Location = new Point(448, 276);
            btnExpenses.Margin = new Padding(4, 4, 4, 4);
            btnExpenses.Name = "btnExpenses";
            btnExpenses.Size = new Size(208, 39);
            btnExpenses.TabIndex = 7;
            btnExpenses.Text = "Expenses";
            btnExpenses.UseVisualStyleBackColor = false;
            btnExpenses.Click += btnExpenses_Click;
            // 
            // btnReceivables
            // 
            btnReceivables.BackColor = Color.LightPink;
            btnReceivables.FlatStyle = FlatStyle.Popup;
            btnReceivables.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReceivables.Location = new Point(655, 276);
            btnReceivables.Margin = new Padding(4, 4, 4, 4);
            btnReceivables.Name = "btnReceivables";
            btnReceivables.Size = new Size(208, 39);
            btnReceivables.TabIndex = 5;
            btnReceivables.Text = "Receivables";
            btnReceivables.UseVisualStyleBackColor = false;
            btnReceivables.Click += btnReceivables_Click;
            // 
            // btnInventory
            // 
            btnInventory.BackColor = Color.LightPink;
            btnInventory.FlatStyle = FlatStyle.Popup;
            btnInventory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnInventory.Location = new Point(862, 276);
            btnInventory.Margin = new Padding(4, 4, 4, 4);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(229, 39);
            btnInventory.TabIndex = 6;
            btnInventory.Text = "Inventory";
            btnInventory.UseVisualStyleBackColor = false;
            btnInventory.Click += btnInventory_Click;
            // 
            // reservePanel
            // 
            reservePanel.BackColor = Color.LightPink;
            reservePanel.BorderStyle = BorderStyle.FixedSingle;
            reservePanel.Location = new Point(0, 314);
            reservePanel.Margin = new Padding(4, 4, 4, 4);
            reservePanel.Name = "reservePanel";
            reservePanel.Size = new Size(240, 360);
            reservePanel.TabIndex = 4;
            // 
            // incomePanel
            // 
            incomePanel.BackColor = Color.LightPink;
            incomePanel.BorderStyle = BorderStyle.FixedSingle;
            incomePanel.Location = new Point(240, 314);
            incomePanel.Margin = new Padding(4, 4, 4, 4);
            incomePanel.Name = "incomePanel";
            incomePanel.Size = new Size(207, 360);
            incomePanel.TabIndex = 0;
            // 
            // expensesPanel
            // 
            expensesPanel.BackColor = Color.LightPink;
            expensesPanel.BorderStyle = BorderStyle.FixedSingle;
            expensesPanel.Location = new Point(448, 314);
            expensesPanel.Margin = new Padding(4, 4, 4, 4);
            expensesPanel.Name = "expensesPanel";
            expensesPanel.Size = new Size(207, 360);
            expensesPanel.TabIndex = 3;
            // 
            // receivablesPanel
            // 
            receivablesPanel.BackColor = Color.LightPink;
            receivablesPanel.BorderStyle = BorderStyle.FixedSingle;
            receivablesPanel.Location = new Point(655, 314);
            receivablesPanel.Margin = new Padding(4, 4, 4, 4);
            receivablesPanel.Name = "receivablesPanel";
            receivablesPanel.Size = new Size(207, 360);
            receivablesPanel.TabIndex = 2;
            // 
            // inventoryPanel
            // 
            inventoryPanel.BackColor = Color.LightPink;
            inventoryPanel.BorderStyle = BorderStyle.FixedSingle;
            inventoryPanel.Location = new Point(862, 314);
            inventoryPanel.Margin = new Padding(4, 4, 4, 4);
            inventoryPanel.Name = "inventoryPanel";
            inventoryPanel.Size = new Size(228, 360);
            inventoryPanel.TabIndex = 1;
            // 
            // businessMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1092, 674);
            Controls.Add(incomePanel);
            Controls.Add(inventoryPanel);
            Controls.Add(receivablesPanel);
            Controls.Add(expensesPanel);
            Controls.Add(reservePanel);
            Controls.Add(btnReceivables);
            Controls.Add(btnInventory);
            Controls.Add(btnExpenses);
            Controls.Add(btnIncome);
            Controls.Add(btnCashReserve);
            Controls.Add(btnLogout);
            Controls.Add(btnChangeInfo);
            Controls.Add(btnKpi);
            Controls.Add(btnDashboard);
            Controls.Add(lblGreeting);
            Controls.Add(businessLogo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "businessMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Business Main";
            Load += businessMain_Load;
            ((System.ComponentModel.ISupportInitialize)businessLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
