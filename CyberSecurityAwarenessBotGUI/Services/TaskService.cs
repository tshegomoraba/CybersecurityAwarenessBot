using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CyberSecurityAwarenessBotGUI.Models;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public class TaskService
    {
        // Update this connection string with your MySQL username and password
        private const string ConnectionString =
            "Server=localhost;Database=CyberBotDB;Uid=root;Pwd=@desireM05;";

        public List<CyberTask> GetAllTasks()
        {
            var tasks = new List<CyberTask>();
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            string sql = "SELECT * FROM Tasks ORDER BY CreatedAt DESC";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new CyberTask
                {
                    Id = reader.GetInt32("Id"),
                    Title = reader.GetString("Title"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                        ? "" : reader.GetString("Description"),
                    ReminderDate = reader.IsDBNull(reader.GetOrdinal("ReminderDate"))
                        ? null : reader.GetDateTime("ReminderDate"),
                    IsCompleted = reader.GetBoolean("IsCompleted"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                });
            }
            return tasks;
        }

        public int AddTask(string title, string description, DateTime? reminderDate)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            string sql = "INSERT INTO Tasks (Title, Description, ReminderDate) " +
                         "VALUES (@title, @desc, @reminder); SELECT LAST_INSERT_ID();";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@desc", description);
            cmd.Parameters.AddWithValue("@reminder", (object?)reminderDate ?? DBNull.Value);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void MarkCompleted(int taskId)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            string sql = "UPDATE Tasks SET IsCompleted = TRUE WHERE Id = @id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", taskId);
            cmd.ExecuteNonQuery();
        }

        public void DeleteTask(int taskId)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            string sql = "DELETE FROM Tasks WHERE Id = @id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", taskId);
            cmd.ExecuteNonQuery();
        }
    }
}