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
using System.Collections;

namespace Ch.Epix.WindMobile.WP7.View
{
    public partial class WindMobileMap : UserControl
    {
        IEnumerable ItemsSource
        {
            get            
            {
                return StationMapControl.ItemsSource;
            }
            set
            {
                StationMapControl.ItemsSource = value;
            }
        }

        public WindMobileMap()
        {
            InitializeComponent();
        }

        private void PushPin_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
