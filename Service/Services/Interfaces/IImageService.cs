using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IImageService
    {
        IEnumerable<Image> GetImage();
        Image GetImage(Guid id);
        void StoreImage(Image img);
    }
}
