using System;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface IImageAPIService
    {
        Task<HttpResponsePayload<Guid>> UploadImage(string path);

        Task<byte[]> GetImageAsync(Guid guid);
    }
}