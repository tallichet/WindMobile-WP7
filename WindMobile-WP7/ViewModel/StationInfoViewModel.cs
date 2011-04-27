using GalaSoft.MvvmLight;
using Ch.Epix.WindMobile.WP7.Model;
using GalaSoft.MvvmLight.Command;
using Ch.Epix.WindMobile.WP7.Service.Job;
using Ch.Epix.WindMobile.WP7.Service.Design;
using System;
using CustomControls.Graph;
using System.Collections.Generic;

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
        private IJob<string, IStationData> GetStationDataJob;
        private IJob<string, IChart> GetStationChartJob;
        
        public string ApplicationTitle
        {
            get
            {
                return "WIND MOBILE";
            }
        }

        public IStationData StationData { get; private set; }
        public IChart StationChart { get; private set; }
        public string ErrorMessage { get; private set; }

        public RelayCommand GetStationDataCommand { get; private set; }
        public RelayCommand<int> GetStationChartCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the StationInfoViewModel class.
        /// </summary>
        public StationInfoViewModel()
        {
            if (IsInDesignMode)
            {
                StationData = new Ch.Epix.WindMobile.WP7.Model.Design.StationData();
                GetStationDataJob = new DesignJobBase<string, IStationData>();
                StationInfo = new Ch.Epix.WindMobile.WP7.Model.Design.StationInfo();
            }
            else
            {
                this.PropertyChanged += StationInfoViewModel_PropertyChanged;
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

                GetStationChartJob = new GetStationChartJob(this.StationInfo);
                GetStationChartJob.JobCompleted += (s, e) =>
                    {
                        StationChart = e.Result as IChart;
                        this.RaisePropertyChanged("StationChart");
                    };
                GetStationChartCommand = new RelayCommand<int>(
                    (i) => GetStationChartJob.Execute(i.ToString())
                    );
            }
        }

        public List<IGraphData> ChartAverage { get; private set; }
        public List<IGraphData> ChartMax { get; private set; }

        void StationInfoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "StationChart")
            {
                if (StationChart != null && StationChart.WindAverage != null &&
                    StationChart.WindAverage.Values != null && StationChart.WindAverage.Values.Count > 0)
                {
                    this.ChartAverage = new List<IGraphData>();
                    foreach (var p in this.StationChart.WindAverage.Values)
                    {
                        ChartAverage.Add(new GraphData(p));
                    }
                    this.RaisePropertyChanged("ChartAverage");
                }
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

        public event EventHandler Activated;

        public void RaiseActivated()
        {
            if (this.Activated != null)
            {
                Activated(this, new EventArgs());
            }
        }
    }

    public class GraphData : IGraphData
    {
        IChartPoint point;

        public GraphData(IChartPoint p)
        {
            this.point = p;
        }

        public double GetX()
        {
            return point.Date.Ticks;
        }

        public double GetY()
        {
            return point.Value;
        }

        public string GetXText(double x)
        {
            return point.Date.ToString();
        }

        public string GetYText(double y)
        {
            return point.Value.ToString();
        }
    }
}