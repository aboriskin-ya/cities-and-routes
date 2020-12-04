using Autofac;
using DesktopApp.APIInteraction;
using DesktopApp.Dialogs;
using DesktopApp.Services;
using DesktopApp.ViewModels;
using Prism.Events;

namespace DesktopApp
{
    public class RegisterServices
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ImageAPIService>().As<IImageAPIService>();
            builder.RegisterType<MapAPIService>().As<IMapAPIService>();
            builder.RegisterType<CityAPIService>().As<ICityAPIService>();
            builder.RegisterType<RouteAPIService>().As<IRouteAPIService>();
            builder.RegisterType<SettingsAPIService>().As<ISettingsAPIService>();
            builder.RegisterType<PathResolverAPIService>().As<IPathResolverAPIService>();
            builder.RegisterType<TravelSalesmanViewModel>().As<ITravelSalesmanViewModel>();
            builder.RegisterType<MapViewModel>().As<IMapViewModel>();
            builder.RegisterType<ShortestPathViewModel>().As<IShortestPathViewModel>();
            builder.RegisterType<CursorPositionViewModel>().As<ICursorPositionViewModel>();
            builder.RegisterType<CityAPIService>().As<ICityAPIService>();
            builder.RegisterType<RouteAPIService>().As<IRouteAPIService>();
            builder.RegisterType<TravelSalesmanService>().As<ITravelSalesmanService>();
            builder.RegisterType<OpenImageDialogService>().As<IOpenImageDialogService>();
            builder.RegisterType<MessageBoxService>().As<IMessageBoxService>();
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<CreateMapViewModel>().AsSelf();
            builder.RegisterType<CreateMapDialog>().AsSelf();
            builder.RegisterType<SelectExistingMapViewModel>().AsSelf();
            builder.RegisterType<SelectExistingMapDialog>().AsSelf();
            builder.RegisterType<SettingsDialog>().AsSelf();
            builder.RegisterType<SettingsViewModel>().AsSelf();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            return builder.Build();
        }
    }
}