namespace PocketPiggy
{
    partial class KpiSummary
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KpiSummary));
            lblKpiSummary = new Label();
            pProfitMargin = new Panel();
            lblProfitMargin = new Label();
            pIncomeGrowth = new Panel();
            lblIncomeGrowth = new Label();
            pExpenseRatio = new Panel();
            lblExpenseRatio = new Label();
            cbDataRange = new ComboBox();
            btnExport = new Button();
            lblDataRange = new Label();
            lblIncomeVsExpenses = new Label();
            chartIncomeVsExpenses = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartExpenseBreakdown = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lblExpenseBreakdown = new Label();
            chartCashFlowTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lblCashFlowTrend = new Label();
            pProfitMargin.SuspendLayout();
            pIncomeGrowth.SuspendLayout();
            pExpenseRatio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartIncomeVsExpenses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartExpenseBreakdown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartCashFlowTrend).BeginInit();
            SuspendLayout();
            // 
            // lblKpiSummary
            // 
            lblKpiSummary.AutoSize = true;
            lblKpiSummary.Font = new Font("Century", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiSummary.Location = new Point(758, 9);
            lblKpiSummary.Name = "lblKpiSummary";
            lblKpiSummary.Size = new Size(298, 47);
            lblKpiSummary.TabIndex = 0;
            lblKpiSummary.Text = "KPI Summary";
            // 
            // pProfitMargin
            // 
            pProfitMargin.BackColor = Color.LightPink;
            pProfitMargin.Controls.Add(lblProfitMargin);
            pProfitMargin.Location = new Point(308, 181);
            pProfitMargin.Name = "pProfitMargin";
            pProfitMargin.Size = new Size(348, 170);
            pProfitMargin.TabIndex = 1;
            // 
            // lblProfitMargin
            // 
            lblProfitMargin.AutoSize = true;
            lblProfitMargin.Font = new Font("Century", 14F, FontStyle.Bold);
            lblProfitMargin.Location = new Point(76, 8);
            lblProfitMargin.Name = "lblProfitMargin";
            lblProfitMargin.Size = new Size(205, 33);
            lblProfitMargin.TabIndex = 0;
            lblProfitMargin.Text = "Profit Margin";
            // 
            // pIncomeGrowth
            // 
            pIncomeGrowth.BackColor = Color.LightPink;
            pIncomeGrowth.Controls.Add(lblIncomeGrowth);
            pIncomeGrowth.Location = new Point(662, 181);
            pIncomeGrowth.Name = "pIncomeGrowth";
            pIncomeGrowth.Size = new Size(383, 170);
            pIncomeGrowth.TabIndex = 2;
            // 
            // lblIncomeGrowth
            // 
            lblIncomeGrowth.AutoSize = true;
            lblIncomeGrowth.Font = new Font("Century", 14F, FontStyle.Bold);
            lblIncomeGrowth.Location = new Point(82, 8);
            lblIncomeGrowth.Name = "lblIncomeGrowth";
            lblIncomeGrowth.Size = new Size(227, 33);
            lblIncomeGrowth.TabIndex = 1;
            lblIncomeGrowth.Text = "Income Growth";
            // 
            // pExpenseRatio
            // 
            pExpenseRatio.BackColor = Color.LightPink;
            pExpenseRatio.Controls.Add(lblExpenseRatio);
            pExpenseRatio.Location = new Point(1051, 181);
            pExpenseRatio.Name = "pExpenseRatio";
            pExpenseRatio.Size = new Size(372, 170);
            pExpenseRatio.TabIndex = 2;
            // 
            // lblExpenseRatio
            // 
            lblExpenseRatio.AutoSize = true;
            lblExpenseRatio.Font = new Font("Century", 14F, FontStyle.Bold);
            lblExpenseRatio.Location = new Point(87, 4);
            lblExpenseRatio.Name = "lblExpenseRatio";
            lblExpenseRatio.Size = new Size(215, 33);
            lblExpenseRatio.TabIndex = 2;
            lblExpenseRatio.Text = "Expense Ratio";
            // 
            // cbDataRange
            // 
            cbDataRange.BackColor = Color.LightPink;
            cbDataRange.FlatStyle = FlatStyle.Popup;
            cbDataRange.FormattingEnabled = true;
            cbDataRange.Location = new Point(497, 111);
            cbDataRange.Name = "cbDataRange";
            cbDataRange.Size = new Size(224, 33);
            cbDataRange.TabIndex = 3;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.LightPink;
            btnExport.FlatStyle = FlatStyle.Popup;
            btnExport.Location = new Point(1107, 111);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(188, 36);
            btnExport.TabIndex = 4;
            btnExport.Text = "Export Excel";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // lblDataRange
            // 
            lblDataRange.AutoSize = true;
            lblDataRange.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDataRange.Location = new Point(374, 111);
            lblDataRange.Name = "lblDataRange";
            lblDataRange.Size = new Size(117, 28);
            lblDataRange.TabIndex = 5;
            lblDataRange.Text = "Data Range:";
            // 
            // lblIncomeVsExpenses
            // 
            lblIncomeVsExpenses.AutoSize = true;
            lblIncomeVsExpenses.Font = new Font("Century", 14F, FontStyle.Bold);
            lblIncomeVsExpenses.Location = new Point(54, 375);
            lblIncomeVsExpenses.Name = "lblIncomeVsExpenses";
            lblIncomeVsExpenses.Size = new Size(292, 33);
            lblIncomeVsExpenses.TabIndex = 7;
            lblIncomeVsExpenses.Text = "Income vs Expenses";
            // 
            // chartIncomeVsExpenses
            // 
            chartArea1.Name = "ChartArea1";
            chartIncomeVsExpenses.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartIncomeVsExpenses.Legends.Add(legend1);
            chartIncomeVsExpenses.Location = new Point(54, 426);
            chartIncomeVsExpenses.Name = "chartIncomeVsExpenses";
            chartIncomeVsExpenses.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartIncomeVsExpenses.Series.Add(series1);
            chartIncomeVsExpenses.Size = new Size(474, 271);
            chartIncomeVsExpenses.TabIndex = 8;
            chartIncomeVsExpenses.Text = "Income VS Expenses";
            // 
            // chartExpenseBreakdown
            // 
            chartArea2.Name = "ChartArea1";
            chartExpenseBreakdown.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartExpenseBreakdown.Legends.Add(legend2);
            chartExpenseBreakdown.Location = new Point(596, 426);
            chartExpenseBreakdown.Name = "chartExpenseBreakdown";
            chartExpenseBreakdown.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartExpenseBreakdown.Series.Add(series2);
            chartExpenseBreakdown.Size = new Size(503, 271);
            chartExpenseBreakdown.TabIndex = 10;
            chartExpenseBreakdown.Text = "Expense Breakdown";
            // 
            // lblExpenseBreakdown
            // 
            lblExpenseBreakdown.AutoSize = true;
            lblExpenseBreakdown.Font = new Font("Century", 14F, FontStyle.Bold);
            lblExpenseBreakdown.Location = new Point(596, 375);
            lblExpenseBreakdown.Name = "lblExpenseBreakdown";
            lblExpenseBreakdown.Size = new Size(297, 33);
            lblExpenseBreakdown.TabIndex = 9;
            lblExpenseBreakdown.Text = "Expense Breakdown";
            // 
            // chartCashFlowTrend
            // 
            chartArea3.Name = "ChartArea1";
            chartCashFlowTrend.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chartCashFlowTrend.Legends.Add(legend3);
            chartCashFlowTrend.Location = new Point(1169, 426);
            chartCashFlowTrend.Name = "chartCashFlowTrend";
            chartCashFlowTrend.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chartCashFlowTrend.Series.Add(series3);
            chartCashFlowTrend.Size = new Size(512, 271);
            chartCashFlowTrend.TabIndex = 14;
            chartCashFlowTrend.Text = "Cash Flow Trend";
            // 
            // lblCashFlowTrend
            // 
            lblCashFlowTrend.AutoSize = true;
            lblCashFlowTrend.Font = new Font("Century", 14F, FontStyle.Bold);
            lblCashFlowTrend.Location = new Point(1169, 375);
            lblCashFlowTrend.Name = "lblCashFlowTrend";
            lblCashFlowTrend.Size = new Size(254, 33);
            lblCashFlowTrend.TabIndex = 13;
            lblCashFlowTrend.Text = "Cash Flow Trend";
            // 
            // KpiSummary
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1739, 748);
            Controls.Add(chartCashFlowTrend);
            Controls.Add(lblCashFlowTrend);
            Controls.Add(chartExpenseBreakdown);
            Controls.Add(lblExpenseBreakdown);
            Controls.Add(chartIncomeVsExpenses);
            Controls.Add(lblIncomeVsExpenses);
            Controls.Add(lblDataRange);
            Controls.Add(btnExport);
            Controls.Add(cbDataRange);
            Controls.Add(pExpenseRatio);
            Controls.Add(pIncomeGrowth);
            Controls.Add(pProfitMargin);
            Controls.Add(lblKpiSummary);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "KpiSummary";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KPI Summary";
            pProfitMargin.ResumeLayout(false);
            pProfitMargin.PerformLayout();
            pIncomeGrowth.ResumeLayout(false);
            pIncomeGrowth.PerformLayout();
            pExpenseRatio.ResumeLayout(false);
            pExpenseRatio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartIncomeVsExpenses).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartExpenseBreakdown).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartCashFlowTrend).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKpiSummary;
        private Panel pProfitMargin;
        private Panel pIncomeGrowth;
        private Panel pExpenseRatio;
        private ComboBox cbDataRange;
        private Label lblProfitMargin;
        private Label lblIncomeGrowth;
        private Label lblExpenseRatio;
        private Button btnExport;
        private Label lblDataRange;
        private Label lblIncomeVsExpenses;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartIncomeVsExpenses;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartExpenseBreakdown;
        private Label lblExpenseBreakdown;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCashFlowTrend;
        private Label lblCashFlowTrend;
    }
}