using DataAccess.Models;
using Repository.Storage;
using Repository;
using System;
using System.Collections.Generic;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        protected readonly CityRouteContext _context;

        public ImageService(IImageRepository repository, CityRouteContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void StoreImage(Image img)
        {
            _repository.Add(img);
            _context.SaveChanges();
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
