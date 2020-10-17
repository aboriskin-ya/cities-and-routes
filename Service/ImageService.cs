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

        public void StoreImage(MapImage img)
        {
            _repository.Add(img);
        }

        public IEnumerable<MapImage> GetImage()
        {
            return _repository.GetAll();
        }

        public MapImage GetImage(Guid id)
        {
            return _repository.Get(id);
        }
    }
}
