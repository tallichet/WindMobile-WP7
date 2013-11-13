using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core.Viewmodel.Runtime
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel()
        {
            CloseStations = new System.Collections.ObjectModel.ObservableCollection<Model.Station>();
        }

        public System.Collections.ObjectModel.ObservableCollection<Model.Station> CloseStations
        {
            get; private set;
        }

        private bool inProgress;
        public bool InProgress
        {
            get { return inProgress; }
            set
            {
                if (value != inProgress) // TODO Change to use a counter
                {
                    inProgress = value;
                    base.RaisePropertyChanged(() => this.InProgress);
                }
            }
        }

        private Model.Station currentStation = null;
        public Model.Station CurrentStation
        {
            get
            {
                return currentStation;   
            }
            set
            {
                currentStation = value;
                RaisePropertyChanged(() => this.CurrentStation);
                refreshCurrentStationData();
            }
        }

        public List<Model.StationData> CurrentStationData
        {
            get; private set;
        }

        public async void SetLocation(Model.Location CurrentLocation)
        {
            InProgress = true;
            currentLocation = CurrentLocation;

            await refreshCloseStations();

            InProgress = false;
        }

        #region refreshing data

        private Model.Location currentLocation;
        private long distanceInMetersForGetSearch = 20000;
        private TimeSpan dataDefaultDuration = TimeSpan.FromHours(1);

        private async Task refreshCloseStations()
        {
            CloseStations.Clear();
            foreach (var station in await ServiceLocator.Current.GetInstance<Service.INetworkService>().GeoSearchStations(currentLocation, distanceInMetersForGetSearch))
            {
                CloseStations.Add(station);
                if (CloseStations.Count == 1)
                {
                    CurrentStation = CloseStations.First();
                }
            }
        }

        private async void refreshCurrentStationData()
        {
            InProgress = true;

            if (currentStation == null)
            {
                CurrentStationData = null;
            }
            else
            {
                CurrentStationData = await ServiceLocator.Current.GetInstance<Service.INetworkService>().GetStationData(currentStation.ID, dataDefaultDuration);
            }
            RaisePropertyChanged(() => CurrentStationData);

            InProgress = false;
        }
        
        #endregion

    }
}
