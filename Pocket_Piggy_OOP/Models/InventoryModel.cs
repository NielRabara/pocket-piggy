using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace PocketPiggy.Models
{
    public class InventoryModel
    {
        public int ItemId { get; set; }
        public int BusinessId { get; set; } 
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        
        public decimal TotalValue => Quantity * CostPrice;
        
        public void Save()
        {
            // Insert into the canonical `inventory` table. Use column names that many DBs use (cost, retail_price, created_at).
            string query = @"INSERT INTO inventory 
                (business_id, item_name, quantity, cost, retail_price, created_at)
                VALUES (@business_id, @item_name, @quantity, @cost, @retail_price, @date_added)";

            Database.ExecuteQuery(query,
                new MySqlParameter("@business_id", BusinessId),
                new MySqlParameter("@item_name", ItemName),
                new MySqlParameter("@quantity", Quantity),
                new MySqlParameter("@cost", CostPrice),
                new MySqlParameter("@retail_price", SellingPrice),
                new MySqlParameter("@date_added", DateAdded)
            );
        }
        
        public void Update()
        {
            // Update the canonical `inventory` table columns
            string query = @"UPDATE inventory
                             SET item_name = @item_name,
                                 quantity = @quantity,
                                 cost = @cost,
                                 retail_price = @retail_price
                             WHERE item_id = @item_id AND business_id = @business_id";

            Database.ExecuteQuery(query,
                new MySqlParameter("@item_name", ItemName),
                new MySqlParameter("@quantity", Quantity),
                new MySqlParameter("@cost", CostPrice),
                new MySqlParameter("@retail_price", SellingPrice),
                new MySqlParameter("@item_id", ItemId),
                new MySqlParameter("@business_id", BusinessId)
            );
        }
        
        public static List<InventoryModel> GetByBusinessId(int businessId)
        {
            // Use COALESCE to support either schema variants (business_inventory or inventory)
            string query = @"SELECT 
                                COALESCE(item_id, 0) AS item_id,
                                COALESCE(business_id, @business_id) AS business_id,
                                COALESCE(item_name, '') AS item_name,
                                COALESCE(quantity, 0) AS quantity,
                                COALESCE(cost_price, cost) AS cost_price,
                                COALESCE(selling_price, retail_price) AS selling_price,
                                COALESCE(date_added, created_at, NOW()) AS date_added
                             FROM inventory
                             WHERE business_id = @business_id
                             ORDER BY COALESCE(date_added, created_at) DESC";

            DataTable dt = Database.GetData(query, new MySqlParameter("@business_id", businessId));

            List<InventoryModel> list = new();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new InventoryModel
                {
                    ItemId = Convert.ToInt32(row["item_id"]),
                    BusinessId = Convert.ToInt32(row["business_id"]),
                    ItemName = row["item_name"].ToString() ?? string.Empty,
                    Quantity = Convert.ToInt32(row["quantity"]),
                    CostPrice = Convert.ToDecimal(row["cost_price"]),
                    SellingPrice = Convert.ToDecimal(row["selling_price"]),
                    DateAdded = Convert.ToDateTime(row["date_added"])
                });
            }

            return list;
        }
        
        public static void Delete(int itemId)
        {
            string query = "DELETE FROM inventory WHERE item_id = @item_id";
            Database.ExecuteQuery(query, new MySqlParameter("@item_id", itemId));
        }
    }
}