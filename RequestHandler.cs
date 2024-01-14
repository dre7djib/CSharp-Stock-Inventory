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
                        // ID
                        Console.WriteLine("Enter an Id for your product: ");
                        string tempId = Console.ReadLine();
                        A1.Id = int.Parse(tempId);
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
                        articles.Add(A1);
                        var sql = "INSERT INTO api_csharp.articles (id, title, description, quantity) VALUES (@id, @title, @description, @quantity)";
                        var cmd = new MySqlCommand(sql, db.connection);
                        cmd.Parameters.AddWithValue("@id", A1.Id);
                        cmd.Parameters.AddWithValue("@title", A1.Title);
                        cmd.Parameters.AddWithValue("@description", A1.Description);
                        cmd.Parameters.AddWithValue("@quantity", A1.Quantity);
                        cmd.ExecuteNonQuery();
                        
                    }
                        //var cmd = new MySqlCommand(sql, db.connection);
                        //var version = cmd.ExecuteScalar().ToString();
                        //Console.WriteLine($"MySQL version: {version}");
                        
                    // Add User
                    if (request.Url.LocalPath == "/users/add") {
                        User U1 = new User();
                        // ID
                        Console.WriteLine("Enter an Id for the user: ");
                        string tempId = Console.ReadLine();
                        U1.Id = int.Parse(tempId);
                        // Username
                        Console.WriteLine("Enter a Username for the user: ");
                        U1.Username = Console.ReadLine();
                        // Email
                        Console.WriteLine("Enter a email for the user: ");
                        U1.Email = Console.ReadLine();

                        users.Add(U1);
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
                            foreach (Api.Article article in  articles) {
                                if (article.Id == id){
                                    responseString = JsonSerializer.Serialize(article);
                                    break;
                                }
                            }
                        }
                    }
                    // Get articles
                    else if (request.Url.LocalPath == "/articles") {
                        responseString = JsonSerializer.Serialize(articles);
                    }

                    // Get User
                    if (request.Url.LocalPath.StartsWith("/users/")) {
                        int id;
                        if (int.TryParse(request.Url.LocalPath.Substring("/users/".Length), out id)) { 
                            foreach (Api.User user in  users) {
                                if (user.Id == id){
                                    responseString = JsonSerializer.Serialize(user);
                                    break;
                                }
                            }
                        }
                    }
                    // Get users
                    else if (request.Url.LocalPath == "/users") {
                        responseString = JsonSerializer.Serialize(users);
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
