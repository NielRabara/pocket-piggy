using PocketPiggy.Models;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using PocketPiggy.ViewModels;

namespace PocketPiggy.ViewModels
{
    public class LoginViewModel
    {
        public (bool success, string username, string userType) AuthenticatePersonalUser(string username, string password)
        {
            try
            {
                string hashedPassword = SignUpViewModel.HashStatic(password);

                string query = @"SELECT user_id, username FROM users 
                                WHERE username = @username 
                                AND password = @password";

                DataTable dt = Database.GetData(query,
                    new MySqlParameter("@username", username),
                    new MySqlParameter("@password", hashedPassword));

                if (dt.Rows.Count > 0)
                {
                    string authenticatedUsername = dt.Rows[0]["username"].ToString();
                    return (true, authenticatedUsername, "Personal");
                }
                
                return (false, null, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error authenticating personal user: {ex.Message}", ex);
            }
        }

        public (bool success, string username, string userType, int businessId, string businessName) 
            AuthenticateBusinessUser(string username, string password)
        {
            try
            {
                string hashedPassword = SignUpBusinessViewModel.HashStatic(password);

                string query = @"SELECT business_id, username, business_name FROM business_users 
                                WHERE username = @username AND password = @password";

                DataTable dt = Database.GetData(query,
                    new MySqlParameter("@username", username),
                    new MySqlParameter("@password", hashedPassword));

                if (dt.Rows.Count > 0)
                {
                    string authenticatedUsername = dt.Rows[0]["username"].ToString();
                    int businessId = Convert.ToInt32(dt.Rows[0]["business_id"]);
                    string businessName = dt.Rows[0]["business_name"].ToString();
                    return (true, authenticatedUsername, "Business", businessId, businessName);
                }
                
                return (false, null, null, 0, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error authenticating business user: {ex.Message}", ex);
            }
        }

        public (bool success, string username, string userType, int? businessId, string businessName) 
            AuthenticateUser(string username, string password, string userType = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(userType))
                {
                    if (userType.Equals("Business", StringComparison.OrdinalIgnoreCase))
                    {
                        var (bizSuccess, bizUser, _, bizId, bizName) = AuthenticateBusinessUser(username, password);
                        if (bizSuccess)
                            return (true, bizUser, "Business", bizId, bizName);
                    }
                    else if (userType.Equals("Personal", StringComparison.OrdinalIgnoreCase))
                    {
                        var (userSuccess, user, userTypeResult) = AuthenticatePersonalUser(username, password);
                        if (userSuccess)
                            return (true, user, userTypeResult, null, null);
                    }
                }
                
                var (pSuccess, pUser, pType) = AuthenticatePersonalUser(username, password);
                if (pSuccess)
                {
                    return (true, pUser, pType, null, null);
                }

                var (bSuccess, bUser, bType, bId, bName) = AuthenticateBusinessUser(username, password);
                if (bSuccess)
                {
                    return (true, bUser, bType, bId, bName);
                }

                return (false, null, null, null, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during authentication: {ex.Message}", ex);
            }
        }
    }
}

