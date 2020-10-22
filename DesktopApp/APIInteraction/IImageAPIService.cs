using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface IImageAPIService
    {
        Task<string> UploadImage(string path);
    }
}
