using Accessibility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp.UserControllers
{
    /// <summary>
    /// Логика взаимодействия для MaoControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        private Vector _RelativeCurrentPosition;
        private Vector _RelativeOffsetValue;
        public MapControl()
        {
            InitializeComponent();
            _RelativeCurrentPosition = new Vector();
            _RelativeCurrentPosition.X += CurrentPosition.X;
            _RelativeCurrentPosition.Y += CurrentPosition.Y;
            this.MouseDown += MapControl_MouseDown;
            this.MouseWheel += MapControl_MouseWheel;
            SetBinding(ZoomCommandProperty, new Binding("ZoomCommand"));
            SetBinding(NavigateCommandProperty, new Binding("NavigateCommand"));
        }

        private void MapControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var point = e.GetPosition(this);
            if (_RelativeCurrentPosition.X < e.GetPosition(this).X / ActualHeight )
            {
                _RelativeCurrentPosition.X += e.GetPosition(this).X / ActualHeight ;
            }
            else _RelativeCurrentPosition.X -= e.GetPosition(this).X / ActualHeight ;
            if (_RelativeCurrentPosition.Y < e.GetPosition(this).Y / ActualWidth )
            {
                _RelativeCurrentPosition.Y += e.GetPosition(this).Y / ActualWidth ;
            }
            else _RelativeCurrentPosition.Y -= e.GetPosition(this).Y / ActualWidth ;
            CurrentPosition = Vector.Add(_RelativeCurrentPosition, new Point());
            if (e.Delta > 0)
            {
                if (ScaleValue >= 1 && ScaleValue < 16) ZoomCommand.Execute(2);
                
            }
            else
                if (ScaleValue > 1 && ScaleValue <= 16) ZoomCommand.Execute(0.5);
        }

        private void MapControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _RelativeOffsetValue = e.GetPosition(this) - new Point();
            this.MouseMove += MapControl_MouseMove;
            this.LostMouseCapture += MapControl_LostMouseCapture;
            this.MouseUp += MapControl_MouseUp;
            Mouse.Capture(this);
        }
        private void MapControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(e);
            Mouse.Capture(null);
        }
        private void UpdatePosition(MouseEventArgs e)
        {
            if (NavigateCommand.CanExecute(CurrentPosition))
            NavigateCommand.Execute(e.GetPosition(this) - _RelativeOffsetValue);
        }
        private void FinishDrag(MouseEventArgs e)
        {
            UpdatePosition(e);
            this.MouseMove -= MapControl_MouseMove;
            this.LostMouseCapture -= MapControl_LostMouseCapture;
            this.MouseUp -= MapControl_MouseUp;
            _RelativeCurrentPosition.X -= (OffsetValue.X / ActualWidth) / ScaleValue;
            _RelativeCurrentPosition.Y -= (OffsetValue.Y / ActualHeight) / ScaleValue;
            CurrentPosition = Vector.Add(_RelativeCurrentPosition, new Point());
            OffsetValue = default;
            Mouse.Capture(null);
        }
        private void MapControl_LostMouseCapture(object sender, MouseEventArgs e)
        {
            FinishDrag(e);
        }
        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            UpdatePosition(e);
        }

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
        #region CurrentPosition
        public Point CurrentPosition
        {
            get { return (Point)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }

        public static readonly DependencyProperty CurrentPositionProperty =
            DependencyProperty.Register("CurrentPosition", typeof(Point), typeof(MapControl));
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

    }
}
