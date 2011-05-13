using System;
using System.Collections.Generic;
using System.Device.Location;
using Ch.Epyx.WindMobile.WP7.Model;
using Ch.Epyx.WindMobile.WP7.Service.Design;
using Ch.Epyx.WindMobile.WP7.Service.Job;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls.Maps;
using Ch.Epyx.WindMobile.WP7.Service.TypedServices;
using Ch.Epyx.WindMobile.WP7.Service;
using System.Windows;

namespace Ch.Epyx.WindMobile.WP7.ViewModel
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
    public class MainViewModel : ApplicationViewModel
    {
        private ListStationInfoService listService;
        private RelayCommand refreshCommand;

        private Visibility progressVisibility;

        public MainViewModel()
        {
            ProgressVisibility = Visibility.Collapsed;
        }

        public string PageName
        {
            get
            {
                return "station list";
            }
        }

        protected ListStationInfoService ListService
        {
            get
            {
                if (listService == null)
                {
                    listService = ServiceCentral.ListService;
                    listService.LastResultChanged += (s, e) =>
                    {
                        RaisePropertyChanged("StationInfoList");
                        RefreshCommand.RaiseCanExecuteChanged();
                        ProgressVisibility = Visibility.Collapsed;
                    };
                    listService.ErrorOccured += (s, e) =>
                    {
                        MessageBox.Show(@"Une erreur s'est produite. 
Le réseau n'est peut-être pas accessible.
Veuillez réessayer plus tard");
                    };
                }
                return listService;
            }
        }

        public List<IStationInfo> StationInfoList
        {
            get
            {
                return ListService.LastResult;
            }
        }

        public IStationInfo CurrentStationInfo {
            get
            {
                return ApplicationRunData.CurrentStationStatic;
            }
            set
            {
                ApplicationRunData.CurrentStationStatic = value;
                RaisePropertyChanged("CurrentStationInfo");
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
                        ProgressVisibility = Visibility.Visible;
                        ListService.Refresh(null);
                        refreshCommand.RaiseCanExecuteChanged();
                    },
                    () => ListService.IsBusy
                );
                }
                return refreshCommand;
            }            
        }

        public Visibility ProgressVisibility
        {
            get { return progressVisibility; }
            set
            {
                progressVisibility = value;
                RaisePropertyChanged("ProgressVisibility");
            }
        }

    }
}