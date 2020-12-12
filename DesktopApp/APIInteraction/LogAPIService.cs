using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    internal class LogAPIService
    {
        public static async Task<HttpResponsePayload<Guid>> LoggingExceptions(string type, string mess, string stacktrace)
        {
            HttpResponseMessage response;
            try
            {
                response = await APIClient.Client.GetAsync($"api/log/{type}/{mess}/{stacktrace}");
            }
            catch
            {
                return new HttpResponsePayload<Guid>() { IsSuccessful = false };
            }
            var responsePayload = new HttpResponsePayload<Guid>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            responsePayload.Payload = await response.Content.ReadAsAsync<Guid>();

            return responsePayload;
        }
    }
}
