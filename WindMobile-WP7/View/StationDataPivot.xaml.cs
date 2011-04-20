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
        public MainViewModel ViewModel 
        {
            get { return this.DataContext as MainViewModel; }
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
            StationPivot.SelectedIndex = ViewModel.StationInfoList.IndexOf(ViewModel.CurrentStationInfo);
        }
    }
}