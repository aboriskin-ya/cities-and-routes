using DataAccess.Models;
using Repository.Storages;
using System;
using System.Collections.Generic;

namespace Service
{
    public class ImageService : IImageService
    {
        private IRepository<MapImage> _repository;

        public ImageService(IRepository<MapImage> repository)
        {
            _repository = repository;
        }

        public void CreateUpdate(MapImage img)
        {
            _repository.CreateUpdate(img);
        }

        public IEnumerable<MapImage> GetImage()
        {
            return _repository.GetAll();
        }

        public MapImage GetImage(int id)
        {
            return _repository.Get(id);
        }
    }
}
