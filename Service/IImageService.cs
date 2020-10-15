using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IImageService
    {
        IEnumerable<MapImage> GetImage();
        MapImage GetImage(int id);
        void CreateUpdate(MapImage img);
    }
}
