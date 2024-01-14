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


namespace Api {
    public class RequestHandler {
        public static void ProcessRequest(HttpListenerContext context, List<Article> articles , List<User> users, DatabaseConnection db) {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string responseString = "";

            switch(request.HttpMethod) {

                case "POST":
                    // Add Article
                    if (request.Url.LocalPath == "/articles/add") {
                        Article A1 = new Article();
                        POST.AddArticle(db, A1);
                    }
                        
                    // Add User
                    if (request.Url.LocalPath == "/users/add") {
                        User U1 = new User();
                        POST.AddUser(db, U1);
                    }
                    break;
                
                case "DELETE":
                    // Delete Article
                    if (request.Url.LocalPath.StartsWith("/articles/delete")) {
                        Console.WriteLine("Enter the id of the products you want to delete");
                        int deleteId = int.Parse(Console.ReadLine());
                        articles.RemoveAt(deleteId - 1);
                    }
                    // Delete User
                    if (request.Url.LocalPath.StartsWith("/users/delete")) {
                        Console.WriteLine("Enter the id of the user you want to delete");
                        int deleteId = int.Parse(Console.ReadLine());
                        users.RemoveAt(deleteId - 1);
                    }
                    break;
                
                case "PUT":
                    // Update Article
                    if (request.Url.LocalPath.StartsWith("/articles/update/")) {
                        int id;
                        if (int.TryParse(request.Url.LocalPath.Substring("/articles/update/".Length), out id)) {
                            foreach (var item in articles) {
                                if (item.Id == id) {
                                    Console.WriteLine("What do you want to update? Title, Description, Quantity");
                                    string updateString = Console.ReadLine();

                                    switch(updateString) {

                                        case "Title":
                                            Console.WriteLine("Enter the new value");
                                            item.Title = Console.ReadLine();
                                            break;
                                        case "Description":
                                            Console.WriteLine("Enter the new value");
                                            item.Description = Console.ReadLine();
                                            break;
                                        case "Quantity":
                                            Console.WriteLine("Enter the new value");
                                            string tempQuantity = Console.ReadLine();
                                            item.Quantity = int.Parse(tempQuantity);
                                            break;
                                        default:
                                            Console.WriteLine("It doesn't exit!");
                                            break;
                                    }
                                }
                            }
                        } 
                    }

                    if (request.Url.LocalPath.StartsWith("/users/update/")) {
                        int id;
                        if (int.TryParse(request.Url.LocalPath.Substring("/users/update/".Length), out id)) {
                            foreach (var item in users) {
                                if (item.Id == id) {
                                    Console.WriteLine("What do you want to update? Username, Email");
                                    string updateString = Console.ReadLine();

                                    switch(updateString) {

                                        case "Username":
                                            Console.WriteLine("Enter the new value");
                                            item.Username = Console.ReadLine();
                                            break;
                                        case "Email":
                                            Console.WriteLine("Enter the new value");
                                            item.Email = Console.ReadLine();
                                            break;
                                        default:
                                            Console.WriteLine("It doesn't exit!");
                                            break;
                                    }
                                }
                            }
                        } 
                    }
                    break;

                case "GET":
                    // Get Article
                    if (request.Url.LocalPath.StartsWith("/articles/")) {
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

                            
                    // Get articles
                    else if (request.Url.LocalPath == "/articles") {
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

                    // Get User
                    if (request.Url.LocalPath.StartsWith("/users/")) {
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

                    // Get users
                    else if (request.Url.LocalPath == "/users") {
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
                    break;

                default:
                    responseString = "Endpoint non pris en charge";
                    break;

            }

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }   
}
