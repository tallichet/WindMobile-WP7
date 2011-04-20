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
using Ch.Epix.WindMobile.WP7.Model;
using System.Diagnostics;
using Microsoft.Phone.Controls.Maps;

namespace Ch.Epix.WindMobile.WP7.View
{
    public partial class MainPanoramaView : PhoneApplicationPage
    {
        private ListBox lastUsedListBox = null;

        public ViewModel.MainViewModel ViewModel
        {
            get
            {
                return (DataContext as ViewModel.MainViewModel);
            }
        }

        public MainPanoramaView()
        {
            InitializeComponent();
            StationMap.SetView(ViewModel.Location, 8.0d);
        }

        private void StationInfoControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/View/StationInfoView.xaml", UriKind.Relative));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Are we selecting an item, and not desellecting one?
            if (e.AddedItems.Count > 0)
            {
                lastUsedListBox = sender as ListBox; // keep track of the list box to clear it after navigated back
                NavigationService.Navigate(new Uri("/View/StationDataPivot.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Clear selection, so we can 
            if (lastUsedListBox != null)
            {
                lastUsedListBox.SelectedItem = null;
            }
        }

        private void PushPin_Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentStationInfo = ((sender as Button).Tag as IStationInfo);
            NavigationService.Navigate(new Uri("/View/StationDataPivot.xaml", UriKind.Relative));
        }
    }
}