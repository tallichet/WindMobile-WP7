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

namespace Ch.Epix.WindMobile.WP7.ViewModel
{
    public class ChartViewModel : ViewModelBase
    {
        private IJob downloadChartDataJob;

        public IStationInfo StationInfo { get; private set; }

        public string LastErrorMessage { get; private set; }
        public IChart LastChartData { get; private set; }

        public int Duration { get; private set; }

        public RelayCommand<int> RefreshCommand { get; private set; }

        public ChartViewModel(IStationInfo station)
        {
            StationInfo = station;
            RaisePropertyChanged("StationInfo");

            downloadChartDataJob = new GetStationChartJob(station);
            downloadChartDataJob.JobError += (s, e) =>
                {
                    LastErrorMessage = "Erreur durant la récupération des données graphique de " + StationInfo.ShortName + "\r\n" + e.Exception.Message;
                    RaisePropertyChanged("LastErrorMessage");
                };
            downloadChartDataJob.JobCompleted += (s, e) =>
                {
                    LastChartData = e.Result as IChart;
                    RaisePropertyChanged("LastChartData");
                };

            RefreshCommand = new RelayCommand<int>(
                (duration) => downloadChartDataJob.Execute(duration.ToString())
            );
        }
    }
}
