using PocketPiggy.Models;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PocketPiggy.ViewModels
{
    public class BusinessDashboardViewModel
    {
        private readonly BusinessTransactionViewModel _transactionViewModel;
        private readonly TransactionViewModel _ratioViewModel = new TransactionViewModel();

        public BusinessDashboardViewModel()
        {
            _transactionViewModel = new BusinessTransactionViewModel();
        }

        public (bool, int, string) GetBusinessInfo(string username)
        {
            try
            {
                string query = @"SELECT business_id, business_name, username 
                                FROM business_users 
                                WHERE username = @username";
                DataTable dt = Database.GetData(query, new MySqlParameter("@username", username));

                if (dt.Rows.Count > 0)
                {
                    int businessId = Convert.ToInt32(dt.Rows[0]["business_id"]);
                    string businessName = dt.Rows[0]["business_name"].ToString();
                    return (true, businessId, businessName);
                }
                else
                {
                    return (false, 0, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading business info: {ex.Message}", ex);
            }
        }

        public decimal GetTotalIncome(int businessId)
        {
            return _transactionViewModel.GetTotalByType(businessId, "Income");
        }

        public decimal GetTotalExpenses(int businessId)
        {
            return _transactionViewModel.GetTotalByType(businessId, "Expense");
        }

        public decimal GetTotalReceivables(int businessId)
        {
            return _transactionViewModel.GetTotalByType(businessId, "Receivable");
        }


        public decimal GetTotalCashReserve(int businessId)
        {
            return _transactionViewModel.GetTotalByType(businessId, "Cash Reserve");
        }

        public DataTable GetRecentTransactions(int businessId, int limit = 10)
        {
            return _transactionViewModel.GetRecentTransactions(businessId, limit);
        }

        public decimal GetSpendingRatio(int businessId)
        {
            _ratioViewModel.Refresh(businessId);
            return _ratioViewModel.SpendingRatio;
        }

        public string GetSpendingWarning(int businessId, bool logWarningToDb = false)
        {
            if (logWarningToDb)
                _ratioViewModel.Refresh(businessId, true);
            else
                _ratioViewModel.Refresh(businessId);

            return _ratioViewModel.WarningMessage;
        }
    }
}

