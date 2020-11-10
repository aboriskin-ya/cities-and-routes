using AutoMapper;
using DataAccess.Models;
using Repository;
using Repository.Storage;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;
        private readonly CityRouteContext _context;
        private readonly IMapper _mapper;
        public SettingsService(ISettingsRepository repository, CityRouteContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public void CreateSettings(SettingsDTO settingsDTO)
        {
            Settings settings = _mapper.Map<Settings>(settingsDTO);
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

        public SettingsDTO GetSettingsOfMap(Guid id)
        {
            return _mapper.Map<Settings, SettingsDTO>(_repository.GetSettingsOfMap(id));
        }

        public IEnumerable<SettingsDTO> GetSettings()
        {
            return _mapper.Map<IEnumerable<Settings>, IEnumerable<SettingsDTO>>(_repository.GetAll());
        }

        public SettingsDTO GetSettings(Guid id)
        {
            return _mapper.Map<Settings, SettingsDTO>(_repository.Get(id));
        }

        public SettingsDTO UpdateSettings(SettingsDTO settingsDTO)
        {
            Settings settings = _mapper.Map<Settings>(settingsDTO);
            _repository.Update(settings);
            return settingsDTO;
        }
    }
}