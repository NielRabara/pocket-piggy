using System;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;

namespace PocketPiggy.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private readonly BusinessTransactionViewModel _data;
        private int _businessId;
        private decimal _incomeTotal;
        private decimal _expenseTotal;
        private decimal _netBalance;
        private decimal _spendingRatio;
        private bool _thresholdExceeded;
        private string _warningMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public int BusinessId
        {
            get => _businessId;
            set
            {
                if (_businessId != value)
                {
                    _businessId = value;
                    OnPropertyChanged(nameof(BusinessId));
                }
            }
        }

        public decimal IncomeTotal
        {
            get => _incomeTotal;
            private set
            {
                if (_incomeTotal != value)
                {
                    _incomeTotal = value;
                    OnPropertyChanged(nameof(IncomeTotal));
                }
            }
        }

        public decimal ExpenseTotal
        {
            get => _expenseTotal;
            private set
            {
                if (_expenseTotal != value)
                {
                    _expenseTotal = value;
                    OnPropertyChanged(nameof(ExpenseTotal));
                }
            }
        }

        public decimal NetBalance
        {
            get => _netBalance;
            private set
            {
                if (_netBalance != value)
                {
                    _netBalance = value;
                    OnPropertyChanged(nameof(NetBalance));
                }
            }
        }

        public decimal SpendingRatio
        {
            get => _spendingRatio;
            private set
            {
                if (_spendingRatio != value)
                {
                    _spendingRatio = value;
                    OnPropertyChanged(nameof(SpendingRatio));
                }
            }
        }

        public bool ThresholdExceeded
        {
            get => _thresholdExceeded;
            private set
            {
                if (_thresholdExceeded != value)
                {
                    _thresholdExceeded = value;
                    OnPropertyChanged(nameof(ThresholdExceeded));
                }
            }
        }

        public string WarningMessage
        {
            get => _warningMessage;
            private set
            {
                if (_warningMessage != value)
                {
                    _warningMessage = value;
                    OnPropertyChanged(nameof(WarningMessage));
                }
            }
        }

        public TransactionViewModel()
        {
            _data = new BusinessTransactionViewModel();
            _warningMessage = string.Empty;
        }

        public void Refresh(int businessId)
        {
            BusinessId = businessId;

            decimal income = _data.GetTotalByType(businessId, "Income");
            decimal expenses = _data.GetTotalByType(businessId, "Expense");

            IncomeTotal = income;
            ExpenseTotal = expenses;
            NetBalance = income - expenses;

            SpendingRatio = income > 0 ? Math.Round(expenses / income, 4) : 0m;

            ThresholdExceeded = income > 0 && (expenses / income) >= 0.45m;
            WarningMessage = ThresholdExceeded
                ? "Warning: Spending has reached or exceeded 45% of income."
                : string.Empty;
        }

        public void Refresh(int businessId, bool logWarningToDb)
        {
            Refresh(businessId);
            if (logWarningToDb && ThresholdExceeded)
            {
                try
                {
                    LogWarning(businessId);
                }
                catch
                {
                    
                }
            }
        }

        private void LogWarning(int businessId)
        {
            string createTable = @"CREATE TABLE IF NOT EXISTS business_warnings (
                id INT AUTO_INCREMENT PRIMARY KEY,
                business_id INT NOT NULL,
                message VARCHAR(500) NOT NULL,
                ratio DECIMAL(10,4) NOT NULL,
                created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                INDEX idx_business (business_id)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            Database.ExecuteQuery(createTable);

            string insert = @"INSERT INTO business_warnings (business_id, message, ratio)
                              VALUES (@business_id, @message, @ratio)";

            Database.ExecuteQuery(insert,
                new MySqlParameter("@business_id", businessId),
                new MySqlParameter("@message", WarningMessage ?? string.Empty),
                new MySqlParameter("@ratio", SpendingRatio));
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
