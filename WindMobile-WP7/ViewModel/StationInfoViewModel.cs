﻿using GalaSoft.MvvmLight;
using Ch.Epix.WindMobile.WP7.Model;
using GalaSoft.MvvmLight.Command;
using Ch.Epix.WindMobile.WP7.Service.Job;
using Ch.Epix.WindMobile.WP7.Service.Design;

namespace Ch.Epix.WindMobile.WP7.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
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
    public class StationInfoViewModel : ViewModelBase
    {
        private IJob GetStationDataJob;
        
        public string ApplicationTitle
        {
            get
            {
                return "WIND MOBILE";
            }
        }

        public IStationData StationData { get; private set; }
        public string ErrorMessage { get; private set; }

        public RelayCommand GetStationDataCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the StationInfoViewModel class.
        /// </summary>
        public StationInfoViewModel()
        {
            if (IsInDesignMode)
            {
                StationData = new Ch.Epix.WindMobile.WP7.Model.Design.StationData();
                GetStationDataJob = new DesignJobBase();
                StationInfo = new Ch.Epix.WindMobile.WP7.Model.Design.StationInfo();
            }
            else
            {
                GetStationDataJob = new GetStationDataJob();
                GetStationDataJob.JobCompleted += (s, e) => 
                    {
                        StationData = e.Result as IStationData;
                        this.RaisePropertyChanged("StationData");
                    };
                GetStationDataJob.JobError += (s, e) =>
                    {
                        ErrorMessage = "Impossible de récupérer les informations pour " + StationInfo.ShortName + "\r\n" + e.Exception;
                        this.RaisePropertyChanged("ErrorMessage");
                    };

                GetStationDataCommand = new RelayCommand(
                    () => GetStationDataJob.Execute(StationInfo.Id)
                );
                StationInfo = ViewModelLocator.MainStatic.CurrentStationInfo;
            }
        }

        public StationInfoViewModel(IStationInfo stationInfo)
            : this()
        {
            StationInfo = stationInfo;
        }

        public IStationInfo StationInfo
        {
            get;
            private set;
        }
    }
}