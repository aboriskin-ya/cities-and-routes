using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Service.DTO;

namespace DesktopApp.APIInteraction
{
    internal class LogAPIService
    {
        public static async Task<HttpResponsePayload<Guid>> LoggingExceptions(string type, string message, string stacktrace)
        {
            HttpResponseMessage response;
            var loggingDTO = new LoggingDTO { ExceptionType = type, ExceptionMessage = message, ExceptionStackTrace = stacktrace };
            try
            {
                response = await APIClient.Client.PostAsJsonAsync($"api/log", loggingDTO);
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