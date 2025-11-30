using System;
using System.Data;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;

namespace PocketPiggy.Repositories
{
    public static class ProfileRepository
    {
        public static void EnsureSchema()
        {
            // Use IF NOT EXISTS where available to avoid errors on repeat runs
            string alterUsers = @"ALTER TABLE users
ADD COLUMN IF NOT EXISTS profile_picture LONGBLOB NULL,
ADD COLUMN IF NOT EXISTS display_name VARCHAR(255) NULL;";

            string alterBusinessUsers = @"ALTER TABLE business_users
ADD COLUMN IF NOT EXISTS profile_picture LONGBLOB NULL,
ADD COLUMN IF NOT EXISTS owner_name VARCHAR(255) NULL;";

            Database.ExecuteQuery(alterUsers);
            Database.ExecuteQuery(alterBusinessUsers);
        }

        // PERSONAL
        public static (int userId, string name, byte[] pic) GetPersonalProfileByUsername(string username)
        {
            string sql = @"SELECT user_id, display_name, profile_picture FROM users WHERE username=@u LIMIT 1";
            var dt = Database.GetData(sql, new MySqlParameter("@u", username));
            if (dt.Rows.Count == 0)
            {
                return (0, null, null);
            }
            var row = dt.Rows[0];
            int id = Convert.ToInt32(row["user_id"]);
            string name = row["display_name"] == DBNull.Value ? null : row["display_name"].ToString();
            byte[] pic = row["profile_picture"] == DBNull.Value ? null : (byte[])row["profile_picture"];
            return (id, name, pic);
        }

        public static void UpdatePersonalProfile(int userId, string name, byte[] profilePic)
        {
            string sql = @"UPDATE users SET display_name=@n, profile_picture=@p WHERE user_id=@id";
            Database.ExecuteQuery(sql,
                new MySqlParameter("@n", string.IsNullOrWhiteSpace(name) ? (object)DBNull.Value : name),
                new MySqlParameter("@p", (object)profilePic ?? DBNull.Value),
                new MySqlParameter("@id", userId));
        }

        // BUSINESS
        public static (string name, byte[] pic) GetBusinessProfile(int businessId)
        {
            string sql = @"SELECT
    COALESCE(owner_name, business_name) AS owner_name,
    profile_picture
FROM business_users WHERE business_id=@id LIMIT 1";
            var dt = Database.GetData(sql, new MySqlParameter("@id", businessId));
            if (dt.Rows.Count == 0)
            {
                return (null, null);
            }
            var row = dt.Rows[0];
            string name = row["owner_name"] == DBNull.Value ? null : row["owner_name"].ToString();
            byte[] pic = row["profile_picture"] == DBNull.Value ? null : (byte[])row["profile_picture"];
            return (name, pic);
        }

        public static void UpdateBusinessProfile(int businessId, string name, byte[] profilePic)
        {
            string sql = @"UPDATE business_users SET owner_name=@n, profile_picture=@p WHERE business_id=@id";
            Database.ExecuteQuery(sql,
                new MySqlParameter("@n", string.IsNullOrWhiteSpace(name) ? (object)DBNull.Value : name),
                new MySqlParameter("@p", (object)profilePic ?? DBNull.Value),
                new MySqlParameter("@id", businessId));
        }
    }
}
