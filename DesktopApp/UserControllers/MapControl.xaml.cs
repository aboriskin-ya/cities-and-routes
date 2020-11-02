﻿using DesktopApp.Models;
using DesktopApp.Services.Helper;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopApp.UserControllers
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

            SetBinding(ZoomCommandProperty, new Binding("ZoomCommand"));
            SetBinding(NavigateCommandProperty, new Binding("NavigateCommand"));
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
            else {

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

            PPW = ImageWidth / ActualWidth;
            PPH = ImageHeight / ActualHeight;
            _RelativeOffsetValue = e.GetPosition(this) - new Point();
            this.MouseMove += MapControl_MouseMove;
            this.LostMouseCapture += MapControl_LostMouseCapture;
            this.MouseUp += MapControl_MouseUp;
            Mouse.Capture(this);
            this.Cursor = Cursors.Hand;
            
            if(IsAbleToSetCity)
                MapControl_SetCity();
        }
        private void MapControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(e);
        }
        private void UpdatePosition(MouseEventArgs e)
        {
            NavigateCommand.Execute(e.GetPosition(this) - _RelativeOffsetValue);
        }
        private void FinishDrag(MouseEventArgs e)
        {
            UpdatePosition(e);
            TransformPosition = MapHelper.GetRelativeCurrentPosit1ion(OffsetValue, ActualHeight, ActualWidth, ScaleValue, out _RelativeTransformPosition);
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
            DependencyProperty.Register("OffsetValue", typeof(Point), typeof(MapControl), new PropertyMetadata(new Point(0,0)));
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

        public ObservableCollection<City> CityCollection
        {
            get { return (ObservableCollection<City>)GetValue(CityCollectionProperty); }
            set { SetValue(CityCollectionProperty, value); }
        }

        public static readonly DependencyProperty CityCollectionProperty =
        DependencyProperty.Register(nameof(CityCollection), typeof(ObservableCollection<City>), typeof(MapControl));

        public City SelectedCity
        {
            get { return (City)GetValue(SelectedCityProperty); }
            set { SetValue(SelectedCityProperty, value); }
        }

        public static readonly DependencyProperty SelectedCityProperty =
            DependencyProperty.Register(nameof(SelectedCity), typeof(City), typeof(MapControl));

        public Settings SettingsMap
        {
            get { return (Settings)GetValue(SettingsMapProperty); }
            set { SetValue(SettingsMapProperty, value); }
        }

        public static readonly DependencyProperty SettingsMapProperty =
            DependencyProperty.Register(nameof(SettingsMap), typeof(Settings), typeof(MapControl));

        #region IsAbleToCreateCity

        public bool IsAbleToCreateCity
        {
            get { return (bool)GetValue(IsAbleToCreateCityProperty); }
            set { SetValue(IsAbleToCreateCityProperty, value); }
        }

        public static readonly DependencyProperty IsAbleToCreateCityProperty =
            DependencyProperty.Register(nameof(IsAbleToCreateCity), typeof(bool), typeof(MapControl));

        #endregion

        #region IsAbleToSetCity
        public bool IsAbleToSetCity
        {
            get { return (bool)GetValue(IsAbleToSetCityProperty); }
            set { SetValue(IsAbleToSetCityProperty, value); }
        }

        public static readonly DependencyProperty IsAbleToSetCityProperty =
            DependencyProperty.Register(nameof(IsAbleToSetCity), typeof(bool), typeof(MapControl));

        #endregion
        
        private void MapControl_SetCity()
        {
            SelectedCity = new City() 
            { 
                X = PosX,// p.X - SettingsMap.VertexSize / 2,
                Y = PosY
            };

            IsAbleToCreateCity = true;
            IsAbleToSetCity = false;
        }

    }
}
