using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DesktopApp.APIInteraction
{
    public class APIClient
    {
        public static HttpClient Client { get; set; }
        public static void InitializeClient(string baseUri)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseUri);
            Client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes("Admin:Cities_Pass")));
        }
    }
}
