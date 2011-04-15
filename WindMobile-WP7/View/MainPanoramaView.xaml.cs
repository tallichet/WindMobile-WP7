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
    public partial class MainPanoramaView : PhoneApplicationPage
    {
        public MainPanoramaView()
        {
            InitializeComponent();
        }

        private void StationInfoControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/View/StationInfoView.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/StationInfoView.xaml", UriKind.Relative));
        }
    }
}