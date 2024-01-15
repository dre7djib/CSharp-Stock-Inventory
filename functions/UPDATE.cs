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

    public class UPDATE {

        public static void updateArticle(DatabaseConnection db, HttpListenerRequest request) { // Update an article from the database
            int id;
            if (int.TryParse(request.Url.LocalPath.Substring("/articles/update/".Length), out id)) {
                string sql = $"SELECT * FROM articles WHERE id = {id}";
                MySqlCommand command = new MySqlCommand(sql, db.connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Article item = new Article();
                item.Id = reader.GetInt32(0);
                item.Title = reader.GetString(1);
                item.Description = reader.GetString(2);
                item.Quantity = reader.GetInt32(3);
                reader.Close();
                Console.WriteLine("What do you want to update? Title, Description, Quantity");
                string updateString = Console.ReadLine();
                switch (updateString){
                    case "Title":
                        Console.WriteLine("Enter the new value");
                        item.Title = Console.ReadLine();
                        string sql_title = $"UPDATE articles SET title = '{item.Title}' WHERE id = {id}";
                        MySqlCommand command_title = new MySqlCommand(sql_title, db.connection);
                        MySqlDataReader reader_title = command_title.ExecuteReader();
                        reader_title.Close();
                        Console.WriteLine("Value Modified");
                        break;
                    case "Description":
                        Console.WriteLine("Enter the new value");
                        item.Description = Console.ReadLine();
                        string sql_description = $"UPDATE articles SET description = '{item.Description}' WHERE id = {id}";
                        MySqlCommand command_description = new MySqlCommand(sql_description, db.connection);
                        MySqlDataReader reader_description = command_description.ExecuteReader();
                        reader_description.Close();
                        Console.WriteLine("Value Modified");
                        break;
                    case "Quantity":
                        Console.WriteLine("Enter the new value");
                        item.Quantity = int.Parse(Console.ReadLine());
                        string sql_quantity = $"UPDATE articles SET quantity = '{item.Quantity}' WHERE id = {id}";
                        MySqlCommand command_quantity = new MySqlCommand(sql_quantity, db.connection);
                        MySqlDataReader reader_quantity = command_quantity.ExecuteReader();
                        reader_quantity.Close();
                        Console.WriteLine("Value Modified");
                        break;
                    default:
                        Console.WriteLine("It doesn't exit!");
                        break;
                }
            } 
        }


        public static void updateUser(DatabaseConnection db, HttpListenerRequest request) { // Update an article from the database
            int id;
            if (int.TryParse(request.Url.LocalPath.Substring("/users/update/".Length), out id)) {
                string sql = $"SELECT * FROM users WHERE id = {id}";
                MySqlCommand command = new MySqlCommand(sql, db.connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                User item = new User();
                item.Id = reader.GetInt32(0);
                item.Username = reader.GetString(1);
                item.Email = reader.GetString(2);
                item.Password = reader.GetString(3);
                reader.Close();
                Console.WriteLine("What do you want to update? Username, Email, Password");
                string updateString = Console.ReadLine();
                switch (updateString){
                    case "Username":
                        Console.WriteLine("Enter the new value");
                        item.Username = Console.ReadLine();
                        string sql_username = $"UPDATE users SET username = '{item.Username}' WHERE id = {id}";
                        MySqlCommand command_username = new MySqlCommand(sql_username, db.connection);
                        MySqlDataReader reader_username = command_username.ExecuteReader();
                        reader_username.Close();
                        Console.WriteLine("Value Modified");
                        break;
                    case "Email":
                        Console.WriteLine("Enter the new value");
                        item.Email = Console.ReadLine();
                        string sql_email= $"UPDATE users SET email= '{item.Email}' WHERE id = {id}";
                        MySqlCommand command_email= new MySqlCommand(sql_email, db.connection);
                        MySqlDataReader reader_email= command_email.ExecuteReader();
                        reader_email.Close();
                        Console.WriteLine("Value Modified");
                        break;
                    case "Password":
                        Console.WriteLine("Enter the new value");
                        item.Password = Console.ReadLine();
                        string sql_password = $"UPDATE users SET password = '{item.Password}' WHERE id = {id}";
                        MySqlCommand command_password = new MySqlCommand(sql_password, db.connection);
                        MySqlDataReader reader_password = command_password.ExecuteReader();
                        reader_password.Close();
                        Console.WriteLine("Value Modified");
                        break;
                    default:
                        Console.WriteLine("It doesn't exit!");
                        break;
                }
            } 
        }
    }
}