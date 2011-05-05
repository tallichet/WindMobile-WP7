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
using Ch.Epix.WindMobile.WP7.ViewModel;

namespace Ch.Epix.WindMobile.WP7.View
{
    public partial class ChartControl : UserControl
    {
        public ChartControl()
        {
            InitializeComponent();
        }
        
        public ChartViewModel ChartViewModel
        {
            get { return (ChartViewModel)GetValue(ChartViewModelProperty); }
            set { SetValue(ChartViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartViewModelProperty =
            DependencyProperty.Register("ChartViewModel", typeof(ChartViewModel), typeof(ChartControl), new PropertyMetadata(new PropertyChangedCallback(ChartViewModelChanged)));

        public static void ChartViewModelChanged(System.Windows.DependencyObject d, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChartControl;
            if (d == null) throw new Exception("Unknown control type");

            var old = e.OldValue as ChartViewModel;
            if (old != null)
            {
                old.ValidChartDataFound -= control.ChartViewModel_ValidChartDataFound;
                old.StartRefreshing -= control.ChartViewModel_StartRefreshing;
            }

            var newV = e.NewValue as ChartViewModel;
            if (newV != null)
            {
                newV.ValidChartDataFound += control.ChartViewModel_ValidChartDataFound;
                newV.StartRefreshing += control.ChartViewModel_StartRefreshing;
                control.MainChart.DataContext = newV;
                control.ButtonPallette.DataContext = newV;
                if (control.Visibility == Visibility.Visible)
                {
                    control.Activate();
                }
            }
        }

        public void Activate()
        {
            //WaitingProgressBar.IsIndeterminate = true;
            //WaitingView.Visibility = System.Windows.Visibility.Visible;
            if (ChartViewModel != null)
            {
                ChartViewModel.RefreshCommand.Execute("3600");
            }
        }

        public void ChartViewModel_ValidChartDataFound(object sender, EventArgs arg)
        {
            WaitingProgressBar.IsIndeterminate = false;
            WaitingView.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void ChartViewModel_StartRefreshing(object sender, EventArgs e)
        {
            WaitingProgressBar.IsIndeterminate = true;
            WaitingView.Visibility = System.Windows.Visibility.Visible;
            
        }
        
    }
}
