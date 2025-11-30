namespace PocketPiggy.View
{
    partial class Income
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Income));
            pIncome = new Panel();
            lblIncome = new Label();
            btnAdd = new Button();
            btnMenu = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pIncome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // pIncome
            // 
            pIncome.BackColor = Color.LightPink;
            pIncome.Controls.Add(lblIncome);
            pIncome.Location = new Point(505, 20);
            pIncome.Margin = new Padding(4, 4, 4, 4);
            pIncome.Name = "pIncome";
            pIncome.Size = new Size(459, 469);
            pIncome.TabIndex = 0;
            // 
            // lblIncome
            // 
            lblIncome.AutoSize = true;
            lblIncome.Font = new Font("Century", 16F, FontStyle.Bold);
            lblIncome.Location = new Point(0, 0);
            lblIncome.Margin = new Padding(4, 0, 4, 0);
            lblIncome.Name = "lblIncome";
            lblIncome.Size = new Size(258, 38);
            lblIncome.TabIndex = 0;
            lblIncome.Text = "Income History";
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightPink;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Location = new Point(812, 506);
            btnAdd.Margin = new Padding(4, 4, 4, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(151, 41);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add Income";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.LightPink;
            btnMenu.FlatStyle = FlatStyle.Popup;
            btnMenu.Location = new Point(15, 506);
            btnMenu.Margin = new Padding(4, 4, 4, 4);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(151, 41);
            btnMenu.TabIndex = 2;
            btnMenu.Text = "Menu";
            btnMenu.UseVisualStyleBackColor = false;
            btnMenu.Click += btnMenu_Click;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(15, 15);
            chart1.Margin = new Padding(4, 4, 4, 4);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(469, 469);
            chart1.TabIndex = 3;
            chart1.Text = "chart1";
            // 
            // Income
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1000, 562);
            Controls.Add(chart1);
            Controls.Add(btnMenu);
            Controls.Add(btnAdd);
            Controls.Add(pIncome);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "Income";
            Text = "Income";
            pIncome.ResumeLayout(false);
            pIncome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pIncome;
        private Label lblIncome;
        private Button btnAdd;
        private Button btnMenu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}