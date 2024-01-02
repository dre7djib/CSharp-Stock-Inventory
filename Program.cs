using System;
using System.Net;

namespace Api
{
    public class SimpleApi
    {
        static void Main()
        {
            string url = "http://localhost:8080/";

            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(url);
                listener.Start();
                Console.WriteLine($"API en cours d'exécution sur {url}");

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    RequestHandler.ProcessRequest(context);
                }
            }
        }
    }
}
