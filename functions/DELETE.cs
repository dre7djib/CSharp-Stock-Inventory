using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace Api{

    public class DELETE {
        
        public static void deleteArticle(DatabaseConnection db, int deleteId, ref string responseString) { // Delete an article from the database
            Console.WriteLine("Are you sure you want to delete this article? (y/n)");
            if (Console.ReadLine() == "y") {
                string sql = $"DELETE FROM articles WHERE id = {deleteId}";
                MySqlCommand command = new MySqlCommand(sql, db.connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
                responseString = "Article deleted";
            }
            else {
                responseString = "Article not deleted";
            }
        }

        public static void deleteUser(DatabaseConnection db, int deleteId, ref string responseString) { // Delete an user from the database
            Console.WriteLine("Are you sure you want to delete this user? (y/n)");
            if (Console.ReadLine() == "y") {
                string sql = $"DELETE FROM users WHERE id = {deleteId}";
                MySqlCommand command = new MySqlCommand(sql, db.connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
                responseString = "User deleted";
            }
            else {
                responseString = "User not deleted";
            }
        }

    }
}