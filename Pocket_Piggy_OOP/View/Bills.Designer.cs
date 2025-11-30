namespace PocketPiggy.View
{
    partial class Bills
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bills));
            pUp = new Panel();
            flowUpcoming = new FlowLayoutPanel();
            lblUpBills = new Label();
            btnAdd = new Button();
            btnViewH = new Button();
            btnMenu = new Button();
            pPending = new Panel();
            flowPending = new FlowLayoutPanel();
            lblPendingBills = new Label();
            pUp.SuspendLayout();
            pPending.SuspendLayout();
            SuspendLayout();
            // 
            // pUp
            // 
            pUp.AutoScroll = true;
            pUp.BorderStyle = BorderStyle.FixedSingle;
            pUp.Controls.Add(flowUpcoming);
            pUp.Controls.Add(lblUpBills);
            pUp.Location = new Point(15, 15);
            pUp.Margin = new Padding(4);
            pUp.Name = "pUp";
            pUp.Size = new Size(483, 481);
            pUp.TabIndex = 7;
            // 
            // flowUpcoming
            // 
            flowUpcoming.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowUpcoming.AutoScroll = true;
            flowUpcoming.BackColor = Color.LightPink;
            flowUpcoming.FlowDirection = FlowDirection.TopDown;
            flowUpcoming.Location = new Point(4, 42);
            flowUpcoming.Margin = new Padding(4);
            flowUpcoming.Name = "flowUpcoming";
            flowUpcoming.Size = new Size(474, 432);
            flowUpcoming.TabIndex = 0;
            flowUpcoming.WrapContents = false;
            // 
            // lblUpBills
            // 
            lblUpBills.AutoSize = true;
            lblUpBills.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUpBills.ImageAlign = ContentAlignment.TopCenter;
            lblUpBills.Location = new Point(4, 0);
            lblUpBills.Margin = new Padding(4, 0, 4, 0);
            lblUpBills.Name = "lblUpBills";
            lblUpBills.Size = new Size(257, 38);
            lblUpBills.TabIndex = 0;
            lblUpBills.Text = "Upcoming Bills";
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightPink;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 9F);
            btnAdd.ForeColor = Color.Black;
            btnAdd.Location = new Point(15, 510);
            btnAdd.Margin = new Padding(4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 44);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add Bill";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAddBill_Click;
            // 
            // btnViewH
            // 
            btnViewH.BackColor = Color.LightPink;
            btnViewH.FlatAppearance.BorderSize = 0;
            btnViewH.FlatStyle = FlatStyle.Popup;
            btnViewH.Font = new Font("Segoe UI", 9F);
            btnViewH.ForeColor = Color.Black;
            btnViewH.Location = new Point(175, 510);
            btnViewH.Margin = new Padding(4);
            btnViewH.Name = "btnViewH";
            btnViewH.Size = new Size(150, 44);
            btnViewH.TabIndex = 9;
            btnViewH.Text = "View History";
            btnViewH.UseVisualStyleBackColor = false;
            btnViewH.Click += btnViewHistory_Click;
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.LightPink;
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatStyle = FlatStyle.Popup;
            btnMenu.Font = new Font("Segoe UI", 9F);
            btnMenu.ForeColor = Color.Black;
            btnMenu.Location = new Point(825, 510);
            btnMenu.Margin = new Padding(4);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(150, 44);
            btnMenu.TabIndex = 10;
            btnMenu.Text = "Back to Menu";
            btnMenu.UseVisualStyleBackColor = false;
            btnMenu.Click += btnBackToMenu_Click;
            // 
            // pPending
            // 
            pPending.AutoScroll = true;
            pPending.BorderStyle = BorderStyle.FixedSingle;
            pPending.Controls.Add(flowPending);
            pPending.Controls.Add(lblPendingBills);
            pPending.Location = new Point(501, 15);
            pPending.Margin = new Padding(4);
            pPending.Name = "pPending";
            pPending.Size = new Size(483, 481);
            pPending.TabIndex = 8;
            // 
            // flowPending
            // 
            flowPending.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowPending.AutoScroll = true;
            flowPending.BackColor = Color.LightPink;
            flowPending.FlowDirection = FlowDirection.TopDown;
            flowPending.Location = new Point(4, 42);
            flowPending.Margin = new Padding(4);
            flowPending.Name = "flowPending";
            flowPending.Size = new Size(474, 432);
            flowPending.TabIndex = 0;
            flowPending.WrapContents = false;
            // 
            // lblPendingBills
            // 
            lblPendingBills.AutoSize = true;
            lblPendingBills.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPendingBills.ImageAlign = ContentAlignment.TopCenter;
            lblPendingBills.Location = new Point(4, 0);
            lblPendingBills.Margin = new Padding(4, 0, 4, 0);
            lblPendingBills.Name = "lblPendingBills";
            lblPendingBills.Size = new Size(229, 38);
            lblPendingBills.TabIndex = 0;
            lblPendingBills.Text = "Pending Bills";
            // 
            // Bills
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1000, 562);
            Controls.Add(pPending);
            Controls.Add(btnMenu);
            Controls.Add(btnViewH);
            Controls.Add(btnAdd);
            Controls.Add(pUp);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "Bills";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bills";
            pUp.ResumeLayout(false);
            pUp.PerformLayout();
            pPending.ResumeLayout(false);
            pPending.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pUp;
        private Label lblUpBills;
        private Panel pPending;
        private Label lblPendingBills;
        private Button btnAdd;
        private Button btnViewH;
        private Button btnMenu;
        private FlowLayoutPanel flowUpcoming;
        private FlowLayoutPanel flowPending;
    }
}