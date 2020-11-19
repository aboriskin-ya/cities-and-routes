using Autofac;
using DesktopApp.APIInteraction;
using DesktopApp.Dialogs;
using DesktopApp.Services;
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
            builder.RegisterType<MapAPIService>().As<IMapAPIService>();
            builder.RegisterType<MapViewModel>().As<IMapViewModel>();
            builder.RegisterType<CursorPositionViewModel>().As<ICursorPositionViewModel>();
            builder.RegisterType<CityAPIService>().As<ICityAPIService>();
            builder.RegisterType<RouteAPIService>().As<IRouteAPIService>();
            builder.RegisterType<OpenImageDialogService>().As<IOpenImageDialogService>();

            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<CreateMapViewModel>().AsSelf();
            builder.RegisterType<CreateMapDialog>().AsSelf();
            builder.RegisterType<SelectExistingMapViewModel>().AsSelf();
            builder.RegisterType<SelectExistingMapDialog>().AsSelf();

            return builder.Build();
        }
    }
}