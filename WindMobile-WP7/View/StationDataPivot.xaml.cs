using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Ch.Epix.WindMobile.WP7.ViewModel;


namespace Ch.Epix.WindMobile.WP7.View
{
    public partial class StationDataPivot : PhoneApplicationPage
    {
        public DataViewModel ViewModel 
        {
            get { return this.DataContext as DataViewModel; }
        }

        public StationDataPivot()
        {
            InitializeComponent();
        }

        private void StationPivot_LoadedPivotItem(object sender, PivotItemEventArgs e)
        {
            (e.Item.Content as StationInfoViewModel).RaiseActivated();
            ViewModel.CurrentStationInfo = (e.Item.Content as StationInfoViewModel).StationInfo;
        }

        private void StationPivot_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < StationPivot.Items.Count; i++)
            {
                if ((StationPivot.Items[i] as StationInfoViewModel).StationInfo == ViewModel.CurrentStationInfo)
                {
                    StationPivot.SelectedIndex = i;
                    break;
                }
            }
        }

        private void ChartButton_Click(object sender, EventArgs e)
        {
            NavigateToChart(true);
        }

        private void NavigateToChart(bool manual)
        {
            NavigationService.Navigate(new Uri("/View/ChartView.xaml?manual=" + manual, UriKind.Relative));
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            if (e.Orientation == PageOrientation.LandscapeLeft || e.Orientation == PageOrientation.LandscapeRight)
            {
                NavigateToChart(false);
            }
        }
    
    }
}