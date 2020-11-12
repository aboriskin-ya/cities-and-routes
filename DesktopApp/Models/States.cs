using DesktopApp.Services.State;
using GalaSoft.MvvmLight;

namespace DesktopApp.Models
{
    public class States : ViewModelBase
    {
        private string _State = "";
        public string State
        {
            get => _State;
            set
            {
                _State = value;
                RaisePropertyChanged(nameof(State));
            }
        }

        #region CreateCityPossibility

        private bool _IsAbleToCreateCity = false;
        public bool IsAbleToCreateCity
        {
            get => _IsAbleToCreateCity;
            set
            {
                _IsAbleToCreateCity = value;
                RaisePropertyChanged(nameof(IsAbleToCreateCity));
                State = CityStatusUpdate();
            }
        }

        #endregion

        #region SetCityPossibility

        private bool _IsAbleToSetCity = false;
        public bool IsAbleToSetCity
        {
            get => _IsAbleToSetCity;
            set
            {
                _IsAbleToSetCity = value;
                RaisePropertyChanged(nameof(IsAbleToSetCity));
                State = CityStatusUpdate();
            }
        }

        #endregion

        #region CreateRoutePossibility

        private bool _IsAbleToCreateRoute = false;
        public bool IsAbleToCreateRoute
        {
            get => _IsAbleToCreateRoute;
            set
            {
                _IsAbleToCreateRoute = value;
                RaisePropertyChanged(nameof(IsAbleToCreateRoute));
                State = RouteStatusUpdate();
            }
        }

        #endregion

        #region SetRoutePossibility

        private bool _IsAbleToPickFirstCity = false;
        public bool IsAbleToPickFirstCity
        {
            get => _IsAbleToPickFirstCity;
            set
            {
                _IsAbleToPickFirstCity = value;
                RaisePropertyChanged(nameof(IsAbleToPickFirstCity));
                State = RouteStatusUpdate();
            }
        }

        private bool _IsAbleToPickSecondCity = false;
        public bool IsAbleToPickSecondCity
        {
            get => _IsAbleToPickSecondCity;
            set
            {
                _IsAbleToPickSecondCity = value;
                RaisePropertyChanged(nameof(IsAbleToPickSecondCity));
                IsAbleToPickFirstCity = IsAbleToPickSecondCity;
            }
        }

        #endregion

        #region SuccessfulCreating

        private bool _IsSuccess;
        public bool IsSuccess
        {
            get => _IsSuccess;
            set
            {
                _IsSuccess = value;
                RaisePropertyChanged(nameof(IsSuccess));
                State = SuccessStatusUpdate();
            }
        }

        #endregion

        private string SuccessStatusUpdate()
        {
            if (IsSuccess)
                return StateLine.Show(StateLineStatus.SaveChanges);
            return StateLine.Show(StateLineStatus.Empty);
        }

        private string CityStatusUpdate()
        {
            if (IsAbleToSetCity)
                return StateLine.Show(StateLineStatus.SetCity);

            if (IsAbleToCreateCity)
                return StateLine.Show(StateLineStatus.CreateCity);

            return StateLine.Show(StateLineStatus.Empty);
        }

        private string RouteStatusUpdate()
        {
            if (IsAbleToPickSecondCity)
                return StateLine.Show(StateLineStatus.SelectSecondCity);
            
            if (IsAbleToPickFirstCity)
                return StateLine.Show(StateLineStatus.SelectFirstCity);

            if (IsAbleToCreateRoute)
                return StateLine.Show(StateLineStatus.CreateRoute);

             return StateLine.Show(StateLineStatus.Empty);
        }
    }
}
