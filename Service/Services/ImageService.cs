using DataAccess.Models;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Storage;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;
        private readonly IImageRepository _repository;
        protected readonly CityRouteContext _context;

        public ImageService(IImageRepository repository, CityRouteContext context, ILogger<ImageService> logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public void StoreImage(Image img)
        {
            _logger.LogInformation("Image upload started");
            _repository.Add(img);
            _context.SaveChanges();
            _logger.LogInformation("Image upload finished");
        }

        public IEnumerable<Image> GetImages()
        {
            _logger.LogInformation("All images get started");
            return _repository.GetAll();
        }

        public Image GetImage(Guid id)
        {
            _logger.LogInformation("Image get started");
            return _repository.Get(id);
        }
    }
}
