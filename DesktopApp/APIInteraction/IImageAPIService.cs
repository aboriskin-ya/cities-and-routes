using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface IImageAPIService
    {
        Task<string> UploadImage(string path);
    }
}
