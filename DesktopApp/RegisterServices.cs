﻿using Autofac;
using DesktopApp.Service;
using DesktopApp.APIInteraction;
using DesktopApp.ViewModels;
using DesktopApp.Dialogs;

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

            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<CreateMapViewModel>().AsSelf();
            builder.RegisterType<CreateMapDialog>().AsSelf();

            return builder.Build();
        }
    }
}
