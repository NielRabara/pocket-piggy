namespace PocketPiggy
{
    partial class Receivables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Receivables));
            pReceivables = new Panel();
            label1 = new Label();
            lblAverage = new Label();
            lblOverdue = new Label();
            lblOutstanding = new Label();
            lblTitle = new Label();
            dgvReceivables = new DataGridView();
            cCustomer = new DataGridViewTextBoxColumn();
            cAmount = new DataGridViewTextBoxColumn();
            cStatus = new DataGridViewTextBoxColumn();
            cInvoice = new DataGridViewTextBoxColumn();
            cDueDate = new DataGridViewTextBoxColumn();
            cOverdue = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnMark = new Button();
            btnSend = new Button();
            btnExport = new Button();
            panel1 = new Panel();
            btnDelete = new Button();
            btnEdit = new Button();
            pReceivables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReceivables).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pReceivables
            // 
            pReceivables.Controls.Add(label1);
            pReceivables.Controls.Add(lblAverage);
            pReceivables.Controls.Add(lblOverdue);
            pReceivables.Controls.Add(lblOutstanding);
            pReceivables.Controls.Add(lblTitle);
            pReceivables.Location = new Point(12, 12);
            pReceivables.Name = "pReceivables";
            pReceivables.Size = new Size(572, 224);
            pReceivables.TabIndex = 1;
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
            // lblAverage
            // 
            lblAverage.AutoSize = true;
            lblAverage.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAverage.Location = new Point(4, 107);
            lblAverage.Name = "lblAverage";
            lblAverage.Size = new Size(317, 32);
            lblAverage.TabIndex = 3;
            lblAverage.Text = "Average Collection Period:";
            // 
            // lblOverdue
            // 
            lblOverdue.AutoSize = true;
            lblOverdue.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverdue.Location = new Point(4, 57);
            lblOverdue.Name = "lblOverdue";
            lblOverdue.Size = new Size(118, 32);
            lblOverdue.TabIndex = 2;
            lblOverdue.Text = "Overdue:";
            // 
            // lblOutstanding
            // 
            lblOutstanding.AutoSize = true;
            lblOutstanding.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOutstanding.Location = new Point(4, 165);
            lblOutstanding.Name = "lblOutstanding";
            lblOutstanding.Size = new Size(226, 32);
            lblOutstanding.TabIndex = 1;
            lblOutstanding.Text = "Total Outstanding:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Century", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(86, -3);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(373, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = " Customer Receivables";
            // 
            // dgvReceivables
            // 
            dgvReceivables.AllowUserToAddRows = false;
            dgvReceivables.AllowUserToDeleteRows = false;
            dgvReceivables.BackgroundColor = Color.LightPink;
            dgvReceivables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReceivables.Columns.AddRange(new DataGridViewColumn[] { cCustomer, cAmount, cStatus, cInvoice, cDueDate, cOverdue });
            dgvReceivables.Location = new Point(16, 268);
            dgvReceivables.Name = "dgvReceivables";
            dgvReceivables.ReadOnly = true;
            dgvReceivables.RowHeadersVisible = false;
            dgvReceivables.RowHeadersWidth = 62;
            dgvReceivables.Size = new Size(1064, 394);
            dgvReceivables.TabIndex = 2;
            // 
            // cCustomer
            // 
            cCustomer.HeaderText = "Customer";
            cCustomer.MinimumWidth = 8;
            cCustomer.Name = "cCustomer";
            cCustomer.ReadOnly = true;
            cCustomer.Width = 185;
            // 
            // cAmount
            // 
            cAmount.HeaderText = "Amount";
            cAmount.MinimumWidth = 8;
            cAmount.Name = "cAmount";
            cAmount.ReadOnly = true;
            cAmount.Width = 175;
            // 
            // cStatus
            // 
            cStatus.HeaderText = "Status";
            cStatus.MinimumWidth = 8;
            cStatus.Name = "cStatus";
            cStatus.ReadOnly = true;
            cStatus.Width = 150;
            // 
            // cInvoice
            // 
            cInvoice.HeaderText = "Invoice";
            cInvoice.MinimumWidth = 8;
            cInvoice.Name = "cInvoice";
            cInvoice.ReadOnly = true;
            cInvoice.Width = 180;
            // 
            // cDueDate
            // 
            cDueDate.HeaderText = "Due Date";
            cDueDate.MinimumWidth = 8;
            cDueDate.Name = "cDueDate";
            cDueDate.ReadOnly = true;
            cDueDate.Width = 191;
            // 
            // cOverdue
            // 
            cOverdue.HeaderText = "Days Overdue";
            cOverdue.MinimumWidth = 8;
            cOverdue.Name = "cOverdue";
            cOverdue.ReadOnly = true;
            cOverdue.Width = 180;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightPink;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.Location = new Point(637, 13);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(198, 66);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add Invoice";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnMark
            // 
            btnMark.BackColor = Color.LightPink;
            btnMark.FlatStyle = FlatStyle.Popup;
            btnMark.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMark.Location = new Point(0, 81);
            btnMark.Name = "btnMark";
            btnMark.Size = new Size(198, 69);
            btnMark.TabIndex = 4;
            btnMark.Text = "Mark as Paid";
            btnMark.UseVisualStyleBackColor = false;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.LightPink;
            btnSend.FlatStyle = FlatStyle.Popup;
            btnSend.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSend.Location = new Point(869, 13);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(167, 67);
            btnSend.TabIndex = 5;
            btnSend.Text = "Send Reminder";
            btnSend.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.LightPink;
            btnExport.FlatStyle = FlatStyle.Popup;
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExport.Location = new Point(234, 84);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(167, 69);
            btnExport.TabIndex = 6;
            btnExport.Text = "Export CSV";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnExport);
            panel1.Controls.Add(btnMark);
            panel1.Location = new Point(636, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(401, 235);
            panel1.TabIndex = 7;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightPink;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.Location = new Point(234, 165);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(167, 69);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightPink;
            btnEdit.FlatStyle = FlatStyle.Popup;
            btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEdit.Location = new Point(0, 165);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(198, 69);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // Receivables
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1092, 674);
            Controls.Add(btnSend);
            Controls.Add(btnAdd);
            Controls.Add(dgvReceivables);
            Controls.Add(pReceivables);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Receivables";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Receivables";
            pReceivables.ResumeLayout(false);
            pReceivables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReceivables).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pReceivables;
        private Label label1;
        private Label lblAverage;
        private Label lblOverdue;
        private Label lblOutstanding;
        private Label lblTitle;
        private DataGridView dgvReceivables;
        private Button btnAdd;
        private Button btnMark;
        private Button btnSend;
        private Button btnExport;
        private Panel panel1;
        private DataGridViewTextBoxColumn cCustomer;
        private DataGridViewTextBoxColumn cAmount;
        private DataGridViewTextBoxColumn cStatus;
        private DataGridViewTextBoxColumn cInvoice;
        private DataGridViewTextBoxColumn cDueDate;
        private DataGridViewTextBoxColumn cOverdue;
        private Button btnDelete;
        private Button btnEdit;
    }
}