namespace PocketPiggy.View
{
    partial class Expenses
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotalExpenses;
        private System.Windows.Forms.Label lblRecentExpenses;
        private System.Windows.Forms.Panel panelExpensesList;
        private System.Windows.Forms.Button btnAddExpense;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.Button btnBackToMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Expenses));
            lblTitle = new Label();
            lblTotalExpenses = new Label();
            lblRecentExpenses = new Label();
            panelExpensesList = new Panel();
            btnAddExpense = new Button();
            btnViewHistory = new Button();
            btnBackToMenu = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Century", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(29, 34);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(252, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Your Expenses";
            // 
            // lblTotalExpenses
            // 
            lblTotalExpenses.AutoSize = true;
            lblTotalExpenses.Font = new Font("Century", 16F, FontStyle.Bold);
            lblTotalExpenses.Location = new Point(31, 102);
            lblTotalExpenses.Margin = new Padding(4, 0, 4, 0);
            lblTotalExpenses.Name = "lblTotalExpenses";
            lblTotalExpenses.Size = new Size(318, 38);
            lblTotalExpenses.TabIndex = 1;
            lblTotalExpenses.Text = "Total Expenses: ₱0";
            // 
            // lblRecentExpenses
            // 
            lblRecentExpenses.AutoSize = true;
            lblRecentExpenses.Font = new Font("Century", 16F, FontStyle.Bold);
            lblRecentExpenses.Location = new Point(31, 166);
            lblRecentExpenses.Margin = new Padding(4, 0, 4, 0);
            lblRecentExpenses.Name = "lblRecentExpenses";
            lblRecentExpenses.Size = new Size(294, 38);
            lblRecentExpenses.TabIndex = 2;
            lblRecentExpenses.Text = "Recent / Planned:";
            // 
            // panelExpensesList
            // 
            panelExpensesList.AutoScroll = true;
            panelExpensesList.BackColor = Color.LightPink;
            panelExpensesList.BorderStyle = BorderStyle.FixedSingle;
            panelExpensesList.Location = new Point(38, 216);
            panelExpensesList.Margin = new Padding(4, 5, 4, 5);
            panelExpensesList.Name = "panelExpensesList";
            panelExpensesList.Size = new Size(571, 330);
            panelExpensesList.TabIndex = 0;
            // 
            // btnAddExpense
            // 
            btnAddExpense.BackColor = Color.LightPink;
            btnAddExpense.FlatStyle = FlatStyle.Popup;
            btnAddExpense.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddExpense.ForeColor = Color.Black;
            btnAddExpense.Location = new Point(642, 240);
            btnAddExpense.Margin = new Padding(4, 5, 4, 5);
            btnAddExpense.Name = "btnAddExpense";
            btnAddExpense.Size = new Size(214, 66);
            btnAddExpense.TabIndex = 3;
            btnAddExpense.Text = "Add Expense";
            btnAddExpense.UseVisualStyleBackColor = false;
            btnAddExpense.Click += btnAddExpense_Click;
            // 
            // btnViewHistory
            // 
            btnViewHistory.BackColor = Color.LightPink;
            btnViewHistory.FlatStyle = FlatStyle.Popup;
            btnViewHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnViewHistory.ForeColor = Color.Black;
            btnViewHistory.Location = new Point(642, 316);
            btnViewHistory.Margin = new Padding(4, 5, 4, 5);
            btnViewHistory.Name = "btnViewHistory";
            btnViewHistory.Size = new Size(214, 66);
            btnViewHistory.TabIndex = 4;
            btnViewHistory.Text = "View History";
            btnViewHistory.UseVisualStyleBackColor = false;
            btnViewHistory.Click += btnViewHistory_Click;
            // 
            // btnBackToMenu
            // 
            btnBackToMenu.BackColor = Color.LightPink;
            btnBackToMenu.FlatStyle = FlatStyle.Popup;
            btnBackToMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBackToMenu.ForeColor = Color.Black;
            btnBackToMenu.Location = new Point(642, 400);
            btnBackToMenu.Margin = new Padding(4, 5, 4, 5);
            btnBackToMenu.Name = "btnBackToMenu";
            btnBackToMenu.Size = new Size(214, 66);
            btnBackToMenu.TabIndex = 5;
            btnBackToMenu.Text = "Back to Menu";
            btnBackToMenu.UseVisualStyleBackColor = false;
            btnBackToMenu.Click += btnBackToMenu_Click;
            // 
            // button1
            // 
            button1.Location = new Point(920, 452);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(60, 10);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // Expenses
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1000, 562);
            Controls.Add(button1);
            Controls.Add(lblTitle);
            Controls.Add(lblTotalExpenses);
            Controls.Add(lblRecentExpenses);
            Controls.Add(panelExpensesList);
            Controls.Add(btnAddExpense);
            Controls.Add(btnViewHistory);
            Controls.Add(btnBackToMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "Expenses";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Expenses";
            Load += Expenses_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        private Button button1;
    }
}
