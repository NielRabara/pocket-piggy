namespace PocketPiggy
{
    partial class BusinessIncome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessIncome));
            pIncome = new Panel();
            label1 = new Label();
            lblIncome = new Label();
            lblAverage = new Label();
            lblSource = new Label();
            lblTitle = new Label();
            dgvIncome = new DataGridView();
            cDate = new DataGridViewTextBoxColumn();
            cSource = new DataGridViewTextBoxColumn();
            cDescription = new DataGridViewTextBoxColumn();
            cAmount = new DataGridViewTextBoxColumn();
            cReserve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnExport = new Button();
            pIncome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIncome).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cReserve).BeginInit();
            SuspendLayout();
            // 
            // pIncome
            // 
            pIncome.Controls.Add(label1);
            pIncome.Controls.Add(lblIncome);
            pIncome.Controls.Add(lblAverage);
            pIncome.Controls.Add(lblSource);
            pIncome.Controls.Add(lblTitle);
            pIncome.Location = new Point(12, 12);
            pIncome.Name = "pIncome";
            pIncome.Size = new Size(495, 224);
            pIncome.TabIndex = 2;
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
            // lblIncome
            // 
            lblIncome.AutoSize = true;
            lblIncome.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIncome.Location = new Point(4, 112);
            lblIncome.Name = "lblIncome";
            lblIncome.Size = new Size(256, 32);
            lblIncome.TabIndex = 3;
            lblIncome.Text = "Top Income (Month):";
            // 
            // lblAverage
            // 
            lblAverage.AutoSize = true;
            lblAverage.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAverage.Location = new Point(4, 57);
            lblAverage.Name = "lblAverage";
            lblAverage.Size = new Size(308, 32);
            lblAverage.TabIndex = 2;
            lblAverage.Text = "Average Monthly Growth:";
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSource.Location = new Point(4, 165);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(240, 32);
            lblSource.TabIndex = 1;
            lblSource.Text = "Top Income Source:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(97, -3);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(288, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Income Overview";
            // 
            // dgvIncome
            // 
            dgvIncome.AllowUserToAddRows = false;
            dgvIncome.AllowUserToDeleteRows = false;
            dgvIncome.AllowUserToResizeColumns = false;
            dgvIncome.BackgroundColor = Color.LightPink;
            dgvIncome.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIncome.Columns.AddRange(new DataGridViewColumn[] { cDate, cSource, cDescription, cAmount });
            dgvIncome.Location = new Point(531, 12);
            dgvIncome.Name = "dgvIncome";
            dgvIncome.ReadOnly = true;
            dgvIncome.RowHeadersVisible = false;
            dgvIncome.RowHeadersWidth = 62;
            dgvIncome.Size = new Size(549, 606);
            dgvIncome.TabIndex = 4;
            // 
            // cDate
            // 
            cDate.HeaderText = "Date";
            cDate.MinimumWidth = 8;
            cDate.Name = "cDate";
            cDate.ReadOnly = true;
            cDate.Width = 126;
            // 
            // cSource
            // 
            cSource.HeaderText = "Source";
            cSource.MinimumWidth = 8;
            cSource.Name = "cSource";
            cSource.ReadOnly = true;
            cSource.Width = 130;
            // 
            // cDescription
            // 
            cDescription.HeaderText = "Description";
            cDescription.MinimumWidth = 8;
            cDescription.Name = "cDescription";
            cDescription.ReadOnly = true;
            cDescription.Width = 150;
            // 
            // cAmount
            // 
            cAmount.HeaderText = "Amount";
            cAmount.MinimumWidth = 8;
            cAmount.Name = "cAmount";
            cAmount.ReadOnly = true;
            cAmount.Width = 140;
            // 
            // cReserve
            // 
            chartArea1.Name = "ChartArea1";
            cReserve.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            cReserve.Legends.Add(legend1);
            cReserve.Location = new Point(12, 259);
            cReserve.Name = "cReserve";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            cReserve.Series.Add(series1);
            cReserve.Size = new Size(495, 403);
            cReserve.TabIndex = 3;
            cReserve.Text = "Cash Reserve Trend";
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightPink;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Location = new Point(531, 628);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 34);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add ";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightPink;
            btnEdit.FlatStyle = FlatStyle.Popup;
            btnEdit.Location = new Point(680, 628);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(105, 34);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightPink;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Location = new Point(833, 628);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(105, 34);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.LightPink;
            btnExport.FlatStyle = FlatStyle.Popup;
            btnExport.Location = new Point(975, 628);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(105, 34);
            btnExport.TabIndex = 8;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // BusinessIncome
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1092, 674);
            Controls.Add(btnExport);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvIncome);
            Controls.Add(cReserve);
            Controls.Add(pIncome);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BusinessIncome";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Income";
            pIncome.ResumeLayout(false);
            pIncome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIncome).EndInit();
            ((System.ComponentModel.ISupportInitialize)cReserve).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pIncome;
        private Label label1;
        private Label lblIncome;
        private Label lblAverage;
        private Label lblSource;
        private Label lblTitle;
        private DataGridView dgvIncome;
        private System.Windows.Forms.DataVisualization.Charting.Chart cReserve;
        private DataGridViewTextBoxColumn cDate;
        private DataGridViewTextBoxColumn cSource;
        private DataGridViewTextBoxColumn cDescription;
        private DataGridViewTextBoxColumn cAmount;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnExport;
    }
}