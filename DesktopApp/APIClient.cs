using System;
using System.Net.Http;

namespace DesktopApp
{
    public class APIClient
    {
        public static HttpClient Client { get; set; }
        public static void InitializeClient(string baseUri)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseUri);
        }
    }
}
