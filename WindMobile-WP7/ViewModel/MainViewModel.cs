using GalaSoft.MvvmLight;
using System.Windows.Input;
using Ch.Epix.WindMobile.WP7.Model;
using System.Collections.Generic;
using Ch.Epix.WindMobile.WP7.Service.Job;
using Ch.Epix.WindMobile.WP7.Service.Design;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls.Maps;
using System;
using System.Device.Location;

namespace Ch.Epix.WindMobile.WP7.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public string ApplicationTitle
        {
            get
            {
                return "WIND MOBILE";
            }
        }

        public string PageName
        {
            get
            {
                return "station list";
            }
        }

        public List<IStationInfo> StationInfoList
        {
            get;
            private set;
        }

        public IStationInfo CurrentStationInfo { get; set; }

        public RelayCommand GetStationInfoListCommand { get; private set; }

        private System.Device.Location.GeoCoordinateWatcher GeoCoordinateWatcher { get; set; }

        public GeoCoordinate Location 
        {
            get
            {
                if (GeoCoordinateWatcher.Position.Location.IsUnknown)
                {
                    return new GeoCoordinate(46.681609, 6.723654);
                }
                else
                {
                    return GeoCoordinateWatcher.Position.Location;
                }
            }
        }

        public CredentialsProvider CredentialsProvider
        {
            get
            {
                return new ApplicationIdCredentialsProvider("Aru7Ud6JR_vLA3MC_Vof2xFOXVejAASIjZzfy5pZuh3OUWLGkwMj--c8GWkutwCj");
            }

        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                listStationInfoJob = new DesignJobBase();
                //GetStationInfoListCommand = new RelayCommand(null);
            }
            else
            {
                // Init Job
                listStationInfoJob = new ListStationInfoJob();
                listStationInfoJob.JobCompleted += (s, e) => {
                    StationInfoList = (List<IStationInfo>)e.Result;
                    RaisePropertyChanged("StationInfoList");
                    GetStationInfoListCommand.RaiseCanExecuteChanged();
                };

                // Init Command
                GetStationInfoListCommand = new RelayCommand(
                    () => {
                        listStationInfoJob.Execute();
                        GetStationInfoListCommand.RaiseCanExecuteChanged();
                    },
                    () => {
                        return listStationInfoJob.IsBusy == false;
                    }
                );

                GetStationInfoListCommand.Execute(null);

                // Maintain only one Coord Watcher for better performance.
                GeoCoordinateWatcher = new System.Device.Location.GeoCoordinateWatcher();
                GeoCoordinateWatcher.TryStart(true, new TimeSpan(0, 0, 2));
            }
        }

        private IJob listStationInfoJob;

        public bool StationExists(string id)
        {
            foreach (var station in StationInfoList)
            {
                if (station.Id == id) return true;
            }
            return false;
        }

        /// <summary>
        /// Maintains multiple view models for stations data, so we can implement a tabbed view
        /// The key if the station id
        /// </summary>
        private Dictionary<string, StationInfoViewModel> stationInfoViewModels;



        public class StationInfoViewModelIndexer
        {
            public MainViewModel MainViewModel { get; private set; }

            private StationInfoViewModelIndexer(MainViewModel mainViewModel) 
            {
                this.MainViewModel = mainViewModel;
            }

            public StationInfoViewModel this[string id]
            {
                get
                {
                    if (MainViewModel.stationInfoViewModels.ContainsKey(id) == false)
                    {
                        if (MainViewModel.StationExists(id))
                        {
                            MainViewModel.stationInfoViewModels[id] = new StationInfoViewModel(id);
                        }
                        else
                        {
                            throw new Exception("Station with ID : " + id + " don't exists");
                        }
                    }
                    return MainViewModel.stationInfoViewModels[id];

                }
            }
        }
    }
}