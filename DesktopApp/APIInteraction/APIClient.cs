using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DesktopApp.APIInteraction
{
    public class APIClient
    {
        public static HttpClient Client { get; set; }
        public static void InitializeClient(string baseUri, string authType, string authInfo)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseUri);
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(authType, Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authInfo)));
        }
    }
}
