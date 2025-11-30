using PocketPiggy.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace PocketPiggy.ViewModels
{
    public class SignUpViewModel
    {
        public (bool, string) RegisterUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) return (false, "Username cannot be empty.");
            if (string.IsNullOrWhiteSpace(password)) return (false, "Password cannot be empty.");
            if (username.Length > 50) return (false, "Username is too long (max 50 characters).");

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    const string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    using (var checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        int existCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (existCount > 0)
                        {
                            return (false, "Username already exists. Please choose another one.");
                        }
                    }

                    string hashed = HashPassword(password);

                    const string insertQuery = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    using (var insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@username", username);
                        insertCmd.Parameters.AddWithValue("@password", hashed);
                        insertCmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return (true, "Account created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message);
            }
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

        public static string HashStatic(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
