using System.IO;
using System.Net;
using System.Text;

namespace Api
{
    public class RequestHandler
    {
        public static void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string responseString = "";

            if (request.Url.LocalPath == "/articles" && request.HttpMethod == "GET")
            {
                responseString = "Liste des articles";
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
