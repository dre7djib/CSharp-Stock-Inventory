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

    public class GET {

        public static void getAllArticles(DatabaseConnection db, ref string responseString) { // Get all articles from the database
            string query = "SELECT * FROM articles";
            MySqlCommand command = new MySqlCommand(query, db.connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<Article> getAllarticles = new List<Article>(); // Créer une liste pour stocker les articles

            while (reader.Read())
            {
                Article A1 = new Article();
                A1.Id = reader.GetInt32("id");
                A1.Title = reader.GetString("title");
                A1.Description = reader.GetString("description");
                A1.Quantity = reader.GetInt32("quantity");
                getAllarticles.Add(A1); // Ajouter chaque article à la liste
            }
            reader.Close();

            responseString = JsonSerializer.Serialize(getAllarticles);
        }

        public static void getAllUsers(DatabaseConnection db, ref string responseString) { // Get all users from the database
            string query = "SELECT * FROM users";
            MySqlCommand command = new MySqlCommand(query, db.connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<User> getAllUsers = new List<User>(); // Créer une liste pour stocker les Users

            while (reader.Read())
            {
                User U1 = new User();
                U1.Id = reader.GetInt32("id");
                U1.Username = reader.GetString("username");
                U1.Email = reader.GetString("email");
                U1.Password = reader.GetString("password");
                getAllUsers.Add(U1); // Ajouter chaque User à la liste
            }
            reader.Close();

            responseString = JsonSerializer.Serialize(getAllUsers);            
        }

        public static void getArticle(DatabaseConnection db, ref string responseString, HttpListenerRequest request) { // Get an article from the database
            int id;
            if (int.TryParse(request.Url.LocalPath.Substring("/articles/".Length), out id)) {
                Article A1 = new Article();
                var sql = "SELECT * FROM articles WHERE id = @id";
                using (var cmd = new MySqlCommand(sql, db.connection)) {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            if (reader.IsDBNull(1)) {
                                responseString = "Endpoint non pris en charge";
                            }
                            else {
                                A1.Id = reader.GetInt32("id");
                                A1.Title = reader.GetString("title");
                                A1.Description = reader.GetString("description");
                                A1.Quantity = reader.GetInt32("quantity");
                                responseString = JsonSerializer.Serialize(A1);
                            }
                        }
                    }
                }  
            }            
        }

        public static void getUser(DatabaseConnection db, ref string responseString, HttpListenerRequest request) { // Get an user from the database
            int id;
            if (int.TryParse(request.Url.LocalPath.Substring("/users/".Length), out id)) {
                User U1 = new User();
                var sql = "SELECT * FROM users WHERE id = @id";
                using (var cmd = new MySqlCommand(sql, db.connection)) {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            if (reader.IsDBNull(1)) {
                                responseString = "Endpoint non pris en charge";
                            }
                            else {
                                U1.Id = reader.GetInt32("id");
                                U1.Username = reader.GetString("username");
                                U1.Email = reader.GetString("email");
                                U1.Password = reader.GetString("password");
                                responseString = JsonSerializer.Serialize(U1);
                            }
                        }
                    }
                } 
            }            
        }

    }
}