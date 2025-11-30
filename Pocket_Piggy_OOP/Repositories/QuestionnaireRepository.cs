using System;
using System.Data;
using MySql.Data.MySqlClient;
using PocketPiggy.Models;

namespace PocketPiggy.Repositories
{
    public static class QuestionnaireRepository
    {
        public static void EnsureSchema()
        {
            string create = @"CREATE TABLE IF NOT EXISTS questionnaire (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_type ENUM('business', 'personal'),
    user_id INT,
    question TEXT,
    answer TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);";
            Database.ExecuteQuery(create);
        }

        public static void InsertAnswer(string userType, int userId, string question, string answer)
        {
            string sql = @"INSERT INTO questionnaire (user_type, user_id, question, answer)
VALUES (@userType, @userId, @question, @answer);";
            Database.ExecuteQuery(sql,
                new MySqlParameter("@userType", userType),
                new MySqlParameter("@userId", userId),
                new MySqlParameter("@question", question),
                new MySqlParameter("@answer", answer));
        }
    }
}
