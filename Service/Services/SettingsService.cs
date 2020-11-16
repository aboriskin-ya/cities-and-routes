using AutoMapper;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SettingsService> _logger;

        public SettingsService(ISettingsRepository repository, CityRouteContext context, IMapper mapper, ILogger<SettingsService> logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public SettingsDTO CreateSettings(SettingsDTO settingsDTO)
        {
            _logger.LogInformation("Create settings started");
            var settings = _mapper.Map<Settings>(settingsDTO);
            _repository.Add(settings);
            _context.SaveChanges();
            _logger.LogInformation("Create settings finished");
            _mapper.Map(settings, settingsDTO);
            return settingsDTO;
        }

        public bool DeleteSettings(Guid id)
        {
            _logger.LogInformation("Delete settings started");
            bool flag = _repository.Delete(id);
            if (flag)
            {
                _context.SaveChanges();
                _logger.LogInformation("Delete settings finished");
            }
            else
            {
                _logger.LogInformation("Delete settings not finished");
            }
            return flag;
        }

        public SettingsDTO GetSettingsOfMap(Guid id)
        {
            _logger.LogInformation("Get settings of map started");
            return _mapper.Map<Settings, SettingsDTO>(_repository.GetSettingsOfMap(id));
        }

        public IEnumerable<SettingsDTO> GetSettings()
        {
            _logger.LogInformation("Get all settings started");
            return _mapper.Map<IEnumerable<Settings>, IEnumerable<SettingsDTO>>(_repository.GetAll());
        }

        public SettingsDTO GetSettings(Guid id)
        {
            _logger.LogInformation("Get all settings started");
            return _mapper.Map<Settings, SettingsDTO>(_repository.Get(id));
        }

        public SettingsDTO UpdateSettings(Guid Id, SettingsUpdateDTO settingsUpdateDTO)
        {
            _logger.LogInformation("Update settings started");
            var settings = _repository.Get(Id);
            _mapper.Map(settingsUpdateDTO, settings);
            settings = _repository.Update(settings);
            _context.SaveChanges();
            _logger.LogInformation("Get all settings finished");
            return _mapper.Map<SettingsDTO>(settings);
        }
    }
}