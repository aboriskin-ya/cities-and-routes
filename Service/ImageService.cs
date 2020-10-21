using DataAccess.Models;
using Repository.Storages;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class ImageService : IImageService
    {
        private IImageRepository _repository;
        protected readonly CityRouteContext _context;

        public ImageService(IImageRepository repository, CityRouteContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void StoreImage(Image img)
        {
            _repository.Add(img);
        }

        public IEnumerable<Image> GetImage()
        {
            return _repository.GetAll();
        }

        public Image GetImage(Guid id)
        {
            return _repository.Get(id);
        }
    }
}
