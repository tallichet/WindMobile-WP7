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
using System.Diagnostics;


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

        //private void ShowChart_Completed(object sender, EventArgs e)
        //{
        //    MainChart.DataContext = ViewModel.CurrentChartViewModel;
        //    ViewModel.CurrentChartViewModel.RefreshCommand.Execute(3600);
        //}

        //private void HideChart_Completed(object sender, EventArgs e)
        //{
        //    MainChart.DataContext = null;
        //}

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            if ((e.Orientation & PageOrientation.Portrait) != 0)
            {
                //NavigationService.Navigate(new Uri("/View/ChartView.xaml", UriKind.Relative));
                VisualStateManager.GoToState(this, "PortraitVisualState", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "LandscapeVisualState", true);
                MainChart.Activate();
            }
            
            base.OnOrientationChanged(e);
        }
    }
}