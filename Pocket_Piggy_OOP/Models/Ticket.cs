using System;
using MySql.Data.MySqlClient;

namespace PocketPiggy.Models
{
    public static class Ticket
    {
        public static void CreateForPersonal(int userId, string ticketType, string subject, string description, string priority = "High")
        {
            string sql = @"INSERT INTO tickets (user_id, ticket_type, subject, description, status, priority, created_at)
                           VALUES (@user_id, @ticket_type, @subject, @description, 'Open', @priority, NOW())";
            Database.ExecuteQuery(sql,
                new MySqlParameter("@user_id", userId),
                new MySqlParameter("@ticket_type", ticketType ?? ""),
                new MySqlParameter("@subject", subject ?? ""),
                new MySqlParameter("@description", description ?? ""),
                new MySqlParameter("@priority", priority ?? "Medium"));
        }

        public static void CreateForBusiness(int businessId, string ticketType, string subject, string description, string priority = "High")
        {
            string sql = @"INSERT INTO tickets (business_id, ticket_type, subject, description, status, priority, created_at)
                           VALUES (@business_id, @ticket_type, @subject, @description, 'Open', @priority, NOW())";
            Database.ExecuteQuery(sql,
                new MySqlParameter("@business_id", businessId),
                new MySqlParameter("@ticket_type", ticketType ?? ""),
                new MySqlParameter("@subject", subject ?? ""),
                new MySqlParameter("@description", description ?? ""),
                new MySqlParameter("@priority", priority ?? "Medium"));
        }
    }
}
