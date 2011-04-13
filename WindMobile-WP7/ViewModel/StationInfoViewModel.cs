using GalaSoft.MvvmLight;
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

        public RelayCommand<string> GetStationDataCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the StationInfoViewModel class.
        /// </summary>
        public StationInfoViewModel()
        {
            if (IsInDesignMode)
            {
                StationData = new Ch.Epix.WindMobile.WP7.Model.Design.StationData();
                GetStationDataJob = new DesignJobBase();
            }
            else
            {
                GetStationDataJob = new GetStationDataJob();
                GetStationDataJob.JobCompleted += (s, e) => 
                    {
                        StationData = e.Result as IStationData;
                        this.RaisePropertyChanged("StationData");
                    };

                GetStationDataCommand = new RelayCommand<string>(
                    (s) => GetStationDataJob.Execute(s),
                    (s) => string.IsNullOrEmpty(s) == false
                );
            }
        }

        public IStationInfo StationInfo
        {
            get
            {
                if (IsInDesignMode)
                {
                    return new Ch.Epix.WindMobile.WP7.Model.Design.StationInfo();
                }
                else
                {
                    return ViewModelLocator.MainStatic.CurrentStationInfo;
                }
            }
        }
    }
}