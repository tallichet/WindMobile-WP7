using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using Ch.Epix.WindMobile.WP7.Service.Job;
using Ch.Epix.WindMobile.WP7.Model;
using GalaSoft.MvvmLight.Command;
using CustomControls.Graph;
using Ch.Epix.WindMobile.WP7.Service.TypedServices;
using Ch.Epix.WindMobile.WP7.Service;

namespace Ch.Epix.WindMobile.WP7.ViewModel
{
    public class ChartViewModel : ApplicationViewModel
    {
        private StationChartService chartService;
        private RelayCommand<int> refreshCommand;

        public IStationInfo StationInfo { get; private set; }

        public string ErrorMessage { get { return (ChartService.LastException != null ? ChartService.LastException.Message : null); } }
        public IChart ChartData { get { return ChartService.LastResult; } }

        public int Duration { get; private set; }

        public ChartViewModel(IStationInfo station)
        {
            if (station == null) throw new Exception("station info cannot be null");
            StationInfo = station;
        }

        protected StationChartService ChartService
        {
            get
            {
                if (chartService == null)
                {
                    chartService = ServiceCentral.ChartServices[StationInfo];
                    chartService.LastResultChanged += (s, e) => RaisePropertyChanged("ChartData");
                }
                return chartService;
            }
        }

        public RelayCommand<int> RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand<int>(
                        (i) =>
                        {
                            ChartService.Refresh(i);
                            refreshCommand.RaiseCanExecuteChanged();
                        },
                        (i) => ChartService.IsBusy
                    );
                }
                return refreshCommand;
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
