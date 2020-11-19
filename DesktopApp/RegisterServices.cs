using Autofac;
using DesktopApp.APIInteraction;
using DesktopApp.Dialogs;
using DesktopApp.Service;
using DesktopApp.ViewModels;

namespace DesktopApp
{
    public class RegisterServices
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MessageBoxService>().As<IMessageBoxService>();
            builder.RegisterType<ImageAPIService>().As<IImageAPIService>();
            builder.RegisterType<MapViewModel>().As<IMapViewModel>();
            builder.RegisterType<CursorPositionViewModel>().As<ICursorPositionViewModel>();
            builder.RegisterType<CityAPIService>().As<ICityAPIService>();
            builder.RegisterType<RouteAPIService>().As<IRouteAPIService>();
            builder.RegisterType<TravelSalesmanService>().As<ITravelSalesmanService>();

            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<CreateMapViewModel>().AsSelf();
            builder.RegisterType<CreateMapDialog>().AsSelf();

            return builder.Build();
        }
    }
}
