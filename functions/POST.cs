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


namespace Api
{
    public class POST {

        public static void AddArticle(DatabaseConnection db, Article A1) { // Add an article to the database
            // Title
            Console.WriteLine("Enter a Title for your product: ");
            A1.Title = Console.ReadLine();
            // Description
            Console.WriteLine("Enter a description for your product: ");
            A1.Description = Console.ReadLine();
            // Quantity
            Console.WriteLine("Enter the Quantity remaining of the product");
            string tempQuantity = Console.ReadLine();
            A1.Quantity = int.Parse(tempQuantity);
            var sql = "INSERT INTO api_csharp.articles (title, description, quantity) VALUES ( @title, @description, @quantity)";
            var cmd = new MySqlCommand(sql, db.connection);
            cmd.Parameters.AddWithValue("@title", A1.Title);
            cmd.Parameters.AddWithValue("@description", A1.Description);
            cmd.Parameters.AddWithValue("@quantity", A1.Quantity);
            cmd.ExecuteNonQuery();
        }

        public static void AddUser(DatabaseConnection db, User U1) { // Add an user to the database
            // Username
            Console.WriteLine("Enter a Username for the user: ");
            U1.Username = Console.ReadLine();
            // Email
            Console.WriteLine("Enter a email for the user: ");
            U1.Email = Console.ReadLine();

            Console.WriteLine("Enter a password for the user: ");
            U1.Password = Console.ReadLine();
            var sql = "INSERT INTO api_csharp.users (username,email,password) VALUES (@username, @email, @password)";
            var cmd = new MySqlCommand(sql, db.connection);
            cmd.Parameters.AddWithValue("@username", U1.Username);
            cmd.Parameters.AddWithValue("@email", U1.Email);
            cmd.Parameters.AddWithValue("@password", U1.Password);
            cmd.ExecuteNonQuery();
        }

    }

}