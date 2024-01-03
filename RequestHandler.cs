using System;
using System.Collections.Generic;
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
            if (request.Url.LocalPath == "/articles/add"){
                Article A1 = new Article();
                // ID
                Console.WriteLine("Enter an Id for your product: ");
                A1.Id = Console.ReadLine();
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

            // Update Article

            // Get Article

            if (request.Url.LocalPath == "/articles" && request.HttpMethod == "GET")
            {
                responseString = JsonSerializer.Serialize(articles);
            }
            else if (request.Url.LocalPath == "/users" && request.HttpMethod == "GET")
            {
                responseString = "Liste des utilisateurs";
            }
            else
            {
                responseString = "Endpoint non pris en charge";
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }

    
}
