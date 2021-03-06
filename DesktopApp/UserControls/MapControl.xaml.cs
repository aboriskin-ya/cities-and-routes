﻿using DesktopApp.Models;
using DesktopApp.Resources;
using DesktopApp.Services.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopApp.UserControls
{
    public partial class MapControl : UserControl
    {
        #region fields
        private Vector _RelativeTransformPosition;
        private Vector _RelativeOffsetValue;
        #endregion
        public MapControl()
        {
            InitializeComponent();
            TransformPosition = new Point(0.5, 0.5);
            _RelativeTransformPosition = new Vector();
            _RelativeTransformPosition.X += TransformPosition.X;
            _RelativeTransformPosition.Y += TransformPosition.Y;
            this.MouseDown += MapControl_MouseDown;
            this.MouseWheel += MapControl_MouseWheel;
            this.SizeChanged += MapControl_WindowResize;

            SetBinding(ZoomCommandProperty, new Binding("ZoomCommand"));
            SetBinding(NavigateCommandProperty, new Binding("NavigateCommand"));
            SetBinding(SelectCityForTravelSalesmanCommandProperty, new Binding("SelectCityForTravelSalesmanCommand"));
            SetBinding(SelectCityForShortestPathCommandProperty, new Binding("SelectCityForShortestPathCommand"));
        }

        private void MapControl_WindowResize(object sender, System.EventArgs e)
        {
            if (InitialHeight == 0)
            {
                InitialHeight = mControl.ActualHeight;
                InitialWidth = mControl.ActualWidth;
            }
        }
        private void MapControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (ScaleValue >= 1 && ScaleValue < 16)
                {
                    ZoomCommand.Execute(2);
                }
            }
            else
            {

                if (ScaleValue > 1 && ScaleValue <= 16)
                {
                    ZoomCommand.Execute(0.5);
                }
            }
            PPW = ImageWidth / ActualWidth;
            PPH = ImageHeight / ActualHeight;
        }

        #region DragActions
        private void MapControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _RelativeOffsetValue = e.GetPosition(this) - new Point();
            this.MouseMove += MapControl_MouseMove;
            this.LostMouseCapture += MapControl_LostMouseCapture;
            this.MouseUp += MapControl_MouseUp;
            Mouse.Capture(this);
            this.Cursor = Cursors.Hand;

            if (AppState.IsAbleToSetCity)
                MapControl_SetCity();
        }
        private void MapControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(e);
        }
        private void UpdatePosition(MouseEventArgs e)
        {
            var transient = e.GetPosition(this) - _RelativeOffsetValue;
            PPW = ImageWidth / ActualWidth;
            PPH = ImageHeight / ActualHeight;
            NavigateCommand.Execute(MapHelper.GetOffsetValue(PPW, PPH, Offset, transient));
        }
        private void FinishDrag(MouseEventArgs e)
        {
            UpdatePosition(e);
            TransformPosition = MapHelper.GetRelativeCurrentPosit1ion(OffsetValue, ActualHeight, ActualWidth, ScaleValue, out _RelativeTransformPosition);
            Offset = MapHelper.GetOffsetAfterDrag(PPW, PPH, Offset, OffsetValue);
            OffsetValue = default;
            this.MouseMove -= MapControl_MouseMove;
            this.LostMouseCapture -= MapControl_LostMouseCapture;
            this.MouseUp -= MapControl_MouseUp;
            Mouse.Capture(null);
            this.Cursor = null;
        }
        private void MapControl_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null);
            FinishDrag(e);
        }
        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            UpdatePosition(e);
        }
        #endregion

        #region InitialHeight
        public double InitialHeight
        {
            get { return (double)GetValue(InitialHeightProperty); }
            set { SetValue(InitialHeightProperty, value); }
        }

        public static readonly DependencyProperty InitialHeightProperty =
            DependencyProperty.Register("InitialHeight", typeof(double), typeof(MapControl));
        #endregion

        #region InitialWidth
        public double InitialWidth
        {
            get { return (double)GetValue(InitialWidthProperty); }
            set { SetValue(InitialWidthProperty, value); }
        }

        public static readonly DependencyProperty InitialWidthProperty =
            DependencyProperty.Register("InitialWidth", typeof(double), typeof(MapControl));
        #endregion

        #region ImageSource
        public ImageSource MapImageSource
        {
            get { return (ImageSource)GetValue(MapImageSourceProperty); }
            set { SetValue(MapImageSourceProperty, value); }
        }

        public static readonly DependencyProperty MapImageSourceProperty =
            DependencyProperty.Register("MapImageSource", typeof(ImageSource), typeof(MapControl));
        #endregion

        #region ScaleValue
        public double ScaleValue
        {
            get { return (double)GetValue(ScaleValueProperty); }
            set { SetValue(ScaleValueProperty, value); }
        }

        public static readonly DependencyProperty ScaleValueProperty =
            DependencyProperty.Register("ScaleValue", typeof(double), typeof(MapControl), new PropertyMetadata(0.0));
        #endregion

        #region OffsetValue
        public Point OffsetValue
        {
            get { return (Point)GetValue(OffsetValueProperty); }
            set { SetValue(OffsetValueProperty, value); }
        }

        public static readonly DependencyProperty OffsetValueProperty =
            DependencyProperty.Register("OffsetValue", typeof(Point), typeof(MapControl), new PropertyMetadata(new Point(0, 0)));
        #endregion

        #region Offset


        public Offset Offset
        {
            get { return (Offset)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(Offset), typeof(MapControl));


        #endregion

        #region ImageHeight


        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(MapControl));


        #endregion

        #region ImageWidth


        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(MapControl));


        #endregion

        #region TransformPosition
        public Point TransformPosition
        {
            get { return (Point)GetValue(TransformPositionProperty); }
            set { SetValue(TransformPositionProperty, value); }
        }

        public static readonly DependencyProperty TransformPositionProperty =
            DependencyProperty.Register("TransformPosition", typeof(Point), typeof(MapControl));
        #endregion

        #region NavigateCommand
        public ICommand NavigateCommand
        {
            get { return (ICommand)GetValue(NavigateCommandProperty); }
            set { SetValue(NavigateCommandProperty, value); }
        }


        public static readonly DependencyProperty NavigateCommandProperty =
            DependencyProperty.Register("NavigateCommand", typeof(ICommand), typeof(MapControl));
        #endregion

        #region ZoomCommand

        public ICommand ZoomCommand
        {
            get { return (ICommand)GetValue(ZoomCommandProperty); }
            set { SetValue(ZoomCommandProperty, value); }
        }

        public static readonly DependencyProperty ZoomCommandProperty =
            DependencyProperty.Register("ZoomCommand", typeof(ICommand), typeof(MapControl));


        #endregion

        #region SelectCityCommand
        public ICommand SelectCityForTravelSalesmanCommand
        {
            get { return (ICommand)GetValue(SelectCityForTravelSalesmanCommandProperty); }
            set { SetValue(SelectCityForTravelSalesmanCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectCityForTravelSalesmanCommandProperty =
            DependencyProperty.Register(nameof(SelectCityForTravelSalesmanCommand), typeof(ICommand), typeof(MapControl));

        public ICommand SelectCityForShortestPathCommand
        {
            get { return (ICommand)GetValue(SelectCityForShortestPathCommandProperty); }
            set { SetValue(SelectCityForShortestPathCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectCityForShortestPathCommandProperty =
            DependencyProperty.Register(nameof(SelectCityForShortestPathCommand), typeof(ICommand), typeof(MapControl));

        #endregion        

        #region PixelPerWidth


        public double PPW
        {
            get { return (double)GetValue(PPWProperty); }
            set { SetValue(PPWProperty, value); }
        }

        public static readonly DependencyProperty PPWProperty =
            DependencyProperty.Register("PPW", typeof(double), typeof(MapControl));


        #endregion

        #region PixelPerHeight


        public double PPH
        {
            get { return (double)GetValue(PPHProperty); }
            set { SetValue(PPHProperty, value); }
        }

        public static readonly DependencyProperty PPHProperty =
            DependencyProperty.Register("PPH", typeof(double), typeof(MapControl));


        #endregion

        #region CursorPosition

        public double PosX
        {
            get { return (double)GetValue(PosXProperty); }
            set { SetValue(PosXProperty, value); }
        }

        public static readonly DependencyProperty PosXProperty =
            DependencyProperty.Register(nameof(PosX), typeof(double), typeof(MapControl));


        public double PosY
        {
            get { return (double)GetValue(PosYProperty); }
            set { SetValue(PosYProperty, value); }
        }

        public static readonly DependencyProperty PosYProperty =
            DependencyProperty.Register(nameof(PosY), typeof(double), typeof(MapControl));


        #endregion

        #region CityProperties

        public ObservableCollection<City> CityCollection
        {
            get { return (ObservableCollection<City>)GetValue(CityCollectionProperty); }
            set { SetValue(CityCollectionProperty, value); }
        }


        public City HighlightedCity
        {
            get { return (City)GetValue(HighlightedCityProperty); }
            set { SetValue(HighlightedCityProperty, value); }
        }

        public static readonly DependencyProperty HighlightedCityProperty =
            DependencyProperty.Register("HighlightedCity", typeof(City), typeof(MapControl));



        public ObservableCollection<City> SelectedCities
        {
            get { return (ObservableCollection<City>)GetValue(SelectedCitiesProperty); }
            set { SetValue(SelectedCitiesProperty, value); }
        }
        public static readonly DependencyProperty SelectedCitiesProperty =
            DependencyProperty.Register("SelectedCities", typeof(ObservableCollection<City>), typeof(MapControl));




        public static readonly DependencyProperty CityCollectionProperty =
        DependencyProperty.Register(nameof(CityCollection), typeof(ObservableCollection<City>), typeof(MapControl));

        public City SelectedCity
        {
            get { return (City)GetValue(SelectedCityProperty); }
            set { SetValue(SelectedCityProperty, value); }
        }

        public static readonly DependencyProperty SelectedCityProperty =
            DependencyProperty.Register(nameof(SelectedCity), typeof(City), typeof(MapControl));


        #endregion

        #region RouteProperties

        public ObservableCollection<Route> RouteCollection
        {
            get { return (ObservableCollection<Route>)GetValue(RouteCollectionProperty); }
            set { SetValue(RouteCollectionProperty, value); }
        }

        public static readonly DependencyProperty RouteCollectionProperty =
        DependencyProperty.Register(nameof(RouteCollection), typeof(ObservableCollection<Route>), typeof(MapControl));

        public Route SelectedRoute
        {
            get { return (Route)GetValue(SelectedRouteProperty); }
            set { SetValue(SelectedRouteProperty, value); }
        }

        public static readonly DependencyProperty SelectedRouteProperty =
            DependencyProperty.Register(nameof(SelectedRoute), typeof(Route), typeof(MapControl));



        public ObservableCollection<Route> SelectedRoutes
        {
            get { return (ObservableCollection<Route>)GetValue(SelectedRoutesProperty); }
            set { SetValue(SelectedRoutesProperty, value); }
        }

        public static readonly DependencyProperty SelectedRoutesProperty =
            DependencyProperty.Register("SelectedRoutes", typeof(ObservableCollection<Route>), typeof(MapControl));
        #endregion

        #region CanDisplay


        public bool CanDisplay
        {
            get { return (bool)GetValue(CanDisplayProperty); }
            set { SetValue(CanDisplayProperty, value); }
        }

        public static readonly DependencyProperty CanDisplayProperty =
            DependencyProperty.Register("CanDisplay", typeof(bool), typeof(MapControl));


        #endregion

        #region PathProperties

        public PathModel Path
        {
            get { return (PathModel)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty =
        DependencyProperty.Register(nameof(Path), typeof(PathModel), typeof(MapControl));

        public List<Point> CitiesPositionOfPath
        {
            get { return (List<Point>)GetValue(CitiesPositionOfPathProperty); }
            set { SetValue(CitiesPositionOfPathProperty, value); }
        }

        public static readonly DependencyProperty CitiesPositionOfPathProperty =
        DependencyProperty.Register(nameof(CitiesPositionOfPath), typeof(List<Point>), typeof(MapControl));

        #endregion

        #region SettingsMap

        public Settings SettingsMap
        {
            get { return (Settings)GetValue(SettingsMapProperty); }
            set { SetValue(SettingsMapProperty, value); }
        }

        public static readonly DependencyProperty SettingsMapProperty =
            DependencyProperty.Register(nameof(SettingsMap), typeof(Settings), typeof(MapControl));

        #endregion

        #region ApplicationState

        public States AppState
        {
            get { return (States)GetValue(AppStateProperty); }
            set { SetValue(AppStateProperty, value); }
        }

        public static readonly DependencyProperty AppStateProperty =
            DependencyProperty.Register(nameof(AppState), typeof(States), typeof(MapControl));

        #endregion

        #region SetCityRoute
        private void MapControl_SetCity()
        {
            var NexPosX = PosX / ActualWidth * InitialWidth;
            var NewPosY = PosY / ActualHeight * InitialHeight;
            SelectedCity = new City()
            {
                X = NexPosX,
                Y = NewPosY
            };
            AppState.IsAbleToCreateCity = true;
            var cityPanel = GetGeneralParent().FindName("cityName") as TextBox;
            if (cityPanel != null)
            {
                Keyboard.Focus(cityPanel);
            }
            AppState.IsAbleToSetCity = false;
        }

        private void MapControl_SetCityToRoute(City city)
        {
            if (!AppState.IsAbleToPickSecondCity)
            {
                SelectedRoute = new Route()
                {
                    FirstCity = city
                };
                AppState.IsAbleToPickSecondCity = true;
            }
            else
            {
                Guid[] SelectedCities = { SelectedRoute.FirstCity.Id, city.Id };
                foreach (var Route in RouteCollection)
                {
                    if (SelectedCities.Contains(Route.FirstCity.Id) && SelectedCities.Contains(Route.SecondCity.Id))
                    {
                        return;
                    }
                }
                SelectedRoute.SecondCity = city;
                AppState.IsAbleToCreateRoute = true;
                var routePanel = GetGeneralParent().FindName("routeDistance") as TextBox;
                if (routePanel != null)
                {
                    Keyboard.Focus(routePanel);
                }
                AppState.IsAbleToPickSecondCity = false;
                AppState.IsAbleToUpdateCity = false;
                AppState.State = "Road: " + SelectedRoute.FirstCity.Name + " -> " + SelectedRoute.SecondCity.Name;
            }
        }

        private void Map_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                AppState.IsAbleToCreateRoute = false;
                AppState.IsAbleToUpdateRoute = false;
                AppState.IsAbleToCreateCity = false;
                AppState.IsAbleToUpdateCity = false;
                AppState.IsAbleToPickSecondCity = false;
                AppState.IsAbleToPickFirstCity = false;
                SelectedCity = new City();
                SelectedRoute = new Route();
                Path = new PathModel();
            }
        }

        private void City_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Panel panel = sender as Panel;
            var City = panel.DataContext;
            if (AppState.CanSelectedCitiesForPath)
            {
                SelectCityForTravelSalesmanCommand.Execute(City);
                return;
            }

            var city = City as City;
            if (AppState.IsAbleToPickShortestPath)
            {
                if (Path.CityFromId == default)
                {
                    Path.CityFromId = city.Id;
                    Path.CityFromName = city.Name;
                }
                else
                {
                    Path.CityToId = city.Id;
                    Path.CityToName = city.Name;
                    AppState.IsAbleToFindShortestPath = true;
                }
                SelectCityForShortestPathCommand.Execute(city.Name);
                return;
            }

            if (!AppState.IsAbleToSetCity && !AppState.IsAbleToPickFirstCity && !AppState.IsAbleToCreateCity && !AppState.IsAbleToCreateRoute)
            {
                AppState.IsAbleToUpdateCity = true;
                var updateCityPanel = GetGeneralParent().FindName("cityNameUpdate") as TextBox;
                if (updateCityPanel != null)
                {
                    Keyboard.Focus(updateCityPanel);
                }
                AppState.IsAbleToUpdateRoute = false;
                SelectedCity = city;
            }

            if (!AppState.IsAbleToPickFirstCity)
                return;

            MapControl_SetCityToRoute(city);
        }

        private void Route_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Panel panel = sender as Panel;
            var Route = panel.DataContext;
            if (!AppState.IsAbleToCreateCity && !AppState.IsAbleToCreateRoute)
            {
                AppState.IsAbleToUpdateRoute = true;
                var updateRoutePanel = GetGeneralParent().FindName("routeUpdate") as TextBox;
                if (updateRoutePanel != null)
                {
                    Keyboard.Focus(updateRoutePanel);
                }
                AppState.IsAbleToUpdateCity = false;
                SelectedRoute = Route as Route;
                AppState.State = "Road: " + SelectedRoute.FirstCity.Name + " -> " + SelectedRoute.SecondCity.Name;
            }
        }
        private FrameworkElement GetGeneralParent()
        {
            var dockPanel = Parent as FrameworkElement;
            return dockPanel.Parent as FrameworkElement;
        }
        #endregion
    }
}