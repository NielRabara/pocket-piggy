namespace PocketPiggy
{
    partial class dashboardSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dashboardSummary));
            lblDashboardSummary = new Label();
            pFinancialOverview = new Panel();
            lblFinancialOverview = new Label();
            lblNetProfit = new Label();
            lblTotalExpenses = new Label();
            lblTotalIncome = new Label();
            lblCurrent = new Label();
            cbDataRange = new ComboBox();
            lblDataRange = new Label();
            btnKpiSummary = new Button();
            pCashBills = new Panel();
            lblCashReserve = new Label();
            lblUpcomingBills = new Label();
            lblCashFlow = new Label();
            lblCashBills = new Label();
            pFinancialOverview.SuspendLayout();
            pCashBills.SuspendLayout();
            SuspendLayout();
            // 
            // lblDashboardSummary
            // 
            lblDashboardSummary.AutoSize = true;
            lblDashboardSummary.Font = new Font("Century", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDashboardSummary.Location = new Point(220, 47);
            lblDashboardSummary.Name = "lblDashboardSummary";
            lblDashboardSummary.Size = new Size(431, 47);
            lblDashboardSummary.TabIndex = 0;
            lblDashboardSummary.Text = "Dashboard Summary";
            // 
            // pFinancialOverview
            // 
            pFinancialOverview.BackColor = Color.LightPink;
            pFinancialOverview.BorderStyle = BorderStyle.FixedSingle;
            pFinancialOverview.Controls.Add(lblFinancialOverview);
            pFinancialOverview.Controls.Add(lblNetProfit);
            pFinancialOverview.Controls.Add(lblTotalExpenses);
            pFinancialOverview.Controls.Add(lblTotalIncome);
            pFinancialOverview.Controls.Add(lblCurrent);
            pFinancialOverview.Location = new Point(39, 234);
            pFinancialOverview.Name = "pFinancialOverview";
            pFinancialOverview.Size = new Size(790, 302);
            pFinancialOverview.TabIndex = 1;
            // 
            // lblFinancialOverview
            // 
            lblFinancialOverview.AutoSize = true;
            lblFinancialOverview.Font = new Font("Century", 16F, FontStyle.Bold);
            lblFinancialOverview.Location = new Point(217, 0);
            lblFinancialOverview.Name = "lblFinancialOverview";
            lblFinancialOverview.Size = new Size(325, 38);
            lblFinancialOverview.TabIndex = 5;
            lblFinancialOverview.Text = "Financial Overview";
            // 
            // lblNetProfit
            // 
            lblNetProfit.AutoSize = true;
            lblNetProfit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNetProfit.Location = new Point(21, 236);
            lblNetProfit.Name = "lblNetProfit";
            lblNetProfit.Size = new Size(134, 32);
            lblNetProfit.TabIndex = 9;
            lblNetProfit.Text = "Net Profit:";
            // 
            // lblTotalExpenses
            // 
            lblTotalExpenses.AutoSize = true;
            lblTotalExpenses.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalExpenses.Location = new Point(21, 125);
            lblTotalExpenses.Name = "lblTotalExpenses";
            lblTotalExpenses.Size = new Size(188, 32);
            lblTotalExpenses.TabIndex = 8;
            lblTotalExpenses.Text = "Total Expenses:";
            // 
            // lblTotalIncome
            // 
            lblTotalIncome.AutoSize = true;
            lblTotalIncome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalIncome.Location = new Point(21, 71);
            lblTotalIncome.Name = "lblTotalIncome";
            lblTotalIncome.Size = new Size(169, 32);
            lblTotalIncome.TabIndex = 7;
            lblTotalIncome.Text = "Total Income:";
            // 
            // lblCurrent
            // 
            lblCurrent.AutoSize = true;
            lblCurrent.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCurrent.Location = new Point(21, 181);
            lblCurrent.Name = "lblCurrent";
            lblCurrent.Size = new Size(203, 32);
            lblCurrent.TabIndex = 6;
            lblCurrent.Text = "Current Balance:";
            // 
            // cbDataRange
            // 
            cbDataRange.BackColor = Color.LightPink;
            cbDataRange.FlatStyle = FlatStyle.Popup;
            cbDataRange.FormattingEnabled = true;
            cbDataRange.Location = new Point(158, 139);
            cbDataRange.Name = "cbDataRange";
            cbDataRange.Size = new Size(207, 33);
            cbDataRange.TabIndex = 2;
            // 
            // lblDataRange
            // 
            lblDataRange.AutoSize = true;
            lblDataRange.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDataRange.Location = new Point(39, 140);
            lblDataRange.Name = "lblDataRange";
            lblDataRange.Size = new Size(117, 28);
            lblDataRange.TabIndex = 3;
            lblDataRange.Text = "Data Range:";
            // 
            // btnKpiSummary
            // 
            btnKpiSummary.BackColor = Color.LightPink;
            btnKpiSummary.FlatStyle = FlatStyle.Popup;
            btnKpiSummary.Location = new Point(599, 139);
            btnKpiSummary.Name = "btnKpiSummary";
            btnKpiSummary.Size = new Size(203, 33);
            btnKpiSummary.TabIndex = 4;
            btnKpiSummary.Text = "View KPI Summary";
            btnKpiSummary.UseVisualStyleBackColor = false;
            btnKpiSummary.Click += btnKpiSummary_Click;
            // 
            // pCashBills
            // 
            pCashBills.BackColor = Color.LightPink;
            pCashBills.BorderStyle = BorderStyle.FixedSingle;
            pCashBills.Controls.Add(lblCashReserve);
            pCashBills.Controls.Add(lblUpcomingBills);
            pCashBills.Controls.Add(lblCashFlow);
            pCashBills.Controls.Add(lblCashBills);
            pCashBills.Location = new Point(39, 600);
            pCashBills.Name = "pCashBills";
            pCashBills.Size = new Size(790, 245);
            pCashBills.TabIndex = 14;
            // 
            // lblCashReserve
            // 
            lblCashReserve.AutoSize = true;
            lblCashReserve.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCashReserve.Location = new Point(21, 181);
            lblCashReserve.Name = "lblCashReserve";
            lblCashReserve.Size = new Size(170, 32);
            lblCashReserve.TabIndex = 8;
            lblCashReserve.Text = "Cash Reserve:";
            // 
            // lblUpcomingBills
            // 
            lblUpcomingBills.AutoSize = true;
            lblUpcomingBills.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUpcomingBills.Location = new Point(21, 125);
            lblUpcomingBills.Name = "lblUpcomingBills";
            lblUpcomingBills.Size = new Size(193, 32);
            lblUpcomingBills.TabIndex = 7;
            lblUpcomingBills.Text = "Upcoming Bills:";
            // 
            // lblCashFlow
            // 
            lblCashFlow.AutoSize = true;
            lblCashFlow.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCashFlow.Location = new Point(21, 71);
            lblCashFlow.Name = "lblCashFlow";
            lblCashFlow.Size = new Size(134, 32);
            lblCashFlow.TabIndex = 6;
            lblCashFlow.Text = "Cash Flow:";
            // 
            // lblCashBills
            // 
            lblCashBills.AutoSize = true;
            lblCashBills.Font = new Font("Century", 16F, FontStyle.Bold);
            lblCashBills.Location = new Point(204, -1);
            lblCashBills.Name = "lblCashBills";
            lblCashBills.Size = new Size(407, 38);
            lblCashBills.TabIndex = 5;
            lblCashBills.Text = "Cash and Bills Overview";
            // 
            // dashboardSummary
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(868, 891);
            Controls.Add(pCashBills);
            Controls.Add(btnKpiSummary);
            Controls.Add(lblDataRange);
            Controls.Add(cbDataRange);
            Controls.Add(pFinancialOverview);
            Controls.Add(lblDashboardSummary);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "dashboardSummary";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Summary";
            pFinancialOverview.ResumeLayout(false);
            pFinancialOverview.PerformLayout();
            pCashBills.ResumeLayout(false);
            pCashBills.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDashboardSummary;
        private Panel pFinancialOverview;
        private Label lblFinancialOverview;
        private ComboBox cbDataRange;
        private Label lblDataRange;
        private Button btnKpiSummary;
        private Label lblNetProfit;
        private Label lblTotalExpenses;
        private Label lblTotalIncome;
        private Label lblCurrent;
        private Panel pCashBills;
        private Label lblCashReserve;
        private Label lblUpcomingBills;
        private Label lblCashFlow;
        private Label lblCashBills;
    }
}