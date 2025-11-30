using PocketPiggy.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace PocketPiggy.ViewModels
{
    public class SignUpBusinessViewModel
    {
        public (bool, string) RegisterBusiness(
            string username,
            string password,
            string businessName,
            string businessAddress,
            string contactNumber,
            string email = null,
            string industry = null)
        {
            if (string.IsNullOrWhiteSpace(username)) return (false, "Username cannot be empty.");
            if (string.IsNullOrWhiteSpace(password)) return (false, "Password cannot be empty.");
            if (string.IsNullOrWhiteSpace(businessName)) return (false, "Business name cannot be empty.");
            if (string.IsNullOrWhiteSpace(businessAddress)) return (false, "Business address cannot be empty.");
            if (string.IsNullOrWhiteSpace(contactNumber)) return (false, "Contact number cannot be empty.");

            if (username.Length > 50) return (false, "Username is too long (max 50 characters).");
            if (password.Length < 6) return (false, "Password must be at least 6 characters long.");
            if (contactNumber.Length < 7 || contactNumber.Length > 15)
                return (false, "Contact number must be between 7 and 15 digits.");

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    const string checkQuery = @"SELECT COUNT(*) FROM business_users 
                                               WHERE username = @username OR business_name = @business_name";
                    using (var checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        checkCmd.Parameters.AddWithValue("@business_name", businessName);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            return (false, "Username or Business Name already exists. Please choose another one.");
                        }
                    }

                    string hashed = HashPassword(password);

                    const string insertQuery = @"
                        INSERT INTO business_users 
                        (username, password, business_name, business_address, contact_number, date_created)
                        VALUES (@username, @password, @business_name, @business_address, @contact_number, NOW())";

                    using (var insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@username", username);
                        insertCmd.Parameters.AddWithValue("@password", hashed);
                        insertCmd.Parameters.AddWithValue("@business_name", businessName);
                        insertCmd.Parameters.AddWithValue("@business_address", businessAddress);
                        insertCmd.Parameters.AddWithValue("@contact_number", contactNumber);
                        insertCmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return (true, "Business account created successfully!");
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message);
            }
        }

        /// <summary>
        /// SHA256 hashing for passwords before saving.
        /// </summary>
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static string HashStatic(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
