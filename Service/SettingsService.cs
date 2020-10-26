using DataAccess.Models;
using Repository;
using Repository.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;
        private readonly CityRouteContext _context;

        public SettingsService(ISettingsRepository repository, CityRouteContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void CreateSettings(Settings settings)
        {
            _repository.Add(settings);
            _context.SaveChanges();
        }

        public bool DeleteSettings(Guid id)
        {
            bool flag = _repository.Delete(id);
            if (flag)
                _context.SaveChanges();
            return flag;
        }

        public Map GetMap(Guid id)
        {
            return _repository.GetMap(id);
        }

        public IEnumerable<Settings> GetSettings()
        {
            return _repository.GetAll();
        }

        public Settings GetSettings(Guid id)
        {
            return _repository.Get(id);
        }

        public Settings UpdateSettings(Settings settings)
        {
            return _repository.Update(settings);
        }
    }
}
