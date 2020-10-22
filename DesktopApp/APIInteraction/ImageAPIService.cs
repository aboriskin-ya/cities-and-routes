using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class ImageAPIService: IImageAPIService
    {
        public async Task<string> UploadImage(string path)
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

            HttpResponseMessage response = await APIClient.Client.PostAsync("image/upload", content);

            string data = null;
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            return data;
        }

    }
}

