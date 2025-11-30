namespace PocketPiggy
{
    partial class BusinessExpenses
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessExpenses));
            pExpenses = new Panel();
            label1 = new Label();
            lblOverdue = new Label();
            lblDue = new Label();
            lblOutstanding = new Label();
            lblTitle = new Label();
            dgvExpenses = new DataGridView();
            cDueDate = new DataGridViewTextBoxColumn();
            cVendor = new DataGridViewTextBoxColumn();
            cDescription = new DataGridViewTextBoxColumn();
            cAmount = new DataGridViewTextBoxColumn();
            cStatus = new DataGridViewTextBoxColumn();
            btnMark = new Button();
            btnAdd = new Button();
            panel1 = new Panel();
            cExpenseBreakdown = new System.Windows.Forms.DataVisualization.Charting.Chart();
            cMonthlyPayables = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel2 = new Panel();
            btnEdit = new Button();
            btnDelete = new Button();
            pExpenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cExpenseBreakdown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cMonthlyPayables).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // pExpenses
            // 
            pExpenses.Controls.Add(label1);
            pExpenses.Controls.Add(lblOverdue);
            pExpenses.Controls.Add(lblDue);
            pExpenses.Controls.Add(lblOutstanding);
            pExpenses.Controls.Add(lblTitle);
            pExpenses.Location = new Point(12, 12);
            pExpenses.Name = "pExpenses";
            pExpenses.Size = new Size(456, 224);
            pExpenses.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(261, 57);
            label1.Name = "label1";
            label1.Size = new Size(0, 32);
            label1.TabIndex = 4;
            // 
            // lblOverdue
            // 
            lblOverdue.AutoSize = true;
            lblOverdue.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverdue.Location = new Point(4, 108);
            lblOverdue.Name = "lblOverdue";
            lblOverdue.Size = new Size(172, 32);
            lblOverdue.TabIndex = 3;
            lblOverdue.Text = "Overdue Bills:";
            // 
            // lblDue
            // 
            lblDue.AutoSize = true;
            lblDue.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDue.Location = new Point(4, 59);
            lblDue.Name = "lblDue";
            lblDue.Size = new Size(190, 32);
            lblDue.TabIndex = 2;
            lblDue.Text = "Due This Week:";
            // 
            // lblOutstanding
            // 
            lblOutstanding.AutoSize = true;
            lblOutstanding.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOutstanding.Location = new Point(4, 162);
            lblOutstanding.Name = "lblOutstanding";
            lblOutstanding.Size = new Size(226, 32);
            lblOutstanding.TabIndex = 1;
            lblOutstanding.Text = "Total Outstanding:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(59, -3);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(317, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Business Expenses";
            // 
            // dgvExpenses
            // 
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.AllowUserToDeleteRows = false;
            dgvExpenses.BackgroundColor = Color.LightPink;
            dgvExpenses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpenses.Columns.AddRange(new DataGridViewColumn[] { cDueDate, cVendor, cDescription, cAmount, cStatus });
            dgvExpenses.Location = new Point(12, 266);
            dgvExpenses.Name = "dgvExpenses";
            dgvExpenses.ReadOnly = true;
            dgvExpenses.RowHeadersVisible = false;
            dgvExpenses.RowHeadersWidth = 62;
            dgvExpenses.Size = new Size(723, 396);
            dgvExpenses.TabIndex = 3;
            // 
            // cDueDate
            // 
            cDueDate.HeaderText = "Due Date";
            cDueDate.MinimumWidth = 8;
            cDueDate.Name = "cDueDate";
            cDueDate.ReadOnly = true;
            cDueDate.Width = 130;
            // 
            // cVendor
            // 
            cVendor.HeaderText = "Vendor";
            cVendor.MinimumWidth = 8;
            cVendor.Name = "cVendor";
            cVendor.ReadOnly = true;
            cVendor.Width = 150;
            // 
            // cDescription
            // 
            cDescription.HeaderText = "Desciption";
            cDescription.MinimumWidth = 8;
            cDescription.Name = "cDescription";
            cDescription.ReadOnly = true;
            cDescription.Width = 170;
            // 
            // cAmount
            // 
            cAmount.HeaderText = "Amount";
            cAmount.MinimumWidth = 8;
            cAmount.Name = "cAmount";
            cAmount.ReadOnly = true;
            cAmount.Width = 140;
            // 
            // cStatus
            // 
            cStatus.HeaderText = "Status";
            cStatus.MinimumWidth = 8;
            cStatus.Name = "cStatus";
            cStatus.ReadOnly = true;
            cStatus.Width = 130;
            // 
            // btnMark
            // 
            btnMark.BackColor = Color.LightPink;
            btnMark.FlatStyle = FlatStyle.Popup;
            btnMark.ForeColor = Color.Black;
            btnMark.Location = new Point(164, 0);
            btnMark.Name = "btnMark";
            btnMark.Size = new Size(175, 39);
            btnMark.TabIndex = 9;
            btnMark.Text = "Mark as Paid";
            btnMark.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightPink;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.ForeColor = Color.Black;
            btnAdd.Location = new Point(0, -1);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(158, 40);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add Bill";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnMark);
            panel1.Location = new Point(741, 579);
            panel1.Name = "panel1";
            panel1.Size = new Size(339, 38);
            panel1.TabIndex = 12;
            // 
            // cExpenseBreakdown
            // 
            chartArea1.Name = "ChartArea1";
            cExpenseBreakdown.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            cExpenseBreakdown.Legends.Add(legend1);
            cExpenseBreakdown.Location = new Point(755, 248);
            cExpenseBreakdown.Name = "cExpenseBreakdown";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            cExpenseBreakdown.Series.Add(series1);
            cExpenseBreakdown.Size = new Size(335, 320);
            cExpenseBreakdown.TabIndex = 13;
            cExpenseBreakdown.Text = "Expense Breakdown by Vendor";
            // 
            // cMonthlyPayables
            // 
            chartArea2.Name = "ChartArea1";
            cMonthlyPayables.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            cMonthlyPayables.Legends.Add(legend2);
            cMonthlyPayables.Location = new Point(480, 9);
            cMonthlyPayables.Name = "cMonthlyPayables";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            cMonthlyPayables.Series.Add(series2);
            cMonthlyPayables.Size = new Size(595, 225);
            cMonthlyPayables.TabIndex = 14;
            cMonthlyPayables.Text = "Monthly Payables Trend";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnEdit);
            panel2.Controls.Add(btnDelete);
            panel2.Location = new Point(741, 624);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 38);
            panel2.TabIndex = 13;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightPink;
            btnEdit.FlatStyle = FlatStyle.Popup;
            btnEdit.ForeColor = Color.Black;
            btnEdit.Location = new Point(0, -1);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(158, 40);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightPink;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.ForeColor = Color.Black;
            btnDelete.Location = new Point(164, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(175, 39);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // BusinessExpenses
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1092, 674);
            Controls.Add(panel2);
            Controls.Add(cMonthlyPayables);
            Controls.Add(cExpenseBreakdown);
            Controls.Add(panel1);
            Controls.Add(dgvExpenses);
            Controls.Add(pExpenses);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BusinessExpenses";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " Expenses";
            pExpenses.ResumeLayout(false);
            pExpenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cExpenseBreakdown).EndInit();
            ((System.ComponentModel.ISupportInitialize)cMonthlyPayables).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pExpenses;
        private Label label1;
        private Label lblOverdue;
        private Label lblDue;
        private Label lblOutstanding;
        private Label lblTitle;
        private DataGridView dgvExpenses;
        private Button btnMark;
        private Button btnAdd;
        private Panel panel1;
        private DataGridViewTextBoxColumn cDueDate;
        private DataGridViewTextBoxColumn cVendor;
        private DataGridViewTextBoxColumn cDescription;
        private DataGridViewTextBoxColumn cAmount;
        private DataGridViewTextBoxColumn cStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart cExpenseBreakdown;
        private System.Windows.Forms.DataVisualization.Charting.Chart cMonthlyPayables;
        private Panel panel2;
        private Button btnEdit;
        private Button btnDelete;
    }
}