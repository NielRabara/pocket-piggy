using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace PocketPiggy.Models
{
    public class BusinessTransactioN
    {
        public int TransactionId { get; set; }
        public int BusinessId { get; set; }
        public DateTime Date { get; set; }
        public string Vendor { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;  
        public string Status { get; set; } = "Active";

        
        public void Save()
        {
            string query = @"INSERT INTO business_transactions 
                (business_id, date, vendor, description, amount, type, status)
                VALUES (@business_id, @date, @vendor, @description, @amount, @type, @status)";

            Database.ExecuteQuery(query,
                new MySqlParameter("@business_id", BusinessId),
                new MySqlParameter("@date", Date),
                new MySqlParameter("@vendor", Vendor),
                new MySqlParameter("@description", Description),
                new MySqlParameter("@amount", Amount),
                new MySqlParameter("@type", Type),
                new MySqlParameter("@status", Status)
            );
        }

        public static List<BusinessTransactioN> GetByBusinessId(int businessId)
        {
            string query = "SELECT * FROM business_transactions WHERE business_id = @business_id ORDER BY date DESC";
            DataTable dt = Database.GetData(query, new MySqlParameter("@business_id", businessId));

            List<BusinessTransactioN> list = new();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new BusinessTransactioN
                {
                    TransactionId = Convert.ToInt32(row["transaction_id"]),
                    BusinessId = Convert.ToInt32(row["business_id"]),
                    Date = Convert.ToDateTime(row["date"]),
                    Vendor = row["vendor"].ToString()!,
                    Description = row["description"].ToString()!,
                    Amount = Convert.ToDecimal(row["amount"]),
                    Type = row["type"].ToString()!,
                    Status = row["status"].ToString()!
                });
            }

            return list;
        }
    }
}
