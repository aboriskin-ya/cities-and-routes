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

        private bool _isAbleToCreateCity = false;
        public bool IsAbleToCreateCity
        {
            get => _isAbleToCreateCity;
            set
            {
                _isAbleToCreateCity = value;
                RaisePropertyChanged(nameof(IsAbleToCreateCity));
                State = CityStatusUpdate();
            }
        }

        #endregion


        #region UpdateCityPossibility

        private bool _isAbleToUpdateCity = false;
        public bool IsAbleToUpdateCity
        {
            get => _isAbleToUpdateCity;
            set
            {
                _isAbleToUpdateCity = value;
                RaisePropertyChanged(nameof(IsAbleToUpdateCity));
            }
        }

        #endregion

        #region SetCityPossibility

        private bool _isAbleToSetCity = false;
        public bool IsAbleToSetCity
        {
            get => _isAbleToSetCity;
            set
            {
                _isAbleToSetCity = value;
                RaisePropertyChanged(nameof(IsAbleToSetCity));
                State = CityStatusUpdate();
            }
        }

        #endregion

        #region CreateRoutePossibility

        private bool _isAbleToCreateRoute = false;
        public bool IsAbleToCreateRoute
        {
            get => _isAbleToCreateRoute;
            set
            {
                _isAbleToCreateRoute = value;
                RaisePropertyChanged(nameof(IsAbleToCreateRoute));
                State = RouteStatusUpdate();
            }
        }

        #endregion

        #region SetRoutePossibility

        private bool _isAbleToPickFirstCity = false;
        public bool IsAbleToPickFirstCity
        {
            get => _isAbleToPickFirstCity;
            set
            {
                _isAbleToPickFirstCity = value;
                RaisePropertyChanged(nameof(IsAbleToPickFirstCity));
                State = RouteStatusUpdate();
            }
        }

        private bool _isAbleToPickSecondCity = false;
        public bool IsAbleToPickSecondCity
        {
            get => _isAbleToPickSecondCity;
            set
            {
                _isAbleToPickSecondCity = value;
                RaisePropertyChanged(nameof(IsAbleToPickSecondCity));
                IsAbleToPickFirstCity = IsAbleToPickSecondCity;
            }
        }

        #endregion

        #region SuccessfulCreating

        private bool _isSuccess;
        public bool IsSuccess
        {
            get => _isSuccess;
            set
            {
                _isSuccess = value;
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
