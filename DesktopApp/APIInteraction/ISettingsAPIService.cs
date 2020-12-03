using DesktopApp.Models;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    internal interface ISettingsAPIService
    {
        Task<HttpResponsePayload<Settings>> UpdateSettingsAsync(Settings settings);

        Task<HttpResponsePayload<Settings>> CreateSettingsAsync(Settings settings);
    }
}