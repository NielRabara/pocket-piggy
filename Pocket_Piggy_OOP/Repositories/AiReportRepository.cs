using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;

namespace PocketPiggy.Repositories
{
    public static class AiReportRepository
    {
        public static void EnsureSchema()
        {
            string createReports = @"CREATE TABLE IF NOT EXISTS ai_reports (
                id INT AUTO_INCREMENT PRIMARY KEY,
                username VARCHAR(255) NOT NULL,
                title VARCHAR(255) NULL,
                report_text LONGTEXT NULL,
                total_income DECIMAL(18,2) NULL,
                total_spending DECIMAL(18,2) NULL,
                net_savings DECIMAL(18,2) NULL,
                savings_rate DECIMAL(9,2) NULL,
                created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            );";

            string createItems = @"CREATE TABLE IF NOT EXISTS ai_report_items (
                id INT AUTO_INCREMENT PRIMARY KEY,
                report_id INT NOT NULL,
                category VARCHAR(255) NULL,
                item_type ENUM('Income','Spending') NOT NULL,
                amount DECIMAL(18,2) NOT NULL,
                percent DECIMAL(9,2) NULL,
                CONSTRAINT fk_ai_report_items_report FOREIGN KEY (report_id)
                    REFERENCES ai_reports(id) ON DELETE CASCADE
            );";

            Database.ExecuteQuery(createReports);
            Database.ExecuteQuery(createItems);
        }

        public static int InsertReport(
            string username,
            string title,
            string reportText,
            decimal totalIncome,
            decimal totalSpending,
            decimal netSavings,
            decimal savingsRate,
            IEnumerable<(string category, string itemType, decimal amount, decimal percent)> items)
        {
            EnsureSchema();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        long reportIdLong;
                        using (var cmd = new MySqlCommand(@"INSERT INTO ai_reports
                            (username, title, report_text, total_income, total_spending, net_savings, savings_rate)
                            VALUES (@u, @t, @rt, @ti, @ts, @ns, @sr);", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@u", username);
                            cmd.Parameters.AddWithValue("@t", (object)title ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@rt", (object)reportText ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ti", totalIncome);
                            cmd.Parameters.AddWithValue("@ts", totalSpending);
                            cmd.Parameters.AddWithValue("@ns", netSavings);
                            cmd.Parameters.AddWithValue("@sr", savingsRate);
                            cmd.ExecuteNonQuery();
                            reportIdLong = cmd.LastInsertedId;
                        }

                        int reportId = (int)reportIdLong;

                        using (var itemCmd = new MySqlCommand(@"INSERT INTO ai_report_items
                            (report_id, category, item_type, amount, percent)
                            VALUES (@rid, @cat, @type, @amt, @pct);", conn, tx))
                        {
                            itemCmd.Parameters.Add("@rid", MySqlDbType.Int32);
                            itemCmd.Parameters.Add("@cat", MySqlDbType.VarChar);
                            itemCmd.Parameters.Add("@type", MySqlDbType.VarChar);
                            itemCmd.Parameters.Add("@amt", MySqlDbType.Decimal);
                            itemCmd.Parameters.Add("@pct", MySqlDbType.Decimal);

                            foreach (var it in items)
                            {
                                itemCmd.Parameters["@rid"].Value = reportId;
                                itemCmd.Parameters["@cat"].Value = (object)it.category ?? DBNull.Value;
                                itemCmd.Parameters["@type"].Value = it.itemType;
                                itemCmd.Parameters["@amt"].Value = it.amount;
                                itemCmd.Parameters["@pct"].Value = it.percent;
                                itemCmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return reportId;
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        public static DataTable GetReportsByUser(string username)
        {
            EnsureSchema();
            string sql = @"SELECT id, title, total_income, total_spending, net_savings, savings_rate, created_at
                           FROM ai_reports WHERE username=@u ORDER BY created_at DESC";
            return Database.GetData(sql, new MySqlParameter("@u", username));
        }

        public static DataTable GetReportItems(int reportId)
        {
            EnsureSchema();
            string sql = @"SELECT id, category, item_type, amount, percent
                           FROM ai_report_items WHERE report_id=@id ORDER BY amount DESC";
            return Database.GetData(sql, new MySqlParameter("@id", reportId));
        }
    }
}
