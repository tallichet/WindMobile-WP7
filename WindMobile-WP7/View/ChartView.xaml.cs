using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Ch.Epix.WindMobile.WP7.View
{
    public partial class ChartView : PhoneApplicationPage
    {
        private bool ManualOpenend; // If true, the user opened from the chart button. These means, we close only manually

        public ViewModel.ChartViewModel ViewModel
        {
            get
            {
                return this.DataContext as ViewModel.ChartViewModel;
            }
        }

        public ChartView()
        {
            InitializeComponent();
            this.ViewModel.ValidChartDataFound += ViewModel_ValidChartDataFound;
        }

        void ViewModel_ValidChartDataFound(object sender, EventArgs e)
        {
            MainChart.Visibility = System.Windows.Visibility.Visible;
            ShowChart.Begin();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshCommand.Execute(3600);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string stringValue = null;
            if (NavigationContext.QueryString.TryGetValue("manual", out stringValue)) {
                ManualOpenend = bool.Parse(stringValue);
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            if (ManualOpenend == false && (e.Orientation == PageOrientation.PortraitUp || e.Orientation == PageOrientation.PortraitDown))
            {
                NavigationService.GoBack();
            }
            else
            {
                base.OnOrientationChanged(e);
            }
        }
    }
}