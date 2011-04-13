using GalaSoft.MvvmLight;
using System.Windows.Input;
using Ch.Epix.WindMobile.WP7.Model;
using System.Collections.Generic;
using Ch.Epix.WindMobile.WP7.Service.Job;
using Ch.Epix.WindMobile.WP7.Service.Design;
using GalaSoft.MvvmLight.Command;

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
            }
        }

        private IJob listStationInfoJob;
    }
}