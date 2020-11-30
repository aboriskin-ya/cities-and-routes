using DesktopApp.Models;
using Prism.Events;

namespace DesktopApp.Services.EventAggregator
{
    public class SettingsSentEvent : PubSubEvent<Settings>
    {
    }
}