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
                    int deleteId;
                    if (request.Url.LocalPath.StartsWith("/articles/delete")) {
                        Console.WriteLine("Enter the id of the products you want to delete");
                        deleteId = int.Parse(Console.ReadLine());
                        DELETE.deleteArticle(db, deleteId, ref responseString);
                    }
                    // Delete User
                    if (request.Url.LocalPath.StartsWith("/users/delete")) {
                        Console.WriteLine("Enter the id of the products you want to delete");
                        deleteId = int.Parse(Console.ReadLine());
                        DELETE.deleteUser(db, deleteId, ref responseString);
                    }
                    break;
                
                case "PUT":
                    // Update Article
                    if (request.Url.LocalPath.StartsWith("/articles/update/")) {
                        UPDATE.updateArticle(db, request);
                    }
                    
                    // Update User
                    if (request.Url.LocalPath.StartsWith("/users/update/")) {
                        UPDATE.updateUser(db, request);
                    }
                    break;

                case "GET":
                    // Get Article
                    if (request.Url.LocalPath.StartsWith("/articles/")) {
                        GET.getArticle(db, ref responseString, request);
                    }

                    // Get all articles
                    else if (request.Url.LocalPath == "/articles") {
                        GET.getAllArticles(db, ref responseString);
                    }

                    // Get User
                    if (request.Url.LocalPath.StartsWith("/users/")) {
                        GET.getUser(db, ref responseString, request);
                    }

                    // Get all users
                    else if (request.Url.LocalPath == "/users") {
                        GET.getAllUsers(db, ref responseString);
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
