using GalaSoft.MvvmLight;
using Ch.Epyx.WindMobile.WP7.Model;
using GalaSoft.MvvmLight.Command;
using Ch.Epyx.WindMobile.WP7.Service.Job;
using Ch.Epyx.WindMobile.WP7.Service.Design;
using System;
using System.Collections.Generic;
using Ch.Epyx.WindMobile.WP7.Service.TypedServices;
using Ch.Epyx.WindMobile.WP7.Service;
using System.Windows;

namespace Ch.Epyx.WindMobile.WP7.ViewModel
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
    public class StationInfoViewModel : ApplicationViewModel
    {
        private StationDataService dataService;
        private RelayCommand refreshCommand;
        public IStationInfo StationInfo { get; private set; }

        public StationInfoViewModel(IStationInfo info) 
        {
            if (info == null) 
            {
                throw new Exception("Station Info cannot be null");
            }
            StationInfo = info;
        }

        public IStationData StationData
        {
            get
            {
                return DataService.LastResult;
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (dataService != null && dataService.LastException != null)
                {
                    return dataService.LastException.Message;
                }
                return null;
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(
                        () =>
                        {
                            DataService.Refresh(null);
                            refreshCommand.RaiseCanExecuteChanged();
                        },
                        () => DataService.IsBusy);
                }
                return refreshCommand;
            }
        }

        public string TimeAgo
        {
            get
            {
                if (StationData == null) return "";
                var span = DateTime.Now - StationData.LastUpdate;
                if (span.Days > 0) return span.Days + " jours";
                if (span.Hours > 0) return span.Hours + " heures";
                if (span.Minutes > 0) return span.Minutes + " minutes";
                return "quelques instants";
            }
        }

        public StationDataService DataService
        {
            get
            {
                if (dataService == null)
                {
                    dataService = ServiceCentral.DataServices[StationInfo];
                    dataService.LastResultChanged += (s, e) =>
                    {
                        RaisePropertyChanged("StationData");
                        RaisePropertyChanged("TimeAgo");
                        RefreshCommand.RaiseCanExecuteChanged();
                    };
                    dataService.ErrorOccured += (s, e) =>
                    {
                        MessageBox.Show("Impossible de récupérer les infos de la balise de " + this.StationInfo.Name);
                        RaisePropertyChanged("ErrorMessage");                        
                    };
                }
                return dataService;
            }
        }
        
        public event EventHandler Activated;

        public void RaiseActivated()
        {
            //if (RefreshCommand.CanExecute(null))
            //{
            //    RefreshCommand.Execute(null);
            //}
            if (this.Activated != null)
            {
                Activated(this, new EventArgs());
            }
        }
    }
}