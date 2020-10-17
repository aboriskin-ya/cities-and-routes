using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IImageService
    {
        IEnumerable<MapImage> GetImage();
        MapImage GetImage(Guid id);
        void StoreImage(MapImage img);
    }
}
