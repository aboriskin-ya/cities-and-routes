using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class ImageAPIService : IImageAPIService
    {
        public async Task<HttpResponsePayload<Guid>> UploadImage(string path)
        {
            var content = new MultipartFormDataContent();

            FileStream fs = File.OpenRead(path);
            var streamContent = new StreamContent(fs);

            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            provider.TryGetContentType(path, out contentType);

            streamContent.Headers.Add("Content-Type", contentType);
            streamContent.Headers.Add("Content-Disposition", "form-data; name=\"file\"; filename=\"" + System.IO.Path.GetFileName(path) + "\"");

            content.Add(streamContent, "file", System.IO.Path.GetFullPath(path));

            HttpResponseMessage response = await APIClient.Client.PostAsync("api/image/upload", content);

            var responsePayload = new HttpResponsePayload<Guid>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false,
                Payload = await response.Content.ReadAsAsync<Guid>()
            };
            return responsePayload;
        }

        public async Task<byte[]> GetImageAsync(Guid guid)
        {
            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.GetAsync($"api/image/{guid}");
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }

            var image = await response.Content.ReadAsStreamAsync();

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                await image.CopyToAsync(ms);
                buffer = ms.ToArray();
            }

            return buffer;
        }
    }
}