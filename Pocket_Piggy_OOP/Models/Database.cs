using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PocketPiggy.Models
{
    public class Database
    {
        public static string ConnectionString
        {
            get
            {
               
                string server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
                string databaseName = Environment.GetEnvironmentVariable("DB_NAME") ?? "pocketpiggydb";
                string user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
                string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
                string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";

                return $"Server={server};Port={port};Database={databaseName};Uid={user};Pwd={password};Charset=utf8mb4;SslMode=None;";
            }
        }

        private static string GetConnectionString()
        {
            return ConnectionString;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: {ex.Message}", ex);
            }
            return dt;
        }

        public static DataTable GetData(string query, params MySqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: {ex.Message}", ex);
            }
            return dt;
        }

        public static void ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: {ex.Message}", ex);
            }
        }

        public static object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            object result = null;
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        result = cmd.ExecuteScalar();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: {ex.Message}", ex);
            }
            return result;
        }
        
        public static DataTable ExecuteSelectQuery(string query, params MySqlParameter[] parameters)
        {
            return GetData(query, parameters);
        }

        public static DataTable ExecuteSelectQuery(string query)
        {
            return GetData(query);
        }
    }
}

