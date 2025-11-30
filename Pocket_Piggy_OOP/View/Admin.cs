using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using System.Security.Cryptography;
using System.Text;

namespace PocketPiggy
{
    public partial class Admin : Form
    {

        public Admin()
        {
            InitializeComponent();
            LoadUsers();
            LoadTickets();
        }

        private void LoadUsers()
        {
            try
            {
                string userQuery = @"SELECT user_id AS 'User ID', username AS 'Username', email AS 'Email', 'Personal' AS 'Type', created_at AS 'Created' 
                                     FROM users 
                                     ORDER BY created_at DESC";
                DataTable userDt = Database.GetData(userQuery);
                
                string businessQuery = @"SELECT business_id AS 'User ID', business_name AS 'Username', email AS 'Email', 'Business' AS 'Type', created_at AS 'Created' 
                                         FROM business_users 
                                         ORDER BY created_at DESC";
                DataTable businessDt = Database.GetData(businessQuery);
                
                DataTable dt = userDt.Copy();
                foreach (DataRow row in businessDt.Rows)
                {
                    dt.ImportRow(row);
                }
                
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadTickets()
        {
            try
            {
                string query = @"SELECT 
                                    t.ticket_id AS 'Ticket ID',
                                    COALESCE(u.username, b.business_name) AS 'User',
                                    t.ticket_type AS 'Type',
                                    t.subject AS 'Subject',
                                    t.status AS 'Status',
                                    t.priority AS 'Priority',
                                    t.created_at AS 'Created',
                                    t.user_id,
                                    t.business_id
                                 FROM tickets t
                                 LEFT JOIN users u ON t.user_id = u.user_id
                                 LEFT JOIN business_users b ON t.business_id = b.business_id
                                 WHERE t.status IN ('Open', 'In Progress')
                                 ORDER BY 
                                    CASE t.priority 
                                        WHEN 'Urgent' THEN 1
                                        WHEN 'High' THEN 2
                                        WHEN 'Medium' THEN 3
                                        WHEN 'Low' THEN 4
                                    END,
                                    t.created_at ASC";
                DataTable dt = Database.GetData(query);
                dgvRequests.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tickets: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
            LoadTickets();
        }
        
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a ticket to approve.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvRequests.SelectedRows[0];
            int ticketId = Convert.ToInt32(selectedRow.Cells["Ticket ID"].Value);
            string ticketType = selectedRow.Cells["Type"].Value?.ToString();
            string subject = selectedRow.Cells["Subject"].Value?.ToString();
            
            object userIdObj = selectedRow.Cells["user_id"].Value;
            object businessIdObj = selectedRow.Cells["business_id"].Value;
            int? userId = userIdObj != DBNull.Value && userIdObj != null ? Convert.ToInt32(userIdObj) : (int?)null;
            int? businessId = businessIdObj != DBNull.Value && businessIdObj != null ? Convert.ToInt32(businessIdObj) : (int?)null;

            try
            {
                if (ticketType == "Password Change")
                {
                    string getTicketQuery = "SELECT description FROM tickets WHERE ticket_id = @ticket_id";
                    var ticketDt = Database.GetData(getTicketQuery, new MySqlParameter("@ticket_id", ticketId));
                    
                    if (ticketDt.Rows.Count > 0)
                    {
                        string description = ticketDt.Rows[0]["description"].ToString();
                        
                        string newPasswordHash = null;
                        string[] lines = description.Split('\n');
                        foreach (string line in lines)
                        {
                            if (line.StartsWith("New Password Hash: "))
                            {
                                newPasswordHash = line.Substring("New Password Hash: ".Length).Trim();
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(newPasswordHash))
                        {
                            DialogResult confirm = MessageBox.Show(
                                $"Approve password change request?\n\n" +
                                $"User: {selectedRow.Cells["User"].Value}\n" +
                                $"The user has provided their new password and verified their current password.\n\n" +
                                $"Click Yes to apply the new password.",
                                "Approve Password Change",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (confirm == DialogResult.Yes)
                            {
                                if (userId.HasValue)
                                {
                                    string updateQuery = "UPDATE users SET password = @password WHERE user_id = @user_id";
                                    Database.ExecuteQuery(updateQuery,
                                        new MySqlParameter("@password", newPasswordHash),
                                        new MySqlParameter("@user_id", userId.Value));
                                }
                                else if (businessId.HasValue)
                                {
                                    string updateQuery = "UPDATE business_users SET password = @password WHERE business_id = @business_id";
                                    Database.ExecuteQuery(updateQuery,
                                        new MySqlParameter("@password", newPasswordHash),
                                        new MySqlParameter("@business_id", businessId.Value));
                                }

                                string updateTicketQuery = @"UPDATE tickets 
                                                            SET status = 'Resolved', 
                                                                resolved_at = NOW(), 
                                                                admin_notes = @notes 
                                                            WHERE ticket_id = @ticket_id";
                                Database.ExecuteQuery(updateTicketQuery,
                                    new MySqlParameter("@ticket_id", ticketId),
                                    new MySqlParameter("@notes", "Password change approved and applied by admin."));

                                MessageBox.Show("Password change approved and applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadTickets();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Could not find new password hash in ticket description. This may be an old-format ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (ticketType == "Account Deletion")
                {
                    DialogResult confirm = MessageBox.Show(
                        $"Are you sure you want to delete this account?\n\nUser: {selectedRow.Cells["User"].Value}\nType: {ticketType}\n\nThis action cannot be undone!",
                        "Confirm Account Deletion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        if (userId.HasValue)
                        {
                            string deleteQuery = "DELETE FROM users WHERE user_id = @user_id";
                            Database.ExecuteQuery(deleteQuery, new MySqlParameter("@user_id", userId.Value));
                        }
                        else if (businessId.HasValue)
                        {
                            string deleteQuery = "DELETE FROM business_users WHERE business_id = @business_id";
                            Database.ExecuteQuery(deleteQuery, new MySqlParameter("@business_id", businessId.Value));
                        }
                        
                        string updateTicketQuery = @"UPDATE tickets 
                                                    SET status = 'Resolved', 
                                                        resolved_at = NOW(), 
                                                        admin_notes = @notes 
                                                    WHERE ticket_id = @ticket_id";
                        Database.ExecuteQuery(updateTicketQuery,
                            new MySqlParameter("@ticket_id", ticketId),
                            new MySqlParameter("@notes", "Account deleted by admin."));

                        MessageBox.Show("Account deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                        LoadTickets();
                    }
                }
                else
                {
                    DialogResult confirm = MessageBox.Show(
                        $"Mark this ticket as resolved?\n\nType: {ticketType}\nSubject: {subject}",
                        "Resolve Ticket",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirm == DialogResult.Yes)
                    {
                        string updateTicketQuery = @"UPDATE tickets 
                                                    SET status = 'Resolved', 
                                                        resolved_at = NOW(), 
                                                        admin_notes = @notes 
                                                    WHERE ticket_id = @ticket_id";
                        Database.ExecuteQuery(updateTicketQuery,
                            new MySqlParameter("@ticket_id", ticketId),
                            new MySqlParameter("@notes", "Resolved by admin."));

                        MessageBox.Show("Ticket resolved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTickets();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a ticket to reject.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvRequests.SelectedRows[0];
            int ticketId = Convert.ToInt32(selectedRow.Cells["Ticket ID"].Value);
            string subject = selectedRow.Cells["Subject"].Value?.ToString();

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to reject this ticket?\n\nSubject: {subject}",
                "Reject Ticket",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string updateTicketQuery = @"UPDATE tickets 
                                                SET status = 'Closed', 
                                                    admin_notes = @notes 
                                                WHERE ticket_id = @ticket_id";
                    Database.ExecuteQuery(updateTicketQuery,
                        new MySqlParameter("@ticket_id", ticketId),
                        new MySqlParameter("@notes", "Rejected by admin."));

                    MessageBox.Show("Ticket rejected and closed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTickets();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error rejecting ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        
        private void dgvRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvRequests.Rows[e.RowIndex].Cells["Ticket ID"].Value != null)
            {
                int ticketId = Convert.ToInt32(dgvRequests.Rows[e.RowIndex].Cells["Ticket ID"].Value);
                ShowTicketDetails(ticketId);
            }
        }

        private void ShowTicketDetails(int ticketId)
        {
            try
            {
                string query = @"SELECT 
                                    t.ticket_id,
                                    COALESCE(u.username, b.business_name) AS user_name,
                                    t.ticket_type,
                                    t.subject,
                                    t.description,
                                    t.status,
                                    t.priority,
                                    t.created_at,
                                    t.updated_at,
                                    t.resolved_at,
                                    t.admin_notes
                                 FROM tickets t
                                 LEFT JOIN users u ON t.user_id = u.user_id
                                 LEFT JOIN business_users b ON t.business_id = b.business_id
                                 WHERE t.ticket_id = @ticket_id";
                DataTable dt = Database.GetData(query, new MySqlParameter("@ticket_id", ticketId));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string details = $"Ticket ID: {row["ticket_id"]}\n\n" +
                                   $"User: {row["user_name"]}\n" +
                                   $"Type: {row["ticket_type"]}\n" +
                                   $"Subject: {row["subject"]}\n" +
                                   $"Status: {row["status"]}\n" +
                                   $"Priority: {row["priority"]}\n\n" +
                                   $"Description:\n{row["description"]}\n\n" +
                                   $"Created: {row["created_at"]}\n" +
                                   (row["admin_notes"] != DBNull.Value ? $"Admin Notes: {row["admin_notes"]}\n" : "") +
                                   (row["resolved_at"] != DBNull.Value ? $"Resolved: {row["resolved_at"]}" : "");

                    MessageBox.Show(details, "Ticket Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ticket details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
