namespace PocketPiggy
{
    partial class CashReserve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashReserve));
            pCashReserve = new Panel();
            pbarReserve = new ProgressBar();
            lblPercent = new Label();
            lblTargetReserve = new Label();
            lblTotalReserve = new Label();
            lblTitle = new Label();
            cReserve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dgvReserveTransactions = new DataGridView();
            cDate = new DataGridViewTextBoxColumn();
            cDescription = new DataGridViewTextBoxColumn();
            cFlow = new DataGridViewTextBoxColumn();
            pControls = new Panel();
            button1 = new Button();
            btnAdd = new Button();
            btnExport = new Button();
            panel1 = new Panel();
            btnEdit = new Button();
            btnDelete = new Button();
            pCashReserve.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cReserve).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReserveTransactions).BeginInit();
            pControls.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pCashReserve
            // 
            pCashReserve.Controls.Add(pbarReserve);
            pCashReserve.Controls.Add(lblPercent);
            pCashReserve.Controls.Add(lblTargetReserve);
            pCashReserve.Controls.Add(lblTotalReserve);
            pCashReserve.Controls.Add(lblTitle);
            pCashReserve.Location = new Point(12, 12);
            pCashReserve.Name = "pCashReserve";
            pCashReserve.Size = new Size(470, 282);
            pCashReserve.TabIndex = 0;
            // 
            // pbarReserve
            // 
            pbarReserve.BackColor = Color.DarkGray;
            pbarReserve.ForeColor = Color.DeepPink;
            pbarReserve.Location = new Point(66, 222);
            pbarReserve.Name = "pbarReserve";
            pbarReserve.Size = new Size(321, 34);
            pbarReserve.TabIndex = 5;
            // 
            // lblPercent
            // 
            lblPercent.AutoSize = true;
            lblPercent.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPercent.Location = new Point(4, 167);
            lblPercent.Name = "lblPercent";
            lblPercent.Size = new Size(271, 32);
            lblPercent.TabIndex = 3;
            lblPercent.Text = "% of Reserve Reached:";
            // 
            // lblTargetReserve
            // 
            lblTargetReserve.AutoSize = true;
            lblTargetReserve.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTargetReserve.Location = new Point(4, 112);
            lblTargetReserve.Name = "lblTargetReserve";
            lblTargetReserve.Size = new Size(189, 32);
            lblTargetReserve.TabIndex = 2;
            lblTargetReserve.Text = "Target Reserve:";
            // 
            // lblTotalReserve
            // 
            lblTotalReserve.AutoSize = true;
            lblTotalReserve.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalReserve.Location = new Point(4, 58);
            lblTotalReserve.Name = "lblTotalReserve";
            lblTotalReserve.Size = new Size(233, 32);
            lblTotalReserve.TabIndex = 1;
            lblTotalReserve.Text = "Total Cash Reserve:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(33, -7);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(389, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Cash Reserve Overview";
            // 
            // cReserve
            // 
            chartArea1.Name = "ChartArea1";
            cReserve.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            cReserve.Legends.Add(legend1);
            cReserve.Location = new Point(23, 319);
            cReserve.Name = "cReserve";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            cReserve.Series.Add(series1);
            cReserve.Size = new Size(470, 334);
            cReserve.TabIndex = 0;
            cReserve.Text = "Cash Reserve Trend";
            // 
            // dgvReserveTransactions
            // 
            dgvReserveTransactions.AllowUserToAddRows = false;
            dgvReserveTransactions.AllowUserToDeleteRows = false;
            dgvReserveTransactions.BackgroundColor = Color.LightPink;
            dgvReserveTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReserveTransactions.Columns.AddRange(new DataGridViewColumn[] { cDate, cDescription, cFlow });
            dgvReserveTransactions.Location = new Point(522, 12);
            dgvReserveTransactions.Name = "dgvReserveTransactions";
            dgvReserveTransactions.RowHeadersVisible = false;
            dgvReserveTransactions.RowHeadersWidth = 62;
            dgvReserveTransactions.Size = new Size(558, 555);
            dgvReserveTransactions.TabIndex = 0;
            // 
            // cDate
            // 
            cDate.HeaderText = "Date";
            cDate.MinimumWidth = 8;
            cDate.Name = "cDate";
            cDate.ReadOnly = true;
            cDate.Width = 177;
            // 
            // cDescription
            // 
            cDescription.HeaderText = "Description";
            cDescription.MinimumWidth = 8;
            cDescription.Name = "cDescription";
            cDescription.ReadOnly = true;
            cDescription.Width = 198;
            // 
            // cFlow
            // 
            cFlow.HeaderText = "InFlow/OutFlow";
            cFlow.MinimumWidth = 8;
            cFlow.Name = "cFlow";
            cFlow.ReadOnly = true;
            cFlow.Resizable = DataGridViewTriState.True;
            cFlow.Width = 180;
            // 
            // pControls
            // 
            pControls.Controls.Add(button1);
            pControls.Controls.Add(btnAdd);
            pControls.Controls.Add(btnExport);
            pControls.Location = new Point(522, 573);
            pControls.Name = "pControls";
            pControls.Size = new Size(558, 42);
            pControls.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.LightPink;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(392, 0);
            button1.Name = "button1";
            button1.Size = new Size(166, 42);
            button1.TabIndex = 2;
            button1.Text = "Export CSV";
            button1.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightPink;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Location = new Point(0, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(166, 42);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add Reserve Goal";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.LightPink;
            btnExport.FlatStyle = FlatStyle.Popup;
            btnExport.Location = new Point(199, 0);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(166, 42);
            btnExport.TabIndex = 0;
            btnExport.Text = "Add Reserve";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnDelete);
            panel1.Location = new Point(622, 621);
            panel1.Name = "panel1";
            panel1.Size = new Size(365, 42);
            panel1.TabIndex = 3;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightPink;
            btnEdit.FlatStyle = FlatStyle.Popup;
            btnEdit.Location = new Point(0, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(166, 42);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightPink;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Location = new Point(199, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(166, 42);
            btnDelete.TabIndex = 0;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // CashReserve
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1092, 674);
            Controls.Add(panel1);
            Controls.Add(pControls);
            Controls.Add(dgvReserveTransactions);
            Controls.Add(cReserve);
            Controls.Add(pCashReserve);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CashReserve";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cash Reserve";
            pCashReserve.ResumeLayout(false);
            pCashReserve.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cReserve).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReserveTransactions).EndInit();
            pControls.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pCashReserve;
        private Label lblTitle;
        private Label lblTotalReserve;
        private Label lblPercent;
        private Label lblTargetReserve;
        private ProgressBar pbarReserve;
        private System.Windows.Forms.DataVisualization.Charting.Chart cReserve;
        private DataGridView dgvReserveTransactions;
        private Panel pControls;
        private Button btnExport;
        private Button btnAdd;
        private Button button1;
        private DataGridViewTextBoxColumn cDate;
        private DataGridViewTextBoxColumn cDescription;
        private DataGridViewTextBoxColumn cFlow;
        private Panel panel1;
        private Button btnEdit;
        private Button btnDelete;
    }
}