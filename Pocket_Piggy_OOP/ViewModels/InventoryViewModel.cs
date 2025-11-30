using PocketPiggy.Models;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PocketPiggy.ViewModels
{
    public class InventoryViewModel
    {
        public (bool, string) AddInventoryItem(int id, int businessId, string itemName, int quantity, decimal cost, decimal? retailPrice = null, string supplier = null)
        {
            try
            {
                if (!BusinessSession.IsLoggedIn || BusinessSession.CurrentBusinessId != businessId)
                {
                    return (false, "Business session not found. Please log in again.");
                }

                if (string.IsNullOrWhiteSpace(itemName))
                {
                    return (false, "Item name cannot be empty.");
                }

                if (quantity < 0)
                {
                    return (false, "Quantity cannot be negative.");
                }

                if (cost < 0)
                {
                    return (false, "Cost cannot be negative.");
                }

                string insertQuery = @"INSERT INTO inventory 
                                      (id, business_id, item_name, quantity, cost, retail_price, supplier) 
                                      VALUES (@business_id, @item_name, @quantity, @cost, @retail_price, @supplier)";

                Database.ExecuteQuery(insertQuery,
                    new MySqlParameter("@business_id", businessId),
                    new MySqlParameter("@item_name", itemName),
                    new MySqlParameter("@quantity", quantity),
                    new MySqlParameter("@cost", cost),
                    new MySqlParameter("@retail_price", retailPrice ?? (object)DBNull.Value),
                    new MySqlParameter("@supplier", string.IsNullOrWhiteSpace(supplier) ? (object)DBNull.Value : supplier));

                return (true, "Inventory item added successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Error adding inventory item: {ex.Message}");
            }
        }

        public DataTable GetInventoryItems(int businessId, int limit = 0)
        {
            try
            {
                string query;
                if (limit > 0)
                {
                    query = @"SELECT item_name, quantity, cost, retail_price, supplier, created_at 
                             FROM inventory 
                             WHERE business_id = @business_id 
                             ORDER BY created_at DESC 
                             LIMIT @limit";
                    return Database.GetData(query,
                        new MySqlParameter("@business_id", businessId),
                        new MySqlParameter("@limit", limit));
                }
                else
                {
                    query = @"SELECT item_name, quantity, cost, retail_price, supplier, created_at 
                             FROM inventory 
                             WHERE business_id = @business_id 
                             ORDER BY created_at DESC";
                    return Database.GetData(query, new MySqlParameter("@business_id", businessId));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading inventory: {ex.Message}", ex);
            }
        }

        public (bool, string) UpdateInventoryQuantity(int businessId, int itemId, int newQuantity)
        {
            try
            {
                if (!BusinessSession.IsLoggedIn || BusinessSession.CurrentBusinessId != businessId)
                {
                    return (false, "Business session not found. Please log in again.");
                }

                string updateQuery = @"UPDATE inventory 
                                      SET quantity = @quantity, updated_at = NOW() 
                                      WHERE id = @id AND business_id = @business_id";

                Database.ExecuteQuery(updateQuery,
                    new MySqlParameter("@quantity", newQuantity),
                    new MySqlParameter("@id", itemId),
                    new MySqlParameter("@business_id", businessId));

                return (true, "Inventory updated successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating inventory: {ex.Message}");
            }
        }
    }
}

