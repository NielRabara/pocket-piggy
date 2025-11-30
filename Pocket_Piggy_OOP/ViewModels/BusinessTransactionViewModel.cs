using PocketPiggy.Models;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PocketPiggy.ViewModels
{
    public class BusinessTransactionViewModel
    {
        public (bool, string) AddTransaction(int businessId, DateTime date, string description, decimal amount, string type, string category = null)
        {
            try
            {
                if (!BusinessSession.IsLoggedIn || BusinessSession.CurrentBusinessId != businessId)
                {
                    return (false, "Business session not found. Please log in again.");
                }

                if (string.IsNullOrWhiteSpace(description))
                {
                    return (false, "Description cannot be empty.");
                }

                if (amount <= 0)
                {
                    return (false, "Amount must be greater than zero.");
                }

                string insertQuery = @"INSERT INTO business_transactions 
                                      (business_id, date, description, amount, type, category) 
                                      VALUES (@business_id, @date, @desc, @amount, @type, @category)";

                Database.ExecuteQuery(insertQuery,
                    new MySqlParameter("@business_id", businessId),
                    new MySqlParameter("@date", date.Date),
                    new MySqlParameter("@desc", description),
                    new MySqlParameter("@amount", amount),
                    new MySqlParameter("@type", type),
                    new MySqlParameter("@category", string.IsNullOrWhiteSpace(category) ? (object)DBNull.Value : category));

                return (true, $"{type} added successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Error adding {type}: {ex.Message}");
            }
        }

        public DataTable GetTransactions(int businessId, string type = null)
        {
            try
            {
                string query;
                if (string.IsNullOrWhiteSpace(type))
                {
                    query = @"SELECT transaction_id, date, description, amount, type, category, created_at 
                             FROM business_transactions 
                             WHERE business_id = @business_id 
                             ORDER BY date DESC, created_at DESC";
                    return Database.GetData(query, new MySqlParameter("@business_id", businessId));
                }
                else
                {
                    query = @"SELECT transaction_id, date, description, amount, type, category, created_at 
                             FROM business_transactions 
                             WHERE business_id = @business_id AND type = @type 
                             ORDER BY date DESC, created_at DESC";
                    return Database.GetData(query,
                        new MySqlParameter("@business_id", businessId),
                        new MySqlParameter("@type", type));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading transactions: {ex.Message}", ex);
            }
        }

        public decimal GetTotalByType(int businessId, string type)
        {
            try
            {
                string query = @"SELECT COALESCE(SUM(amount), 0) as total 
                                FROM business_transactions 
                                WHERE business_id = @business_id AND type = @type";
                object result = Database.ExecuteScalar(query,
                    new MySqlParameter("@business_id", businessId),
                    new MySqlParameter("@type", type));
                return Convert.ToDecimal(result ?? 0);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calculating total: {ex.Message}", ex);
            }
        }

        public DataTable GetRecentTransactions(int businessId, int limit = 10)
        {
            try
            {
                string query = @"SELECT transaction_id, date, description, amount, type 
                                FROM business_transactions 
                                WHERE business_id = @business_id 
                                ORDER BY date DESC, created_at DESC 
                                LIMIT @limit";
                return Database.GetData(query,
                    new MySqlParameter("@business_id", businessId),
                    new MySqlParameter("@limit", limit));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading recent transactions: {ex.Message}", ex);
            }
        }

        public (bool, string) UpdateTransaction(int transactionId, DateTime date, string description, decimal amount, string category = null)
        {
            try
            {
                if (transactionId <= 0) return (false, "Invalid transaction id.");
                if (amount <= 0) return (false, "Amount must be greater than zero.");

                string query = @"UPDATE business_transactions 
                                 SET date=@date, description=@desc, amount=@amount, category=@category 
                                 WHERE transaction_id=@id";
                Database.ExecuteQuery(query,
                    new MySqlParameter("@date", date.Date),
                    new MySqlParameter("@desc", description ?? string.Empty),
                    new MySqlParameter("@amount", amount),
                    new MySqlParameter("@category", string.IsNullOrWhiteSpace(category) ? (object)DBNull.Value : category),
                    new MySqlParameter("@id", transactionId));
                return (true, "Transaction updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating transaction: {ex.Message}");
            }
        }

        public (bool, string) DeleteTransaction(int transactionId)
        {
            try
            {
                if (transactionId <= 0) return (false, "Invalid transaction id.");
                string query = "DELETE FROM business_transactions WHERE transaction_id=@id";
                Database.ExecuteQuery(query, new MySqlParameter("@id", transactionId));
                return (true, "Transaction deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting transaction: {ex.Message}");
            }
        }
    }
}

