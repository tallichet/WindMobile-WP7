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
            Stations = new System.Collections.ObjectModel.ObservableCollection<Model.Station>();
            init();
        }

        public System.Collections.ObjectModel.ObservableCollection<Model.Station> Stations
        {
            get; private set;
        }

        private bool inProgress;
        public bool InProgress
        {
            get { return inProgress; }
            set
            {
                if (value != inProgress)
                {
                    inProgress = value;
                    base.RaisePropertyChanged(() => this.InProgress);
                }
            }
        }

        private async void init()
        {
            await refreshStationsList();
        }

        #region refreshing data

        private async Task refreshStationsList()
        {
            Stations.Clear();
            foreach (var station in await ServiceLocator.Current.GetInstance<Service.INetworkService>().ListStations())
            {
                Stations.Add(station);
            }
        }
        
        #endregion


    }
}
