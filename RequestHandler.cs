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
                string tempQuantity = Console.ReadLine();
                A1.Quantity = int.Parse(tempQuantity);


                articles.Add(A1);
            }
            
            // Delete Article
            if (request.Url.LocalPath == "/articles/delete" && request.HttpMethod == "DELETE") {
                Console.WriteLine("Enter the id of the products you want to delete");
                int deleteId = int.Parse(Console.ReadLine());
                articles.RemoveAt(deleteId - 1);
            }
        
            // Update Article
            if (request.Url.LocalPath.StartsWith("/articles/update/") && request.HttpMethod == "PUT") {
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
