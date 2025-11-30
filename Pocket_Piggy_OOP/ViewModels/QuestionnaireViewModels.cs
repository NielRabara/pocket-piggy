using MySql.Data.MySqlClient;
using PocketPiggy.Models;
using System;

namespace PocketPiggy.ViewModels
{
    public class QuestionnaireViewModel
    {
        public bool SaveResponses(
            int userId,
            string fullName, int age, string birthMonth, int birthDay, int birthYear,
            string gender, string occupation, string sourceOfIncome, string maritalStatus,
            string averageIncome, string monthlySpend, string expenseFreq,
            string financialGoal, string saveGoal, string confidence, string reminderFreq)
        {
            try
            {
                string query = @"
                    INSERT INTO questionnaire (
                        user_id, full_name, age, birth_month, birth_day, birth_year,
                        gender, occupation, source_of_income, marital_status,
                        average_income, monthly_spend, expense_frequency,
                        financial_goal, save_goal, confidence_level, reminder_frequency
                    ) VALUES (
                        @user_id, @full_name, @age, @birth_month, @birth_day, @birth_year,
                        @gender, @occupation, @source_of_income, @marital_status,
                        @average_income, @monthly_spend, @expense_frequency,
                        @financial_goal, @save_goal, @confidence_level, @reminder_frequency
                    )";

                Database.ExecuteQuery(query,
                    new MySqlParameter("@user_id", userId),
                    new MySqlParameter("@full_name", fullName),
                    new MySqlParameter("@age", age),
                    new MySqlParameter("@birth_month", birthMonth),
                    new MySqlParameter("@birth_day", birthDay),
                    new MySqlParameter("@birth_year", birthYear),
                    new MySqlParameter("@gender", gender),
                    new MySqlParameter("@occupation", occupation),
                    new MySqlParameter("@source_of_income", sourceOfIncome),
                    new MySqlParameter("@marital_status", maritalStatus),
                    new MySqlParameter("@average_income", averageIncome),
                    new MySqlParameter("@monthly_spend", monthlySpend),
                    new MySqlParameter("@expense_frequency", expenseFreq),
                    new MySqlParameter("@financial_goal", financialGoal),
                    new MySqlParameter("@save_goal", saveGoal),
                    new MySqlParameter("@confidence_level", confidence),
                    new MySqlParameter("@reminder_frequency", reminderFreq)
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Questionnaire Error] {ex.Message}");
                return false;
            }
        }
    }
}
