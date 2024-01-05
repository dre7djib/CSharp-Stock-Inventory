using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;


namespace Api {
    public class RequestHandler {
        public static void ProcessRequest(HttpListenerContext context, List<Article> articles ) {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string responseString = "";



            // Méthode CRUD 
            // Add Article
            if (request.Url.LocalPath == "/articles/add" && request.HttpMethod == "POST"){
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
                A1.Quantity = Console.ReadLine();

                articles.Add(A1);
            }
            
            // Delete Article
            if (request.Url.LocalPath == "/articles/delete" && request.HttpMethod == "DELETE") {
                Console.WriteLine("Enter the id of the products you want to delete");
                int deleteId = int.Parse(Console.ReadLine());
                articles.RemoveAt(deleteId - 1);
            }
        
            // Update Article

            // Get Article
            if (request.Url.LocalPath.StartsWith("/articles/") && request.HttpMethod == "GET") {
                int id;
                if (int.TryParse(request.Url.LocalPath.Substring("/articles/".Length), out id)) { 
                    foreach (var item in articles) {
                        if (item.Id == id){
                            responseString = JsonSerializer.Serialize(item);
                        }
                        else {
                            responseString = "Endpoint non pris en charge";
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                        }
                    }
                }

            }

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }   
}
