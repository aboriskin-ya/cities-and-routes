using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    internal class SettingsAPIService : ISettingsAPIService
    {
        public async Task<HttpResponsePayload<Settings>> UpdateSettingsAsync(Settings settings)
        {
            var settingsDTO = AppMapper.GetAppMapper().Mapper.Map<SettingsUpdateDTO>(settings);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PutAsJsonAsync($"api/settings/{settings.Id}", settingsDTO);
            }
            catch
            {
                return new HttpResponsePayload<Settings>() { IsSuccessful = false };
            }

            var responsePayload = new HttpResponsePayload<Settings>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var settingsGetDTO = await response.Content.ReadAsAsync<SettingsGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<Settings>(settingsGetDTO);

            return responsePayload;
        }

        public async Task<HttpResponsePayload<Settings>> CreateSettingsAsync(Settings settings)
        {
            var settingsDTO = AppMapper.GetAppMapper().Mapper.Map<SettingsCreateDTO>(settings);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PostAsJsonAsync($"api/settings", settingsDTO);
            }
            catch
            {
                return new HttpResponsePayload<Settings>() { IsSuccessful = false };
            }

            var responsePayload = new HttpResponsePayload<Settings>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var settingsGetDTO = await response.Content.ReadAsAsync<SettingsGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<Settings>(settingsGetDTO);

            return responsePayload;
        }
    }
}